using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Малое описание")]
        public string LowDescription { get; set; }

        [Required]
        [Display(Name = "Большое описание")]
        public string HighDescription { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена со скидкой")]
        public decimal PriceLow { get; set; }

        [Required]
        [Display(Name = "Ссылка на изображение")]
        public string ImgUrl { get; set; }

        [Required]
        [Display(Name = "Ссылка на изображений")]
        public string ImgListUrl { get; set; }
    }
}
