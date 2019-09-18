using System;

namespace InvoiceMaker.Models
{
    public class FeeLineItem : ILineItem
    {
        public FeeLineItem(decimal amount, string description, DateTimeOffset when)
        {
            Description = description;
            Amount = amount;
            When = when;
        }

        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public DateTimeOffset When { get; private set; }
    }
}