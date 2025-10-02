using QRCoder;

namespace HappyBFDay.Helpers
{
    public class QRCodeHelper
    {
        public static byte[] GenerateQRCode(string text, int pixelsPerModule = 20)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            
            return qrCode.GetGraphic(pixelsPerModule);
        }

        public static byte[] GenerateColoredQRCode(string text, int pixelsPerModule = 20, 
            byte[]? darkColor = null, byte[]? lightColor = null)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            
            // Default colors if not provided
            darkColor ??= new byte[] { 255, 105, 180 }; // Hot pink
            lightColor ??= new byte[] { 255, 255, 255 }; // White
            
            return qrCode.GetGraphic(pixelsPerModule, darkColor, lightColor, true);
        }

        public static string GenerateQRCodeBase64(string text, int pixelsPerModule = 20)
        {
            byte[] qrCodeBytes = GenerateQRCode(text, pixelsPerModule);
            return Convert.ToBase64String(qrCodeBytes);
        }

        public static string GenerateColoredQRCodeBase64(string text, int pixelsPerModule = 20,
            byte[]? darkColor = null, byte[]? lightColor = null)
        {
            byte[] qrCodeBytes = GenerateColoredQRCode(text, pixelsPerModule, darkColor, lightColor);
            return Convert.ToBase64String(qrCodeBytes);
        }
    }
}