using EventsWebServiceTests.Configuration;
using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.RequestsFactories;
using EventsWebServiceTests.Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace EventsWebServiceTests.Tests;

public class BaseTest
{

    private string _connectionString;

    protected IConfiguration Configuration { get; private set; }

    protected RestClient RestClient { get; private set; }

    protected EventsdbContext EventsdbContext { get; private set; }

    protected IntKeyRepository<object> IntKeyRepository { get; set; }

    protected GuidKeyRepository<object> GuidKeyRepository { get; set; }

    protected FileDownloadEventRepository FileDownloadEventRepository { get; set; }

    protected UserLoginEventRepository UserLoginEventRepository { get; private set; }

    protected UserLogoutEventRepository UserLogoutEventRepository { get; private set; }

    protected UserRepository UserRepository { get; private set; }

    protected ProductActionTrakingRepository ProductActionTrakingRepository { get; private set; }

    protected FileDownloadRequestFactory FileDownlioadRequestFactory { get; private set; }

    protected UserLoginRequestFactory UserLoginRequestFactory { get; private set; }

    public UserLogoutRequestFactory UserLogoutRequestFactory { get; private set; }

    public UserRegisteredRequestFactory UserRegisteredRequestFactory { get; private set; }

    public ProductActionRequestFactory ProductActionRequestFactory { get; private set; }

    protected RestRequest Request { get; set; }

    protected RestResponse Response { get; set; }

    protected FileDownloadDto FileDownloadDto { get; set; }

    protected UserLoginDto UserLoginDto { get; set; }

    protected UserLogoutDto UserLogoutDto { get; set; }

    protected UserRegisteredDto UserRegisteredDto { get; set; }

    protected ProductActionDto ProductActionDto { get; set; }


    [OneTimeSetUp]
    public virtual void ClassSetup()
    {
        _connectionString = ConfigurationReader.GetConnectionString();

        FileDownlioadRequestFactory = new FileDownloadRequestFactory();
        UserLoginRequestFactory = new UserLoginRequestFactory();
        UserLogoutRequestFactory = new UserLogoutRequestFactory();
        UserRegisteredRequestFactory = new UserRegisteredRequestFactory();
        ProductActionRequestFactory = new ProductActionRequestFactory();
    }

    [SetUp]
    public virtual void TestSetup()
    {
        var options = new RestClientOptions("http://localhost:60715/")
        {
            ThrowOnAnyError = false,
        };
        RestClient = new RestClient(options);

        EventsdbContext = new EventsdbContext(_connectionString);
        IntKeyRepository = new IntKeyRepository<object>(EventsdbContext);
        GuidKeyRepository = new GuidKeyRepository<object>(EventsdbContext);

        FileDownloadEventRepository = new FileDownloadEventRepository(EventsdbContext);
        UserLoginEventRepository = new UserLoginEventRepository(EventsdbContext);
        UserLogoutEventRepository = new UserLogoutEventRepository(EventsdbContext);
        UserRepository = new UserRepository(EventsdbContext);
        ProductActionTrakingRepository = new ProductActionTrakingRepository(EventsdbContext);
    }

    [TearDown]
    public virtual void TestCleanup()
    {
        RestClient.Dispose();
        EventsdbContext.Dispose();
        IntKeyRepository.Dispose();
        GuidKeyRepository.Dispose();
        FileDownloadEventRepository.Dispose();
        UserLoginEventRepository.Dispose();
        UserRepository.Dispose();
        UserLogoutEventRepository.Dispose();
        ProductActionTrakingRepository.Dispose();
    }
}