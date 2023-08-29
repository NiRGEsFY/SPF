using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class TypeCategory
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int TypeId { get; set; }
        public SPF.Models.Items.Type Type { get; set; }
    }
}
