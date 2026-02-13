using System.ComponentModel.DataAnnotations;

namespace cse325_team1.Data.Models
{
    public class JournalEntry
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Entry { get; set; } = "";

        [Required]
        public string Content { get; set; } = string.Empty;

        public int UserId { get; set; }

        public UserProfile? User { get; set; }

        public List<ImageBlob> Images { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}