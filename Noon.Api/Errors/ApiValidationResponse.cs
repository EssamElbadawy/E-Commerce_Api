namespace Noon.Api.Errors
{
    public class ApiValidationResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationResponse( string? errorMessage = null) : base(400, errorMessage)
        {
            Errors = new List<string>();
        }

    }
}
