using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.Testing.Interface.Features.TestAnswers
{
    public class TestAnswerListQuery : PagedListQuery<TestAnswerListResponse>
    {
        public Guid TestId { get; set; }
    }
}