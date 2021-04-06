using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESchool.ClassRegister.Api.Infrastructure
{
    public class ClassRegisterContextFactory : ITenantDbContextFactory<ClassRegisterContext>
    {
        private readonly IMediator mediator;
        private readonly ILogger<ClassRegisterContext> logger;
        private readonly DbContextOptions<ClassRegisterContext> options;
        
        public ClassRegisterContextFactory(
            IMediator mediator,
            ILogger<ClassRegisterContext> logger,
            DbContextOptions<ClassRegisterContext> options)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.options = options;
        }
        
        public ClassRegisterContext CreateContext(Tenant tenant)
        {
            return new ClassRegisterContext(
                options,
                mediator,
                logger,
                tenant);
        }
    }
}