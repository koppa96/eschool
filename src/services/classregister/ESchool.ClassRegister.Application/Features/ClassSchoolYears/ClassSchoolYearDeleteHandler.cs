using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.Libs.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.ClassSchoolYears
{
    public class ClassSchoolYearDeleteCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
    
    public class ClassSchoolYearDeleteHandler : IRequestHandler<ClassSchoolYearDeleteCommand>
    {
        private readonly ClassRegisterContext context;
        private readonly IIdentityService identityService;

        public ClassSchoolYearDeleteHandler(ClassRegisterContext context, IIdentityService identityService)
        {
            this.context = context;
            this.identityService = identityService;
        }
        
        public async Task<Unit> Handle(ClassSchoolYearDeleteCommand request, CancellationToken cancellationToken)
        {
            
            var classSchoolYear = await context.ClassSchoolYears.Include(x => x.ClassSchoolYearSubjects)
                .SingleAsync(
                    x => x.ClassId == request.ClassId && x.SchoolYearId == request.SchoolYearId, cancellationToken);

            if (classSchoolYear != null)
            {
                if (classSchoolYear.ClassSchoolYearSubjects.Count > 0)
                {
                    throw new InvalidOperationException(
                        "Can not remove a class from a school year when subject are already assigned to it.");
                }
                context.ClassSchoolYears.Remove(classSchoolYear);
                await context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}