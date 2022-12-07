namespace AssessmentTask.ModelDTO
{
    public class Response
    {
        public char IsError { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class ResponseToken : Response
    {
        public string Token { get; set; } = string.Empty;
    }
}
