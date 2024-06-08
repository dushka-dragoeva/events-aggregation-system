using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Database.Repositories;
using Microsoft.Extensions.Configuration;

namespace EventsWebServiceTests.Tests;

public class BaseTest
{
    protected EventsdbContext EventsdbContext { get; set; }

    protected Repository<User> UserRepository { get; set; }

    protected IConfiguration Configuration { get; set; }


    [OneTimeSetUp]
    public void ClassSetup()
    {
        string connectionString = ConfigurationReader.GetConnectionString();
        EventsdbContext = new EventsdbContext();
        UserRepository = new Repository<User>(EventsdbContext);
    }

    [OneTimeTearDown]
    public void ClassCleanup()
    {
        UserRepository.Dispose();
        EventsdbContext.Dispose();
    }


    [Test]
    public async Task Test2()
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

    [Test]
    public void Test3()
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "Test2",
            UserCompanyName = "TestCo",
            UserEmail = "Test2@gmail.com",
            DateRegistered = DateTime.Now.ToString(),
        };
    }
}