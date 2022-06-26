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

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для MakeCred.xaml
    /// </summary>
    public partial class MakeCred : Window
    {
        private DB db = new DB();
        private string name = "";

        public MakeCred(string _name)
        {
            InitializeComponent();
            name = _name;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            db.NewCredit(Convert.ToDouble(perc.Text), Convert.ToDecimal(sum.Text), DateTime.Now, date.SelectedDate, purp.Text);
            db.createOp("Кредит", Convert.ToDecimal(sum.Text), "открытие кредита", DateTime.Now, name, "Другое");
        }
    }
}