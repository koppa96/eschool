using ESchool.Frontend.Network.ClassRegister.Endpoints;
using ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears;
using ESchool.Frontend.Network.ClassRegister.Endpoints.Subjects;

namespace ESchool.Frontend.Network.ClassRegister
{
    public class ClassRegisterApi
    {
        public const string BasePath = Api.BasePath + "/class-register";
        
        public AbsencesEndpoint Absences { get; }
        public ClassesEndpoint Classes { get; }
        public ClassroomsEndpoint Classrooms { get; }
        public ClassTypesEndpoint ClassTypes { get; }
        public GradeKindsEndpoint GradeKinds { get; }
        public LessonsEndpoint Lessons { get; }
        public MessagesEndpoint Messages { get; }
        public StudentsEndpoint Students { get; }
        public SchoolYearsEndpoint SchoolYears { get; }
        public SubjectsEndpoint Subjects { get; }
        public TeachersEndpoint Teachers { get; }

        public ClassRegisterApi(
            AbsencesEndpoint absencesEndpoint,
            ClassesEndpoint classesEndpoint,
            ClassroomsEndpoint classroomsEndpoint,
            ClassTypesEndpoint classTypesEndpoint,
            GradeKindsEndpoint gradeKindsEndpoint,
            LessonsEndpoint lessonsEndpoint,
            MessagesEndpoint messagesEndpoint,
            StudentsEndpoint studentsEndpoint,
            SchoolYearsEndpoint schoolYearsEndpoint,
            SubjectsEndpoint subjectsEndpoint,
            TeachersEndpoint teachersEndpoint)
        {
            Absences = absencesEndpoint;
            Classes = classesEndpoint;
            Classrooms = classroomsEndpoint;
            ClassTypes = classTypesEndpoint;
            GradeKinds = gradeKindsEndpoint;
            Lessons = lessonsEndpoint;
            Messages = messagesEndpoint;
            Students = studentsEndpoint;
            SchoolYears = schoolYearsEndpoint;
            Subjects = subjectsEndpoint;
            Teachers = teachersEndpoint;
        }
    }
}