using System.Linq;
using AutoMapper;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;
using ESchool.Testing.Interface.Features.TestAnswers;

namespace ESchool.Testing.Application.Features.TestAnswers
{
    public class TestAnswerListHandler : AutoMapperPagedListHandler<TestAnswerListQuery, TestAnswer, TestAnswerListResponse>
    {
        public TestAnswerListHandler(
            TestingContext context,
            IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }

        protected override IQueryable<TestAnswer> Filter(IQueryable<TestAnswer> entities, TestAnswerListQuery query)
        {
            return entities.Where(x => x.StudentTest.TestId == query.TestId);
        }

        protected override IOrderedQueryable<TestAnswer> Order(IQueryable<TestAnswer> entities, TestAnswerListQuery query)
        {
            return entities.OrderBy(x => x.StudentTest.Student.User.Name);
        }
    }
}