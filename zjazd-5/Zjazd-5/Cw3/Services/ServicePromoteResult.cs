using Cw4.DTOs.Responses;

namespace Cw4.Services
{
    public class ServicePromoteResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public EnrollPromoteResponse Model { get; set; }
    }
}
