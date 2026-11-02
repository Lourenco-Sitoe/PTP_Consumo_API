using System.Text.Json.Serialization;

namespace Consumo_API_CSharp.Models;

public class BibleResponse
{
    [JsonPropertyName("reference")]
    public string Reference { get; set; } = string.Empty;

    [JsonPropertyName("verses")]
    public List<Verse> Verses { get; set; } = new();

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("translation_id")]
    public string TranslationId { get; set; } = string.Empty;

    [JsonPropertyName("translation_name")]
    public string TranslationName { get; set; } = string.Empty;

    [JsonPropertyName("translation_note")]
    public string TranslationNote { get; set; } = string.Empty;
}
