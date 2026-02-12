using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using URLShortener.Data;
using URLShortener.Models;
using URLShortener.Services;

namespace URLShortener.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UrlShorteningService _service;
        private readonly AppDbContext _context;

        public IndexModel(UrlShorteningService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [BindProperty]
        public string? OriginalUrl { get; set; }

        public string? ShortUrl { get; set; }
        public List<ShortLink> Links { get; set; } = new();

        public async Task OnGetAsync()
        {
            Links = await _context.ShortLinks
                .OrderByDescending(x => x.CreatedAt)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(OriginalUrl))
            {
                await OnGetAsync();
                return Page();
            }

            if (!Uri.TryCreate(OriginalUrl, UriKind.Absolute, out _))
            {
                ModelState.AddModelError("", "Некорректный URL");
                await OnGetAsync();
                return Page();
            }

            var shortCode = await _service.CreateShortLinkAsync(OriginalUrl);
            ShortUrl = $"http://localhost:5042/{shortCode}";

            await OnGetAsync();
            return Page();
        }
    }
}
