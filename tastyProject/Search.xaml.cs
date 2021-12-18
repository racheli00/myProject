using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.ComponentModel;

namespace tastyProject
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
            Data.openWindows.Add(this);
            Data.basketGrid = afterDrop;
            basket.Drop += BL.basket_Drop;
            BL.addColumnsToGrid(mainGrd);
        }
        void basket_mouseDown(object sender, MouseButtonEventArgs e)
        {
            BL.getRecipes(this);
        }

        void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            BL.Image_MouseDown(image.Name, this);
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Data.returnProblem == false)
                BL.Window_Closing(e);
        }
    
    }
}