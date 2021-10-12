using System;
using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Lessons
{
    public class LessonListQuery : PagedListQuery<HomeAssignmentsLessonListResponse>
    {
        public Guid SchoolYearId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
    }

    public class HomeAssignmentsLessonListResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
    
    public class LessonListHandler : AutoMapperPagedListHandler<LessonListQuery, Lesson, HomeAssignmentsLessonListResponse>
    {
        public LessonListHandler(HomeAssignmentsContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IOrderedQueryable<Lesson> Order(IQueryable<Lesson> entities, LessonListQuery query)
        {
            return entities.OrderBy(x => x.Title);
        }
    }
}