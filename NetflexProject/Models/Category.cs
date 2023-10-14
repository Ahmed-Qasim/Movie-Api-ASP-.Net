using System.ComponentModel.DataAnnotations;

namespace NetflexProject.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]

        public string Type { get; set; }

        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
        public ICollection<Novel> Novels { get; set; } = new HashSet<Novel>();


    }
}
