using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.ComponentModel;

namespace tastyProject
{
    public partial class specific_Recipe : Window
    {
        int recipeCode;//unique recipe code if will be multiple windows
        string categoryName;//unique category name if will be multiple windows
        List<string> suggestedRecipes = new List<string>();
        public specific_Recipe()
        {
            InitializeComponent();
            Data.openWindows.Add(this);
            recipeCode = BL.getRecipeCode();
            categoryName = BL.getName("SP_getCategoryNameByRecipe", "@recipeCode", recipeCode);
            BL.specificRecipe(image, category, recipeName, ings, prepertion, amount, preperTm, time);
            BL.addToLastRecipesEntered(recipeCode);
            BL.suggests(rndList, suggestedRecipes, recipeCode);

            this.Title = recipeName.Text;

            //img if recipe is liked or not
            if (BL.isRecipe("SP_isRecipeLiked", recipeCode) == 1)
                like.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"..\..\..\images\design\afterLike.png"));
        }

        public void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = (Image)sender;
            BL.Image_MouseDown(image.Name, this);
        }

        private void rndList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView recipe = (ListView)sender;
            BL.openSuggestedRecipe(suggestedRecipes[recipe.SelectedIndex], this);
        }

        private void Button_Like(object sender, RoutedEventArgs e)
        {
            if (like.Source.ToString().Contains("beforeLike"))
            {
                like.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"..\..\..\images\design\afterLike.png"));
                BL.insertOrDelete("SP_insertIntoTable", recipeCode ,"likedRecipes");
            }

            else
            {
                like.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"..\..\..\images\design\beforeLike.png"));
                BL.insertOrDelete("SP_deleteByRecipeCode", recipeCode, "likedRecipes");
            }
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Data.returnProblem == false)
                BL.Window_Closing(e);
        }

    }
}
