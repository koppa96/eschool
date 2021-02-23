using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Application.Features.Classes.Common;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Classes
{
    public class ClassCreateCommand : IRequest<ClassDetailsResponse>
    {
        public Guid ClassTypeId { get; set; }
        public Guid HeadTeacherId { get; set; }
        public Guid StartingSchoolYearId { get; set; }
    }

    public class ClassCreateHandler : IRequestHandler<ClassCreateCommand, ClassDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IMapper mapper;

        public ClassCreateHandler(ClassRegisterContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<ClassDetailsResponse> Handle(ClassCreateCommand request, CancellationToken cancellationToken)
        {
            var classType = await context.ClassTypes.SingleAsync(x => x.Id == request.ClassTypeId, cancellationToken);
            var headTeacher = await context.Teachers.SingleAsync(x => x.Id == request.HeadTeacherId, cancellationToken);
            var startingSchoolYear =
                await context.SchoolYears.SingleAsync(x => x.Id == request.StartingSchoolYearId, cancellationToken);

            var @class = new Class
            {
                ClassType = classType,
                HeadTeacher = headTeacher,
                ClassSchoolYears = new List<ClassSchoolYear>
                {
                    new ClassSchoolYear
                    {
                        SchoolYear = startingSchoolYear
                    }
                }
            };

            context.Classes.Add(@class);
            await context.SaveChangesAsync(cancellationToken);
            return mapper.Map<ClassDetailsResponse>(@class);
        }
    }
}