using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Markup;
using System.ComponentModel;
using System.Windows.Documents;


namespace tastyProject
{
    public static class BL
    {
        public static DataTable recipes = new DataTable();
        static List<SqlParameter> param = new List<SqlParameter>();
        static SqlParameter p;
        static string image = "";
        static List<string> basketIngredientsNames = new List<string>();
        static List<ingridientsImages> list = new List<ingridientsImages>();
        static List<Ellipse> ingsList = new List<Ellipse>();
        static messageBox mb;

        //Dal Functions

        //1
        public static int countTable(string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);

            return (int)Dal.Scalar("SP_countTableRows", param);
        }

        //2
        public static void insertOrDelete(string sp, int recipeCode, string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);
            p = new SqlParameter("@recipeCode", SqlDbType.Int);
            p.Direction = ParameterDirection.Input;
            p.Value = recipeCode;
            param.Add(p);
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);
            Dal.ExecuteNonQuery(sp, param);
        }

        //3
        public static void deleteTop()
        {
            param.Clear();
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);
            Dal.ExecuteNonQuery("SP_deleteTopLastRecipesEntered", param);
        }

        //4
        public static void Delete(string userID, string userName, string userPassword)
        {
            List<SqlParameter> listParam = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@userID", userID);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);
             p = new SqlParameter("@userName", userName);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);
            p = new SqlParameter("@userPassword", userPassword);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);
            Dal.ExecuteNonQuery("dbo.SP_Delete", listParam);
        }

        //5
        public static int isRecipe(string sp, int recipeCode)
        {
            param.Clear();
            p = new SqlParameter("@recipeCode", SqlDbType.Int);
            p.Direction = ParameterDirection.Input;
            p.Value = recipeCode;
            param.Add(p);
            p = new SqlParameter("@userID", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = Data.userId;
            param.Add(p);
            return (int)Dal.Scalar(sp, param);
        }

        //6
        public static DataTable getTable(string sp, string tableName)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Direction = ParameterDirection.Input;
            p.Value = tableName;
            param.Add(p);
            if (string.Compare(sp, "SP_getTable") != 0)
            {
                p = new SqlParameter("@userID", SqlDbType.NChar);
                p.Direction = ParameterDirection.Input;
                p.Value = Data.userId;
                param.Add(p);
            }

            return Dal.getTable(sp, param);
        }

        //7
        public static void getRecipesNames(List<string> recipesNames)
        {
            param.Clear();
            p = new SqlParameter("@tableName", SqlDbType.NChar);
            p.Value = "Recipes";
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            DataTable dt = Dal.getTable("SP_getTable", param);
            if (dt == null)
                return;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                recipesNames.Add(dt.Rows[i]["recipeName"].ToString());
            }
        }

        //8
        public static DataTable parameterForTable(string sp, int code, string parameterName)
        {
            param.Clear();
            p = new SqlParameter(parameterName, DbType.Int32);
            p.Direction = ParameterDirection.Input;
            p.Value = code;
            param.Add(p);
            return Dal.getTable(sp, param);
        }

        //9
        public static DataTable parameterForTable(string sp, string name, string parameterName)
        {
            param.Clear();
            p = new SqlParameter(parameterName, DbType.String);
            p.Direction = ParameterDirection.Input;
            p.Value = name;
            param.Add(p);
            return Dal.getTable(sp, param);
        }

        //10
        public static int getCode(string sp, string name)
        {
            param.Clear();
            p = new SqlParameter("@name", name);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            return (int)Dal.Scalar(sp, param);
        }
        
        //11
        public static string getName(string sp, string parameterName, int code)
        {
            param.Clear();
            p = new SqlParameter(parameterName, code);
            p.DbType = DbType.Int32;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            return ((string)Dal.Scalar(sp, param)).Trim();
        } 
        
        //12
        public static string getUserId(string userName)
        {
            param.Clear();
            p = new SqlParameter("@userName", userName);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            param.Add(p);
            return (string)Dal.Scalar("SP_ReturnId", param);
        }

        //13
        public static void listToTable(List<SearchRecipesNode> myList)
        {
            param.Clear();
            recipes = new DataTable();
            recipes.Clear();

            recipes.Columns.Add("recipeCode", typeof(int));
            recipes.Columns.Add("categoryCode", typeof(int));
            recipes.Columns.Add("preperation", typeof(string));
            recipes.Columns.Add("picture", typeof(string));
            recipes.Columns.Add("recipeName", typeof(string));

            DataTable recipe;
            DataRow newRow;

            foreach (SearchRecipesNode item in myList)
            {
                p = new SqlParameter("@recipeCode", DbType.Int32);
                p.Direction = ParameterDirection.Input;
                p.Value = item.RecipeCode;
                param.Add(p);

                newRow = recipes.NewRow();
                recipe = Dal.getTable("SP_getRecipesByCode", param);
                newRow["recipeCode"] = recipe.Rows[0]["recipeCode"];
                newRow["recipeName"] = recipe.Rows[0]["recipeName"];
                newRow["preperation"] = recipe.Rows[0]["preperation"];
                newRow["picture"] = recipe.Rows[0]["picture"];
                newRow["categoryCode"] = recipe.Rows[0]["categoryCode"];

                recipes.Rows.Add(newRow);

                param.Clear();
            }
        }


        //Tasty Function
        
        //1
        public static void openCategory(Window thisWindow, string pageName)
        {
            thisWindow.Hide();
            specific_Categories sc = new specific_Categories();
            sc.Title = pageName;
            sc.pageTitle.Content = pageName;
            sc.ShowDialog();
        }

        //2
        public static void recipesForWindow(Label label, Grid grid, Window thisWindow, DataTable Recipes)
        {
            int recipesNumber = Recipes.Rows.Count;
            label.Content = recipesNumber.ToString()+" תוצאות ";

            TextBlock textBox;
            int rows, columns;
            int countRows = (recipesNumber % 3 == 0) ? (recipesNumber / 3) - 1 : recipesNumber / 3;
            string categoryName;
            GridLength length = new GridLength(250);
            RowDefinition row;

            for (rows = 0; rows <= countRows; rows++)
            {
                row = new RowDefinition();
                row.Height = length;
                grid.RowDefinitions.Add(row);

                for (columns = 0; columns < 3 && recipesNumber > 0; columns++)
                {
                    textBox = new TextBlock();
                    Image img = new Image();
                    DataRow recipe = Recipes.Rows[recipesNumber - 1];

                    categoryName = BL.getName("SP_getCategoryNameByRecipe", "@recipeCode", int.Parse(recipe["recipeCode"].ToString()));//for the path

                    img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"..\..\..\images\recipes\" + categoryName + "\\" + recipe["picture"].ToString()));
                    img.Stretch = Stretch.Fill;
                    img.MouseLeftButtonDown += recipe_Click;
                    img.Margin = new Thickness(20,20,20,35);
                    img.Name = "button" + recipesNumber;
                    img.Cursor = Cursors.Hand;
                    img.SetValue(Grid.ColumnProperty, columns);
                    img.SetValue(Grid.RowProperty, rows);
                    
                    textBox.FontSize = 20;
                    textBox.FontFamily = new FontFamily(Environment.CurrentDirectory + @"..\..\..\" + @"\Fonts\Heebo-Light.ttf");
                    textBox.Text = recipe["recipeName"].ToString();
                    textBox.SetValue(Grid.ColumnProperty, columns);
                    textBox.SetValue(Grid.RowProperty, rows);
                    textBox.Margin = new Thickness(15, 200, 15, 0);
                    textBox.TextAlignment = TextAlignment.Center;
                    textBox.VerticalAlignment = VerticalAlignment.Bottom;

                    grid.Children.Add(img);
                    grid.Children.Add(textBox);

                    recipesNumber--;
                }
            }
        }

        //3
        public static void addColumnsToGrid(Grid mainGrd)
        {
            int i = 0;
            int j;
            ColumnDefinition column;
            List<Label> labels = mainGrd.Children.OfType<Label>().ToList();

            foreach (Grid item in mainGrd.Children.OfType<Grid>().ToList())
            {
                item.Width = Double.NaN;
                item.Margin = new Thickness(170, 32, 0, 0);
                item.AllowDrop = false;

                for (j = 0; j < 18; j++)
                {
                    column = new ColumnDefinition();
                    column.Width = new GridLength(70, GridUnitType.Star);
                    column.MinWidth = 40;
                    item.ColumnDefinitions.Add(column);
                }
                ingredientsForWindow(item, labels[i].Content.ToString(), i);
                i++;
            }
        }

        //4
        public static void ingredientsForWindow(Grid grid, string ingredientCategory, int gridNumber)
        {
            grid.Margin = new Thickness(0, 35, 0, 0);
            DataTable ingredients = parameterForTable("SP_getIngredientsByCategory", ingredientCategory, "@ingredientName");
            int ingredientsNumber = ingredients.Rows.Count;
            Ellipse ellipse;
            ImageBrush imgBrush;
            StackPanel stkPnl;
            TextBlock tb;
            int rows, columns;
            int countRows = (ingredientsNumber % 18 == 0) ? (ingredientsNumber / 18) - 1 : ingredientsNumber / 18;
            GridLength length = new GridLength(100);
            RowDefinition row;
            string ingredientName = "";
            string IdName = "";
            string fileLocation = "";
            char[] charsToTrim = { '.', ',', '\'', '-', '(', ')', ' ' };

            for (rows = 0; rows <= countRows; rows++)
            {
                row = new RowDefinition();
                row.Height = length;
                grid.RowDefinitions.Add(row);

                for (columns = 17; columns >= 0 && ingredientsNumber > 0; columns--)
                {
                    tb = new TextBlock();
                    stkPnl = new StackPanel();
                    ellipse = new Ellipse();
                    imgBrush = new ImageBrush();
                    DataRow ingredient = ingredients.Rows[ingredientsNumber - 1];
                    ingredientName = ingredient["ingredientName"].ToString();
                    fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory + @"..\..\..\images\search\" + ingredientCategory + "\\", ingredientName + ".jpg");
                    imgBrush.ImageSource = new BitmapImage(new Uri(fileLocation));
                    imgBrush.Stretch = Stretch.Fill;

                    ellipse.Fill = imgBrush;
                    ellipse.VerticalAlignment = VerticalAlignment.Top;
                    ellipse.Height = 50;
                    ellipse.MinHeight = 30;
                    ellipse.Margin = new Thickness(5, 0, 5, 0);
                    ellipse.Cursor = Cursors.Hand;
                    IdName = new String(ingredientName.Where(Char.IsLetter).ToArray());
                    ellipse.Name = IdName;
                    ellipse.MouseLeftButtonDown += MouseLeftButtonDown;
                    
                    ellipse.SetValue(Grid.ColumnProperty, columns);
                    ellipse.SetValue(Grid.RowProperty, rows);

                    tb.Margin = new Thickness(5, 50, 5, 0);
                    tb.TextAlignment = TextAlignment.Center;
                    tb.Text = ingredientName;
                    tb.TextWrapping = TextWrapping.Wrap;
                    tb.SetValue(Grid.ColumnProperty, columns);
                    tb.SetValue(Grid.RowProperty, rows);

                    grid.Children.Add(ellipse);
                    grid.Children.Add(tb);

                    ingridientsImages item = new ingridientsImages(IdName, fileLocation);
                    list.Add(item);
                    ingredientsNumber--;
                }
            }
        }

        //5
        public static void recipe_Click(object sender, RoutedEventArgs e)
        {
            Image img = (Image)sender;
            Data.specificRecipeName = img.Name;//save the recipe name
            Data.openWindows.Last().Hide();
            specific_Recipe sr = new specific_Recipe();
            Data.pageName = "";
            sr.ShowDialog();
        }

        //6
        public static void specificRecipe(Image image, TextBlock category, TextBlock recipeName, TextBlock ings, TextBlock preperation, TextBlock amount, TextBlock preperTm, TextBlock time)
        {
            DataRow myRecipe;

            myRecipe = (BL.parameterForTable("SP_getRecipesByCode", getCode("SP_getRecipeCodeByName", Data.specificRecipeName), "@recipeCode")).Rows[0];

            string categoryName = BL.getName("SP_getCategoryNameByCode", "@categoryCode", int.Parse(myRecipe["categoryCode"].ToString()));//for the path

            category.Text = categoryName + " >>> " + myRecipe["recipeName"].ToString();
            recipeName.Text = myRecipe["recipeName"].ToString();
            image.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"..\..\..\images\recipes\" + categoryName + "\\" + myRecipe["picture"].ToString()));
            ings.Text = "";
            preperation.Text = "";
            DataTable ingredients = parameterForTable("SP_getIngredientsByRecipe", (int)myRecipe["recipeCode"], "recipeCode");

            if (string.Compare(Data.openWindows[Data.openWindows.Count-2].Title, "מתכונים של החיפוש")==0)
            {
                Run run;
                foreach (DataRow row in ingredients.Rows)
                {
                    run = new Run(row["amount"].ToString().Trim() + " " + row["ingredientName"].ToString() + "\r");   
                    foreach (string basketIngredient in BL.basketIngredientsNames)
                    {
                        run.Foreground = Brushes.Black;
                        if (string.Compare(row["ingredientName"].ToString(), basketIngredient)==0)
                        {
                            run.Foreground = Brushes.Red;
                            break;
                        }
                    }
                    ings.Inlines.Add(run);
                }
            }

            else
            {
                foreach (DataRow row in ingredients.Rows)
                    ings.Text += row["amount"].ToString().Trim() + " " + row["ingredientName"].ToString() + "\r";
            }
           
            preperation.Text = myRecipe["preperation"].ToString() + "\r";

            amount.Text = myRecipe["amount"].ToString();
            time.Text = myRecipe["preparationTime"].ToString();
            preperTm.Text = myRecipe["Baking/CookingTime"].ToString();
        }

        //7
        public static void suggests(ListView rndList, List<string> suggestedRecipes, int recipeCode)
        {
            StackPanel sp;
            Image img;
            Label lb;
            Border border;
            string IsThisImg = "";
            int listViewNum = 0;
            string categoryName = BL.getName("SP_getCategoryNameByRecipe", "@recipeCode", recipeCode);
            BL.recipes = BL.parameterForTable("SP_getRecipesByCategory", BL.getCode("getCategoryCodeByName", categoryName), "@categoryCode");
            int recipesNum = BL.recipes.Rows.Count;
            for (int i = 0; listViewNum < 3 && recipesNum > 0; i++, listViewNum++, recipesNum--)
            {
                sp = new StackPanel();
                img = new Image();
                lb = new Label();
                border = new Border();

                img.Source = new BitmapImage(new Uri(Environment.CurrentDirectory + @"..\..\..\images\recipes\" + categoryName + "\\" + recipes.Rows[i]["picture"].ToString()));
                IsThisImg = parameterForTable("SP_getRecipesByCode", recipeCode, "@recipeCode").Rows[0]["picture"].ToString();
                
                if (string.Compare(recipes.Rows[i]["picture"].ToString(), IsThisImg) == 0)
                {
                    listViewNum--;
                    continue;
                }

                img.Stretch = Stretch.Fill;
                img.Height = 100;
                img.Margin = new Thickness(10, 10, 10, 0);
                lb.Content = BL.recipes.Rows[i]["recipeName"].ToString();
                lb.FontSize = 15;
                lb.Height = 30;
                lb.Margin = new Thickness(10, 0, 10, 0);
                lb.HorizontalContentAlignment = HorizontalAlignment.Center;
                lb.VerticalAlignment = VerticalAlignment.Bottom;

                var bc = new BrushConverter();
                border.BorderBrush = (Brush)bc.ConvertFrom("#c32026");

                border.BorderThickness = new Thickness(2);
                border.CornerRadius = new CornerRadius(3);
                sp.Width = 200;
                border.Margin = new Thickness(0, 10, 0, 10);
                border.Child = sp;

                sp.Height = 140;
                sp.Children.Add(img);
                sp.Children.Add(lb);
                rndList.Items[listViewNum] = border;
                rndList.Visibility = Visibility.Visible;
                rndList.Cursor = Cursors.Hand;
                suggestedRecipes.Add(recipes.Rows[i]["recipeName"].ToString());
            }
        }

        //8
        public static void LB_selecionChanged(Window thisWindow, ListBox lbSuggestion)
        {
            if (lbSuggestion.SelectedIndex >= 0)
            {
                Data.specificRecipeName = lbSuggestion.SelectedItem.ToString();
                BL.getName("SP_getCategoryNameByRecipe", "@recipeCode", BL.getCode("SP_getRecipeCodeByName", Data.specificRecipeName));
                specific_Recipe sr = new specific_Recipe();
                thisWindow.Hide();
                sr.ShowDialog();
                thisWindow.ShowDialog();
            }
        }

        //9
        public static void textbox_changed(TextBox tb, ListBox lbSuggestion, List<string> recipesList)
        {
            List<string> autoList = new List<string>();
            string name = "";

            if (tb.Text.Length == 0)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
                return;
            }

            name = tb.Text;
            autoList.Clear();
            lbSuggestion.ItemsSource = null;

            foreach (string item in recipesList)
            {
                if (item.Contains(name))
                    autoList.Add(item);
            }

            if (autoList == null || autoList.Count <= 0)
            {
                lbSuggestion.Visibility = Visibility.Collapsed;
                lbSuggestion.ItemsSource = null;
            }

            else
            {
                lbSuggestion.ItemsSource = autoList;
                lbSuggestion.Visibility = Visibility.Visible;
            }
        }

        //10
        public static void getRecipes(Window thisWindow)
        {
            List<SearchRecipesNode> recipesForUser = new List<SearchRecipesNode>();
            int ingredientsInRecipe = 0, recipeCode;
            DataTable myRecipes = BL.parameterForTable("SP_getTable", "Recipes", "@tableName");
            DataTable ingredients;
            SearchRecipesNode node;
            for (int i = 0; i < myRecipes.Rows.Count; i++)
            {
                recipeCode = BL.getCode("SP_getRecipeCodeByName", myRecipes.Rows[i]["recipeName"].ToString());
                ingredients = BL.parameterForTable("SP_getIngredientsByRecipe", recipeCode, "@recipeCode");
                
                foreach (DataRow ingredient in ingredients.Rows)
                {
                    foreach (var item in basketIngredientsNames)
                    {
                        if (string.Compare(item, ingredient["ingredientName"].ToString()) == 0)
                            ingredientsInRecipe++;
                    }
                }
                if (ingredientsInRecipe > 0)
                {
                    node = new SearchRecipesNode(recipeCode, ingredientsInRecipe);
                    recipesForUser.Add(node);
                    ingredientsInRecipe = 0;
                }
            }
            if (recipesForUser.Count > 0)
            {
                quickSort(recipesForUser, 0, recipesForUser.Count - 1);
                BL.listToTable(recipesForUser);
                BL.openCategory(thisWindow, "מתכונים של החיפוש");
                thisWindow.Show();
            }
            else
            {
                MessageBox.Show("אין תוצאות");
            }

        }

        //11
        private static void quickSort(List<SearchRecipesNode> arr, int low, int high)
        {
            if (low < high)
            {
                int pivot = partition(arr, low, high);
                quickSort(arr, low, pivot - 1);
                quickSort(arr, pivot + 1, high);
            }
        }
        private static int partition(List<SearchRecipesNode> arr, int low, int high)
        {
            int pivot = low++;
            while (low <= high)
            {
                if (arr[low].SameIngredientsNum <= arr[pivot].SameIngredientsNum) low++;
                else if (arr[high].SameIngredientsNum > arr[pivot].SameIngredientsNum) high--;
                else swap(arr, low, high);
            }
            swap(arr, high, pivot);
            return high;
        }
        public static void swap(List<SearchRecipesNode> arr, int high, int pivot)
        {
            SearchRecipesNode temp = arr[high];
            arr[high] = arr[pivot];
            arr[pivot]= temp;
        }

        //12
        static Ellipse ellipse;
        public static void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ellipse = sender as Ellipse;

            foreach (var item in ingsList)
            {
                if (string.Compare(item.Name, ellipse.Name)==0)
                {
                    msgBoxOK msg = new msgBoxOK("המצרך כבר קיים");
                    msg.ShowDialog();
                    return;
                }
            }

            foreach (var item in list)
            {
                if (string.Compare(ellipse.Name, item.Code) == 0)
                {
                    image = item.Image;
                    break;
                }
            }

            DragDrop.DoDragDrop((DependencyObject)sender, e.Source, DragDropEffects.Copy);
        }

        //13
        public static void basket_Drop(object sender, DragEventArgs e)
        {
            GridLength length = new GridLength(60);
            RowDefinition row;

            basketIngredientsNames.Add((image.Substring(image.LastIndexOf('\\') + 1)).Replace(".jpg", ""));

            if (BL.ingsList.Count % 3 == 0)
            {
                row = new RowDefinition();
                row.Height = length;
                Data.basketGrid.RowDefinitions.Add(row);
            }
           
            int rows = Data.basketGrid.RowDefinitions.Count - 1;
            int columns = BL.ingsList.Count % 3;

            Ellipse newE = XamlReader.Parse(XamlWriter.Save(ellipse)) as Ellipse;
            newE.Name = ellipse.Name;
            newE.Margin = new Thickness(5);
            newE.Height = 45;
            newE.SetValue(Grid.ColumnProperty, columns);
            newE.SetValue(Grid.RowProperty, rows);
            ingsList.Add(newE);
            Data.basketGrid.Children.Add(newE);
            addDeleteButton(rows, columns);
        }

        //14
        public static void addDeleteButton(int countRows, int columns)
        {
            Button delete = new Button();
            delete.HorizontalAlignment = HorizontalAlignment.Right;
            delete.VerticalAlignment = VerticalAlignment.Top;
            delete.Height = 10;
            delete.Width = 10;
            delete.BorderThickness = new Thickness(0);
            delete.PreviewMouseDown += deleteIngredient;
            ImageBrush icon = new ImageBrush();
            string fileLocation = System.IO.Path.Combine(Environment.CurrentDirectory + @"..\..\..\images\design\redX.png");
            icon.ImageSource = new BitmapImage(new Uri(fileLocation));
            delete.Background = icon;
            delete.SetValue(Grid.ColumnProperty, columns);
            delete.SetValue(Grid.RowProperty, countRows);

            Data.basketGrid.Children.Add(delete);
        }

        //15
        public static void deleteIngredient(object sender, MouseButtonEventArgs e)
        {
            Button deleteButton = (Button)sender;
            int ingRow = Grid.GetRow(deleteButton) * 3;
            int ingColumn = Grid.GetColumn(deleteButton);
            ingsList[ingRow + ingColumn].Visibility=Visibility.Collapsed;
            ingsList.RemoveAt(ingRow + ingColumn);
            basketIngredientsNames.RemoveAt(ingRow + ingColumn);

            Data.basketGrid.RowDefinitions.Clear();
            Data.basketGrid.Children.RemoveRange(0, Data.basketGrid.Children.Count);

            if (BL.ingsList.Count == 0)
                return;
            else
            {
                int countRows = (BL.ingsList.Count % 3 == 0) ? BL.ingsList.Count / 3 : (BL.ingsList.Count / 3) + 1;
                RowDefinition row;
                GridLength length = new GridLength(60);
                for (int i = 0; i < countRows; i++)
                {
                    row = new RowDefinition();
                    row.Height = length;
                    Data.basketGrid.RowDefinitions.Add(row);
                }
            }

            int ingsNum = BL.ingsList.Count;
            for (int i = 0; i < Data.basketGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < 3 && ingsNum > 0; j++)
                {
                    ingsList[i * 3 + j].SetValue(Grid.RowProperty, i);
                    ingsList[i * 3 + j].SetValue(Grid.ColumnProperty, j);
                    Data.basketGrid.Children.Add(ingsList[i * 3 + j]);
                    addDeleteButton(i, j);
                    ingsNum--;
                }
            }
        }
        
        //16
        public static void Image_MouseDown(string name, Window window)
        {
            switch (name)
            {
                case "logo":
                    if (window is MainWindow)
                        break;
                    Data.returnProblem = true;
                    window.Close();
                    Data.returnProblem = false;
                    Data.openWindows.RemoveRange(2, Data.openWindows.Count-2);
                    Data.openWindows[1].ShowDialog();
                    break;

                case "signOff":
                    mb = new messageBox();
                    mb.textMsg.Text = "?בטוח/ה שאת/ה רוצה להתנתק";
                    mb.ShowDialog();
                    if (Data.whichB == 1)
                    {
                        Data.returnProblem = true;
                        window.Close();
                        Data.returnProblem = false;

                        Data.openWindows.RemoveRange(1, Data.openWindows.Count - 1);
                        Data.openWindows[0].ShowDialog();
                    }
                    break;

                case "likedRecipe":
;
                    BL.recipes = BL.getTable("SP_getRecipesByUserID", "likedRecipes");
                    openCategory(window, "מתכונים שאהבת");
                    break;

                case "lastRecipes":

                    BL.recipes = BL.getTable("SP_getRecipesByUserID", "lastRecipesEntered");
                    openCategory(window, "מתכונים אחרונים שראית");
                    break;

                case "search":
                    if (window is Search)
                        break;
                    window.Hide();
                    Search sch = new Search();
                    sch.ShowDialog();
                    break;

                case "return":
                        if (window is MainWindow)
                        {
                            mb = new messageBox();
                            mb.textMsg.Text = "?בטוח/ה שאת/ה רוצה להתנתק";
                            mb.ShowDialog();
                            if (Data.whichB == 1)
                            {
                                Data.returnProblem = true;
                                window.Close();
                                Data.returnProblem = false;
                                string aa = Data.openWindows[1].Title;
                                Data.openWindows.RemoveAt(1);
                            }
                            return;
                        }
                        else
                        {
                            Data.returnProblem = true;
                            window.Close();
                            Data.returnProblem = false;
                            Data.openWindows.RemoveAt(Data.openWindows.Count - 1);

                            switch (Data.openWindows.Last().Name)//in case of change in these tables
                            {
                                case "מתכונים שאהבת":
                                    BL.recipes = getTable("SP_getRecipesByUserID", "likedRecipes");
                                    ((specific_Categories)Data.openWindows.Last()).newRecipes(BL.recipes);
                                    break;

                                case "מתכונים אחרונים שראית":
                                    BL.recipes = getTable("SP_getRecipesByUserID", "lastRecipesEntered");
                                    ((specific_Categories)Data.openWindows.Last()).newRecipes(BL.recipes);
                                    break;

                            }
                            Data.openWindows.Last().ShowDialog();
                        }
                    break;
            }
        }

        //17
        public static void Window_Closing(CancelEventArgs e)
        {
            messageBox mb = new messageBox();
            mb.textMsg.Text = "?בטוח/ה שאת/ה רוצה לצאת";
            mb.ShowDialog();
            if (Data.whichB==1)
                Environment.Exit(1);
            else e.Cancel = true;
        }

        //18
        public static int AddNewUser(string userName, string userPassword, string userID)
        {
            param.Clear();
            List<SqlParameter> listParam = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@userName", userName);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);

            p = new SqlParameter("@userPassword", userPassword);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);

            p = new SqlParameter("@userID", userID);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);

            return (int)Dal.Scalar("dbo.SP_AddNewUser", listParam);
        }

        //19
        public static int CheckIfUserExist(string userName, string userPassword)
        {
            List<SqlParameter> listParam = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@userName", userName);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);

            p = new SqlParameter("@userPassword", userPassword);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);

            return (int)Dal.Scalar("dbo.SP_CheckIfUserExist", listParam);
        }

        //20
        public static int CheckIfIdExist(string userID)
        {
            List<SqlParameter> listParam = new List<SqlParameter>();
            p = new SqlParameter("@userID", userID);
            p.DbType = DbType.String;
            p.Direction = ParameterDirection.Input;
            listParam.Add(p);

            return (int)Dal.Scalar("dbo.SP_CheckIIdExist", listParam);
        }

        //21
        public static void openSuggestedRecipe(string name, Window window)
        {
            Data.specificRecipeName = name;
            specific_Recipe sr = new specific_Recipe();
            window.Hide();
            sr.ShowDialog();
        }

        //22
        public static int getRecipeCode()
        {
            int recipeCode;
            if (Data.specificRecipeName.StartsWith("button"))//case button: from all pages, case name: from searching recipe in search textbox
            {
                // explanation //
                //example: name: "button3" -> replace -> "3" ->  3 -> minus 1 for arr of the table -> 3-1 -> 2
                
                int recipeRowNumber = int.Parse((Data.specificRecipeName.Replace("button", ""))) - 1;
                specific_Categories sc = (specific_Categories)Data.openWindows[Data.openWindows.Count-2];
                Data.specificRecipeName = sc.getRecipes().Rows[recipeRowNumber]["recipeName"].ToString();
            }
            recipeCode = BL.getCode("SP_getRecipeCodeByName", Data.specificRecipeName);
            return recipeCode;
        }

        //23
        public static void addToLastRecipesEntered(int recipeCode)
        {
            if (BL.isRecipe("SP_isRecipeExsist", recipeCode) == 1)
                BL.insertOrDelete("SP_updateDate", recipeCode, "lastRecipesEntered");
            else
                BL.insertOrDelete("SP_insertIntoTable", recipeCode, "lastRecipesEntered");
            if (BL.countTable("lastRecipesEntered") > 30)
                BL.deleteTop();
        }

        //24
        public static void verifyId(string id, int length)
        {
            int s = Convert.ToInt32(id);
            int temp = 1, sum = 0, x = 10;
            int shomerDigitBikoret = s % 10;
            s /= 10;
            length -= 1;
            if (length % 2 == 0)//לבדוק אי זוגי או זוגי כדי לדעם אם להכפיל באפס או אחד
                temp = 2;

            for (int i = 0; i < length; i++)//לעשות אפשרות של תז פחות מתשע או רק תשע? 
            {
                if (((s % 10) * temp) > 9)//אם החיבור שווה 2 ספרות מחבר אותן לספרה אחת
                    x = (((s % 10) * temp) % 10) + (((s % 10) * temp) / 10);
                else
                    x = (s % 10) * temp;
                sum += x;
                s /= 10;

                if (temp == 1)
                    temp = 2;
                else
                    temp = 1;
            }

            //בשני התנאים האלה מאמת ספרת ביקורת האם נכונה
            if (sum < 10 && sum != shomerDigitBikoret)
                MessageBox.Show("תעודת הזהות שהוקשה לא תקינה\n אנא הקש שוב");
            if (sum > 10)
            {
                sum %= 10;
                sum = 10 - sum;
                if (sum != shomerDigitBikoret)
                    MessageBox.Show("תעודת הזהות שהוקשה לא תקינה\n אנא הקש שוב");
                return;//מה לשים פה? 
            }

        }

        public static void funUser_exist_Click(Button combobox1, TextBox TextUserName, Button password1, PasswordBox TextPassword)
        {
            combobox1.Visibility = Visibility.Collapsed;
            TextUserName.Visibility = Visibility.Visible;

            if (TextPassword.Password == "")
            {
                password1.Visibility = Visibility.Visible;
                TextPassword.Visibility = Visibility.Collapsed;
            }
        }

        public static void funPassword_Click(Button combobox1, TextBox TextUserName, Button password1, PasswordBox TextPassword)
        {
            password1.Visibility = Visibility.Collapsed;
            TextPassword.Visibility = Visibility.Visible;

            if (TextUserName.Text == "")
            {
                combobox1.Visibility = Visibility.Visible;
                TextUserName.Visibility = Visibility.Collapsed;
            }
        }

        public static void funEnter_Click(Button combobox1, TextBox TextUserName, Button password1, PasswordBox TextPassword, Window window)
        {
            if (TextUserName.Text == "" || TextUserName.Text == " " || TextPassword.Password == "" || TextPassword.Password == " ")
            {
                if (TextUserName.Text == "" || TextUserName.Text == " ")
                {
                    combobox1.Visibility = Visibility.Visible;
                    TextUserName.Visibility = Visibility.Collapsed;
                }
                if (TextPassword.Password == "" || TextPassword.Password == " ")
                {
                    password1.Visibility = Visibility.Visible;//גורם שאם שאר הכפתורים ריקים יחזור להם עיצוב בכפתור
                    TextPassword.Visibility = Visibility.Collapsed;
                }
                msgBoxOK msg = new msgBoxOK("שם משתמש או סיסמא חסרים");
                msg.ShowDialog();
                return;
            }
            else
            {
                if (BL.CheckIfUserExist(TextUserName.Text, TextPassword.Password) == 1)
                {
                    Data.userId = BL.getUserId(TextUserName.Text).Trim();
                    MainWindow mw = new MainWindow();
                    window.Hide();
                    mw.ShowDialog();
                }
                else
                {
                    msgBoxOK msg = new msgBoxOK("שם משתמש או סיסמא לא קיימים");
                    msg.ShowDialog();
                    return;
                }
            }
            if (TextPassword.Password == "")//גורם שאם שאר הכפתורים ריקים יחזור להם עיצוב בכפתור
            {
                password1.Visibility = Visibility.Visible;
                TextPassword.Visibility = Visibility.Collapsed;
            }

            if (TextUserName.Text == "")//גורם שאם שאר הכפתורים ריקים יחזור להם עיצוב בכפתור
            {
                combobox1.Visibility = Visibility.Visible;
                TextUserName.Visibility = Visibility.Collapsed;
            }
            Data.openWindows[0].ShowDialog();

        }

        public static void funAdd_user_Click(Button addUser, TextBox TBNewUser, Button addPassword, PasswordBox TBNewPassword, Button addId, TextBox TBID)
        {
            addUser.Visibility = Visibility.Collapsed;
            TBNewUser.Visibility = Visibility.Visible;

            if (TBNewPassword.Password == "")
            {
                TBNewPassword.Visibility = Visibility.Collapsed;
                addPassword.Visibility = Visibility.Visible;
            }
            if (TBID.Text == "")
            {
                TBID.Visibility = Visibility.Collapsed;
                addId.Visibility = Visibility.Visible;
            }
        }




        public static void funAdd_password_Click(Button addUser, TextBox TBNewUser, Button addPassword, PasswordBox TBNewPassword, Button addId, TextBox TBID)
        {
            addPassword.Visibility = Visibility.Collapsed;
            TBNewPassword.Visibility = Visibility.Visible;

            if (TBNewUser.Text == "")
            {
                TBNewUser.Visibility = Visibility.Collapsed;
                addUser.Visibility = Visibility.Visible;
            }
            if (TBID.Text == "")
            {
                TBID.Visibility = Visibility.Collapsed;
                addId.Visibility = Visibility.Visible;
            }
        }
        public static void funAdd_id_Click(Button addUser, TextBox TBNewUser, Button addPassword, PasswordBox TBNewPassword, Button addId, TextBox TBID)
        {
            addId.Visibility = Visibility.Collapsed;
            TBID.Visibility = Visibility.Visible;

            if (TBNewUser.Text == "")
            {
                TBNewUser.Visibility = Visibility.Collapsed;
                addUser.Visibility = Visibility.Visible;
            }
            if (TBNewPassword.Password == "")
            {
                TBNewPassword.Visibility = Visibility.Collapsed;
                addPassword.Visibility = Visibility.Visible;
            }
        }
        public static void funuser_reset_Click(Button resetUserName, TextBox TextBoxnewpassword, Button resetPassword, PasswordBox PasswordBoxnewpassword, Button IdToRecognize, TextBox TBIDRecognize)
        {
            resetUserName.Visibility = Visibility.Collapsed;
            TextBoxnewpassword.Visibility = Visibility.Visible;

            if (PasswordBoxnewpassword.Password.Length == 0)//
            {
                PasswordBoxnewpassword.Visibility = Visibility.Collapsed;
                resetPassword.Visibility = Visibility.Visible;
            }
            if (TBIDRecognize.Text == "")
            {
                TBIDRecognize.Visibility = Visibility.Collapsed;
                IdToRecognize.Visibility = Visibility.Visible;
            }
        }
    } 
}
