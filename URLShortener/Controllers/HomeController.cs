using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using URLShortener.Data;

namespace URLShortener.Controllers
{
    public class HomeController(DataContext _context) : Controller
    {
        public async Task<IActionResult> Index() => View(await _context.Urls.ToListAsync() ?? new List<Models.Url>());

        public async Task<IActionResult> ShortenURL(string main, int expiration)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var shortenedURL = ShortenURL(main);
            DateTime expire = DateTime.Now.AddMinutes(expiration);

            var qrCode = GenerateQRCode(main);
            await UrlSaveAsync(main, shortenedURL, expire, qrCode);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult OpenURL(int id)
        {
            var item = _context.Urls.Where(n => n.Id == id);
            var isExpired = item.FirstOrDefault()?.Expiration < DateTime.Now;

            if (isExpired)
                return Content("Shortened Link has expired");

            var originalLink = item.Select(n => n.Main).FirstOrDefault();

            item.FirstOrDefault().Clicked++;
            _context.SaveChanges();

            return Redirect(originalLink);
        }
        public IActionResult Delete(int id)
        {
            var item = _context.Urls.Find(id);
            _context.Urls.Remove(item);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private async Task UrlSaveAsync(string oldURL, string newShortenedURL, DateTime expiration, string qrCode)
        {
            _context.Add(
               new Models.Url
               {
                   Main = oldURL,
                   Shortened = newShortenedURL,
                   CreatedAt = DateTime.Now,
                   Expiration = expiration,
                   QRCode = qrCode
               });
           await _context.SaveChangesAsync();
        }

        private string ShortenURL(string url)
        {
            var prefix = "https://short.link/";
            var hash = url.GetHashCode().ToString("X");

            return $"{prefix}{hash}";
        }

        public string GenerateQRCode(string url)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCoder.QRCode(qrCodeData);
            var qrImage = qrCode.GetGraphic(20);

            using (var ms = new MemoryStream())
            {
                qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var base64String = Convert.ToBase64String(ms.ToArray());
                return $"data:image/png;base64,{base64String}";
            }

        }
    }
}
