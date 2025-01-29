using Student_Grading_Service_API.DAO;
using Student_Grading_Service_API.Model;

namespace Student_Grading_Service_API.Services.ServiceImplementation
{
   
    public class StudentGradeService : IStudentGradeService
    {
        private readonly IStudentGradeManagementDAO _studentGradeManagementDAO;
        public StudentGradeService(IStudentGradeManagementDAO studentGradeManagementDAO)
        {
            _studentGradeManagementDAO = studentGradeManagementDAO;
        }

        public async Task<int> CreateStudentGrade(StudentGrade studentGrade)
        {
            var createStudentGrade = await _studentGradeManagementDAO.CreateStudentGrade(studentGrade);
            return createStudentGrade;
        }

        public async Task<int> CreateSubject(Subject subject)
        {
            var createSubject = await _studentGradeManagementDAO.CreateSubject(subject);    
            return createSubject;
        }

        public Task<bool> DeleteStudentGrade(string studentGradeId)
        {
            var deleteGame = _studentGradeManagementDAO.DeleteStudentGrade(studentGradeId);
            return deleteGame;
        }

        public async Task<List<StudentGrade>> GetStudentGrades()
        {
            var studentGrades = await _studentGradeManagementDAO.GetStudentGrades();
            return studentGrades;
        }

        public async Task<StudentGrade> GetStudentGradesById(string studentGradeId)
        {
            var studentGrade = await _studentGradeManagementDAO.GetStudentGradesById(studentGradeId);
            return studentGrade;
        }

        public async Task<List<Subject>> GetSubjects()
        {
            var subjects = await _studentGradeManagementDAO.GetSubjects();
            return subjects;
        }

        public async Task<int> UpdateStudentGrade(StudentGrade studentGrade)
        {
            var updateStudentGrade = await _studentGradeManagementDAO.UpdateStudentGrade(studentGrade);
            return updateStudentGrade;
        }
    }
}
