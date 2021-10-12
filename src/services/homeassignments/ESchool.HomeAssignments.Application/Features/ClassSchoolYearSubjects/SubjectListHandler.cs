using System;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Interface.Features;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Services;
using ESchool.Libs.Interface.Query;

namespace ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects
{
    public class SubjectListQuery : PagedListQuery<ClassRegisterItemResponse>
    {
        public Guid SchoolYearId { get; set; }
    }
    
    public class SubjectListHandler : PagedListHandler<SubjectListQuery, ClassSchoolYearSubject, ClassRegisterItemResponse>
    {
        private readonly IConfigurationProvider configurationProvider;
        private readonly IIdentityService identityService;

        public SubjectListHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider,
            IIdentityService identityService) : base(context)
        {
            this.configurationProvider = configurationProvider;
            this.identityService = identityService;
        }

        protected override IQueryable<ClassSchoolYearSubject> Filter(IQueryable<ClassSchoolYearSubject> entities, SubjectListQuery query)
        {
            var currentUserId = identityService.GetCurrentUserId();
            return entities.Where(x => x.ClassSchoolYearSubjectStudents.Any(s => s.Student.UserId == currentUserId));
        }

        protected override IQueryable<ClassRegisterItemResponse> Map(IQueryable<ClassSchoolYearSubject> entities, SubjectListQuery query)
        {
            return entities.Select(x => x.Subject)
                .ProjectTo<ClassRegisterItemResponse>(configurationProvider);
        }

        protected override IOrderedQueryable<ClassSchoolYearSubject> Order(IQueryable<ClassSchoolYearSubject> entities, SubjectListQuery query)
        {
            return entities.OrderBy(x => x.Subject.Name);
        }
    }
}