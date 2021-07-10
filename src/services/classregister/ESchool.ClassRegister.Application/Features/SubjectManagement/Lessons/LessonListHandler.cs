using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.ClassRegister.Application.Features.Classrooms;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Lessons;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Lessons
{
    public class LessonListHandler : IRequestHandler<LessonListQuery, List<LessonListResponse>>
    {
        private readonly ClassRegisterContext context;
        private readonly IConfigurationProvider configurationProvider;

        public LessonListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<LessonListResponse>> Handle(LessonListQuery request, CancellationToken cancellationToken)
        {
            return context.Lessons.Where(x =>
                    x.ClassSchoolYearSubject.ClassSchoolYear.SchoolYearId == request.SchoolYearId &&
                    x.ClassSchoolYearSubject.ClassSchoolYear.ClassId == request.ClassId &&
                    x.StartsAt > request.From &&
                    x.EndsAt < request.To &&
                    request.ShowCanceled || !x.Canceled)
                .ProjectTo<LessonListResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}