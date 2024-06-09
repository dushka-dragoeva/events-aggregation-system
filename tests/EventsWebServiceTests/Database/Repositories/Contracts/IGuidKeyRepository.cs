namespace EventsWebServiceTests.Database.Repositories.Contracts
{
    public interface IGuidKeyRepository<T> : IRepository<T, string> where T : class
    {
    }
}
