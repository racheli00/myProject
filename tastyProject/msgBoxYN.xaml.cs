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
    /// Interaction logic for messageBox.xaml
    /// </summary>
    public partial class messageBox : Window
    {
        public messageBox()
        {
            InitializeComponent();
        }

        private void yesB_Click(object sender, RoutedEventArgs e)
        {
            Data.whichB = 1;
            this.Close();
        }

        private void noB_click(object sender, RoutedEventArgs e)
        {
            Data.whichB = 0;
            this.Close();
        }
    }
}
