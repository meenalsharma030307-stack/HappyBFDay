namespace HappyBFDay.Models
{
    public class QRCodeModel
    {
        public string Text { get; set; } = string.Empty;
        public IFormFile[]? Photos { get; set; }
        public string SongUrl { get; set; } = string.Empty;
        public string StyleType { get; set; } = "heart";
        public string ColorScheme { get; set; } = "pink";
        public int Size { get; set; } = 20;
    }

    public class QRCodeResult
    {
        public string QRCodeImage { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }

    public class QRCodeViewModel
    {
        public QRCodeModel Model { get; set; } = new QRCodeModel();
        public QRCodeResult? Result { get; set; }
    }

    public class RomanticMessageViewModel
    {
        public string Message { get; set; } = string.Empty;
        public List<string> PhotoNames { get; set; } = new List<string>();
        public string SongUrl { get; set; } = string.Empty;
    }
}