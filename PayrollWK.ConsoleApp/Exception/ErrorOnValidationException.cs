namespace PayrollWK.ConsoleApp.Exception
{
    public class ErrorOnValidationException : SystemException
    {
        public List<string> Errors { get; set; } = [];
        public string Error { get; set; } = string.Empty;
        public ErrorOnValidationException(List<string> errorMessages)
        {
            Errors = errorMessages;
        }

        public ErrorOnValidationException(string errorMessages)
        {
            Error = errorMessages;
        }
    }
}
