using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Controllers
{
    [ApiExplorerSettings(GroupName = nameof(GlobalRoleType.TenantAdministrator))]
    [Authorize(nameof(GlobalRoleType.TenantAdministrator))]
    [ApiController]
    public class TenantAdministratorControllerBase : ControllerBase
    {
        
    }
}