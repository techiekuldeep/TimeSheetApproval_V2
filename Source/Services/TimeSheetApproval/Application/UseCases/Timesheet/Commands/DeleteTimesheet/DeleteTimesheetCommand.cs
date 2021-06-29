using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.Timesheet.Commands
{
    public class DeleteTimesheetCommand : IRequest<Response<bool>>
    {
        public long TimesheetId { get; set; }
    }
    public class DeleteTimesheetCommandHandler : IRequestHandler<DeleteTimesheetCommand, Response<bool>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Timesheet> _genRepo;
        private readonly IMapper _mapper;
        public DeleteTimesheetCommandHandler(IGenericRepositoryAsync<Domain.Entities.Timesheet> genRepo, IMapper mapper)
        {
            _genRepo = genRepo;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(DeleteTimesheetCommand request, CancellationToken cancellationToken)
        {
            var TimesheetObj = await _genRepo.GetByIdAsync(request.TimesheetId);
            if (TimesheetObj == null)
            { throw new ApiException($"Timesheet with Id = {request.TimesheetId} not found."); }
            else
            {
                var cmdObj = _mapper.Map<Domain.Entities.Timesheet>(request);
                await _genRepo.DeleteAsync(cmdObj);
                return new Response<bool>(true);
            }
        }
    }
}
