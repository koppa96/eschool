﻿using System.Collections.Generic;
using ESchool.HomeAssignments.Domain.Entities.ClassRegisterData;

namespace ESchool.HomeAssignments.Domain.Entities.Users
{
    public class Teacher : HomeAssignmentsUserRole
    {
        public virtual ICollection<ClassSchoolYearSubjectTeacher> ClassSchoolYearSubjectTeachers { get; set; }
        public virtual ICollection<HomeworkReview> Reviews { get; set; }
    }
}