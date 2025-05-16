using System.ComponentModel.DataAnnotations;

namespace MyBookWorld.Models;
public class Quote {
    [Key]
    public int Id {get; set;}

    [Required]
    [StringLength(200)]
    public string TheQuote {get; set;} = string.Empty;

    [Required]
    public string Speaker {get; set;} = string.Empty;
}