using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBookWorld.Models;
public class Book {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(200)]
    public string Title {get; set;} = string.Empty;

    [StringLength(500)]
    public string Description  {get; set;} = string.Empty;

    [DisplayName("Book's Cover Url")]
    public string CoverImageUrl  {get; set;} = string.Empty;

    public DateTime PublishedDate {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public string Summary {get; set;} = string.Empty;

    public byte[]? Content {get; set;}
    public string? ContentType {get; set;}

    public int AuthorId {get; set;}

    [ForeignKey("AuthorId")]
    public Author? Author {get; set;}

    public int CategoryId {get; set;}
    
    [ForeignKey("CategoryId")]
    public Category? Category {get; set;}

}