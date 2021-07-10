using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.ClassSchoolYears;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassSchoolYears
{
    public class ClassSchoolYearCreateHandler : IRequestHandler<ClassSchoolYearCreateCommand>
    {
        private readonly ClassRegisterContext context;

        public ClassSchoolYearCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(ClassSchoolYearCreateCommand request, CancellationToken cancellationToken)
        {
            context.ClassSchoolYears.Add(new ClassSchoolYear
            {
                ClassId = request.ClassId,
                SchoolYearId = request.SchoolYearId
            });
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}