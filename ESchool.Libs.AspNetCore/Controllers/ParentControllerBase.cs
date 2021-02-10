using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Controllers
{
    [ApiExplorerSettings(GroupName = nameof(TenantRoleType.Parent))]
    [Authorize(nameof(TenantRoleType.Parent))]
    [ApiController]
    public class ParentControllerBase : ControllerBase
    {
        
    }
}