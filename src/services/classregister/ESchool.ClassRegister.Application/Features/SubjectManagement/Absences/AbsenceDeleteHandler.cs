using System;
using System.Linq;
using System.Threading.Tasks;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities.SubjectManagement;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.SubjectManagement.Absences
{
    public class AbsenceDeleteCommand : DeleteCommand
    {
    }
    
    public class AbsenceDeleteHandler : DeleteHandler<DeleteCommand, Absence>
    {
        private readonly IIdentityService identityService;

        public AbsenceDeleteHandler(ClassRegisterContext context, IIdentityService identityService) : base(context)
        {
            this.identityService = identityService;
        }
    }
}