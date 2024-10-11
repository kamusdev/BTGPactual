namespace Fondos.Lambda.Models.Interfaces
{
    public interface IClient
    {
        int Id { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        decimal Balance { get; set; }
    }
}
