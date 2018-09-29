using DataAdapter.Exceptions;
using System;

namespace DataAdapter
{
    public static class DataValidator
    {
        /// <summary>
        /// Валидация обязательных текстовых полей (Имя, Фамилия)
        /// </summary>
        /// <param name="data">Текст</param>
        /// <param name="fieldName">Название поля</param>
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
                || data.Contains("_")
                || data.Contains(",")
                || data.Contains("."))
            {
                throw new ValidationErrorException(fieldName);
            }
        }

        /// <summary>
        /// Валидация текстовых полей (Отчество)
        /// </summary>
        /// <param name="data">Текст</param>
        /// <param name="fieldName">Название поля</param>
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
                || data.Contains("_")
                || data.Contains(",")
                || data.Contains("."))
            {
                throw new ValidationErrorException(fieldName);
            }
        }

        /// <summary>
        /// Валидация текстовых полей, расширенная (Группа)
        /// </summary>
        /// <param name="data">Текст</param>
        /// <param name="fieldName">Название поля</param>
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
                || data.Contains("!")
                || data.Contains(",")
                || data.Contains("."))
            {
                throw new ValidationErrorException(fieldName);
            }
        }
    }
}
