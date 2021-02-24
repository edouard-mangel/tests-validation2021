using System;
using System.Collections.Generic;

namespace Domain
{
    public struct AmortizationTable : IEquatable<AmortizationTable>
    {
        public List<Term> Terms { get; } 

        public AmortizationTable(List<Term> Terms)
        {
            this.Terms = new List<Term>();
        }

        internal void AddTerm(Term term)
        {
            Terms.Add(term);
        }

        public override bool Equals(object obj)
        {
            return obj is AmortizationTable table && this.Equals(table);
        }

        public bool Equals(AmortizationTable other)
        {
            return EqualityComparer<List<Term>>.Default.Equals(this.Terms, other.Terms);
        }

        public static bool operator ==(AmortizationTable left, AmortizationTable right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AmortizationTable left, AmortizationTable right)
        {
            return !(left == right);
        }
    }
}