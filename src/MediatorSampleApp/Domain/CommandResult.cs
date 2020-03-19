namespace Domain
{
    public struct CommandResult
    {
        private CommandResult(bool isSuccess, object? value, bool isBadRequest, bool isError, string errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            IsBadRequest = isBadRequest;
            IsError = isError;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool IsBadRequest { get; }

        public bool IsError { get; }

        public bool IsSuccess { get; }

        public object? Value { get; }

        public static CommandResult BadRequest() => BadRequest(string.Empty);

        public static CommandResult BadRequest(string errorMessage) => new CommandResult(false, null, true, false, errorMessage);

        public static CommandResult Error(string errorMessage) => new CommandResult(false, null, false, true, errorMessage);

        public static CommandResult Ok(object? value) => new CommandResult(true, value, false, false, string.Empty);

        public static CommandResult Ok() => Ok(null);
    }
}
