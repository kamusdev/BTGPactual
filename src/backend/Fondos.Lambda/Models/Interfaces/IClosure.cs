using Fondos.Lambda.Enums;
using System;

namespace Fondos.Lambda.Models.Interfaces
{
    public interface IClosure
    {
        int ClientId { get; set; }
        int FundId { get; set; }
        Guid MandateId { get; set; }
        OperationType OperationType { get; set; }
        DateTime Date { get; set; }
        decimal Value { get; set; }
    }
}
