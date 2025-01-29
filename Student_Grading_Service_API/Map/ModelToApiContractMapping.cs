using Student_Grading_Service_API.Model;

namespace Student_Grading_Service_API.Map
{
    public static class ModelToApiContractMapping
    {

        public static StudentGrade ToStudentGradesResponse(this StudentGrade studentGrade)
        {
           

            return new StudentGrade
            {
                StudentGradeId = studentGrade.StudentGradeId,
                FirstName = studentGrade.FirstName,
                LastName = studentGrade.LastName,
                SubjectId = studentGrade.SubjectId,
                Grade = studentGrade.Grade,
                Remarks = studentGrade.Remarks,
                AssignDate = DateTime.UtcNow.ToString(),
            };
        }

        public static Subject ToSubjectsResponse(this Subject subject)
        {
            

            return new Subject
            {
                SubjectId = subject.SubjectId,
                SubjectName = subject.SubjectName,
                IsActive = subject.IsActive,
            };
        }
    }
}
