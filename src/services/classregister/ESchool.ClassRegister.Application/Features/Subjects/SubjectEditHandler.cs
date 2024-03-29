﻿using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Application.Features.Subjects.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Interface.Commands;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectEditHandler : IRequestHandler<EditCommand<SubjectEditCommand, SubjectDetailsResponse>, SubjectDetailsResponse>
    {
        private readonly ClassRegisterContext context;

        public SubjectEditHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<SubjectDetailsResponse> Handle(EditCommand<SubjectEditCommand, SubjectDetailsResponse> request,
            CancellationToken cancellationToken)
        {
            var subject = await context.Subjects.FindOrThrowAsync(request.Id, cancellationToken);
            subject.Name = request.InnerCommand.Name;

            await context.SaveChangesAsync(cancellationToken);
            return new SubjectDetailsResponse
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }
    }
}