using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataAdapter.Inside;
using DataAdapter.Inside.Stubs;
using DataAdapter.Exceptions;
using VCSwin.DataObjects;
using DataAdapter;
using DataAdapter.Outside;

namespace VCSwin
{
    /// <summary>
    /// Interaction logic for StudentInfoPage.xaml
    /// </summary>
    public partial class StudentInfoPage : Window
    {
        public delegate void ReturnStudent(Student student);
        ReturnStudent returnStudent;

        public StudentInfoPage(ReturnStudent returnStudent)
        {
            InitializeComponent();
            this.returnStudent = returnStudent;
        }

        private void SearchClickEvent(object sender, RoutedEventArgs e)
        {
            //var student = new StudentSearchObject
            //{
            //    FirstName = tbFirstName.Text,
            //    LastName = tbLastName.Text,
            //    PastName = tbPastName.Text,
            //    Group = cmbGroupName.Text
            //}.ValidateData();

            // > Поиск в базе

            // > Создание объектов совпадений в List

            // > FillDataGrid -> List
            
            try
            {
                DataValidator.ValidateFieldTextRequired(tbFirstName.Text, "Имя");
                DataValidator.ValidateFieldTextRequired(tbLastName.Text, "Фамилия");
                DataValidator.ValidateFieldText(tbPastName.Text, "Отчество");

                var sql = new MySql();
                List<Student> listResult = sql.GetStudent(
                    new Student(
                        1,
                        tbFirstName.Text,
                        tbLastName.Text,
                        "Михайлович",
                        true,
                        "ИВБО-06-16"));

                FillDataGrid(listResult);
            }
            catch (ValidationErrorException exception)
            {
                MessageBox.Show($"Поле '{exception.FieldName}' не заполнено! {exception.ErrorMessage}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //DownloadStudentFromDB(new Student(0, tbFirstName.Text, tbLastName.Text, tbPastName.Text, false, cmbGroupName.Text));
            DownloadStudentFromDB(new Student(1, "Егор", "Петров", "Михайлович", true, "ИВБО-06-16"));
        }

        private void FillDataGrid(List<Student> students)
        {
            grdSearchResults.ItemsSource = students;
        }

        private void DataGridPickStudentEvent(object sender, MouseButtonEventArgs e)
        {
            returnStudent.Invoke((Student)grdSearchResults.CurrentItem);
            Close();
        }
        private void DownloadStudentFromDB(Student student)
        {
            MySql mysql = new MySql();
            List<Student> students = mysql.GetStudent(student);

        }
    }
}
