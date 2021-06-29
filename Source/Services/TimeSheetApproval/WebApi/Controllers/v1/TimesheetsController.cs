using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Parameters;
using TimeSheetApproval.Application.UseCases.Timesheet.Commands;
using TimeSheetApproval.Application.UseCases.Timesheet.Queries;

namespace TimeSheetApproval.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class TimesheetsController : BaseApiController
    {
        [HttpPost("get-timesheets-details")]
        //[Authorize]
        public async Task<IActionResult> Get([FromBody] SearchFilters filter)
        {
            return Ok(await Mediator.Send(new GetAllTimeSheetsbyFiltersQuery() { SearchFilter = filter }));
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateTimesheetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("{timesheetId}")]
        //[Authorize]
        public async Task<IActionResult> Put(int timesheetId, UpdateTimesheetCommand command)
        {
            if (timesheetId != command.TimesheetId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{timesheetId}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int timesheetId, DeleteTimesheetCommand command)
        {
            if (timesheetId != command.TimesheetId)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
