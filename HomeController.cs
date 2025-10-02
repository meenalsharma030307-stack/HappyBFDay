using Microsoft.AspNetCore.Mvc;
using HappyBFDay.Helpers;
using HappyBFDay.Models;
using System.Diagnostics;

namespace HappyBFDay.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            try
            {
                // Automatically generate QR code with your romantic message
                var romanticMessage = "Happy Boyfriend's Day my baby 💕";
                var songUrl = "https://www.youtube.com/watch?v=X5QzOTLYzG4"; // I Wanna Be Yours - Arctic Monkeys
                
                // Store the message and song in session
                HttpContext.Session.SetString("RomanticMessage", romanticMessage);
                HttpContext.Session.SetString("SongUrl", songUrl);
                HttpContext.Session.SetString("PhotoNames", ""); // No photos for simple version
                
                // Create the URL that the QR code will point to (use actual IP for phone access)
                var romanticUrl = "http://192.168.29.112:5000/Home/RomanticMessage";
                
                // Generate beautiful pink QR code
                byte[] darkColor = new byte[] { 255, 20, 147 }; // Deep pink
                byte[] lightColor = new byte[] { 255, 240, 245 }; // Lavender blush
                
                string qrCodeBase64 = QRCodeHelper.GenerateColoredQRCodeBase64(
                    romanticUrl, 50, darkColor, lightColor); // Much larger size for better scanning
                
                var result = new QRCodeResult
                {
                    QRCodeImage = qrCodeBase64,
                    Message = "Your romantic QR code is ready! 💕",
                    Success = true
                };
                
                var viewModel = new QRCodeViewModel
                {
                    Result = result
                };
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Handle any errors gracefully
                var errorResult = new QRCodeResult
                {
                    Message = $"Error: {ex.Message}",
                    Success = false
                };
                
                var errorViewModel = new QRCodeViewModel
                {
                    Result = errorResult
                };
                
                return View(errorViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GenerateQR(QRCodeModel model, IFormFile[] photos)
        {
            try
            {
                // Create the URL that the QR code will point to
                var baseUrl = $"{Request.Scheme}://{Request.Host}";
                var romanticUrl = $"{baseUrl}/Home/RomanticMessage";

                // Save uploaded photos
                var photoNames = new List<string>();
                if (photos != null && photos.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "Images", "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    foreach (var photo in photos)
                    {
                        if (photo.Length > 0)
                        {
                            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                            var filePath = Path.Combine(uploadsFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await photo.CopyToAsync(stream);
                            }
                            photoNames.Add(fileName);
                        }
                    }
                }

                // Store session data for the romantic message
                HttpContext.Session.SetString("RomanticMessage", model.Text);
                HttpContext.Session.SetString("PhotoNames", string.Join(",", photoNames));
                HttpContext.Session.SetString("SongUrl", model.SongUrl);

                // Generate beautiful QR code
                byte[] darkColor = model.ColorScheme switch
                {
                    "pink" => new byte[] { 255, 20, 147 }, // Deep pink
                    "purple" => new byte[] { 138, 43, 226 }, // Blue violet
                    "red" => new byte[] { 220, 20, 60 }, // Crimson
                    _ => new byte[] { 255, 20, 147 } // Default pink
                };

                byte[] lightColor = new byte[] { 255, 240, 245 }; // Lavender blush

                string qrCodeBase64 = QRCodeHelper.GenerateColoredQRCodeBase64(
                    romanticUrl, model.Size, darkColor, lightColor);

                var result = new QRCodeResult
                {
                    QRCodeImage = qrCodeBase64,
                    Message = "Your romantic QR code has been generated! 💕",
                    Success = true
                };

                var viewModel = new QRCodeViewModel
                {
                    Model = model,
                    Result = result
                };

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                var result = new QRCodeResult
                {
                    Message = $"Error generating QR code: {ex.Message}",
                    Success = false
                };

                var viewModel = new QRCodeViewModel
                {
                    Model = model,
                    Result = result
                };

                return View("Index", viewModel);
            }
        }

        public IActionResult RomanticMessage()
        {
            var message = HttpContext.Session.GetString("RomanticMessage") ?? "Happy Boyfriend's Day my baby 💕";
            var photoNamesStr = HttpContext.Session.GetString("PhotoNames") ?? "";
            var songUrl = HttpContext.Session.GetString("SongUrl") ?? "https://www.youtube.com/watch?v=X5QzOTLYzG4";

            var photoNames = string.IsNullOrEmpty(photoNamesStr) 
                ? new List<string>() 
                : photoNamesStr.Split(',').ToList();

            var model = new RomanticMessageViewModel
            {
                Message = message,
                PhotoNames = photoNames,
                SongUrl = songUrl
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}