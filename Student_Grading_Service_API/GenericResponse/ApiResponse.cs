namespace Student_Grading_Service_API.GenericResponse
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }

        public string? Message { get; set; }

        public ApiResponse(bool success, T data, string? Message = null)
        {
            Success = success;
            Data = data;
            this.Message = Message;
        }
    }

    public class ApiResponseList<T>
    {
        public bool Success { get; set; }
        public List<T> Data { get; set; }

        public string? Message { get; set; }

        public ApiResponseList(bool success, List<T> data, string? Message = null)
        {
            Success = success;
            Data = data;
            this.Message = Message;
        }

    }
}
