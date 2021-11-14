using System.Linq;
using AutoMapper;
using ESchool.HomeAssignments.Domain;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Interface.Features.Lessons;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.HomeAssignments.Application.Features.Lessons
{
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