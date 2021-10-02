using System;
using System.Linq;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.SharedDomain.Enums;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Interface.Query;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class LessonAbsenceListQuery : PagedListQuery<LessonAbsenceListResponse>
    {
        public Guid LessonId { get; set; }
    }

    public class LessonAbsenceListResponse
    {
        public Guid Id { get; set; } 
        public AbsenceState AbsenceState { get; set; }
        public UserRoleListResponse Student { get; set; }
    }
    
    public class LessonAbsenceListHandler : AutoMapperPagedListHandler<LessonAbsenceListQuery, Absence, LessonAbsenceListResponse>
    {
        public LessonAbsenceListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) :
            base(context, configurationProvider)
        {
        }

        protected override IQueryable<Absence> Filter(IQueryable<Absence> entities, LessonAbsenceListQuery query)
        {
            return entities.Where(x => x.LessonId == query.LessonId);
        }

        protected override IOrderedQueryable<Absence> Order(IQueryable<Absence> entities, LessonAbsenceListQuery query)
        {
            return entities.OrderBy(x => x.Student.User.Name);
        }
    }
}