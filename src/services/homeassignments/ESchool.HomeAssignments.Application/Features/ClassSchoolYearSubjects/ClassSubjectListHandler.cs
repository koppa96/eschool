using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Interface.Features;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects
{
    public class ClassSubjectListQuery : PagedListQuery<ClassSubjectListResponse>
    {
        public Guid SchoolYearId { get; set; }
    }

    public class ClassSubjectListResponse
    {
        public ClassRegisterItemResponse Class { get; set; }
        public ClassRegisterItemResponse Subject { get; set; }
    }

    public class ClassSubjectListHandler : AutoMapperPagedListHandler<ClassSubjectListQuery, ClassSchoolYearSubject, ClassSubjectListResponse>
    {
        public ClassSubjectListHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IOrderedQueryable<ClassSchoolYearSubject> Order(IQueryable<ClassSchoolYearSubject> entities, ClassSubjectListQuery query)
        {
            return entities.OrderBy(x => x.Class.Name)
                .ThenBy(x => x.Subject.Name);
        }
    }
}