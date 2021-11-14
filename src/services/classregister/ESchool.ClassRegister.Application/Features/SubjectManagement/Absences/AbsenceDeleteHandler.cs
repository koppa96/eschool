using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceDeleteHandler : DeleteHandler<AbsenceDeleteCommand, Absence>
    {
        private readonly IIdentityService identityService;

        public AbsenceDeleteHandler(ClassRegisterContext context, IIdentityService identityService) : base(context)
        {
            this.identityService = identityService;
        }
    }
}