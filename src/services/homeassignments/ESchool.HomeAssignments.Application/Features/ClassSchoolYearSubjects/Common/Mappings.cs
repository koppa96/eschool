﻿using AutoMapper;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;
using ESchool.HomeAssignments.Interface.Features;

namespace ESchool.HomeAssignments.Application.Features.ClassSchoolYearSubjects.Common
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<ClassRegisterEntity, ClassRegisterItemResponse>();
            CreateMap<ClassSchoolYearSubject, ClassSubjectListResponse>();
        }
    }
}