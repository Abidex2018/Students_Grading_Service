using Student_Grading_Service_API.Model;
using Student_Grading_Service_API.Model.RequestModel;

namespace Student_Grading_Service_API.Map
{
    public static class ApiContractToModelMapping
    {

        public static StudentGrade ToStudentGrades(this StudentGradeRequest studentGrade)
        {
            string uniqueStudentGradeID = Guid.NewGuid().ToString("N").Substring(0, 5);

            return new StudentGrade
            {
                StudentGradeId = uniqueStudentGradeID,
                FirstName = studentGrade.FirstName,
                LastName = studentGrade.LastName,
                SubjectId = studentGrade.SubjectId,
                Remarks = studentGrade.Remarks,
                Grade = studentGrade.Grade,
                AssignDate = DateTime.UtcNow.ToString(),
            };
        }

        public static StudentGrade ToStudentGrades(this UpdateStudentGradeRequest studentGrade)
        {
            string uniqueStudentGradeID = Guid.NewGuid().ToString("N").Substring(0, 5);

            return new StudentGrade
            {
                StudentGradeId = studentGrade.StudentGradeId,
                FirstName = studentGrade.FirstName,
                LastName = studentGrade.LastName,
                SubjectId = studentGrade.SubjectId,
                Remarks = studentGrade.Remarks,
                Grade = studentGrade.Grade,
                AssignDate = DateTime.UtcNow.ToString(),
            };
        }

        public static Subject ToSubjects(this SubjectRequest subject)
        {
            string uniqueSubjectId = "SUB" + Guid.NewGuid().ToString("N").Substring(0, 6);

            return new Subject
            {
                SubjectId = uniqueSubjectId,
                SubjectName = subject.SubjectName,
                IsActive = subject.IsActive,
            };
        }
    }
}
