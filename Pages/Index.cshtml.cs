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
        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            var shortLink = await _context.ShortLinks.FindAsync(id);
            if (shortLink != null)
            {
                _context.ShortLinks.Remove(shortLink);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var shortLink = await _context.ShortLinks.FindAsync(id);
            if (shortLink != null)
            {
                OriginalUrl = shortLink.OriginalUrl;
                ShortUrl = $"http://localhost:5042/{shortLink.ShortCode}"; 
            }
            await OnGetAsync();
            return Page();
        }
public async Task<IActionResult> OnPostUpdateAsync([FromBody] UpdateRequest request)
{
    try 
    {
        Console.WriteLine($"🔄 Update id={request.Id}, url={request.OriginalUrl}");
        
        var shortLink = await _context.ShortLinks.FindAsync(request.Id);
        if (shortLink != null)
        {
            shortLink.OriginalUrl = request.OriginalUrl;
            await _context.SaveChangesAsync();
            Console.WriteLine(" Update OK");
            return new JsonResult(new { success = true });
        }
        else
        {
            Console.WriteLine("❌ Record not found");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Update error: {ex.Message}");
    }
    return new JsonResult(new { success = false });
}


public class UpdateRequest
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; } = "";
}
    }
}
