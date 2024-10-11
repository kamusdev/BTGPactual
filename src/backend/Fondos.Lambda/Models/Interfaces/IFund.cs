namespace Fondos.Lambda.Models.Interfaces
{
    public interface IFund
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal MinValue { get; set; }
        string Category { get; set; }
    }
}
