namespace MyPortolio.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // Þifre sýfýrlama için özel hata mesajý
        public string ErrorMessage { get; set; }
    }
}
