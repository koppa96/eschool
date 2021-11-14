using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Interface.Features;
using ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects
{
    public class StudentSchoolYearListHandler : IRequestHandler<StudentSchoolYearListQuery, List<ClassRegisterItemResponse>>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IConfigurationProvider configurationProvider;

        public StudentSchoolYearListHandler(
            HomeAssignmentsContext context,
            IIdentityService identityService, 
            IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.identityService = identityService;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<ClassRegisterItemResponse>> Handle(StudentSchoolYearListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            
            return context.ClassSchoolYearSubjects.Where(x =>
                    x.ClassSchoolYearSubjectStudents.Any(t => t.Student.UserId == currentUserId))
                .Select(x => x.SchoolYear)
                .Distinct()
                .OrderBy(x => x.Name)
                .ProjectTo<ClassRegisterItemResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}