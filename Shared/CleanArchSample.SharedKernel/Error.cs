namespace CleanArchSample.SharedKernel
{
    public record Error
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new("Error.NullValue", "The specific result value is null.", ErrorType.Failure);

        public static implicit operator Result(Error error) => Result.Failure(error);

        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }
        public string Code { get; }
        public string Description { get; }
        public ErrorType ErrorType { get; }

        public static Error Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);
        public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);
        public static Error Validation(string code, string description) =>
            new(code, description, ErrorType.Validation);
        public static Error Conflict(string code, string description) =>
            new(code, description, ErrorType.Conflict);
        
    }
}
