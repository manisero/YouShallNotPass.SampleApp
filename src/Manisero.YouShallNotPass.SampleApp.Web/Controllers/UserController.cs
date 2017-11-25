﻿using System;
using System.Net;
using System.Web.Http;
using Manisero.YouShallNotPass.SampleApp.Commands;

namespace Manisero.YouShallNotPass.SampleApp.Web.Controllers
{
    public class UserController : ApiController
    {
        public IHttpActionResult Get(int userId)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Post(CreateUserCommand command)
        {
            return HandleCommand(command);
        }

        public IHttpActionResult Put(UpdateUserCommand command)
        {
            return HandleCommand(command);
        }

        private IHttpActionResult HandleCommand<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            var result = AppGateway.Instance.Handle(command);

            if (result.Success())
            {
                return Ok();
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ValidationError);
            }
        }
    }
}
