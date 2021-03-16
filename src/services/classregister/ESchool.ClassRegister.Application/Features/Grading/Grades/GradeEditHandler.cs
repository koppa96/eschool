using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Grading.Grades.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Enums;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Grading.Grades
{
    public class GradeEditCommand
    {
        public GradeValue Value { get; set; }
        public string Description { get; set; }
        public Guid GradeKindId { get; set; }
    }
    
    public class GradeEditHandler : IRequestHandler<EditCommand<GradeEditCommand, GradeDetailsResponse>, GradeDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;

        public GradeEditHandler(ClassRegisterContext context, IIdentityService identityService, IMapper mapper)
        {
            this.context = context;
            this.identityService = identityService;
            this.mapper = mapper;
        }
        
        public async Task<GradeDetailsResponse> Handle(EditCommand<GradeEditCommand, GradeDetailsResponse> request, CancellationToken cancellationToken)
        {
            var grade = await context.Grades.Include(x => x.Kind)
                .Include(x => x.Student)
                .Include(x => x.Teacher)
                .Include(x => x.ClassSchoolYearSubject)
                    .ThenInclude(x => x.Subject)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            var gradeKind = await context.GradeKinds.FindOrThrowAsync(request.InnerCommand.GradeKindId, cancellationToken);

            grade.Value = request.InnerCommand.Value;
            grade.Description = request.InnerCommand.Description;
            grade.Kind = gradeKind;

            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<GradeDetailsResponse>(grade);
        }
    }
}