using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для BalanceStat.xaml
    /// </summary>
    public partial class BalanceStat : Window
    {
        public BalanceStat()
        {
            InitializeComponent();
            doStat();
        }

        private void days_TextChanged(object sender, TextChangedEventArgs e)
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
            var table = db.getBalanceStat(Convert.ToDouble(days.Text)).Tables[0];
            table.Columns[0].ColumnName = "Дата";
            table.Columns[1].ColumnName = "Баланс";
            table.Columns[2].ColumnName = "Общий баланс";
            dgbalance.ItemsSource = table.DefaultView;

            var balList = table.AsEnumerable()
                .Select(r => r.Field<Decimal>("Баланс"))
                .ToList();

            List<double> vs = new List<double>();
            foreach (var item in balList)
            {
                vs.Add(Convert.ToDouble(item));
            }

            double[] values2 = vs.ToArray();
            double[] values = new double[values2.Length];
            values[0] = values2[0];
            double sum = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                values[i] = values2[i] - sum;
                sum += values[i];
            }
            double[] offsets = Enumerable.Range(0, values.Length).Select(x => values.Take(x).Sum()).ToArray();

            var dateList = table.AsEnumerable()
                .Select(r => r.Field<DateTime>("Дата"))
                .ToList();

            double[] positions = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                positions[i] = i * 0.1;
            }
            string[] labels = new string[vs.Count];
            for (int i = 0; i < vs.Count; i++)
            {
                labels[i] = dateList[i].ToString("d");
            }
            var bar = plot.Plot.AddBar(values, positions);
            bar.ValueOffsets = offsets;
            bar.FillColorNegative = System.Drawing.Color.Red;
            bar.FillColor = System.Drawing.Color.Green;
            bar.ShowValuesAboveBars = true;
            bar.BarWidth = 0.1;
            plot.Plot.XTicks(positions, labels);
            plot.Plot.SetAxisLimits(yMin: 0, xMin: 0);
            plot.Plot.XLabel("Даты изменения");
            plot.Plot.XAxis.TickLabelStyle(rotation: 90);
            plot.Plot.YLabel("Рублей");
            plot.Plot.XAxis.ManualTickSpacing(1);
            plot.Plot.Title("История изменения баланса.");
            try
            {
                plot.Refresh();
            }
            catch { System.Windows.MessageBox.Show("Нет данных за этот период."); }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            var path = new StringBuilder("");
            var folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path.Append(folderBrowserDialog1.SelectedPath).Append("\\BalanceStat.png");
            }
            System.Windows.MessageBox.Show($"Файл сохранен. Полный путь: {path}");
            try
            {
                plot.Plot.SaveFig(path.ToString());
            }
            catch { System.Windows.MessageBox.Show("Сохранение отменено."); }
        }
    }
}