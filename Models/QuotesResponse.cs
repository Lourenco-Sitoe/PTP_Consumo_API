namespace Consumo_API_CSharp.Models;

public class QuotesResponse
{
    public List<Quote> Quotes { get; set; } = new();

    public int Total { get; set; }

    public int Skip { get; set; }

    public int Limit { get; set; }
}
