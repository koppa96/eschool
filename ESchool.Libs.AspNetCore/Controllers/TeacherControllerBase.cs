using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Controllers
{
    [ApiExplorerSettings(GroupName = nameof(TenantRoleType.Teacher))]
    [Authorize(nameof(TenantRoleType.Teacher))]
    [ApiController]
    public class TeacherControllerBase : ControllerBase
    {
        
    }
}