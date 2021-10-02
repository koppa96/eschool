using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Entities;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Libs.Application.Cqrs.Handlers;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Subjects
{
    public class SubjectListHandler : AutoMapperPagedListHandler<SubjectListQuery, Subject, SubjectListResponse>
    {
        public SubjectListHandler(ClassRegisterContext context, IConfigurationProvider configurationProvider) : base(context, configurationProvider)
        {
        }
        
        protected override IOrderedQueryable<Subject> Order(IQueryable<Subject> entities, SubjectListQuery query)
        {
            return entities.OrderBy(x => x.Name);
        }
    }
}