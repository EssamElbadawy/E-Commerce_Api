namespace Noon.Api.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }
        public ApiExceptionResponse(int statusCode, string? details = null, string? errorMessage = null) : base(statusCode, errorMessage)
        {
            Details = details;
        }
    }
}
