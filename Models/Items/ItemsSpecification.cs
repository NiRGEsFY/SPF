using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class ItemsSpecification
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Название группы")]
        public string Name { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Размеры")]
        public string? Size { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Материал")]
        public string? Material { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Модель")]
        public string? Model { get; set; }

        [Required]
        [MaxLength(25)]
        [Display(Name = "Цвет")]
        public string? Color { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Класс защиты")]
        public string? ProtectiveClass { get; set; }

        [Required]
        [Display(Name = "Весс, кг")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Weight { get; set; }

        [Required]
        [MaxLength(400)]
        [Display(Name = "Особенность")]
        public string? Character { get; set; }

        [MaxLength(100)]
        [Display(Name = "Материал внутри")]
        public string? MaterialInside { get; set; }

        [MaxLength(25)]
        [Display(Name = "Рост")]
        public string? Growth { get; set; }

        [MaxLength(50)]
        [Display(Name = "Класс вязки")]
        public string? MatingClass { get; set; }

        [MaxLength(50)]
        [Display(Name = "Кол-во нитей")]
        public string? Thread { get; set; }

        [MaxLength(25)]
        [Display(Name = "Высота, мм")]
        public string? Height { get; set; }
    }
}
