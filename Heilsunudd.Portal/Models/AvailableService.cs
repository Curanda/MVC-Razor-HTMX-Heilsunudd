public class AvailableService
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public required int Duration { get; set; }
    public required decimal Price { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required bool IsActive { get; set; }
    public required string Location { get; set; }
}