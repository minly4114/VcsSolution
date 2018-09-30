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
using DataAdapter.Exceptions;

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
            if(cmbPresense.SelectedIndex == 0)
            {
                visit.SetPresense(true);
            } else
            {
                visit.SetPresense(false);
            }
            changedPresense.Invoke(visit);
            Close();
        }
    }
}
