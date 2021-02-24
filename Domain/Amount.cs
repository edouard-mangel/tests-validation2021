using System;

namespace Domain
{
    public struct Amount : IEquatable<Amount>
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            this.Value = ConvertAsCurrencyAmount(value);
        }

        public override bool Equals(object other)
        {
            return other is Amount value && this.Equals(value);
        }
        public bool Equals(Amount other)
        {
            return this.Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public static bool operator ==(Amount left, Amount right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Amount left, Amount right)
        {
            return !(left == right);
        }

        public static implicit operator decimal(Amount amount)
        {
            return amount.Value;
        }

        public static implicit operator Amount(decimal amount)
        {
            return new Amount(amount);
        }

        private static decimal ConvertAsCurrencyAmount(decimal amount)
        {
            return decimal.Round(amount, 2);
        }
    }
}