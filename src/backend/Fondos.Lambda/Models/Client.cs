using Fondos.Lambda.Models.Interfaces;

namespace Fondos.Lambda.Models
{
    public class Client : IClient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
