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

namespace VCSwin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        StudentInfoPage studentInfoPage;

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

        }

        private void StudentPicked(Student student)
        {
            // > Отобразить студента
        }
    }
}
