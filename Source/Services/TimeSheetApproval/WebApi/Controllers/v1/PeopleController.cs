using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.People.Commands;
using TimeSheetApproval.Application.UseCases.People.Queries;

namespace TimeSheetApproval.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class PeopleController : BaseApiController
    {
        [HttpPost("get-people-details")]
        //[Authorize]
        public async Task<IActionResult> Get([FromBody] SearchFilters filter)
        {
            return Ok(await Mediator.Send(new GetAllPeoplesbyFiltersQuery() { SearchFilter = filter }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreatePeopleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{peopleId}")]
        //[Authorize]
        public async Task<IActionResult> Put(int peopleId, UpdatePeopleCommand command)
        {
            if (peopleId != command.PeopleId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{peopleId}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int peopleId, DeletePeopleCommand command)
        {
            if (peopleId != command.PeopleId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
