using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore
{
    [ApiController]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [ProducesResponseType(404)]
    public class ESchoolControllerBase : ControllerBase
    {
    }
}