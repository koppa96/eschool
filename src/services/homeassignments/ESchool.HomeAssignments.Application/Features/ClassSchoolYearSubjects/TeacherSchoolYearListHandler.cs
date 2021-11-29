using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Grpc;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Interface.Features;
using ESchool.HomeAssignments.Interface.Features.ClassSchoolYearSubjects;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects
{
    public class TeacherSchoolYearListHandler : IRequestHandler<TeacherSchoolYearListQuery, List<ClassRegisterItemResponse>>
    {
        private readonly HomeAssignmentsContext context;
        private readonly IIdentityService identityService;
        private readonly IConfigurationProvider configurationProvider;

        public TeacherSchoolYearListHandler(HomeAssignmentsContext context,
            IIdentityService identityService,
            IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.identityService = identityService;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<ClassRegisterItemResponse>> Handle(TeacherSchoolYearListQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = identityService.GetCurrentUserId();
            
            return context.ClassSchoolYearSubjects.Where(x =>
                    x.ClassSchoolYearSubjectTeachers.Any(t => t.Teacher.UserId == currentUserId))
                .Select(x => x.SchoolYear)
                .Distinct()
                .OrderBy(x => x.Name)
                .ProjectTo<ClassRegisterItemResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}