using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Database.Repositories;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace EventsWebServiceTests.Tests;

public class BaseTest
{
    private string _connectionString;

    protected RestClient _restClient;

    protected EventsdbContext EventsdbContext { get; set; }

    protected GuidKeyRepository<User> UserRepository { get; set; }

    protected IConfiguration Configuration { get; set; }

    protected RestResponse Response;


    [OneTimeSetUp]
    public virtual void ClassSetup()
    {
        _connectionString = ConfigurationReader.GetConnectionString();
    }

    [SetUp]
    public virtual void TestSetup()
    {
        EventsdbContext = new EventsdbContext(_connectionString);
        UserRepository = new GuidKeyRepository<User>(EventsdbContext);
        var options = new RestClientOptions("http://localhost:60715/")
        {
            ThrowOnAnyError = false,
        };
        _restClient = new RestClient(options);
    }

    [TearDown]
    public virtual void TestCleanup()
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