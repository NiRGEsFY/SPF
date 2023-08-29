using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class CategoryGroup
    {
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
