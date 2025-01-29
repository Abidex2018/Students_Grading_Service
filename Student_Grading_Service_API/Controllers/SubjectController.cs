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
    public class SubjectController : ControllerBase
    {
        private readonly ILogger<IStudentGradeService> _logger;
        private readonly IStudentGradeService _studentGradeService;

        public SubjectController(IStudentGradeService studentGradeService, ILogger<IStudentGradeService> logger)
        {
            _logger = logger;
            _studentGradeService = studentGradeService;
        }

        [HttpPost("CreateSubject")]
        public async Task<ActionResult> CreateSubject([FromBody] SubjectRequest subject)
        {
            try
            {
                var createSubject = subject.ToSubjects();
                var subjectCreated = await _studentGradeService.CreateSubject(createSubject);
                

                if(subjectCreated == 0)
                {
                    var message = "An Error occur, Please try again later";
                    return BadRequest(new ApiResponse<Subject>(false, null, message));
                }
                var subjectResponse = createSubject.ToSubjectsResponse();
                return Ok(new ApiResponse<Subject>(true, subjectResponse, null));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.ToString());
                throw;
            }
        }

        [HttpGet("GetSubjects")]
        public async Task<ActionResult> GetSubjects()
        {
            try
            {
                string message;
                var getSubjects = await _studentGradeService.GetSubjects();


                if (!getSubjects.Any())
                {
                    message = "Subject is Empty";
                    return Ok(new ApiResponseList<Subject>(true, new List<Subject>(), message));
                }

                var subjectList = getSubjects.Select(subject => new Subject
                {
                    SubjectId = subject.SubjectId,
                    SubjectName = subject.SubjectName,
                    IsActive = subject.IsActive,
                }).ToList();
                message = "Successful";

               
                return Ok(new ApiResponseList<Subject>(true, subjectList, message));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.ToString());
                throw;
            }
        }


        [HttpGet("GetBySubjectId/{subjectId}")]
        public async Task<ActionResult> GetBySubjectId(string subjectId)
        {
            try
            {
                string message;

                var getSubjects = await _studentGradeService.GetSubjects();
                var getSubjectById = getSubjects.FirstOrDefault(subject => subject.SubjectId == subjectId);


                if (getSubjectById is null)
                {
                    message = "No Matched Subject";
                    return Ok(new ApiResponse<Subject>(true, new Subject(), message));
                }

                var subjectItem = new Subject
                {
                    SubjectId = getSubjectById.SubjectId,
                    SubjectName = getSubjectById.SubjectName,
                    IsActive = getSubjectById.IsActive,
                };
                message = "Successful";


                return Ok(new ApiResponse<Subject>(true, subjectItem, message));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogInformation(ex.ToString());
                return BadRequest(new ApiResponse<Subject>(true, null, ex.Message));
                throw;
            }
        }



    }
}
