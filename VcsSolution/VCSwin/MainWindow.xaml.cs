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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAdapter.Inside;
using DataAdapter.Inside.Stubs;
using DataAdapter.Exceptions;
using DataAdapter.Outside;
using DataAdapter;

namespace VCSwin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        StudentInfoPage studentInfoPage;
        EditVisitPage editVisitPage;
        Student Student;
        List<StudentVisit> studentVisits;
        int visitId;

        public MainWindow()
        {
            InitializeComponent();
            MySql mySql1 = new MySql();
            var clasrooms = mySql1.GetClassrooms();
            var subjects = mySql1.GetSubjects();
            foreach(var c in clasrooms)
            {
                cmbClassroom.Items.Add(c);
            }
            foreach(var s in subjects)
            {
                cmbSubject.Items.Add(s);
            }
            
        }

        private void PickStudentClickEvent(object sender, RoutedEventArgs e)
        {
            studentInfoPage = new StudentInfoPage(StudentPicked);
            studentInfoPage.Show();
        }

        private void LoadInfoClickEvent(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Student == null)
                {
                    throw new ValidationErrorException("Студент", "Выберите студента!");
                }
                if(cmbClassroom.Text.Length < 1)
                {
                    throw new ValidationErrorException("Аудитория", "Выберите аудиторию!");
                }
                if (cmbSubject.Text.Length < 1)
                {
                    throw new ValidationErrorException("Предмет", "Выберите предмет!");
                }
                DataValidator.ValidateDateTime(dpDate.SelectedDate, "Дата");

                var sql = new MySql();
                studentVisits = sql.GetStudentVisits(new StudentVisitSearchObject(
                    Student,
                    (DateTime)dpDate.SelectedDate,
                    cmbClassroom.Text,
                    cmbSubject.Text));
                grdPresenseInfoTable.ItemsSource = studentVisits;
            } catch (NullReferenceException)
            {
                MessageBox.Show($"Не заполнено одно из обязательных полей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (ValidationErrorException ex)
            {
                MessageBox.Show($"Ошибка валидации данных! Поле '{ex.FieldName}' {ex.ErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            MySql mysql = new MySql();
            mysql.GetStudentVisits(new StudentVisitSearchObject(new Student(1, "Егор", "Петров", "Михайлович", true, "ИВБО-06-16"), new DateTime(2018, 10, 05, 18, 00, 00), "А5", "Архитектура вычислительных машин и систем"));
        }

        private void StudentPicked(Student student)
        {
            // > Отобразить студента
            tbStudent.Text = $"{student.FirstName} {student.LastName} {student.PastName} | {student.Group}";
            Student = student;
        }

        private void ChangeVisitState(object sender, MouseButtonEventArgs e)
        {
            visitId = grdPresenseInfoTable.SelectedIndex;
            editVisitPage = new EditVisitPage(studentVisits[visitId], ChangedVisitInfo);
            editVisitPage.Show();
        }

        public void ChangedVisitInfo(StudentVisit studentVisit)
        {
            studentVisits[visitId] = studentVisit;
            grdPresenseInfoTable.ItemsSource = null;
            grdPresenseInfoTable.ItemsSource = studentVisits;
        }
    }
}
