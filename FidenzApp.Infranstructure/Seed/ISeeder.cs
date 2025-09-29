namespace FidenzApp.Infranstructure.Seed
{
    public interface ISeeder
    {
        Task SeedUserAsync();
        Task SeedCustomerAsync();
    }
}
