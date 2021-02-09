using ESchool.Libs.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Libs.AspNetCore.Controllers
{
    [Authorize(nameof(TenantRoleType.Parent))]
    [ApiController]
    public class ParentControllerBase : ControllerBase
    {
        
    }
}