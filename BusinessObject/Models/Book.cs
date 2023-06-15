using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("Book")]
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("book_id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; } = null!;

        [Column("type")]
        public string Type { get; set; } = null!;

        [Column("pub_id")]
        [ForeignKey("pub_id")]
        public int PublisherId { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("advance")]
        public float Advance { get; set; }

        [Column("royality")]
        public float Royality { get; set; }

        [Column("ytd_sales")]
        public int YtdSales { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("published_date")]
        public DateTime PulishedDate { get; set; }

        public Publisher Publisher { get; set; } = null!;

        [JsonIgnore]
        public ICollection<BookAuthor> BookAuthors { get; set; } = null!;
    }
}
