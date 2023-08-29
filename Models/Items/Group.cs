using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SPF.Models.Items
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
