using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnuInstitute.Models
{
    // The Content class represents a single piece of content in our CMS.
    public class Content
    {
        // Primary key for the content item.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // The title of the content, required.
        [Required]
        [MaxLength(255)]
        public string ?Title { get; set; }

        // The main body of the content, required.
        [Required]
        public string ?Body { get; set; }

        // Author of the content.
        public string ?Author { get; set; }

        // The date the content was last updated.
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        // A list of tags for the content.
        public List<string> Tags { get; set; } = new List<string>();
    }
}
