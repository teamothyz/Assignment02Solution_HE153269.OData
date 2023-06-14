using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models
{
    [Table("BookAuthor")]
    [Index(nameof(AuthorId), nameof(BookId), IsUnique = true)]
    public class BookAuthor
    {
        [Column("bookauthor_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("author_id", Order = 1)]
        [ForeignKey("author_id")]
        public int AuthorId { get; set; }

        [Column("book_id", Order = 2)]
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
