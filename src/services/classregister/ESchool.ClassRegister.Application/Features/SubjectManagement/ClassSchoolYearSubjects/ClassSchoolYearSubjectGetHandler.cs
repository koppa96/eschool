using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSchoolYearSubjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectGetHandler : IRequestHandler<ClassSchoolYearSubjectQuery, ClassSchoolYearSubjectDetailsResponse>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public ClassSchoolYearSubjectGetHandler(ClassRegisterContext context,
            IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<ClassSchoolYearSubjectDetailsResponse> Handle(ClassSchoolYearSubjectQuery request, CancellationToken cancellationToken)
        {
            return context.ClassSchoolYearSubjects.Where(x =>
                    x.ClassSchoolYear.ClassId == request.ClassId &&
                    x.ClassSchoolYear.SchoolYearId == request.SchoolYearId && x.SubjectId == request.SubjectId)
                .ProjectTo<ClassSchoolYearSubjectDetailsResponse>(configurationProvider)
                .SingleAsync(cancellationToken);
        }
    }
}