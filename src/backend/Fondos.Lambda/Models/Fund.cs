using Fondos.Lambda.Models.Interfaces;

namespace Fondos.Lambda.Models
{
    public class Fund : IFund
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal MinValue { get; set; }
        public string Category { get; set; }
    }
}
