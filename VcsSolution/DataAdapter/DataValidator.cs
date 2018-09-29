using DataAdapter.Exceptions;

namespace DataAdapter
{
    public static class DataValidator
    {
        public static void ValidateFieldTextRequired(string data, string fieldName)
        {
            for (int i = 0; i < 10; i++)
            {
                if (data.Contains(i.ToString()))
                    throw new ValidationErrorException(fieldName);
            }
            if (data.Length == 0
                || data.Contains(" ") 
                || data.Contains("$")
                || data.Contains("#")
                || data.Contains("%")
                || data.Contains("^")
                || data.Contains("&")
                || data.Contains("*")
                || data.Contains("(")
                || data.Contains(")")
                || data.Contains(";")
                || data.Contains(":")
                || data.Contains("№")
                || data.Contains("\"")
                || data.Contains("!")
                || data.Contains("-")
                || data.Contains("_"))
            {
                throw new ValidationErrorException(fieldName);
            }
        }

        public static void ValidateFieldText(string data, string fieldName)
        {
            for (int i = 0; i < 10; i++)
            {
                if (data.Contains(i.ToString()))
                    throw new ValidationErrorException(fieldName);
            }
            if (data.Contains("$")
                || data.Contains("#")
                || data.Contains("%")
                || data.Contains("^")
                || data.Contains("&")
                || data.Contains("*")
                || data.Contains("(")
                || data.Contains(")")
                || data.Contains(";")
                || data.Contains(":")
                || data.Contains("№")
                || data.Contains("\"")
                || data.Contains("!")
                || data.Contains("-")
                || data.Contains("_"))
            {
                throw new ValidationErrorException(fieldName);
            }
        }

        public static void ValidateFieldTextAdvanced(string data, string fieldName)
        {
            if (data.Contains("$")
                || data.Contains("#")
                || data.Contains("%")
                || data.Contains("^")
                || data.Contains("&")
                || data.Contains("*")
                || data.Contains("(")
                || data.Contains(")")
                || data.Contains(";")
                || data.Contains(":")
                || data.Contains("№")
                || data.Contains("\"")
                || data.Contains("!"))
            {
                throw new ValidationErrorException(fieldName);
            }
        }
    }
}
