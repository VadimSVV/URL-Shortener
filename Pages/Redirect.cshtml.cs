using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using URLShortener.Data;

namespace URLShortener.Pages
{
    public class RedirectModel : PageModel
    {
        private readonly AppDbContext _context;

        public RedirectModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string shortCode)
        {
            var shortLink = await _context.ShortLinks
            .FirstOrDefaultAsync(s => s.ShortCode == shortCode);

            if (shortLink != null)
            {
                shortLink.ClickCount++;
                await _context.SaveChangesAsync();
                Response.Redirect(shortLink.OriginalUrl);
            }
            else
            {
                HttpContext.Response.StatusCode = 404;
            }
        }
    }
}