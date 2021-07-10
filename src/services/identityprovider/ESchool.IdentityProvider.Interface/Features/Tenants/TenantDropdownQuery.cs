using System.Collections.Generic;
using ESchool.Libs.Interface.Response;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Tenants
{
    public class TenantDropdownQuery : IRequest<List<DropdownResponse>>
    {
    }
}