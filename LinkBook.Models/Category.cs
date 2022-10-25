using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LinkBook.Models;

public class Category
{
    [Key] [DisplayName("Book ID")] public int Id { get; set; }


    [Required] [DisplayName("Book Name")] public string Name { get; set; }


    [DisplayName("Display Order")] public int DisplayOrder { get; set; }


    [DisplayName("Create Date Time")] public DateTime CreateDateTime { get; set; } = DateTime.Now;
}