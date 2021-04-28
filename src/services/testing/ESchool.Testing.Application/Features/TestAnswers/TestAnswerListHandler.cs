using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Grpc;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Testing.Domain;
using ESchool.Testing.Domain.Entities;

namespace ESchool.Testing.Application.Features.TestAnswers
{
    public class TestAnswerListQuery : PagedListQuery<TestAnswerListResponse>
    {
        public Guid TestId { get; set; }
    }

    public class TestAnswerListResponse
    {
        public Guid Id { get; set; }
        public UserListResponse Student { get; set; }
        public bool HasBeenCorrected { get; set; }
    }

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

        protected override IOrderedQueryable<TestAnswer> Order(IQueryable<TestAnswer> entities)
        {
            return entities.OrderBy(x => x.StudentTest.Student.User.Name);
        }
    }
}