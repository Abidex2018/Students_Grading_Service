using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student_Grading_Service_API.GenericResponse;
using Student_Grading_Service_API.Map;
using Student_Grading_Service_API.Model;
using Student_Grading_Service_API.Model.RequestModel;
using Student_Grading_Service_API.Services;

namespace Student_Grading_Service_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentGradeController : ControllerBase
    {
        private readonly ILogger<IStudentGradeService> _logger;
        private readonly IStudentGradeService _studentGradeService;

        public StudentGradeController(IStudentGradeService studentGradeService, ILogger<IStudentGradeService> logger)
        {
            _logger = logger;
            _studentGradeService = studentGradeService;
        }

        [HttpPost("CreateStudentGrade")]
        public async Task<ActionResult> CreateStudentGrade([FromBody] StudentGradeRequest studentGrade)
        {
            var createStudentGrade = studentGrade.ToStudentGrades();
            string message;

            var getSubject = await _studentGradeService.GetSubjects();
            var SubjectId = getSubject.FirstOrDefault(x=>x.SubjectId ==  createStudentGrade.SubjectId);

            try
            {
                if(SubjectId == null)
                {
                    message = "Invalid Subject";
                    return BadRequest(new ApiResponse<StudentGrade>(false, createStudentGrade, message));
                }
                var studentGradeCreated = await _studentGradeService.CreateStudentGrade(createStudentGrade);
                if (studentGradeCreated == 0)
                {
                    message = "An Error occur, Please try again later";
                    return Ok(new ApiResponse<StudentGrade>(false, createStudentGrade, message));
                }
                
                var studentGradeResponse = createStudentGrade.ToStudentGradesResponse();
                message = "Successfully Added ";
                return Ok(new ApiResponse<StudentGrade>(true, studentGradeResponse, null));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.ToString());
                return BadRequest(new ApiResponse<StudentGrade>(false, createStudentGrade, ex.Message));
            }
        }

        [HttpPut("UpdateStudentGrade")]
        public async Task<ActionResult> UpdateStudentGrade([FromBody] UpdateStudentGradeRequest studentGrade)
        {
            string message;
            var createStudentGrade = studentGrade.ToStudentGrades();
            try
            {
                var studentGradeUpdate = await _studentGradeService.UpdateStudentGrade(createStudentGrade);

                var getSubject = await _studentGradeService.GetSubjects();
                var SubjectId = getSubject.FirstOrDefault(x => x.SubjectId ==  createStudentGrade.SubjectId);

                if (SubjectId == null)
                {
                    message = "Invalid Subject";
                    return BadRequest(new ApiResponse<StudentGrade>(false, createStudentGrade, message));
                }
                if (studentGradeUpdate == 0)
                {
                    message = "An Error occur, Please try again later";
                    return Ok(new ApiResponse<StudentGrade>(false, createStudentGrade, message));
                }
                var studentGradeResponse = createStudentGrade.ToStudentGradesResponse();
                message = "Update successful";
                return Ok(new ApiResponse<StudentGrade>(true, studentGradeResponse, message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.ToString());
                return BadRequest(new ApiResponse<StudentGrade>(false, createStudentGrade, ex.Message));
            }
        }

        [HttpDelete("DeleteStudentGrade/{studentGradeId}")]
        public async Task<ActionResult> DeleteStudentGrade(string studentGradeId)
        {
            string message;
            var response = new StudentGrade
            {
                StudentGradeId = studentGradeId
            };

            try
            {
                var deleteStudentGrade = await _studentGradeService.DeleteStudentGrade(studentGradeId);
                

                if (!deleteStudentGrade)
                {
                    message =  "Delete Unsuccessfully Or No studentGradeId Found";
                    return Ok(new ApiResponse<StudentGrade>(false, response, message));
                }
                message =  "Deleted Successfully";

                return Ok(new ApiResponse<StudentGrade>(true, response, message));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                _logger.LogError(ex.ToString());
                return BadRequest(new ApiResponse<StudentGrade>(false, response, ex.Message));
            }
            
        }

        [HttpGet("GetStudentGrades")]
        public async Task<ActionResult> GetStudentGrades()
        {
            
            string message;
            try
            {
                var studentGradeList = await _studentGradeService.GetStudentGrades();

                if (!studentGradeList.Any())
                {
                    message =  "No student Grade Found ";
                    return Ok(new ApiResponseList<StudentGrade>(false, new List<StudentGrade>(), message));
                }

                var studentGradeResponses = studentGradeList.Select(stuGrade => new StudentGrade
                {

                    StudentGradeId = stuGrade.StudentGradeId,
                    FirstName = stuGrade.FirstName,
                    LastName = stuGrade.LastName,
                    Remarks = stuGrade.Remarks,
                    Grade = stuGrade.Grade,
                    AssignDate = stuGrade.AssignDate,
                    SubjectId = stuGrade.SubjectId

                }).ToList();

                message = "Students Grade List generated Succesfully";

                return Ok(new ApiResponseList<StudentGrade>(true, studentGradeResponses, message));


            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex.Message);
                _logger.LogError(ex.ToString());
                throw;
            }

        }

        [HttpGet("GetStudentGradesById/{studentGradeId}")]
        public async Task<ActionResult> GetStudentGradesById(string studentGradeId)
        {

            string message;
            try
            {
                var studentGrade = await _studentGradeService.GetStudentGradesById(studentGradeId);

                if (studentGrade == null)
                {
                    message =  "No student Grade Found ";
                    return Ok(new ApiResponse<StudentGrade>(false, new StudentGrade(), message));
                }

              

                message = "Students Grade List generated Succesfully";

                return Ok(new ApiResponse<StudentGrade>(true, studentGrade, message));


            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                _logger.LogError(ex.ToString());
                throw;
            }

        }

    }
}
