using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Controllers
{
    [ApiExplorerSettings(GroupName = nameof(TenantRoleType.Administrator))]
    [Authorize(nameof(TenantRoleType.Administrator))]
    [ApiController]
    public class AdministratorControllerBase : ControllerBase
    {
        
    }
}