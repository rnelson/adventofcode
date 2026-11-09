namespace advent2026.ApiService.Models;

public class AdventInput(int Day)
{
    public IEnumerable<string> TestInputA { get; set; } = [];
    public IEnumerable<string> InputA { get; set; } = [];
    
    public IEnumerable<string> TestInputB { get; set; } = [];
    public IEnumerable<string> InputB { get; set; } = [];
}