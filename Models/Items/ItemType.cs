using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class ItemType
    {
        public int Id { get; set; }

        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        public int TypeId { get; set; }
        public SPF.Models.Items.Type Type { get; set; }
    }
}
