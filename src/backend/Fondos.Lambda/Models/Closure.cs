using Fondos.Lambda.Enums;
using Fondos.Lambda.Models.Interfaces;
using System;

namespace Fondos.Lambda.Models
{
    public class Closure : IClosure
    {
        public int ClientId { get; set; }
        public int FundId { get; set; }
        public Guid MandateId { get; set; }
        public OperationType OperationType { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
