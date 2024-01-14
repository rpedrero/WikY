namespace WikY.Business.Exceptions
{
    public class DataValidationException : Exception
    {
        public string FieldName = string.Empty;

        public DataValidationException() : base() { }

        public DataValidationException(string message) : base(message) { }

        public DataValidationException(string message, Exception innerException) : base(message, innerException) { }

        public DataValidationException(string message, string fieldName) : this(message)
        {
            this.FieldName = fieldName;
        }
    }
}
