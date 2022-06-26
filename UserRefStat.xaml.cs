using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для UserRefStat.xaml
    /// </summary>
    public partial class UserRefStat : Window
    {
        public UserRefStat()
        {
            InitializeComponent();
        }

        private void saveURS_Click(object sender, RoutedEventArgs e)
        {
            var path = new StringBuilder("");
            var folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path.Append(folderBrowserDialog1.SelectedPath).Append("\\UserRefStat.png");
            }
            System.Windows.MessageBox.Show($"Файл сохранен. Полный путь: {path}");
            try
            {
                plot.Plot.SaveFig(path.ToString());
            }
            catch { System.Windows.MessageBox.Show("Сохранение отменено."); }
        }

        private void daysURS_TextChanged(object sender, TextChangedEventArgs e)
        {
            plot.Plot.Clear();
            try
            {
                if (Convert.ToDouble(days.Text) < 1)
                    days.Text = "1";
            }
            catch { days.Text = "30"; }
            doStat();
        }

        private void doStat()
        {
            var db = new DB();
            var table = db.GetUserRefStat(Convert.ToDouble(days.Text)).Tables[0];
            table.Columns[0].ColumnName = "Пользователь";
            table.Columns[1].ColumnName = "Сумма пополнений";
            dgbalance.ItemsSource = table.DefaultView;

            var sums = table.AsEnumerable()
                .Select(r => r.Field<decimal>("Сумма пополнений"))
                .ToList();

            var labs = table.AsEnumerable()
                .Select(r => r.Field<string>("Пользователь"))
                .ToList();

            double[] values = new double[table.Rows.Count];
            string[] labels = labs.ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = Convert.ToDouble(sums[i]);
            }
            var pie = plot.Plot.AddPie(values);
            pie.SliceLabels = labels;
            pie.ShowValues = true;
            pie.ShowPercentages = true;
            plot.Plot.Legend();
            plot.Plot.Title("Соотношение доходов по пользователям.");
            try
            {
                plot.Refresh();
            }
            catch { System.Windows.MessageBox.Show("Нет данных за этот период."); }
        }
    }
}