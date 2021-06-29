using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TimeSheetApproval.Application.Exceptions;
using TimeSheetApproval.Application.Interfaces;
using TimeSheetApproval.Application.Wrappers;
using TimeSheetApproval.Domain.Entities;

namespace TimeSheetApproval.Application.UseCases.Timesheet.Commands
{
    public class UpdateTimesheetCommand : IRequest<Response<long>>
    {
        public long TimesheetId { get; set; }
        public long PeopleId { get; set; }
        public long TssTypeId { get; set; }
        public DateTime TimesheetDate { get; set; }
        public DateTime WorkFromTime { get; set; }
        public DateTime WorkToTime { get; set; }
        public decimal WorkTotalTime { get; set; }
    }
    public class UpdateTimesheetCommandHandler : IRequestHandler<UpdateTimesheetCommand, Response<long>>
    {
        private readonly IGenericRepositoryAsync<Domain.Entities.Timesheet> _genRepo;
        private readonly IMapper _mapper;
        public UpdateTimesheetCommandHandler(IGenericRepositoryAsync<Domain.Entities.Timesheet> genRepo, IMapper mapper)
        {
            _genRepo = genRepo;
            _mapper = mapper;
        }
        public async Task<Response<long>> Handle(UpdateTimesheetCommand request, CancellationToken cancellationToken)
        {
            var TimesheetObj = await _genRepo.GetByIdAsync(request.TimesheetId);
            if (TimesheetObj == null)
            { throw new ApiException($"Timesheet with Id = {request.TimesheetId} not found."); }
            else
            {
                var cmdObj = _mapper.Map<Domain.Entities.Timesheet>(request);
                await _genRepo.UpdateAsync(cmdObj);
                return new Response<long>(cmdObj.TimesheetId);
            }
        }
    }
}
