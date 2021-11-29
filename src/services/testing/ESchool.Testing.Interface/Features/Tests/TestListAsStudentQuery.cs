using System;
using ESchool.Libs.Interface.Query;

namespace ESchool.Testing.Interface.Features.Tests
{
    public class TestListAsStudentQuery : PagedListQuery<TestListResponse>
    {
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
}