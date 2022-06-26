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
    /// Логика взаимодействия для Prices.xaml
    /// </summary>
    public partial class Prices : Window
    {
        private MyParser parse = new MyParser();
        private DB db = new DB();

        public Prices()
        {
            InitializeComponent();
            var table = db.GetPyat().Tables[0];

            name.Text = "Авокадо 1шт";
            var row = table.Rows[0].ItemArray;
            price.Text = string.Format("{0:C2}", row[1].ToString());
            code.Text = row[2].ToString();

            name2.Text = "Апельсины 1кг";
            row = table.Rows[1].ItemArray;
            price2.Text = string.Format("{0:C2}", row[1].ToString());
            code2.Text = row[2].ToString();

            name3.Text = "Арбузы 1кг";
            row = table.Rows[2].ItemArray;
            price3.Text = string.Format("{0:C2}", row[1].ToString());
            code3.Text = row[2].ToString();

            name4.Text = "Баклажан 1кг";
            row = table.Rows[3].ItemArray;
            price4.Text = string.Format("{0:C2}", row[1].ToString());
            code4.Text = row[2].ToString();

            name5.Text = "Виноград 1кг";
            row = table.Rows[4].ItemArray;
            price5.Text = string.Format("{0:C2}", row[1].ToString());
            code5.Text = row[2].ToString();

            name6.Text = "Гранат 1кг";
            row = table.Rows[5].ItemArray;
            price6.Text = string.Format("{0:C2}", row[1].ToString());
            code6.Text = row[2].ToString();

            name7.Text = "Шампиньоны 500г";
            row = table.Rows[6].ItemArray;
            price7.Text = string.Format("{0:C2}", row[1].ToString());
            code7.Text = row[2].ToString();

            name8.Text = "Груши 1кг";
            row = table.Rows[7].ItemArray;
            price8.Text = string.Format("{0:C2}", row[1].ToString());
            code8.Text = row[2].ToString();

            name9.Text = "Капуста 1кг";
            row = table.Rows[8].ItemArray;
            price9.Text = string.Format("{0:C2}", row[1].ToString());
            code9.Text = row[2].ToString();

            name10.Text = "Картофель 1кг";
            row = table.Rows[9].ItemArray;
            price10.Text = string.Format("{0:C2}", row[1].ToString());
            code10.Text = row[2].ToString();

            name11.Text = "Морковь 1кг";
            row = table.Rows[10].ItemArray;
            price11.Text = string.Format("{0:C2}", row[1].ToString());
            code11.Text = row[2].ToString();

            name12.Text = "Огурцы 1кг";
            row = table.Rows[11].ItemArray;
            price12.Text = string.Format("{0:C2}", row[1].ToString());
            code12.Text = row[2].ToString();

            name13.Text = "Редис 500г";
            row = table.Rows[12].ItemArray;
            price13.Text = string.Format("{0:C2}", row[1].ToString());
            code13.Text = row[2].ToString();

            name14.Text = "Свекла 1кг";
            row = table.Rows[13].ItemArray;
            price14.Text = string.Format("{0:C2}", row[1].ToString());
            code14.Text = row[2].ToString();

            name15.Text = "Томаты 700г";
            row = table.Rows[14].ItemArray;
            price15.Text = string.Format("{0:C2}", row[1].ToString());
            code15.Text = row[2].ToString();

            name16.Text = "Яблоко 1кг";
            row = table.Rows[15].ItemArray;
            price16.Text = string.Format("{0:C2}", row[1].ToString());
            code16.Text = row[2].ToString();
        }
    }
}