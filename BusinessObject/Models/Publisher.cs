using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Publisher")]
    public class Publisher
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pub_id")]
        public int Id { get; set; }

        [Column("publisher_name")]
        public string Name { get; set; } = null!;

        [Column("city")]
        public string City { get; set; } = null!;

        [Column("state")]
        public string State { get; set; } = null!;

        [Column("country")]
        public string Country { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = null!;

        public ICollection<User> Users { get; set; } = null!;
    }
}
