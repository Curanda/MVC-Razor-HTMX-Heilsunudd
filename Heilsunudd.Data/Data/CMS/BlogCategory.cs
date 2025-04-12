using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.CMS;

public class BlogCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBlogCategory { get; set; }
    
    [Required(ErrorMessage = "Please provide category name")]
    [MaxLength(50, ErrorMessage = "Category name can contain up to 50 characters")]
    [Display(Name = "Category name")]
    [Column(TypeName = "nvarchar(50)")]
    public required string CategoryName { get; set; }
    
    
    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}