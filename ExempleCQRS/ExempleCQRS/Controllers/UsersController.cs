using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExempleCQRS.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExempleCQRS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async  Task<GetUserById.Dto> Get(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserById(id);
            return await _mediator.Send(query, cancellationToken);
        }
    }
}