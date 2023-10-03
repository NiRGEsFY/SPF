using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
