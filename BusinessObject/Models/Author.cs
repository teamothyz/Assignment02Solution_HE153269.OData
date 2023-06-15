using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Author")]
    public class Author
    {
        [Column("author_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Column("phone")]
        public string Phone { get; set; } = null!;

        [Column("address")]
        public string Address { get; set; } = null!;

        [Column("city")]
        public string City { get; set; } = null!;

        [Column("state")]
        public string State { get; set; } = null!;

        [Column("zip")]
        public string Zip { get; set; } = null!;

        [Column("email_address")]
        public string Email { get; set; } = null!;

        [JsonIgnore]
        public ICollection<BookAuthor> BookAuthors { get; set; } = null!;
    }
}
