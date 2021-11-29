using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.ClassRegister.Interface.Features.Students;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Students
{
    public class StudentSchoolYearListHandler : IRequestHandler<StudentSchoolYearListQuery, List<SchoolYearListResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public StudentSchoolYearListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<SchoolYearListResponse>> Handle(StudentSchoolYearListQuery request, CancellationToken cancellationToken)
        {
            return context.Students.Where(x => x.Id == request.StudentId)
                .SelectMany(x => x.Class.ClassSchoolYears.Select(x => x.SchoolYear))
                .ProjectTo<SchoolYearListResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}