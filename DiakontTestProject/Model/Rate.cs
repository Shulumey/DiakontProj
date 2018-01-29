using System;

namespace DiakontTestProject.Model
{
    public class Rate
    {
        public ReferenceItem Position { get; set; }
        public int Amount { get; set; }
        public DateTime ApplyFromDate { get; set; }

        public Rate(ReferenceItem position, int amount, DateTime applyFrom)
        {
            Position = position;
            Amount = amount;
            ApplyFromDate = applyFrom;
        }
    }
}
