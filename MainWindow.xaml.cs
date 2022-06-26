using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace VKR_Abrashkov_V_V
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isAuthorized = true;
        public string authName = "Абрашков Вадим";
        private SByte access = 0;
        private SByte isAdmin = 0;
        private decimal cursUSD = 69;
        private decimal cursEUR = 71;
        private DB db = new DB();
        private MyParser parser = new MyParser();
        private bool internet = true;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                cursUSD = Convert.ToDecimal(parser.ParseCur("RY0101095000839420"));
                cursEUR = Convert.ToDecimal(parser.ParseCur("RY0101095000946869"));
            }
            catch
            {
                MessageBox.Show("Отсутствует интернет соединение.");
                internet = false;
            }

            var acc = db.getAccess(authName); //
            access = acc.Item1;               // Убрать
            isAdmin = acc.Item2;              //
            try
            {
                tb2.Text = parser.getAllCur(internet);
            }
            catch { MessageBox.Show("Неудалось получить курс валют. Отсутствует соединение с интернетом."); }
            Loaded += Authorize;
            Loaded += setBalance;
            Loaded += refresh_Click;
        }

        private void setBalance(object sender, RoutedEventArgs e)
        {
            var balances = db.getBalance();
            tbb2.Text = balances.Item1.ToString();
            tbb4.Text = balances.Item2.ToString();
        }

        private void setBalance()
        {
            var balances = db.getBalance();
            tbb2.Text = balances.Item1.ToString();
            tbb4.Text = balances.Item2.ToString();
        }

        private void setDgOps()
        {
            dgOps.ItemsSource = db.GetOps(100).Tables[0].DefaultView;
        }

        private void setUs()
        {
            dgus.ItemsSource = db.GetUsers(100).Tables[0].DefaultView;
        }

        private void setDgLogHis()
        {
            dgLogHis.ItemsSource = db.GetLogHis(100).Tables[0].DefaultView;
        }

        private void Authorize(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                var authorization = new Authorization(isAuthorized);
                authorization.Owner = this;
                var t = authorization.Auth();
                if (t.Item1)
                {
                    isAuthorized = true;
                    if (t.Item2 != "")
                        authName = t.Item2;
                    privet.Text = string.Format("Добрый день, {0}.", authName);
                    break;
                }
            }
            var acc = db.getAccess(authName);
            access = acc.Item1;
            isAdmin = acc.Item2;

            db.Login(authName);
        }

        private void doOp_Click(object sender, RoutedEventArgs e)
        {
            var operation = new DoOperation(authName, access, cursUSD, cursEUR);
            operation.Owner = this;
            try
            {
                operation.Show();
            }
            catch { }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            setBalance();
            setUs();
            setDgOps();
            setDgLogHis();
        }

        private void adUs_Click(object sender, RoutedEventArgs e)
        {
            var addUS = new AddUser();
            addUS.Owner = this;
            addUS.Show();
        }

        private void BalStat_Click(object sender, RoutedEventArgs e)
        {
            var bs = new BalanceStat();
            bs.Owner = this;
            bs.Show();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            setDgOps();
            setDgLogHis();
            tb2.Text = parser.getAllCur(internet);
        }

        private void URS_Click(object sender, RoutedEventArgs e)
        {
            var urs = new UserRefStat();
            urs.Owner = this;
            urs.Show();
        }

        private void UWS_Click(object sender, RoutedEventArgs e)
        {
            var uws = new UserWithStat();
            uws.Owner = this;
            uws.Show();
        }

        private void WithSegm_Click(object sender, RoutedEventArgs e)
        {
            var wss = new WithSegmStat();
            wss.Owner = this;
            wss.Show();
        }

        private void delUs_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin == 0)
            {
                MessageBox.Show($"Недостатчно прав дл явыполнения данной операции.");
                return;
            }
            var us = dgus.SelectedItem as DataRowView;
            string username = us?.Row.ItemArray[0].ToString();
            if (username == authName)
            {
                MessageBox.Show($"Нельзя выбирать себя.");
                return;
            }
            db.DelUs(username);
            setUs();
            if (!string.IsNullOrEmpty(username))
                MessageBox.Show($"Пользователь {username} успешно удален.");
            else MessageBox.Show($"Пользователь не был выбран.");
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            var fe = new FileExecutor();
            fe.Export();
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            var fe = new FileExecutor();
            fe.Import();
        }

        private void products_Click(object sender, RoutedEventArgs e)
        {
            var prices = new Prices();
            prices.Owner = this;
            prices.Show();
        }

        private void renew_Click(object sender, RoutedEventArgs e)
        {
            if (internet)
                parser.addPrice();
            else MessageBox.Show("Отсутствует интернет соединение.");
        }

        private void mkContr_Click(object sender, RoutedEventArgs e)
        {
            var mkCon = new MakeContr(authName);
            mkCon.Owner = this;
            mkCon.Show();
        }

        private void mkCredit_Click(object sender, RoutedEventArgs e)
        {
            var mkCre = new MakeCred(authName);
            mkCre.Owner = this;
            mkCre.Show();
        }
    }
}