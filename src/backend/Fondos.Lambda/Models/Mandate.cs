using Fondos.Lambda.Models.Interfaces;
using System;

namespace Fondos.Lambda.Models
{
    public class Mandate : IMandate
    {
        public Guid Id { get; set; }
        public int ClientId { get; set; }
        public int FundId { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
