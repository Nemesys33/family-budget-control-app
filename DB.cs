using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace VKR_Abrashkov_V_V
{
    internal class DB
    {
        private static string connectionString = "server=localhost;port=3306;username=root;password=12345;database=vkr_abrashkov_v_v";
        private MySqlConnection connection = new MySqlConnection(connectionString);

        public void openCon()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void closeCon()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getCon()
        {
            return connection;
        }

        public (SByte, SByte) getAccess(string name)
        {
            openCon();
            var str = new StringBuilder("-1");
            var table = new DataSet();
            var command = new MySqlCommand("SELECT access, admin FROM vkr_abrashkov_v_v.users WHERE name = @Na;");

            SByte acc = 0;
            SByte adm = 0;
            var adapter = new MySqlDataAdapter();

            command.Parameters.Add("@Na", MySqlDbType.VarChar).Value = name;
            adapter.SelectCommand = command;
            adapter.SelectCommand.Connection = getCon();

            adapter.Fill(table);
            List<SByte> access;
            List<SByte> admin;

            access = table.Tables[0].AsEnumerable()
                .Select(r => r.Field<SByte>("access"))
                .ToList();

            if (access.Count > 0)
            {
                str.Clear();
                foreach (var t in access)
                {
                    acc = t;
                }
            }

            admin = table.Tables[0].AsEnumerable()
                .Select(r => r.Field<SByte>("admin"))
                .ToList();

            if (admin.Count > 0)
            {
                str.Clear();
                foreach (var t in admin)
                {
                    adm = t;
                }
            }

            closeCon();
            return (acc, adm);
        }

        public string getPass(string name)
        {
            openCon();
            var str = new StringBuilder("-1");
            var table = new DataSet();
            var command = new MySqlCommand("SELECT password, access, admin FROM vkr_abrashkov_v_v.users WHERE name = @Na;");
            var adapter = new MySqlDataAdapter();
            List<string> list;

            command.Parameters.Add("@Na", MySqlDbType.VarChar).Value = name;
            adapter.SelectCommand = command;
            adapter.SelectCommand.Connection = getCon();

            adapter.Fill(table);
            List<SByte> access;
            List<SByte> admin;
            list = table.Tables[0].AsEnumerable()
                .Select(r => r.Field<String>("password"))
                .ToList();

            if (list.Count > 0)
            {
                str.Clear();
                foreach (var t in list)
                {
                    str.Append(t);
                }
            }

            access = table.Tables[0].AsEnumerable()
                .Select(r => r.Field<SByte>("access"))
                .ToList();

            if (list.Count > 0)
            {
                str.Clear();
                foreach (var t in list)
                {
                    str.Append(t);
                }
            }

            admin = table.Tables[0].AsEnumerable()
                .Select(r => r.Field<SByte>("admin"))
                .ToList();

            if (list.Count > 0)
            {
                str.Clear();
                foreach (var t in list)
                {
                    str.Append(t);
                }
            }

            closeCon();
            return str.ToString();
        }

        public void createOp(string opType, decimal sum, string prod, DateTime dt, string name, string segm = "")
        {
            openCon();
            var command = new MySqlCommand("INSERT INTO execoperations(name, type, date, amount) VALUES(@name, @type, @date, @sum); ");
            var command2 = new MySqlCommand("SELECT LAST_INSERT_ID();", getCon());
            command.Connection = getCon();
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@type", MySqlDbType.VarChar).Value = opType;
            command.Parameters.Add("@date", MySqlDbType.DateTime).Value = dt;
            command.Parameters.Add("@sum", MySqlDbType.Decimal).Value = sum;

            command.ExecuteNonQuery();

            var reader = command2.ExecuteReader();
            reader.Read();
            int id = reader.GetInt32(0);
            reader.Close();

            if (opType.Equals("Расход"))
                createWithd(id, prod, segm, sum);
            else
            {
                if (opType.Equals("Доход"))
                {
                    createRefill(id, prod, sum);
                }
                else
                {
                    if (opType.Equals("Вклад"))
                    {
                        var command3 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");
                        var balances = getBalance();
                        command3.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                        command3.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = balances.Item1 - sum;
                        command3.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = balances.Item2;
                        command3.Connection = getCon();
                        openCon();
                        command3.ExecuteNonQuery();
                        closeCon();
                    }
                    else
                    {
                        if (opType.Equals("Кредит"))
                        {
                            var command3 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");
                            var balances = getBalance();
                            command3.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                            command3.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = balances.Item1 + sum;
                            command3.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = balances.Item2 + sum;
                            command3.Connection = getCon();
                            openCon();
                            command3.ExecuteNonQuery();
                            closeCon();
                        }
                        else
                        {
                            if (opType.Equals("Пополнение вклада"))
                            {
                                var command3 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");
                                var balances = getBalance();
                                command3.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                                command3.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = balances.Item1 - sum;
                                command3.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = balances.Item2;
                                command3.Connection = getCon();
                                openCon();
                                command3.ExecuteNonQuery();
                                closeCon();
                            }
                            else
                            {
                                if (opType.Equals("Снятие со вклада"))
                                {
                                    var command3 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");
                                    var balances = getBalance();
                                    command3.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                                    command3.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = balances.Item1 + sum;
                                    command3.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = balances.Item2;
                                    command3.Connection = getCon();
                                    openCon();
                                    command3.ExecuteNonQuery();
                                    closeCon();
                                }
                                else
                                {
                                    if (opType.Equals("Погашение кредита"))
                                    {
                                        var command3 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");
                                        var balances = getBalance();
                                        command3.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                                        command3.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = balances.Item1 - sum;
                                        command3.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = balances.Item2 - sum;
                                        command3.Connection = getCon();
                                        openCon();
                                        command3.ExecuteNonQuery();
                                        closeCon();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            closeCon();
        }

        public void UpdateContr(decimal newBal, int id)
        {
            var command = new MySqlCommand("UPDATE contributions SET balance = @balance WHERE idcontributions = @id; ");
            command.Parameters.Add("@balance", MySqlDbType.Decimal).Value = newBal;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Connection = getCon();
            openCon();
            command.ExecuteNonQuery();
            closeCon();
        }

        public void UpdateCred(decimal newBal, int id)
        {
            var command = new MySqlCommand("UPDATE credit SET balance = @balance WHERE idcredit = @id; ");
            command.Parameters.Add("@balance", MySqlDbType.Decimal).Value = newBal;
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Connection = getCon();
            openCon();
            command.ExecuteNonQuery();
            closeCon();
        }

        private void createWithd(int id, string prod, string segm, decimal sum)
        {
            openCon();
            var command = new MySqlCommand("INSERT INTO withdraws(idwithdraw,withdrawprod,segment) VALUES(@id, @prod, @segm); ");
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@prod", MySqlDbType.VarChar).Value = prod;
            command.Parameters.Add("@segm", MySqlDbType.VarChar).Value = segm;
            command.Connection = getCon();
            command.ExecuteNonQuery();

            var command2 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");
            var balances = getBalance();
            command2.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command2.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = balances.Item1 - sum;
            command2.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = balances.Item2 - sum;
            command2.Connection = getCon();
            openCon();
            command2.ExecuteNonQuery();
            closeCon();
        }

        private void createRefill(int id, string source, decimal sum)
        {
            var balances = getBalance();
            var b = balances.Item1 + sum;
            var fb = balances.Item2 + sum;
            if (b < 0)
            {
                MessageBox.Show("Недостаточно средств на счете.");
                return;
            }
            openCon();
            var command = new MySqlCommand("INSERT INTO refills(idrefills,refillsource) VALUES(@id, @source); ");
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@source", MySqlDbType.VarChar).Value = source;
            command.Connection = getCon();
            command.ExecuteNonQuery();

            var command2 = new MySqlCommand("INSERT INTO balance(idbalance,fbalance,fullbalance) VALUES(@id, @fbalance, @fullbalance); ");

            command2.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            command2.Parameters.Add("@fbalance", MySqlDbType.Decimal).Value = b;
            command2.Parameters.Add("@fullbalance", MySqlDbType.Decimal).Value = fb;
            openCon();
            command2.Connection = getCon();
            command2.ExecuteNonQuery();
            closeCon();
        }

        public void Login(string name)
        {
            var dt = DateTime.Now;
            openCon();
            var command = new MySqlCommand("INSERT INTO loginhistory(name,date) VALUES(@name, @date); ");
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@date", MySqlDbType.DateTime).Value = dt;
            command.Connection = getCon();
            command.ExecuteNonQuery();
        }

        public (decimal, decimal) getBalance()
        {
            openCon();
            decimal balance, fullbalance;
            var command = new MySqlCommand("SELECT fbalance,fullbalance FROM vkr_abrashkov_v_v.balance WHERE idbalance = (SELECT MAX(idbalance) FROM balance); ");
            command.Connection = getCon();
            var reader = command.ExecuteReader();
            reader.Read();
            balance = reader.GetDecimal(0);
            fullbalance = reader.GetDecimal(1);
            closeCon();
            return (balance, fullbalance);
        }

        public void adduser(string name, string mail, string password, SByte access, SByte admin)
        {
            openCon();
            var command = new MySqlCommand("INSERT INTO users(name, email, access, admin, password) VALUES(@name, @mail, @access, @admin, @password); ");
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Parameters.Add("@mail", MySqlDbType.VarChar).Value = mail;
            command.Parameters.Add("@access", MySqlDbType.UByte).Value = access;
            command.Parameters.Add("@admin", MySqlDbType.UByte).Value = admin;
            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = password;
            command.Connection = getCon();
            command.ExecuteNonQuery();
            closeCon();
        }

        public void addPyat(string price, string code)
        {
            openCon();
            var command = new MySqlCommand("INSERT INTO pyaterochka(price, codTovara) VALUES(@price, @code); ");
            command.Parameters.Add("@price", MySqlDbType.VarChar).Value = price;
            command.Parameters.Add("@code", MySqlDbType.VarChar).Value = code;
            command.Connection = getCon();
            command.ExecuteNonQuery();
            closeCon();
        }

        public void updPyat(string price, string code)
        {
            openCon();
            var command = new MySqlCommand("Update pyaterochka SET price = @price WHERE codTovara = @code; ");
            command.Parameters.Add("@price", MySqlDbType.VarChar).Value = price;
            command.Parameters.Add("@code", MySqlDbType.VarChar).Value = code;
            command.Connection = getCon();
            command.ExecuteNonQuery();
            closeCon();
        }

        public DataSet GetPyat()
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT * FROM pyaterochka ORDER BY id; ");
            command.Connection = getCon();
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            closeCon();
            return ds;
        }

        public DataSet getBalanceStat(double dt)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT execoperations.date, balance.fbalance, balance.fullbalance FROM vkr_abrashkov_v_v.balance JOIN vkr_abrashkov_v_v.execoperations ON(balance.idbalance = execoperations.idexecoperations) Where date > @span ORDER BY date; ");
            command.Connection = getCon();
            command.Parameters.Add("@span", MySqlDbType.DateTime).Value = DateTime.Now.AddDays(-dt);
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            closeCon();
            return ds;
        }

        public DataSet GetUserRefStat(double dt)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT name, SUM(amount) FROM vkr_abrashkov_v_v.execoperations WHERE type = \"Доход\" AND date > @span GROUP BY name; ");
            command.Connection = getCon();
            command.Parameters.Add("@span", MySqlDbType.DateTime).Value = DateTime.Now.AddDays(-dt);
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            closeCon();
            return ds;
        }

        public DataSet GetUserWithStat(double dt)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT name, SUM(amount) FROM vkr_abrashkov_v_v.execoperations WHERE type = \"Расход\" AND date > @span GROUP BY name; ");
            command.Connection = getCon();
            command.Parameters.Add("@span", MySqlDbType.DateTime).Value = DateTime.Now.AddDays(-dt);
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            closeCon();
            return ds;
        }

        public DataSet GetWithSegmStat(double dt)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT segment, SUM(amount) FROM execoperations JOIN withdraws ON(idwithdraw = idexecoperations) WHERE date > @span GROUP BY segment; ");
            command.Connection = getCon();
            command.Parameters.Add("@span", MySqlDbType.DateTime).Value = DateTime.Now.AddDays(-dt);
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            closeCon();
            return ds;
        }

        public DataSet GetOps(int col = 10000)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT * FROM vkr_abrashkov_v_v.execoperations ORDER BY date DESC LIMIT @num; ");
            command.Connection = getCon();
            command.Parameters.Add("@num", MySqlDbType.Int32).Value = col;
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            var table = ds.Tables[0];
            table.Columns[0].ColumnName = "id";
            table.Columns[1].ColumnName = "Фамилия имя";
            table.Columns[2].ColumnName = "Тип операции";
            table.Columns[3].ColumnName = "Дата";
            table.Columns[4].ColumnName = "Сумма";
            closeCon();
            return ds;
        }

        public DataSet GetContr()
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT * FROM vkr_abrashkov_v_v.contributions ORDER BY idcontributions DESC");
            command.Connection = getCon();
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            var table = ds.Tables[0];
            table.Columns[0].ColumnName = "id";
            table.Columns[1].ColumnName = "Процент";
            table.Columns[2].ColumnName = "Баланс";
            table.Columns[3].ColumnName = "Дата открытия";
            table.Columns[4].ColumnName = "Дата закрытия";
            closeCon();
            return ds;
        }

        public DataSet GetCred()
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT * FROM vkr_abrashkov_v_v.credit");
            command.Connection = getCon();
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            var table = ds.Tables[0];
            table.Columns[0].ColumnName = "id";
            table.Columns[1].ColumnName = "Процент";
            table.Columns[2].ColumnName = "Баланс";
            table.Columns[3].ColumnName = "Дата открытия";
            table.Columns[4].ColumnName = "Дата закрытия";
            table.Columns[5].ColumnName = "Цель кредита";
            closeCon();
            return ds;
        }

        public DataSet GetLogHis(int col)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT * FROM vkr_abrashkov_v_v.loginhistory ORDER BY id DESC LIMIT @num; ");
            command.Connection = getCon();
            command.Parameters.Add("@num", MySqlDbType.Int32).Value = col;
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            var table = ds.Tables[0];
            table.Columns[1].ColumnName = "Фамилия имя";
            table.Columns[2].ColumnName = "Дата входа";
            closeCon();
            return ds;
        }

        public DataSet GetUsers(int col)
        {
            var ds = new DataSet();
            openCon();
            var command = new MySqlCommand("SELECT name FROM users LIMIT @num");
            command.Parameters.Add("@num", MySqlDbType.Int32).Value = col;
            command.Connection = getCon();
            var adapter = new MySqlDataAdapter(command);
            adapter.Fill(ds);
            var table = ds.Tables[0];
            table.Columns[0].ColumnName = "Фамилия имя";
            closeCon();
            return ds;
        }

        public void DelUs(string name)
        {
            openCon();
            var command = new MySqlCommand("DELETE FROM users WHERE name = @name");
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = name;
            command.Connection = getCon();
            command.ExecuteNonQuery();
            closeCon();
        }

        public void NewContribution(double perc, decimal balance, DateTime open, DateTime? term)
        {
            openCon();
            var command = new MySqlCommand("INSERT INTO contributions(percent,balance,opendate, term) VALUES(@perc, @balance, @opendate, @term); ");
            command.Parameters.Add("@perc", MySqlDbType.Double).Value = perc;
            command.Parameters.Add("@balance", MySqlDbType.Decimal).Value = balance;
            command.Parameters.Add("@opendate", MySqlDbType.DateTime).Value = open;
            command.Parameters.Add("@term", MySqlDbType.DateTime).Value = term;
            command.Connection = getCon();
            command.ExecuteNonQuery();
        }

        public void NewCredit(double perc, decimal balance, DateTime open, DateTime? term, string purp)
        {
            openCon();
            var command = new MySqlCommand("INSERT INTO credit(percent,balance,dateopen, dateend, purp) VALUES(@perc, @balance, @opendate, @term, @purp); ");
            command.Parameters.Add("@perc", MySqlDbType.Double).Value = perc;
            command.Parameters.Add("@balance", MySqlDbType.Decimal).Value = balance;
            command.Parameters.Add("@opendate", MySqlDbType.DateTime).Value = open;
            command.Parameters.Add("@term", MySqlDbType.DateTime).Value = term;
            command.Parameters.Add("@purp", MySqlDbType.VarChar).Value = purp;
            command.Connection = getCon();
            command.ExecuteNonQuery();
        }
    }
}