using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using DataAdapter.Inside;
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
            LoadGroupList();           
        }

        private void LoadGroupList()
        {
            var mySql = new MySql();
            var groups = mySql.GetStudentGroups();
            if (groups.Count < 1)
            {
                var result = MessageBox.Show(
                    "Ошибка при получении списка групп! Повторить запрос списка групп?",
                    "Ошибка!",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Error);
                if(result == MessageBoxResult.Yes)
                {
                    LoadGroupList();
                }
                return;
            }
            foreach (var g in groups)
            {
                cmbGroupName.Items.Add(g);
            }
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

        /// <summary>
        /// Метод для автотестов
        /// </summary>
        /// <param name="studentSearch">Параметры студента</param>
        /// <returns></returns>
        public StudentInfoPage FillPage(StudentSearchObject studentSearch)
        {
            tbFirstName.Text = studentSearch.FirstName;
            tbLastName.Text = studentSearch.LastName;
            tbPastName.Text = studentSearch.PastName;
            if (studentSearch.Male)
                rbMan.IsChecked = true;
            else
                rbWoman.IsChecked = true;
            cmbGroupName.SelectedIndex = cmbGroupName.Items.IndexOf(studentSearch.Group);
            SearchClickEvent(this, null);
            return this;
        }

        /// <summary>
        /// Метод для автотеста
        /// </summary>
        public void PickFirstStudent()
        {
            grdSearchResults.CurrentItem = grdSearchResults.Items[0];
            DataGridPickStudentEvent(null, null);
        }
    }
}
