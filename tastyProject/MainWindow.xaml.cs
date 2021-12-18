using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace tastyProject
{
    public partial class MainWindow : Window
    {
        List<string> recipesList = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            Data.openWindows.Add(this);
            BL.getRecipesNames(recipesList);
            BL.recipes= BL.parameterForTable("SP_getTable", "Recipes", "@tableName");
            textBox1.TextChanged += new TextChangedEventHandler(textBox1_TextChanged);

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            BL.textbox_changed(textBox1, lbSuggestion, recipesList);
        }

        public void LB_selecionChanged(object sender, SelectionChangedEventArgs e)
        {
            BL.LB_selecionChanged(this, lbSuggestion);
        }

        private void Button_OpenCategory(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            BL.recipes = BL.parameterForTable("SP_getRecipesByCategory", BL.getCode("getCategoryCodeByName", b.Content.ToString()), "@categoryCode");
            BL.openCategory(this, b.Content.ToString());
        }
        
        private void ButtonClick_toAllSearches(object sender, RoutedEventArgs e)
        {
            if (lbSuggestion.Items.Count == 0)
                return;
            Data.specificRecipeName = textBox1.Text;
            BL.recipes = BL.parameterForTable("SP_getRecipesBySearch", Data.specificRecipeName, "@recipeName");
            BL.openCategory(this, "כל התוצאות: \'"+Data.specificRecipeName+"\'");
            textBox1.Text = "";
        }

        private void textBox1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            textBox1.Text = "";
            textBox1.MouseLeftButtonDown -= textBox1_MouseLeftButtonDown;
            textBox1.Foreground = new SolidColorBrush(Colors.Black);
        }

        public void Image_MouseDown(object sender, MouseButtonEventArgs e)
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
