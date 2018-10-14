using DataAdapter.Exceptions;
using System;

namespace DataAdapter
{
    public static class DataValidator
    {
        /// <summary>
        /// Валидация обязательных текстовых полей (Фамилия)
        /// </summary>
        /// <param name="data">Текст</param>
        /// <param name="fieldName">Название поля</param>
        public static void ValidateFieldTextRequired(string data, string fieldName)
        {
            for (int i = 0; i < 10; i++)
            {
                if (data.Contains(i.ToString()))
                    throw new ValidationErrorException(fieldName, "Поле не может содержать цифры!");
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
                || data.Contains("/")
                || data.Contains("!")
                || data.Contains("_")
                || data.Contains(",")
                || data.Contains("."))
            {
                throw new ValidationErrorException(fieldName, "Должны быть только буквы!");
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
                    throw new ValidationErrorException(fieldName, "Поле не может содержать цифры!");
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
                || data.Contains("/")
                || data.Contains("!")
                || data.Contains("_")
                || data.Contains(",")
                || data.Contains("."))
            {
                throw new ValidationErrorException(fieldName, "Поле не может содержать символы $#%^&*();:№\\!-_,.");
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
                || data.Contains("/")
                || data.Contains("!")
                || data.Contains(",")
                || data.Contains("."))
            {
                throw new ValidationErrorException(fieldName, "Поле не может содержать символы $#%^&*();:№\\!,.");
            }
        }

        public static void ValidateDateTime(DateTime? date, string fieldName)
        {
            if(date == null)
            {
                throw new ValidationErrorException(fieldName, "Выберите дату занятия!");
            }
            if (date > DateTime.Now)
            {
                throw new ValidationErrorException(fieldName, "Дата больше текущей!");
            }
        }

        public static void ThrowException(string fieldName, string message)
        {
            throw new ValidationErrorException(fieldName, message);
        }
    }
}
