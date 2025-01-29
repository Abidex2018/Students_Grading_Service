namespace Student_Grading_Service_API.Model
{
    public class StudentGrade
    {
        public  string StudentGradeId { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public string SubjectId { get; set; }
        public  string Grade { get; set; }
        public string? Remarks { get; set; }
        public string AssignDate { get; set; }
    }
}
