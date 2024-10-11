using Fondos.Lambda.Enums;
using System;

namespace Fondos.Lambda.Models.Interfaces
{
    public interface ITransaction
    {
        Guid Id { get; set; }
        Guid MandateId { get; set; }
        int ClientId { get; set; }
        OperationType OperationType { get; set; }
        int FundId { get; set; }
        DateTime Date { get; set; }
        decimal Value { get; set; }
    }
}
