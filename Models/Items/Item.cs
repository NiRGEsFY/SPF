using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace SPF.Models.Items
{
    [Index(nameof(LowDescription), IsUnique = true)]
    public class Item
    {
        public int Id { get; set; }

        public long Top { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Малое описание")]
        public string LowDescription { get; set; }

        [Required]
        [MaxLength(4000)]
        [Display(Name = "Большое описание")]
        public string HighDescription { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [AllowNull]
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
