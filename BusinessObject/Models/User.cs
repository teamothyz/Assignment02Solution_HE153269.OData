using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("User")]
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int Id { get; set; }

        [Column("email_address")]
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("source")]
        public string Source { get; set; } = null!;

        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Column("middle_name")]
        public string MiddleName { get; set; } = null!;

        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [Column("role_id")]
        [ForeignKey("role_id")]
        public int RoleId { get; set; }

        [Column("pub_id")]
        [ForeignKey("pub_id")]
        public int PublisherId { get; set; }

        [Column("hire_date")]
        public DateTime HireDate { get; set; }

        public Publisher Publisher { get; set; } = null!;

        public Role Role { get; set; } = null!;
    }
}
