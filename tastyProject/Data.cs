using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace tastyProject
{
    public class Data
    {
        public static string userId="";
        public static int whichB = 0;
        public static string specificRecipeName="";
        public static string pageName = "";
        public static Grid basketGrid;
        public static bool returnProblem = false;
        public static List<Window> openWindows = new List<Window>();
    }
}
