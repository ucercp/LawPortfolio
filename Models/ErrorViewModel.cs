namespace MyPortolio.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        // �ifre s�f�rlama i�in �zel hata mesaj�
        public string ErrorMessage { get; set; }
    }
}
