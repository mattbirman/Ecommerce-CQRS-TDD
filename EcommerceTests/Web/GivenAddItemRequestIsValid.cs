using System;
using Ecommerce;
using Moq;
using Xunit;
using Ecommerce.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceTests.Web
{
    public class GivenAnAddItemRequest
    {
        protected Mock<ICommandBus> MockCommandBus;
        protected AddItemController AddItemController;

        public GivenAnAddItemRequest()
        {
            MockCommandBus = new Mock<ICommandBus>();
            AddItemController = new AddItemController(MockCommandBus.Object);
        }
    }

    public class GivenAddItemRequestIsValid : GivenAnAddItemRequest
    {
        public Guid ItemId { get; }
        public AddItemRequest AddItemRequest { get; }

        public GivenAddItemRequestIsValid() : base()
        {
            ItemId = Guid.NewGuid();
            AddItemRequest = new AddItemRequest(ItemId);
        }

        [Fact]
        public void WhenAnAddItemRequestIsReceived_ThenAnAddItemCommandIsSent()
        {
            //Act
            AddItemController.PostAction(AddItemRequest);

            //Test
            MockCommandBus.Verify(x => x.Send(It.IsAny<AddItemCommand>()));
        }

        [Fact]
        public void WhenIPostTheRequest_ThenItShouldCreateAnItemResource()
        {
            var result = AddItemController.PostAction(AddItemRequest) as CreatedResult;
            var returnedResource = result.Value as Item;
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("item/" + ItemId, result.Location);
            Assert.Equal(ItemId, returnedResource.Id);
        }
    }

    public class GivenAddItemRequestIsInvalid : GivenAnAddItemRequest
    {
        public AddItemRequest AddItemRequest { get; }
        public GivenAddItemRequestIsInvalid() : base()
        {
            AddItemRequest = new AddItemRequest(Guid.Empty);;
        }

        [Fact]
        public void WhenAnAddItemRequestIsReceived_ThenAnAddItemCommandIsNotSent()
        {
            //Act
            AddItemController.PostAction(AddItemRequest);

            //Test
            MockCommandBus.Verify(x => x.Send(It.IsAny<AddItemCommand>()), Times.Never);
        }

        [Fact]
        public void WhenAnAddItemRequestIsReceived_ThenAnAddItemControllerShouldReturn400StatusCode()
        {
            //Act
            var result = AddItemController.PostAction(AddItemRequest) as BadRequestResult;

            //Test
            Assert.Equal(400, result?.StatusCode);
        }
    }
}
