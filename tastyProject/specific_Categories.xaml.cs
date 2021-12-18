using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace tastyProject
{
    public partial class specific_Categories : Window
    {
        public DataTable recipes = BL.recipes;//uniqe recipes table for every page for case of multiple windows

        public specific_Categories()
        {
            InitializeComponent();
            Data.openWindows.Add(this);
            BL.recipesForWindow(resultsNum, grid, this, recipes);
        }

        public DataTable getRecipes()
        {
            return this.recipes;
        }

        public void newRecipes(DataTable newRecipesTable)
        {
            grid.RowDefinitions.Clear();
            recipes = newRecipesTable;
            BL.recipesForWindow(resultsNum, grid, this, recipes);
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

