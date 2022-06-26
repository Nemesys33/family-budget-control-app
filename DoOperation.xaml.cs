using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для DoOperation.xaml
    /// </summary>
    public partial class DoOperation : Window
    {
        private DB db = new DB();
        private string name;
        private int id = 0;
        private decimal perc = 1;
        private decimal cursUSD;
        private decimal cursEUR;
        private decimal balance;
        private decimal balanceCon;
        private decimal balanceCred;
        private string optype = "";

        public DoOperation(string _name, SByte _access, decimal _cursUSD = 69M, decimal _cursEUR = 71M)
        {
            InitializeComponent();
            name = _name;
            cursEUR = _cursEUR;
            cursUSD = _cursUSD;
            if (_access == 0)
            {
                this.Close();
                MessageBox.Show("Недостаточно прав доступа для совершения операции");
            }

            balance = db.getBalance().Item1;
            var tableContr = db.GetContr().Tables[0];
            var rowCon = tableContr.Rows[0].ItemArray;
            balanceCon = Convert.ToDecimal(rowCon[2]);
            dgContr.ItemsSource = tableContr.DefaultView;

            var tableCred = db.GetCred().Tables[0];
            var rowCred = tableCred.Rows[0].ItemArray;
            balanceCred = Convert.ToDecimal(rowCred[2]);
            dgCred.ItemsSource = tableCred.DefaultView;
        }

        private void acceptOp_Click(object sender, RoutedEventArgs e)
        {
            var dt = new DateTime();
            dt = DateTime.Now;

            try
            {
                if (tbsum1.Text != "" && tbprod1.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm1;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur1;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum1.Text) * perc, tbprod1.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 1. Данная операция не выполнена");
                Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum2.Text != "" && tbprod2.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm2;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur2;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum2.Text) * perc, tbprod2.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 2. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum3.Text != "" && tbprod3.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm3;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur3;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum3.Text) * perc, tbprod3.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 3. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum4.Text != "" && tbprod4.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm4;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur4;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum4.Text) * perc, tbprod4.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 4. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum5.Text != "" && tbprod5.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm5;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur5;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum5.Text) * perc, tbprod5.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 5. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum6.Text != "" && tbprod6.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm6;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur6;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum6.Text) * perc, tbprod6.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 6. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum7.Text != "" && tbprod7.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm7;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur7;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum7.Text) * perc, tbprod7.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 7. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum8.Text != "" && tbprod8.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm8;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur8;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum8.Text) * perc, tbprod8.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 8. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum9.Text != "" && tbprod9.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm9;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur9;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum9.Text) * perc, tbprod9.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 9. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum10.Text != "" && tbprod10.Text != "")
                {
                    ComboBox comboBox = (ComboBox)segm10;
                    ComboBoxItem ci = (ComboBoxItem)comboBox.SelectedItem;
                    ComboBox comboBoxC = (ComboBox)cur10;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Расход", Convert.ToDecimal(tbsum10.Text) * perc, tbprod10.Text, dt, name, ci.Content.ToString());
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 10. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }

            this.Close();
        }

        private void cancelOp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void acceptOpR_Click(object sender, RoutedEventArgs e)
        {
            var dt = new DateTime();
            dt = DateTime.Now;
            try
            {
                if (tbsum1R.Text != "" && tbprod1R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur1R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum1R.Text) * perc, tbprod1R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 1. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum2R.Text != "" && tbprod2R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur2R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum2R.Text) * perc, tbprod2R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 2. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum3R.Text != "" && tbprod3R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur3R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum3R.Text) * perc, tbprod3R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 3. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum4R.Text != "" && tbprod4R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur4R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum4R.Text) * perc, tbprod4R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 4. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum5R.Text != "" && tbprod5R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur5R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum5R.Text) * perc, tbprod5R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 5. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum6R.Text != "" && tbprod6R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur6R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum6R.Text) * perc, tbprod6R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 6. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum7R.Text != "" && tbprod7R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur7R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum7R.Text) * perc, tbprod7R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 7. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum8R.Text != "" && tbprod8R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur8R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum8R.Text) * perc, tbprod8R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 8. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum9R.Text != "" && tbprod9R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur9R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum9R.Text) * perc, tbprod9R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 9. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }
            try
            {
                if (tbsum10R.Text != "" && tbprod10R.Text != "")
                {
                    ComboBox comboBoxC = (ComboBox)cur10R;
                    ComboBoxItem cu = (ComboBoxItem)comboBoxC.SelectedItem;
                    string cur = cu.Content.ToString();
                    if (cur == "Рублей")
                        perc = 1;
                    if (cur == "Долларов")
                        perc = cursUSD;
                    if (cur == "Евро")
                        perc = cursEUR;
                    db.createOp("Доход", Convert.ToDecimal(tbsum10R.Text) * perc, tbprod10R.Text, dt, name, "");
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат введенных данных в строке 10. Данная операция не выполнена"); Owner.WindowState = WindowState.Normal;
                Owner.WindowState = WindowState.Maximized;
            }

            this.Close();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            var us = dgContr.SelectedItem as DataRowView;
            string idS = us?.Row.ItemArray[0].ToString();
            int id = Convert.ToInt32(idS);
            ComboBox comboBox = contrType;
            ComboBoxItem type = (ComboBoxItem)comboBox.SelectedItem;
            string typeS = type.Content.ToString();
            if (typeS.Equals("Пополнение"))
            {
                balanceCon += Convert.ToDecimal(ContrSum.Text);

                optype = "Пополнение вклада";
            }
            if (typeS.Equals("Снятие"))
            {
                balanceCon -= Convert.ToDecimal(ContrSum.Text);
                if (balanceCon < 0)
                {
                    MessageBox.Show("Недостаточная сумма на вкладе");
                    return;
                }
                optype = "Снятие со вклада";
            }
            db.UpdateContr(balanceCon, id);
            db.createOp(optype, Convert.ToDecimal(ContrSum.Text), optype, DateTime.Now, name);
            MessageBox.Show("Операция выполнена успешно.");
        }

        private void AcceptCred_Click(object sender, RoutedEventArgs e)
        {
            var us = dgCred.SelectedItem as DataRowView;
            string idS = us?.Row.ItemArray[0].ToString();
            int id = Convert.ToInt32(idS);
            balanceCred -= Convert.ToDecimal(CredSum.Text);
            if (balanceCred < 0)
            {
                MessageBox.Show("Слишком большая сумма погашения");
                return;
            }
            db.UpdateCred(balanceCred, id);
            optype = "Погашение кредита";
            db.createOp(optype, Convert.ToDecimal(CredSum.Text), optype, DateTime.Now, name);
            MessageBox.Show("Операция выполнена успешно.");
        }
    }
}