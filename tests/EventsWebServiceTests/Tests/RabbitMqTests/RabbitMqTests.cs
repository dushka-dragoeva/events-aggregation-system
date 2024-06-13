using EventsWebServiceTests.Database.Factories;
using EventsWebServiceTests.Database.Models;
using EventsWebServiceTests.Database.Repositories;
using EventsWebServiceTests.HttpInfrastructure.Factorties.DtoFactories;
using EventsWebServiceTests.Infrastructure.Dtos;
using EventsWebServiceTests.RabbitMq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RestSharp;

namespace EventsWebServiceTests.Tests.RabbitMqTests
{
    [TestFixture]
    public class RabbitMqTests : RabbitMqBaseTest
    {
        private string _expectedMessage;
        private dynamic _expectedEvent;
        private dynamic _actualEvent;

        [TearDown]
        public new async Task TestCleanup()
        {
            await DeleteEntity();
            Connection.Dispose();
            Channel.Dispose();
            base.TestCleanup();
        }

        [Test]
        public async Task CorrectMessageIsSentToRabbitMqTest_And_DatabaseRecordIsAdded_When_PostFileDownloadEvent()
        {
            // Arrange
            FileDownloadDto = FileDownloadEventDtoFactory.BuildValidDto();
            Request = FileDownlioadRequestFactory.BuildValidRequest(FileDownloadDto);
            _expectedMessage = JsonConvert.SerializeObject(MesageModelsFactory.BuildFileDownloadEventMessage(FileDownloadDto));
            _expectedEvent = EventFactory.BuildExpextedFileDounloadEvent(FileDownloadDto) as FileDownloadEvent;
            string consumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            // Act
            Response = await RestClient.ExecuteAsync(Request);
            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: Consumer);

            WaitDatabaseToBeUpdated();
            _actualEvent = await FileDownloadEventRepository.GetByEventIdAcync(FileDownloadDto.Id) as FileDownloadEvent;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(ReceivedMessage, Is.EqualTo(_expectedMessage));
                Assert.AreEqual(_expectedEvent.EventId, _actualEvent.EventId);
                Assert.AreEqual(_expectedEvent.Date, _actualEvent.Date);
                Assert.AreEqual(_expectedEvent.FileName, _actualEvent.FileName);
                Assert.AreEqual(_expectedEvent.FileLenght, _actualEvent.FileLenght);
            });
        }

        [Test]
        public async Task CorrectMessageIsSentToRabbitMqTest_And_DatabaseRecordIsAdded_When_PostUserLoginEvent()
        {
            // Arrange
            UserLoginDto = UserLoginEventDtoFactory.BuildValidDto();
            Request = UserLoginRequestFactory.BuildValidRequest(UserLoginDto);
            _expectedMessage = JsonConvert.SerializeObject(MesageModelsFactory.BuildUserLoginEventMessage(UserLoginDto));
            _expectedEvent = EventFactory.BuildExpectedUserLoginEvent(UserLoginDto) as UserLoginEvent;
            string consumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            // Act
            Response = await RestClient.ExecuteAsync(Request);
            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: Consumer);

            WaitDatabaseToBeUpdated();
            _actualEvent = await UserLoginEventRepository.GetByUserIdAcync(UserLoginDto.UserId) as UserLoginEvent;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(ReceivedMessage, Is.EqualTo(_expectedMessage));
                Assert.AreEqual(_expectedEvent.UserId, _actualEvent.UserId);
                Assert.AreEqual(_expectedEvent.Date, _actualEvent.Date);
                Assert.AreEqual(_expectedEvent.Email, _actualEvent.Email);
            });
        }

        [Test]
        public async Task CorrectMessageIsSentToRabbitMqTest_And_DatabaseRecordIsAdded_When_PostUserLogoutEvent()
        {
            // Arrange
            UserLogoutDto = UserLogoutEventDtoFactory.BuildValidDto();
            Request = UserLogoutRequestFactory.BuildValidRequest(UserLogoutDto);
            _expectedMessage = JsonConvert.SerializeObject(MesageModelsFactory.BuildUserLogoutEventMessage(UserLogoutDto));
            _expectedEvent = EventFactory.BuildExpectedUserLogoutEvent(UserLogoutDto) as UserLogOutEvent;
            string consumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            // Act
            Response = await RestClient.ExecuteAsync(Request);
            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: Consumer);

            WaitDatabaseToBeUpdated();
            _actualEvent = await UserLogoutEventRepository.GetByEmailAcync(UserLogoutDto.Email) as UserLogOutEvent;

            Assert.Multiple(() =>
            {
                Assert.That(ReceivedMessage, Is.EqualTo(_expectedMessage));
                Assert.AreEqual(_expectedEvent.LogoutTime, _actualEvent.LogoutTime);
                Assert.AreEqual(_expectedEvent.Email, _actualEvent.Email);
            });
        }

        [Test]
        public async Task CorrectMessageIsSentToRabbitMqTest_And_DatabaseRecordIsAdded_When_PostUserRegisteredEvent()
        {
            // Arrange
            UserRegisteredDto = UserRegisteredEventDtoFactory.BuildValidDto();
            Request = UserRegisteredRequestFactory.BuildValidRequest(UserRegisteredDto);
            _expectedMessage = JsonConvert.SerializeObject(MesageModelsFactory.BuildUserRegisteredEventMessage(UserRegisteredDto));
            _expectedEvent = EventFactory.BuildExpectedUserRegisteredEvent(UserRegisteredDto) as User;
            string consumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            // Act
            Response = await RestClient.ExecuteAsync(Request);
            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: Consumer);

            WaitDatabaseToBeUpdated();
            _actualEvent = await UserRepository.GetByEmailAcync(UserRegisteredDto.Email) as User;

            Assert.Multiple(() =>
            {
                Assert.That(ReceivedMessage, Is.EqualTo(_expectedMessage));
                Assert.AreEqual(_expectedEvent.UserCompanyName, _actualEvent.UserCompanyName);
                Assert.AreEqual(_expectedEvent.UserEmail, _actualEvent.UserEmail);
                Assert.AreEqual(_expectedEvent.UserName, _actualEvent.UserName);
            });
        }

        [Test]
        public async Task CorrectMessageIsSentToRabbitMqTest_And_DatabaseRecordIsAdded_When_PostProductInstalledEvent()
        {
            // Arrange
            ProductActionDto = ProductActionEventDtoFactory.BuildValidDto();
            Request = ProductActionRequestFactory.BuildValidRequest(ProductActionDto, EventType.ProductInstalled);
            _expectedMessage = JsonConvert.SerializeObject(MesageModelsFactory.BuildProductInstalledEventMessage(ProductActionDto));
            _expectedEvent = EventFactory.BuildExpectedProductActionEvent(ProductActionDto) as ProductActionTraking;
            string consumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            // Act
            Response = await RestClient.ExecuteAsync(Request);
            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: Consumer);

            WaitDatabaseToBeUpdated();
            _actualEvent = await ProductActionTrakingRepository.GetByUserIdAcync(ProductActionDto.UserId) as ProductActionTraking;

            Assert.Multiple(() =>
            {
                Assert.That(ReceivedMessage, Is.EqualTo(_expectedMessage));
                Assert.AreEqual(_expectedEvent.ProductName, _actualEvent.ProductName);
                Assert.AreEqual(_expectedEvent.ProductVersion, _actualEvent.ProductVersion);
                Assert.AreEqual("Instalation", _actualEvent.ActionType);
                Assert.AreEqual(_expectedEvent.UserId, _actualEvent.UserId);
                Assert.AreEqual(_expectedEvent.Date, _actualEvent.Date);
            });
        }

        [Test]
        public async Task CorrectMessageIsSentToRabbitMqTest_And_DatabaseRecordIsAdded_When_PostProductUninstalledEvent()
        {
            // Arrange
            ProductActionDto = ProductActionEventDtoFactory.BuildValidDto();
            Request = ProductActionRequestFactory.BuildValidRequest(ProductActionDto, EventType.ProductUninstalled);
            _expectedMessage = JsonConvert.SerializeObject(MesageModelsFactory.BuildProductUninstalledEventMessage(ProductActionDto));
            _expectedEvent = EventFactory.BuildExpectedProductActionEvent(ProductActionDto) as ProductActionTraking;
            string consumerTag = Channel.BasicConsume(QueueName, false, Consumer);

            // Act
            Response = await RestClient.ExecuteAsync(Request);
            Channel.BasicConsume(queue: QueueName, autoAck: true, consumer: Consumer);

            WaitDatabaseToBeUpdated();
            _actualEvent = await ProductActionTrakingRepository.GetByUserIdAcync(ProductActionDto.UserId) as ProductActionTraking;

            Assert.Multiple(() =>
            {
                Assert.That(ReceivedMessage, Is.EqualTo(_expectedMessage));
                Assert.AreEqual(_expectedEvent.ProductName, _actualEvent.ProductName);
                Assert.AreEqual(_expectedEvent.ProductVersion, _actualEvent.ProductVersion);
                Assert.AreEqual("Uninstalation", _actualEvent.ActionType);
                Assert.AreEqual(_expectedEvent.UserId, _actualEvent.UserId);
                Assert.AreEqual(_expectedEvent.Date, _actualEvent.Date);
            });
        }

        public async Task DeleteEntity()
        {
            if (_actualEvent != null)
            {
                var type = _actualEvent.GetType();
                switch (type)
                {
                    case Type t when t == typeof(FileDownloadEvent):
                        WaitDatabaseToBeUpdated();
                        await FileDownloadEventRepository.DeleteAsync(_actualEvent.Id);
                        break;

                    case Type t when t == typeof(UserLoginEvent):
                        WaitDatabaseToBeUpdated();
                        await UserLoginEventRepository.DeleteAsync(_actualEvent.Id);
                        break;

                    case Type t when t == typeof(UserLogOutEvent):
                        WaitDatabaseToBeUpdated();
                        await UserLogoutEventRepository.DeleteAsync(_actualEvent.Id);
                        break;

                    case Type t when t == typeof(User):
                        WaitDatabaseToBeUpdated();
                        await UserRepository.DeleteAsync(_actualEvent.Id);
                        break;

                    case Type t when t == typeof(ProductActionTraking):
                        WaitDatabaseToBeUpdated();
                        await ProductActionTrakingRepository.DeleteAsync(_actualEvent.Id);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}