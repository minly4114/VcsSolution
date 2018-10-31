using System.Windows;
using DataAdapter.Inside;
using DataAdapter.Outside;

namespace VCSwin
{
    /// <summary>
    /// Interaction logic for EditVisitPage.xaml
    /// </summary>
    public partial class EditVisitPage : Window
    {
        public delegate void ChangedPresense(StudentVisit studentVisit);
        ChangedPresense changedPresense;
        StudentVisit visit;

        public EditVisitPage(StudentVisit studentVisit, ChangedPresense changedPresense)
        {
            InitializeComponent();
            if (studentVisit.Presense)
            {
                cmbPresense.SelectedIndex = 0;
            }
            else
            {
                cmbPresense.SelectedIndex = 1;
            }
            visit = studentVisit;
            this.changedPresense = changedPresense;
        }

        private void SaveEvent(object sender, RoutedEventArgs e)
        {
            MySql sql = new MySql();
            if (cmbPresense.SelectedIndex == 0)
            {
                visit.SetPresense(true);
            } else
            {
                visit.SetPresense(false);
            }           
            sql.SetStudentVisit(visit);
            changedPresense.Invoke(visit);
            Close();
        }
    }
}
