using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LinkBook.Models;

public class CoverType
{
    [Key] public int Id { get; set; }

    [Required]
    [DisplayName("Cover Name")]
    public string Name { get; set; }
}