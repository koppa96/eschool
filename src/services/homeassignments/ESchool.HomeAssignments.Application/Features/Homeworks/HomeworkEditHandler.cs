﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.HomeAssignments.Application.Extensions;
using ESchool.HomeAssignments.Application.Features.Homeworks.Common;
using ESchool.HomeAssignments.Domain;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Homeworks
{
    public class HomeworkEditCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Optional { get; set; }
        public DateTime Deadline { get; set; }
    }
    
    public class HomeworkEditHandler : IRequestHandler<EditCommand<HomeworkEditCommand, HomeworkDetailsResponse>, HomeworkDetailsResponse>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IMapper mapper;

        public HomeworkEditHandler(HomeAssignmentsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<HomeworkDetailsResponse> Handle(EditCommand<HomeworkEditCommand, HomeworkDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var homework = await context.Homeworks.Include(x => x.CreatedBy)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);
            
            homework.Title = request.InnerCommand.Title;
            homework.Description = request.InnerCommand.Description;
            homework.Optional = request.InnerCommand.Optional;
            homework.Deadline = homework.Deadline <= request.InnerCommand.Deadline
                ? request.InnerCommand.Deadline
                : throw new InvalidOperationException("A határidő nem módosítható az eredetinél későbbi időpontra.");

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<HomeworkDetailsResponse>(homework);
        }
    }
}