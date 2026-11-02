using System.Text.Json.Serialization;

namespace Consumo_API_CSharp.Models;

public class Verse
{
    [JsonPropertyName("book_id")]
    public string BookId { get; set; } = string.Empty;

    [JsonPropertyName("book_name")]
    public string BookName { get; set; } = string.Empty;

    [JsonPropertyName("chapter")]
    public int Chapter { get; set; }

    [JsonPropertyName("verse")]
    public int Number { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}
