namespace advent2026.ApiService.Models;

public class AdventResponse
{
    public int Day { get; set; }
    public int Year => 2026;
    
    public string? PartA { get; set; }
    public string? PartB { get; set; }

    public IEnumerable<string>? Messages { get; set; }
    public IEnumerable<string>? Errors { get; set; }
    
    public object? AdditionalData { get; set; } = null;
}