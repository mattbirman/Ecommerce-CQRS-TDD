﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class AddItemController : Controller
    {
        private readonly ICommandBus _commandBus;

        public AddItemController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public ActionResult PostAction(AddItemRequest addItemRequest)
        {
            if (IsNotValid(addItemRequest))
                return BadRequest();

            _commandBus.Send(new AddItemCommand());

            return Created("item/" + addItemRequest.Id, new Item(addItemRequest.Id));
        }

        private static bool IsNotValid(AddItemRequest addItemRequest)
        {
            return addItemRequest.Id == Guid.Empty;
        }
    }


    public interface ICommandBus
    {
        void Send(AddItemCommand addItemCommand);
    }

    public class AddItemCommand
    {
    }

    public class AddItemRequest
    {
        public AddItemRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}