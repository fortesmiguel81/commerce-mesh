namespace OrderService.Domain.Shared;

public record Currency
{
    internal static readonly Currency None = new("");

    public static readonly Currency Usd = new("USD");

    public static readonly Currency Eur = new("EUR");

    private Currency(string code) => Code = code;

    public string Code { get; init; }

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
               throw new ApplicationException($"Currency with code {code} is not supported.");
    }

    public static readonly IReadOnlyCollection<Currency> All = new List<Currency> { Usd, Eur };
}
