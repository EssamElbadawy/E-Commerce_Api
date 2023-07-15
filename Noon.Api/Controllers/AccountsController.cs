using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Noon.Api.Dtos;
using Noon.Api.Errors;
using Noon.Api.Extensions;
using Noon.Core.Entities.Identity;
using Noon.Core.Services;

namespace Noon.Api.Controllers
{

    public class AccountsController : GenericController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }




        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            return user == null
                ? Unauthorized(new ApiResponse(401))
                :
                !_signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false).Result.Succeeded
                    ?
                    Unauthorized(new ApiResponse(401))
                    : Ok(new UserDto()
                    {
                        Email = user.Email,
                        DisplayName = user.DisplayName,
                        Token = await _tokenService.CreateToken(user, _userManager)
                    });
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result)
                return BadRequest(new ApiValidationResponse()
                { Errors = new List<string>() { "this Email is already in use!!" } });

            var user = new AppUser()
            {
                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            return !result.Succeeded
                ? BadRequest(new ApiResponse(400))
                : Ok(new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _tokenService.CreateToken(user, _userManager)
                });
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.GetUserWithAddress(User);

            var address = _mapper.Map<Address, AddressDto>(user.Address);

            return Ok(address);
        }


        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto updatedAddress)
        {
            var user = await _userManager.GetUserWithAddress(User);
            var address = _mapper.Map<AddressDto, Address>(updatedAddress);
            if (user.Address != null)
            {
                address.Id = user.Address.Id;
                user.Address = address;
            }

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? Ok(updatedAddress) : BadRequest(new ApiResponse(400));
        }


        [HttpGet("emailExists")]
        public async Task<bool> CheckEmailExists(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);

            return result is not null;
        }
    }
}
