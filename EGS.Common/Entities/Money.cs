using EGS.Domain.Enums;
using System.Text.Json.Serialization;

namespace EGS.Domain.Entities
{
    public readonly struct Money
    {
        [JsonConstructor]
        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public override string ToString()
            => (Currency == Currency.USD ? "$" : "£") + Amount;

        public decimal Amount { get; }
        public Currency Currency { get; }
    }
}
