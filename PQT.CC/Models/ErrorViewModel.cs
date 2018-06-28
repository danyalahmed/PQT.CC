namespace PQT.CC.Models
{
    public class ErrorViewModel
    {
        public int? ErrorCode { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}