using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Student_Grading_Service_API.DAO
{
    public class SQLiteContext
    {
        private readonly IConfiguration configuration;

        public SQLiteContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            
        }

        protected IDbConnection Connection()
        {
            return new SqliteConnection(configuration.GetConnectionString("SQLiteConnection"));
        }

        public async Task Init()
        {
            using var connection = Connection();
            await _initStudentGrades();
            await _initSubjects();

            async Task _initSubjects()
            {
                var sql = @"CREATE TABLE IF NOT EXISTS Subject (
                            SubjectId TEXT PRIMARY KEY,
                            SubjectName TEXT,
                            IsActive INTERGER);";

                await connection.ExecuteAsync(sql);
            }

            async Task _initStudentGrades()
            {



                //var sql = @"DROP TABLE StudentGrade;";
                var sql = @"CREATE TABLE IF NOT EXISTS StudentGrade (
                            StudentGradeId TEXT PRIMARY KEY,
                            FirstName TEXT,                            
                            LastName TEXT,
                            SubjectId TEXT,
                            Grade TEXT,
                            Remarks TEXT,
                            AssignDate TEXT,
                            FOREIGN KEY (SubjectId) REFERENCES Subject(SubjectId));";

                await connection.ExecuteAsync(sql);
            }
        }
    }
}
