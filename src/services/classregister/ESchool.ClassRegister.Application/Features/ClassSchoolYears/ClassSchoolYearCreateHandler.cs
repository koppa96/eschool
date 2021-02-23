using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.Libs.Domain.Extensions;
using MediatR;

namespace ESchool.ClassRegister.Application.Features.ClassSchoolYears
{
    public class ClassSchoolYearCreateCommand : IRequest
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
    }
    
    public class ClassSchoolYearCreateHandler : IRequestHandler<ClassSchoolYearCreateCommand>
    {
        private readonly ClassRegisterContext context;

        public ClassSchoolYearCreateHandler(ClassRegisterContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(ClassSchoolYearCreateCommand request, CancellationToken cancellationToken)
        {
            // Azért kell felhúzni, hogy ne állíthassa egyik tenant adminisztrátora a másik tenantét
            // A query filter miatt csak a saját tenantja osztályai és tanévei jönnek fel
            // Meg lehetne ezt valahogy enélkül is?
            var @class = await context.Classes.FindOrThrowAsync(request.ClassId, cancellationToken);
            var schoolYear = await context.SchoolYears.FindOrThrowAsync(request.SchoolYearId, cancellationToken);

            context.ClassSchoolYears.Add(new ClassSchoolYear
            {
                Class = @class,
                SchoolYear = schoolYear
            });
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}