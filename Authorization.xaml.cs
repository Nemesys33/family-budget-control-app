using System;
using System.Windows;

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private bool isAuthorize = false;
        private bool isAuthorized;

        public Authorization(bool _isAuthorized)
        {
            InitializeComponent();
            isAuthorized = _isAuthorized;
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            isAuthorize = true;
            this.DialogResult = true;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            if (!isAuthorized)
                this.Owner.Close();
            else this.Close();
        }

        public (bool, string) Auth()
        {
            if (this.ShowDialog() == true)
            {
                string myname = string.Format("{0} {1}", surname.Text.Trim(), name.Text.Trim());
                var db = new DB();
                if (password.Password.Trim() == db.getPass(myname).ToString().Trim() && surname.Text.Trim() != "" && name.Text.Trim() != "")
                {
                    isAuthorize = true;
                    MessageBox.Show("Авторизация пройдена");
                    return (true, myname.Trim());
                }
                else
                {
                    MessageBox.Show("Неудачная авторизация, попробуйте снова.");
                    return (false, myname.Trim());
                }
            }
            else
            {
                return (true, name.Text.Trim());
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isAuthorize && !isAuthorized)
                e.Cancel = true;
        }
    }
}