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

namespace tastyProject
{
    /// <summary>
    /// Interaction logic for msgBoxOK.xaml
    /// </summary>
    public partial class msgBoxOK : Window
    {
        public msgBoxOK(string message)
        {
            InitializeComponent();
            textMsg.Text = message;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
