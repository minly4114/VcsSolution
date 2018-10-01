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
        }

        private void PickStudentClickEvent(object sender, RoutedEventArgs e)
        {
            studentInfoPage = new StudentInfoPage(StudentPicked);
            studentInfoPage.Show();
        }

        private void LoadInfoClickEvent(object sender, RoutedEventArgs e)
        {
            studentVisits = new List<StudentVisit>();
            try
            {             
                studentVisits.Add(PresenceLoadStub.GetStudentVisit(
                    Student.FirstName,
                    Student.LastName,
                    Student.PastName,
                    Student.Group,
                    "DefaultClassroom",
                    "DefaultSubject",
                    dpDate.DisplayDate,
                    true));
                studentVisits.Add(PresenceLoadStub.GetStudentVisit(
                    Student.FirstName,
                    Student.LastName,
                    Student.PastName,
                    Student.Group,
                    "DefaultClassroom",
                    "DefaultSubject",
                    dpDate.DisplayDate.AddHours(2),
                    false));
                grdPresenseInfoTable.ItemsSource = studentVisits;
            } catch (NullReferenceException)
            {
                MessageBox.Show($"Не заполнено одно из обязательных полей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (ValidationErrorException ex)
            {
                MessageBox.Show($"Ошибка валидации данных! Поле '{ex.FieldName}' {ex.ErrorMessage}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
