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

        private void InitCmbGroups()
        {
            
        }

        private void SearchClickEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                DataValidator.ValidateFieldTextRequired(tbFirstName.Text, "Имя");
                DataValidator.ValidateFieldTextRequired(tbLastName.Text, "Фамилия");
                DataValidator.ValidateFieldText(tbPastName.Text, "Отчество");
                if (cmbGroupName.Text.Length < 1)
                {
                    throw new ValidationErrorException("Группа", "Выберите группу!");
                }
                var sql = new MySql();

                List<Student> listResult = sql.GetStudent(
                    new StudentSearchObject(
                        tbFirstName.Text,
                        tbLastName.Text,
                        tbPastName.Text.Length > 0 ? tbPastName.Text : null,
                        (bool)rbMan.IsChecked,
                        cmbGroupName.Text));

                FillDataGrid(listResult);
            }
            catch (ValidationErrorException exception)
            {
                MessageBox.Show($"Поле '{exception.FieldName}' не заполнено! {exception.ErrorMessage}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
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
