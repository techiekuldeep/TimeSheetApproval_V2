using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Commands;
using TimeSheetApproval.Application.UseCases.TimesheetsStatusType.Queries;

namespace TimeSheetApproval.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class TimesheetsStatusTypesController : BaseApiController
    {
        [HttpPost("get-Timesheet-status-types")]
        //[Authorize]
        public async Task<IActionResult> Get([FromBody] SearchFilters filter)
        {
            return Ok(await Mediator.Send(new GetAllTimesheetsStatusTypesbyFiltersQuery() { SearchFilter = filter }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateTimesheetsStatusTypeCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{TimesheetsStatusTypeId}")]
        //[Authorize]
        public async Task<IActionResult> Put(int TimesheetsStatusTypeId, UpdateTimesheetsStatusTypeCommand command)
        {
            if (TimesheetsStatusTypeId != command.TssTypeId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{TimesheetsStatusTypeId}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int TimesheetsStatusTypeId, DeleteTimesheetsStatusTypesCommand command)
        {
            if (TimesheetsStatusTypeId != command.TssTypeId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
