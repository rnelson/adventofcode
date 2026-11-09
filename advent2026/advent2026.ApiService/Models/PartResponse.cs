namespace advent2026.ApiService.Models;

public class PartResponse
{
    public enum PartTypes
    {
        Undefined,
        PartA,
        PartB
    }

    public PartTypes Part { get; set; } = PartTypes.Undefined;
    
    public string Result { get; set; } = string.Empty;

    public IEnumerable<string>? Messages { get; set; } = null;
    public IEnumerable<string>? Errors { get; set; } = null;
    
    public object? AdditionalData { get; set; } = null;
}