using Moq;
using Xunit;

namespace EcommerceTests.Web
{
    public class AddItemControllerTests
    {
        public AddItemControllerTests()
        {
        }

        [Fact]
        public void GivenAnAddItemRequestIsValid_WhenAnAddItemRequestIsReceived_ThenAnAddItemCommandIsSent()
        {
            //Setup
            var mockCommandBus = new Mock<ICommandBus>();
            var addItemController = new AddItemController(mockCommandBus.Object);
            var addItemRequest = new AddItemRequest();


            //Act
            addItemController.PostAction(addItemRequest);

            //Test
            mockCommandBus.Verify(x => x.Send(It.IsAny<AddItemCommand>()));
        }
    }

    public interface ICommandBus
    {
        void Send(AddItemCommand @is);
    }

    public class AddItemCommand
    {
    }

    public class AddItemRequest
    {
    }

    public class AddItemController
    {
        private readonly ICommandBus _commandBus;

        public AddItemController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public void PostAction(AddItemRequest addItemRequest)
        {
            _commandBus.Send(new AddItemCommand());
        }
    }
}
