using Dapper;
using Microsoft.Extensions.Caching.Memory;
using Student_Grading_Service_API.Model;

namespace Student_Grading_Service_API.DAO.Implementation
{
    public class StudentGradeManagementDAO : SQLiteContext, IStudentGradeManagementDAO
    {

        ILogger<StudentGradeManagementDAO> logger;
        private readonly IMemoryCache _cache;
        public StudentGradeManagementDAO(IConfiguration configuration, ILogger<StudentGradeManagementDAO> logger, IMemoryCache cache) : base(configuration)
        {
            _cache = cache;
            this.logger = logger;
        }

        public async Task<int> CreateStudentGrade(StudentGrade studentGrade)
        {

            try
            {
                using (var conn = Connection())
                {
                    var sql = "INSERT INTO StudentGrade(StudentGradeId, FirstName, LastName, Grade, SubjectId, Remarks, AssignDate) VALUES(@StudentGradeId, @FirstName, @LastName, @Grade, @SubjectId, @Remarks, @AssignDate);";


                    var res = await conn.ExecuteAsync(sql, studentGrade);
                    return res;
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
                throw;
            }
           
        }

        public async Task<int> CreateSubject(Subject subject)
        {

            try
            {
                using (var conn = Connection())
                {
                    var sql = "INSERT INTO Subject(SubjectId, SubjectName, IsActive) VALUES(@SubjectId, @SubjectName, @IsActive);";

                    var res = await conn.ExecuteAsync(sql, subject);
                    return res;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
           
        }

        public async Task<bool> DeleteStudentGrade(string studentGradeId)
        {
            try
            {
                using (var conn = Connection())
                {
                    var sql = "DELETE FROM StudentGrade WHERE StudentGradeId = @studentGradeId;";

                    var count = await conn.ExecuteAsync(sql, new { studentGradeId = studentGradeId });
                    if (count > 0)
                        return true;

                    return false;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<StudentGrade>> GetStudentGrades()
        {
            try
            {

                const string cacheKey = "StudentGradeCache";

                if (_cache.TryGetValue(cacheKey, out List<StudentGrade> cachedStudentGrades))
                {
                    return cachedStudentGrades;
                }

                using (var conn = Connection())
                {
                  
                    var sql = $"SELECT StudentGradeId, FirstName, LastName, Grade, SubjectId, Remarks, AssignDate FROM StudentGrade";
                    var studentGrades = await conn.QueryAsync<StudentGrade>(sql);

                    

                    if (studentGrades != null)
                    {
                        var ressultList = studentGrades.ToList();
                        _cache.Set(cacheKey, ressultList,TimeSpan.FromSeconds(0.5));
                        return ressultList;

                    }
                    return new List<StudentGrade>();

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Subject>> GetSubjects()
        {
            try
            {
                const string cacheKey = "SubjectCache";

                if (_cache.TryGetValue(cacheKey, out List<Subject> cachedSubjects))
                {
                    return cachedSubjects;
                }
                using (var conn = Connection())
                {
                    //var games = new List<GameDto>();
                    var sql = $"SELECT * FROM Subject";
                    var subjects = await conn.QueryAsync<Subject>(sql);

                    if (subjects != null)
                    {
                        var resultList = subjects.ToList(); 
                        _cache.Set(cacheKey, resultList, TimeSpan.FromSeconds(0.5));
                        return  subjects.ToList();

                    }
                    return new List<Subject>();

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<int> UpdateStudentGrade(StudentGrade studentGrade)
        {
            try
            {
                using (var conn = Connection())
                {
                    var sql = "UPDATE StudentGrade  SET StudentGradeId = @StudentGradeId,FirstName = @FirstName, LastName = @LastName, Grade = @Grade, SubjectId = @SubjectId, Remarks = @Remarks, AssignDate = @AssignDate WHERE StudentGradeId = @StudentGradeId;";

                    var res = await conn.ExecuteAsync(sql, studentGrade);
                    return res;
                }
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<StudentGrade> GetStudentGradesById(string studentGradeId)
        {
            try
            {
                using (var conn = Connection())
                {
                    var sql = $"SELECT StudentGradeId, FirstName, LastName, Grade, SubjectId, Remarks FROM StudentGrade WHERE StudentGradeId = @studentGradeId;";
                    var studentGrade = await conn.QueryFirstOrDefaultAsync<StudentGrade>(sql, new { studentGradeId = studentGradeId });
                    if (studentGrade != null) 
                    {
                        return studentGrade;
                    }
                    return new StudentGrade();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
