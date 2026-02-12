using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models
{
    public class ShortLink
    {
            public int Id { get; set; }
            [Required, MaxLength(2048)]
            public string OriginalUrl { get; set; } = string.Empty;

            [Required, MaxLength(7)] // непредсказуемо
            public string ShortCode { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
            public int ClickCount { get; set; } = 0;
    }
}