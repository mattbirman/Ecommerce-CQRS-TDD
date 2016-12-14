using System;
using Moq;
using Xunit;
using Ecommerce.Controllers;

namespace EcommerceTests.Web
{
    public class AddItemControllerTests
    {
        private readonly Mock<ICommandBus> _mockCommandBus;
        private readonly AddItemController _addItemController;

        public AddItemControllerTests()
        {
            _mockCommandBus = new Mock<ICommandBus>();
            _addItemController = new AddItemController(_mockCommandBus.Object);
        }

        [Fact]
        public void GivenAnAddItemRequestIsValid_WhenAnAddItemRequestIsReceived_ThenAnAddItemCommandIsSent()
        {
            //Setup
            var addItemRequest = new AddItemRequest(Guid.NewGuid());

            //Act
            _addItemController.PostAction(addItemRequest);

            //Test
            _mockCommandBus.Verify(x => x.Send(It.IsAny<AddItemCommand>()));
        }

        [Fact]
        public void GivenTheUserEntersInvalidData_WhenAnAddItemRequestIsReceived_ThenAnAddItemCommandIsNotSent()
        {
            //Setup
            var addItemRequest = new AddItemRequest(Guid.Empty);

            //Act
            _addItemController.PostAction(addItemRequest);

            //Test
            _mockCommandBus.Verify(x => x.Send(It.IsAny<AddItemCommand>()), Times.Never);
        }

        [Fact]
        public void GivenTheUserEntersInvalidData_WhenAnAddItemRequestIsReceived_ThenAnAddItemControllerShouldReturn400StatusCode()
        {
            //Setup
            var addItemRequest = new AddItemRequest(Guid.Empty);

            //Act
            _addItemController.PostAction(addItemRequest);

            //Test
//            Assert.Equal(Ba,_addItemController.PostAction(addItemRequest));
        }
    }


}
