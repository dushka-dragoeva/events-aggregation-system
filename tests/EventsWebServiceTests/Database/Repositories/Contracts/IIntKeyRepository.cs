namespace EventsWebServiceTests.Database.Repositories.Contracts
{
    public interface IIntKeyRepository<T> : IRepository<T, int> where T : class
    {
    }
}
