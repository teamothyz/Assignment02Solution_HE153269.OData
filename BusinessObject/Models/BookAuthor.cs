using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("BookAuthor")]
    public class BookAuthor
    {
        [Column("author_id")]
        [ForeignKey("author_id")]
        public int AuthorId { get; set; }

        [Column("book_id")]
        [ForeignKey("book_id")]
        public int BookId { get; set; }

        [Column("author_order")]
        public int AuthorOrder { get; set; }

        [Column("royality_percentage")]
        public float RoyalityPercentage { get; set; }

        public Book Book { get; set; } = null!;

        public Author Author { get; set; } = null!;
    }
}
