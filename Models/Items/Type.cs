using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class Type
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
