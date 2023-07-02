using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPF.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int? VendorCode { get; set; }
        public string? Title { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public decimal? Discount { get; set; }
    }
}
