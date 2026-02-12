using Microsoft.EntityFrameworkCore;
using URLShortener.Data;
using URLShortener.Models;

namespace URLShortener.Services
{
    public class UrlShorteningService
    {
        private readonly AppDbContext _context;
        private readonly Random _random = new();

        public UrlShorteningService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateShortLinkAsync(string originalUrl)
        {
            string shortCode;
            do
            {
                shortCode = GenerateRandomCode();  // 7 симв Base62
            } while (await ShortCodeExistsAsync(shortCode));

            var shortLink = new ShortLink
            {
                OriginalUrl = originalUrl,
                ShortCode = shortCode,
                CreatedAt = DateTime.UtcNow,
                ClickCount = 0
            };

            _context.ShortLinks.Add(shortLink);
            await _context.SaveChangesAsync();
            return shortCode;
        }

        public async Task<ShortLink?> GetByShortCodeAsync(string shortCode)
        {
            return await _context.ShortLinks
                .FirstOrDefaultAsync(s => s.ShortCode == shortCode);
        }

        private string GenerateRandomCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] code = new char[7];
            for (int i = 0; i < 7; i++)
            {
                code[i] = chars[_random.Next(chars.Length)];
            }
            return new string(code);  // ex: "X7kP9mQ"
        }

        private async Task<bool> ShortCodeExistsAsync(string shortCode)
        {
            return await _context.ShortLinks
                .AnyAsync(s => s.ShortCode == shortCode);
        }
    }
}
