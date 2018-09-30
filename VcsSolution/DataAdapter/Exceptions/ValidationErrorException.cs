using System;

namespace DataAdapter.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public string FieldName { get; private set; }
        public string ErrorMessage { get; private set; }

        public ValidationErrorException(string fieldName, string message)
        {
            FieldName = fieldName;
            ErrorMessage = message;
        }
    }
}
