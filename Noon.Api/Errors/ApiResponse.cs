namespace Noon.Api.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ApiResponse(int statusCode, string? errorMessage = null)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage ?? GetDefaultValueForStatusCode(statusCode);
        }

        private string? GetDefaultValueForStatusCode(int statusCode)
            => statusCode switch
            {
                400 => "Bad Request",
                401 => "UnAuthorized",
                404 => "Resource Not Found",
                500 => "Internal Server Error",
                _ => null

            };
    }
}
