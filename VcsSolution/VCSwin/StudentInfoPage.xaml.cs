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
                FillDataGrid(
                    new List<Student>
                    {
                        StudentStub.GetStudent(tbFirstName.Text, tbLastName.Text, tbPastName.Text), // > Убрать заглушку
                    }
                );

            }
            catch (ValidationErrorException exception)
            {
                MessageBox.Show($"Поле '{exception.FieldName}' не заполнено!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }       
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
    }
}
