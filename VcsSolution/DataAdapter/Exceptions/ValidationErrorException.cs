using System;

namespace DataAdapter.Exceptions
{
    public class ValidationErrorException : Exception
    {
        public string FieldName { get; private set; }

        public ValidationErrorException(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}
