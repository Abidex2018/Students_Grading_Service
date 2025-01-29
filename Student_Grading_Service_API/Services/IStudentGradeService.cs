using Student_Grading_Service_API.Model;

namespace Student_Grading_Service_API.Services
{
    public interface IStudentGradeService
    {
        Task<List<StudentGrade>> GetStudentGrades();
        Task<StudentGrade> GetStudentGradesById(string studentGradeId);
        Task<int> CreateStudentGrade(StudentGrade studentGrade);
        Task<int> CreateSubject(Subject subject);
        Task<List<Subject>> GetSubjects();
        Task<bool> DeleteStudentGrade(string studentGradeId);
        Task<int> UpdateStudentGrade(StudentGrade studentGrade);
    }
}
