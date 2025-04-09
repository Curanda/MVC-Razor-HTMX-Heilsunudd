using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heilsunudd.Data.Data.CMS;

public class BlogPost
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdBlogPost { get; set; }
    
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title can contain up to 100 characters")]
    [Display(Name = "Title")]
    public required string Title { get; set; }
    
    [Required(ErrorMessage = "Content is required")]
    [MaxLength(3000, ErrorMessage = "Content can contain up to 3000 characters")]
    [Display(Name = "Content")]
    public required string Content { get; set; }
    
    [Required(ErrorMessage = "Image URL is required")]
    [MaxLength(200, ErrorMessage = "Image URL can contain up to 200 characters")]
    [Display(Name = "Image URL")]
    [DataType(DataType.ImageUrl)]
    [Url(ErrorMessage = "Invalid URL format")]
    public required UrlAttribute ImageUrl { get; set; }
    
    [Required(ErrorMessage = "Author name is required")]
    [MaxLength(50, ErrorMessage = "Author name can contain up to 50 characters")]
    [Display(Name = "Author name")]
    public required string AuthorName { get; set; }
    [Required(ErrorMessage = "Publication date is required")]
    [Display(Name = "Publication date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public required DateOnly PublicationDate { get; set; }
    
    [Required(ErrorMessage = "Category is required")]
    [MaxLength(50, ErrorMessage = "Category can contain up to 50 characters")]
    [Display(Name = "Category")]
    [ForeignKey("BlogCategory.CategoryName")]
    public required string CategoryName { get; set; }
    
    [Required(ErrorMessage = "Provide at least 2 tags separated by coma")]
    [MaxLength(100, ErrorMessage = "Tags can contain up to 100 characters")]
    [Display(Name = "Tags")]
    public required string Tags { get; set; }
}