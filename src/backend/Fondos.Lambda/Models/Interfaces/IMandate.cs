using System;

namespace Fondos.Lambda.Models.Interfaces
{
    public interface IMandate
    {
        Guid Id { get; set; }
        int ClientId { get; set; }
        int FundId { get; set; }
        DateTime Date { get; set; }
        decimal Value { get; set; }
    }
}
