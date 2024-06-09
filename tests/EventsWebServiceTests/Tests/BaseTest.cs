using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Database.Repositories;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace EventsWebServiceTests.Tests;

public class BaseTest
{
    protected RestClient _restClient;

    protected EventsdbContext EventsdbContext { get; set; }

    protected GuidKeyRepository<User> UserRepository { get; set; }

    protected IConfiguration Configuration { get; set; }

    protected RestResponse Response;


    [OneTimeSetUp]
    public virtual void ClassSetup()
    {
        string connectionString = ConfigurationReader.GetConnectionString();
        EventsdbContext = new EventsdbContext(connectionString);
        UserRepository = new GuidKeyRepository<User>(EventsdbContext);
        _restClient = new RestClient();
    }

    [OneTimeTearDown]
    public virtual void ClassCleanup()
    {
        UserRepository.Dispose();
        EventsdbContext.Dispose();
        _restClient.Dispose();
    }


    [Test]
    public async Task DatabaseTest()
    {
        var users = await UserRepository.GetAllAsync();
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Test2",
            UserCompanyName = "TestCo",
            UserEmail = "Test2@gmail.com",
            DateRegistered = DateTime.Now.ToString(),
        };

        await UserRepository.AddAsync(user);
        await UserRepository.DeleteAsync(user.Id);
    }
}