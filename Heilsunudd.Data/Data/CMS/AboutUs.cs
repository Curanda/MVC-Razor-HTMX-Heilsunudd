using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.CMS;

public class AboutUs
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAboutUs { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(50, ErrorMessage = "Title can contain up to 50 characters")]
    [Display(Name = "Title")]
    [Column(TypeName = "nvarchar(50)")]
    public required string Title { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    [MaxLength(2000, ErrorMessage = "Content can contain up to 2000 characters")]
    [Display(Name = "Content")]
    [Column(TypeName = "nvarchar(max)")]
    public required string Content { get; set; }
    
    [Required(ErrorMessage = "Image URL is required")]
    [MaxLength(200, ErrorMessage = "Image URL can contain up to 200 characters")]
    [Display(Name = "Image URL")]
    [Url(ErrorMessage = "Invalid URL format")]
    [Column(TypeName = "nvarchar(200)")]
    public required string ImageUrl { get; set; }
    
    [Required(ErrorMessage = "Position is required")]
    [Display(Name = "Position on website")]
    public int Position { get; set; }
}