using System.ComponentModel.DataAnnotations;

using MyBookWorld.Models;
public class Category {
    [Key]
    public int Id {get; set;}
    [Required]
    [StringLength(100)]
    public string Name {get; set;} = string.Empty;

     public ICollection<Book>? Books { get; set; }

}