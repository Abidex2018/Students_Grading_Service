namespace Student_Grading_Service_API.Model.RequestModel
{
    public class UpdateStudentGradeRequest
    {
        public string StudentGradeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SubjectId { get; set; }
        public string Grade { get; set; }
        public string? Remarks { get; set; }
    }
}
