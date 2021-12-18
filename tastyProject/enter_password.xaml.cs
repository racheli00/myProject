using System;
using System.Windows;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;

namespace tastyProject
{
    /// <summary>
    /// Interaction logic for enter_password.xaml
    /// </summary>
    /// 
    public partial class enter_password : Window
    {
        public enter_password()
        {
            InitializeComponent();
            Data.openWindows.Add(this);
        }

        //כניסת משתמש קיים
        private void user_exist_Click(object sender, RoutedEventArgs e)//שם משתמש
        {
            BL.funUser_exist_Click(combobox1, TextUserName, password1 , TextPassword);
        }
        private void password_Click(object sender, RoutedEventArgs e)//סיסמא
        {
            BL.funPassword_Click(combobox1, TextUserName, password1, TextPassword);
        }
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            BL.funEnter_Click(combobox1, TextUserName, password1, TextPassword, this);
        }

        //יצירת שם משתמש חדש
        private void new_user_Click(object sender, RoutedEventArgs e)//מעבר ליצירת חשבון
        {
            TextUserName.Text = null;
            TextPassword.Password = null;
            TBIDRecognize.Text = null;

            status.Content = "משתמש חדש";
            resetUserName.Visibility = Visibility.Collapsed;
            TextBoxnewpassword.Visibility = Visibility.Collapsed;
            resetPassword.Visibility = Visibility.Collapsed;
            PasswordBoxnewpassword.Visibility = Visibility.Collapsed;
            combobox1.Visibility = Visibility.Collapsed;
            TextUserName.Visibility = Visibility.Collapsed;
            password1.Visibility = Visibility.Collapsed;
            TextPassword.Visibility = Visibility.Collapsed;
            enter.Visibility = Visibility.Collapsed;
            IdToRecognize.Visibility = Visibility.Collapsed;

            TBIDRecognize.Visibility = Visibility.Collapsed;

            addPassword.Visibility = Visibility.Visible;
            Restore.Visibility = Visibility.Collapsed;

            addUser.Visibility = Visibility.Visible;
            TBNewUser.Visibility = Visibility.Collapsed;

            TBNewPassword.Visibility = Visibility.Collapsed;
            addId.Visibility = Visibility.Visible;
            TBID.Visibility = Visibility.Collapsed;

            haveUser.Visibility = Visibility.Visible;
            createAccount.Visibility = Visibility.Visible;
            forgetPassword.Visibility = Visibility.Collapsed;
            rememberPassword.Visibility = Visibility.Collapsed;
            reseatUsrPswrd.Visibility = Visibility.Collapsed;
            new_user.Visibility = Visibility.Collapsed;
        }


        private void add_user_Click(object sender, RoutedEventArgs e)//הכנסת שם משתמש- משתמש חדש
        {
            BL.funAdd_user_Click(addUser, TBNewUser, addPassword, TBNewPassword, addId, TBID);
        }
        private void add_password_Click(object sender, RoutedEventArgs e)//הכנסת סיסמא- משתמש חדש
        {
            BL.funAdd_password_Click(addUser, TBNewUser, addPassword, TBNewPassword, addId, TBID);
        }
        private void add_id_Click(object sender, RoutedEventArgs e)//הכנסת תז- משתמש חדש
        {
            BL.funAdd_id_Click(addUser, TBNewUser, addPassword, TBNewPassword, addId, TBID);
        }

        private void have_user_Click(object sender, RoutedEventArgs e)
        {
            TBNewUser.Text = null;
            TBNewPassword.Password = null;
            TBID.Text = null;
            status.Content = "משתמש קיים";
            IdToRecognize.Visibility = Visibility.Collapsed;
            TBIDRecognize.Visibility = Visibility.Collapsed;
            TBID.Visibility = Visibility.Collapsed;
            TBNewPassword.Visibility = Visibility.Collapsed;
            TBNewUser.Visibility = Visibility.Collapsed;
            Restore.Visibility = Visibility.Collapsed;
            addId.Visibility = Visibility.Collapsed;
            addPassword.Visibility = Visibility.Collapsed;
            addUser.Visibility = Visibility.Collapsed;
            resetUserName.Visibility = Visibility.Collapsed;
            TextBoxnewpassword.Visibility = Visibility.Collapsed;
            resetPassword.Visibility = Visibility.Collapsed;
            PasswordBoxnewpassword.Visibility = Visibility.Collapsed;

            combobox1.Visibility = Visibility.Visible;
            TextUserName.Visibility = Visibility.Collapsed;
            password1.Visibility = Visibility.Visible;
            TextPassword.Visibility = Visibility.Collapsed;
            //lableExistUser.Visibility = Visibility.Visible;
            enter.Visibility = Visibility.Visible;


            haveUser.Visibility = Visibility.Collapsed;
            createAccount.Visibility = Visibility.Collapsed;
            rememberPassword.Visibility = Visibility.Collapsed;
            reseatUsrPswrd.Visibility = Visibility.Collapsed;

            forgetPassword.Visibility = Visibility.Visible;
            new_user.Visibility = Visibility.Visible;

        }
        private void forget_Password_Click(object sender, RoutedEventArgs e)
        {
            TextUserName.Text = null;
            TextPassword.Password = null;
            status.Content = "איפוס פרטי חשבון";
            resetUserName.Visibility = Visibility.Visible;
            TextBoxnewpassword.Visibility = Visibility.Collapsed;
            resetPassword.Visibility = Visibility.Visible;
            PasswordBoxnewpassword.Visibility = Visibility.Collapsed;


            IdToRecognize.Visibility = Visibility.Visible;
            TBIDRecognize.Visibility = Visibility.Collapsed;

            TextPassword.Visibility = Visibility.Collapsed;

            combobox1.Visibility = Visibility.Collapsed;
            password1.Visibility = Visibility.Collapsed;
            enter.Visibility = Visibility.Collapsed;
            TextUserName.Visibility = Visibility.Collapsed;
            addUser.Visibility = Visibility.Collapsed;
            TBNewUser.Visibility = Visibility.Collapsed;
            addPassword.Visibility = Visibility.Collapsed;
            TBNewPassword.Visibility = Visibility.Collapsed;
            addId.Visibility = Visibility.Collapsed;
            TBID.Visibility = Visibility.Collapsed;
            Restore.Visibility = Visibility.Collapsed;

            haveUser.Visibility = Visibility.Collapsed;
            createAccount.Visibility = Visibility.Collapsed;
            forgetPassword.Visibility = Visibility.Collapsed;
            new_user.Visibility = Visibility.Collapsed;

            rememberPassword.Visibility = Visibility.Visible;
            reseatUsrPswrd.Visibility = Visibility.Visible;
        }
        private void Recognize_Click(object sender, RoutedEventArgs e)//לפתור את הבעיה שכשלוחצים על התיבת טקסט השניה והאחרת נשארה ריקה יש להוסיף את שם הכפתור
        {
            IdToRecognize.Visibility = Visibility.Collapsed;
            TBIDRecognize.Visibility = Visibility.Visible;

            if (TextBoxnewpassword.Text == "")
            {
                TextBoxnewpassword.Visibility = Visibility.Collapsed;
                resetUserName.Visibility = Visibility.Visible;
            }
            if (PasswordBoxnewpassword.Password.Length == 0)//
            {
                PasswordBoxnewpassword.Visibility = Visibility.Collapsed;
                resetPassword.Visibility = Visibility.Visible;
            }
        }

        private void user_reset_Click(object sender, RoutedEventArgs e)//לפתור את הבעיה שכשלוחצים על התיבת טקסט השניה והאחרת נשארה ריקה יש להוסיף את שם הכפתור
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
        private void password_reset_Click(object sender, RoutedEventArgs e)//לפתור את הבעיה שכשלוחצים על התיבת טקסט השניה והאחרת נשארה ריקה יש להוסיף את שם הכפתור
        {
            resetPassword.Visibility = Visibility.Collapsed;
            PasswordBoxnewpassword.Visibility = Visibility.Visible;

            if (TBIDRecognize.Text == "")
            {
                TBIDRecognize.Visibility = Visibility.Collapsed;
                IdToRecognize.Visibility = Visibility.Visible;
            }
            if (TextBoxnewpassword.Text == "")
            {
                TextBoxnewpassword.Visibility = Visibility.Collapsed;
                resetUserName.Visibility = Visibility.Visible;
            }

        }
        private void remember_Password_Click(object sender, RoutedEventArgs e)
        {
            TextBoxnewpassword.Text = null;
            PasswordBoxnewpassword.Password = null;
            status.Content = "משתמש קיים";

            combobox1.Visibility = Visibility.Visible;
            password1.Visibility = Visibility.Visible;
            enter.Visibility = Visibility.Visible;


            resetPassword.Visibility = Visibility.Collapsed;
            resetUserName.Visibility = Visibility.Collapsed;
            Restore.Visibility = Visibility.Collapsed;
            TextBoxnewpassword.Visibility = Visibility.Collapsed;
            PasswordBoxnewpassword.Visibility = Visibility.Collapsed;
            TextPassword.Visibility = Visibility.Collapsed;
            TextUserName.Visibility = Visibility.Collapsed;
            addUser.Visibility = Visibility.Collapsed;
            TBNewUser.Visibility = Visibility.Collapsed;
            addPassword.Visibility = Visibility.Collapsed;
            TBNewPassword.Visibility = Visibility.Collapsed;
            addId.Visibility = Visibility.Collapsed;
            TBIDRecognize.Visibility = Visibility.Collapsed;
            TBID.Visibility = Visibility.Collapsed;
            IdToRecognize.Visibility = Visibility.Collapsed;

            haveUser.Visibility = Visibility.Collapsed;
            createAccount.Visibility = Visibility.Collapsed;
            rememberPassword.Visibility = Visibility.Collapsed;
            reseatUsrPswrd.Visibility = Visibility.Collapsed;

            new_user.Visibility = Visibility.Visible;
            forgetPassword.Visibility = Visibility.Visible;
        }
        bool mouseIsDown = System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed;


        static Random random = new Random();
        private void reseatUsrPswrd_Click(object sender, RoutedEventArgs e)
        {
            if (TBIDRecognize.Text == "")
            {
                TBIDRecognize.Visibility = Visibility.Collapsed;
                IdToRecognize.Visibility = Visibility.Visible;
            }
            if (TextBoxnewpassword.Text == "")
            {
                TextBoxnewpassword.Visibility = Visibility.Collapsed;
                resetUserName.Visibility = Visibility.Visible;
            }
            if (PasswordBoxnewpassword.Password.Length == 0)
            {
                PasswordBoxnewpassword.Visibility = Visibility.Collapsed;
                resetPassword.Visibility = Visibility.Visible;
            }

            if (TextBoxnewpassword.Text.Length > 0 && PasswordBoxnewpassword.Password.Length > 0 && TBIDRecognize.Text != "")//איפוס יתאפשר רק אם שם משתמש וסיסמה קיימים 
            {

                if (!(isAllDigits(TBIDRecognize.Text)))
                {
                    msgBoxOK msg = new msgBoxOK("תעודת זהות חייבת להיות מורכבת מספרות בלבד");
                    msg.ShowDialog();
                    return;
                }

                if (TBIDRecognize.Text.Length == 9)
                    BL.verifyId(TBIDRecognize.Text, 9);//בדיקת תקינות תעודת זהות
                else
                {
                    msgBoxOK msg = new msgBoxOK("תעודת זהות חייבת להכיל 9 ספרות");
                    msg.ShowDialog();
                    return;
                }

                if (BL.CheckIfUserExist(TextBoxnewpassword.Text, PasswordBoxnewpassword.Password) == 1)
                {
                    msgBoxOK msg = new msgBoxOK("שם המשתמש שהוקש כבר קיים במערכת");
                    msg.ShowDialog();
                    return;
                }
                else
                {
                    string temp = TBIDRecognize.Text;//שומר את הערך של התז שהתמשתמש הקיש ואח"כ שולח אותו עם השם מתמש וסיסמא החדשים ליצירת איש קשר חדש
                    if (BL.CheckIfIdExist(TBIDRecognize.Text) == 1)
                    {
                        BL.Delete(TBIDRecognize.Text, TextBoxnewpassword.Text, PasswordBoxnewpassword.Password);//מוחק את כל השורה של איש הקשר שהתאפס ע"י תז שהשתמש הקיש
                                                      //אם מוסיפים את הפרוצדורה למחיקה אז לשים בערך של תז בפרוצדורת ההוספה  את הערך טמפ
//                        BL.AddNewUser(TextBoxnewpassword.Text, PasswordBoxnewpassword.Password, temp);//מוסיף את איש הקשר החדש

                        Data.userId = TBIDRecognize.Text;
                        MainWindow mw = new MainWindow();
                        this.Hide();
                        mw.ShowDialog();
                    }
                    else
                    {
                        msgBoxOK msg = new msgBoxOK("תז שהוקשה לא קיימת אנא הקש תז קיימת על מנת שנוכל לאפס את חשבונך");
                        msg.ShowDialog();
                        return;
                    }
                }
            }
            else
            {
                msgBoxOK msg = new msgBoxOK("אחד או יותר מהערכים חסרים");
                msg.ShowDialog();
                return;
            }
            Data.openWindows[0].ShowDialog();
        }

        public void create_account_Click(object sender, RoutedEventArgs e)
        {
            if (TBNewPassword.Password.Length == 0 || TBNewUser.Text == "" || TBNewUser.Text == " " || TBID.Text == "" || TBID.Text == " ")//בדיקה שכל הערכים אכן הוזנו
            {
                if(TBNewPassword.Password.Length == 0)
                {
                    TBNewPassword.Visibility = Visibility.Collapsed;
                    addPassword.Visibility = Visibility.Visible;
                }
                if (TBNewUser.Text == "" || TBNewUser.Text == " ")
                {
                    TBNewUser.Visibility = Visibility.Collapsed;
                    addUser.Visibility = Visibility.Visible;

                }
                if(TBID.Text == "" || TBID.Text == " ")
                {
                    TBID.Visibility = Visibility.Collapsed;
                    addId.Visibility = Visibility.Visible;
                }
                msgBoxOK msg = new msgBoxOK("אחד מערכים לא הוקש");
                msg.ShowDialog();
                return;
            }
            else
            {
                if (!(isAllDigits(TBID.Text)))
                {
                    msgBoxOK msg = new msgBoxOK("תעודת זהות חייבת להיות מורכבת מספרות בלבד");
                    msg.ShowDialog();
                    return;
                }
                if (TBID.Text.Length == 9)
                    BL.verifyId(TBID.Text, 9);//בדיקת תקינות תעודת זהות
                else
                {
                    msgBoxOK msg = new msgBoxOK("תעודת זהות חייבת להכיל 9 ספרות");
                    msg.ShowDialog();
                    return;
                }
                //הכנסת שם משתמש, סיסמא ותעודת זהות ובדיקה אם קיים כזה ע"י הפרוצדורה מאסקיואל
                if (BL.AddNewUser(TBNewUser.Text, TBNewPassword.Password, TBID.Text) == -1)
                {
                    Data.userId = TBID.Text;
                    MainWindow mw = new MainWindow();
                    this.Hide();
                    mw.ShowDialog();
                }


                else
                {
                    msgBoxOK msg = new msgBoxOK("שם המשתמש או תעודת הזהות שהוקשו קיימים במערכת");
                    msg.ShowDialog();
                    return;
                }
            }
            Data.openWindows[0].ShowDialog();
        }

        bool isAllDigits(string ID)
        {
            foreach (char item in ID)
            {
                if (!(char.IsDigit(item)))
                    return false;
            }
            return true;
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Data.returnProblem == false)
                BL.Window_Closing(e);
        }
    }

}

