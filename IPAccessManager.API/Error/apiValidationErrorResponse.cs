using Microsoft.AspNetCore.Mvc;

namespace IPAccessManager.API.Error
{
    public class apiValidationErrorResponse: ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public apiValidationErrorResponse()
            :base(400)
        {
            Errors = new List<string>();

        }

    }
}
