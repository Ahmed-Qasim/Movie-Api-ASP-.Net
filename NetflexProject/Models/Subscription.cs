using System.ComponentModel.DataAnnotations;

namespace NetflexProject.Models
{
    public class Subscription
    {
        [Key]
        public int SubID { get; set; }
        [Required]

        public string Type { get; set; }
        [Required]

        public int Price { get; set; }
        [Required]

        public int Duration { get; set; }

        public ICollection<User> Users { get; set; } = new HashSet<User>();

        public ICollection<Novel> Novels { get; set; } = new HashSet<Novel>();
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();


    }
}
