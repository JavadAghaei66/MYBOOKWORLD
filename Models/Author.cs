using System.ComponentModel.DataAnnotations;

namespace MyBookWorld.Models;
public class Author {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(100)]
    public string Name {get; set;} = string.Empty;

    public string Biography  {get; set;} = string.Empty;

    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public ICollection<Book>? Books { get; set; }
}
