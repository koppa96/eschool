using System;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.Testing.Interface.Features.TestAnswers
{
    public class TestAnswerListResponse
    {
        public Guid Id { get; set; }
        public UserRoleListResponse Student { get; set; }
        public bool HasBeenCorrected { get; set; }
    }
}