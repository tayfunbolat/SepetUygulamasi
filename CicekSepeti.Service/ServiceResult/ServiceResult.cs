
    public class ServiceResult
    {
        public object Data { get; set; }

        public int StatusCode { get; set; }
    public string Message { get; internal set; }
}

    public class ErrorServiceResult:ServiceResult
    {
        public string ErrorMessage { get; set; }

    }

