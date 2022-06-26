using System;
using System.Windows;
using System.Windows.Controls;

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void accept_Click(object sender, RoutedEventArgs e)
        {
            if (tbname.Text != "" && tbmail.Text != "" && tbpass.Text != "")
            {
                try
                {
                    var db = new DB();
                    SByte one = 1;
                    SByte zero = 0;
                    ComboBox comboBoxC = (ComboBox)cbacce;
                    ComboBoxItem acc = (ComboBoxItem)comboBoxC.SelectedItem;
                    string access = acc.Content.ToString();
                    ComboBox comboBoxB = (ComboBox)cbadmi;
                    ComboBoxItem adm = (ComboBoxItem)comboBoxB.SelectedItem;
                    string admin = adm.Content.ToString();
                    db.adduser(tbname.Text, tbmail.Text, tbpass.Text, access == "Да" ? one : zero, admin == "Да" ? one : zero);
                }
                catch { MessageBox.Show("Операция завершена неудачно."); }
            }

            this.Close();
            MessageBox.Show("операция выполнена.");
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}