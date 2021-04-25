using ESchool.Testing.Application.Features.TaskAnswers.Common;
using ESchool.Testing.Application.Features.TaskAnswers.Common.CreateEdit;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Testing.Api.Controllers
{
    [Route("task-type-test")]
    [ApiController]
    public class TaskTypeTestController : ControllerBase
    {
        [HttpGet]
        public TaskAnswerCreateEditCommand SerializationTest()
        {
            return new FreeTextTaskAnswerCreateEditCommand
            {
                Answer = "Anyád"
            };
        }

        [HttpPost]
        public void DeserializationTest([FromBody] TaskAnswerCreateEditCommand command)
        {
            
        }
    }
}