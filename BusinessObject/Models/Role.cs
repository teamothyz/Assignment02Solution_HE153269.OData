using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Role")]
    public class Role
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("role_id")]
        public int Id { get; set; }

        [Column("role_desc")]
        public string Description { get; set; } = null!;

        [JsonIgnore]
        public ICollection<User> Users { get; set; } = null!;
    }
}
