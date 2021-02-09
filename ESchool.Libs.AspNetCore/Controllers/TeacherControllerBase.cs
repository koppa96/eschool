using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Controllers
{
    [Authorize(nameof(TenantRoleType.Teacher))]
    [ApiController]
    public class TeacherControllerBase : ControllerBase
    {
        
    }
}