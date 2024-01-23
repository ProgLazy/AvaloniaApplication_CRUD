using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Script_C_;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ExcelDataReader;
using Org.BouncyCastle.Utilities.Net;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using IPAddress = Org.BouncyCastle.Utilities.Net.IPAddress;


namespace AvaloniaApplication1;

public partial class AdminWorm : Window
{
    private List<seeding> _seedings;
    private List<Scripts> _script;
    private List<Svoystva_vesh> _svoystvaVeshes;
    private List<monitor> _monitors;
    private List<stream_vbr> _vbrs;
    private string _connString = "server=localhost;Port=3301;Database=metrology;UserID=root;password=Ghost45)";
    private MySqlConnection _sqlConnection;
    private List<filter_Meteo> _filterMeteos;
    string[,] list = new string[50, 5];

    public AdminWorm()
    {
        InitializeComponent();
        string sql = "SELECT * FROM meteo_climat";
        string sql2 = "SELECT * FROM svo_vesh";
        string sql3 = "SELECT * FROM monitoring";
        string sql4 = "SELECT * FROM stream_vbros";
        Tables1(sql);
        Tables2(sql2);
        Tables3(sql3);
        Tables4(sql4);
        Tables5();
        FilterUser();
        ProcessClientAsync();
    }


    private void Tables1(string sql)
    {
        _script = new List<Scripts>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentmeteo = new Scripts()
            {
                id = reader.GetInt32("id"),
                stancia = reader.GetInt32("stan_con"),
                data_vre = reader.GetDateTime("data_vrem"),
                temperature = reader.GetString("temper"),
                vlash_H2 = reader.GetString("vlazh_v"),
                sp_vetra = reader.GetString("sp_veter"),
                napravlenie = reader.GetString("napravlenie"),
                atmos_dav = reader.GetString("atmos_davle"),
                oblach = reader.GetString("oblach"),
                nalichie_osad = reader.GetString("nal_osad")
            };
            _script.Add(currentmeteo);
        }

        _sqlConnection.Close();
        Grid1.ItemsSource = _script;
    }

    private void Tables2(string sql)
    {
        _svoystvaVeshes = new List<Svoystva_vesh>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new Svoystva_vesh()
            {
                id1 = reader.GetInt32("id"),
                zagryz_ve = reader.GetInt32("zagryz_vesh"),
                average_sutoch = reader.GetString("sred_sut"),
                pdk_m = reader.GetInt32("pdk_m_r"),
                pdk_rab = reader.GetInt32("pdk_v_rab"),
                pdk_poch = reader.GetString("pdk_poch"),
                cl_opasnos = reader.GetInt32("class_dang"),
                plots = reader.GetString("plot"),
                temperature = reader.GetString("temper")
            };
            _svoystvaVeshes.Add(current);
        }

        _sqlConnection.Close();
        Grid2.ItemsSource = _svoystvaVeshes;
    }

    private void Tables3(string sql)
    {
        _monitors = new List<monitor>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new monitor()
            {
                id2 = reader.GetInt32("id"),
                stanc = reader.GetInt32("stancia"),
                gryaz = reader.GetInt32("zagryaz"),
                data_pr = reader.GetDateTime("data_prob"),
                value_con = reader.GetInt32("value_conc")
            };
            _monitors.Add(current);
        }

        _sqlConnection.Close();
        Grid3.ItemsSource = _monitors;
    }

    private void Tables4(string sql)
    {
        _vbrs = new List<stream_vbr>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new stream_vbr()
            {
                id3 = reader.GetInt32("id"),
                name_st = reader.GetString("name_str"),
                type_st = reader.GetString("type_str"),
                zagryaz_d = reader.GetInt32("zagryz_id"),
                ustanov = reader.GetInt32("ustanovka"),
                techno_proc = reader.GetInt32("tehno_process"),
                valov_mos = reader.GetString("valov_mosh"),
                temp_v = reader.GetInt32("temper_v"),
                sp_v = reader.GetInt32("speed_v"),
                ob_ras = reader.GetString("ob_rash"),
                dolya_ves = reader.GetString("dolya_vesh")
            };
            _vbrs.Add(current);
        }

        _sqlConnection.Close();
        Grid5.ItemsSource = _vbrs;
    }


    private void AddOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            MySqlCommand command =
                new MySqlCommand(
                    $"Insert into meteo_climat  (stan_con, data_vrem, temper, vlazh_v, sp_veter, napravlenie, atmos_davle, oblach, nal_osad) Values ('" +
                    t1.Text + "', '" + t2.Text + "', '" + t3.Text + "', '" + t4.Text + "', '" + t5.Text + "', '" +
                    t6.Text + "', '" + t7.Text + "', '" + t8.Text + "', '" + t9.Text + "')", _sqlConnection);

            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text = exception.Message);
        }
    }

    private void SaveOnClick(object? sender, RoutedEventArgs e)
    {
        _script = new List<Scripts>();
        string sql = "SELECT * FROM meteo_climat";
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentmeteo = new Scripts()
            {
                id = reader.GetInt32("id"),
                stancia = reader.GetInt32("stan_con"),
                data_vre = reader.GetDateTime("data_vrem"),
                temperature = reader.GetString("temper"),
                vlash_H2 = reader.GetString("vlazh_v"),
                sp_vetra = reader.GetString("sp_veter"),
                napravlenie = reader.GetString("napravlenie"),
                atmos_dav = reader.GetString("atmos_davle"),
                oblach = reader.GetString("oblach"),
                nalichie_osad = reader.GetString("nal_osad")
            };
            _script.Add(currentmeteo);
        }

        _sqlConnection.Close();
        Grid1.ItemsSource = _script;
    }

    private void DeleteOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            string QeuryString = $"delete from meteo_climat where ID = {ID_TextBox.Text}";
            MySqlCommand command = new MySqlCommand(QeuryString, _sqlConnection);
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception)
        {

            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text);
        }
    }

    private void UpdateOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            string QueryString = $"update meteo_climat set stan_con = '" + t1.Text + "', data_vrem = '" + t2.Text +
                                 "', temper = '" + t3.Text + "', vlazh_v = '" + t4.Text + "', sp_veter = '" + t5.Text +
                                 "', napravlenie = '" + t6.Text + "', atmos_davle = '" + t7.Text + "', oblach = '" +
                                 t8.Text + "', nal_osad = '" + t9.Text + "' where ID = '" + ID_TextBox.Text + "'";
            MySqlCommand command = new MySqlCommand(QueryString, _sqlConnection);
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception)
        {

            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text);
        }
    }

    private void ExcelOnClick(object? sender, RoutedEventArgs e)
    {
        string FileName = "asd";

        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Лист1");
        worksheet.Cell("A" + 1).Value = "Column1";
        worksheet.Cell("B" + 1).Value = "Column2";
        worksheet.Cell("C" + 1).Value = "Column3";
        worksheet.Cell("D" + 1).Value = "Column4";
        worksheet.Cell("E" + 1).Value = "Column5";
        worksheet.Cell("F" + 1).Value = "Column6";
        worksheet.Cell("G" + 1).Value = "Column7";
        worksheet.Cell("H" + 1).Value = "Column8";
        worksheet.Cell("I" + 1).Value = "Column9";
        worksheet.Cell("J" + 1).Value = "Column10";
        int row = 2;

        foreach (Scripts data in _script)
        {
            worksheet.Cell("A" + row).Value = data.id;
            worksheet.Cell("B" + row).Value = data.stancia;
            worksheet.Cell("C" + row).Value = data.data_vre;
            worksheet.Cell("D" + row).Value = data.temperature;
            worksheet.Cell("E" + row).Value = data.vlash_H2;
            worksheet.Cell("F" + row).Value = data.sp_vetra;
            worksheet.Cell("G" + row).Value = data.napravlenie;
            worksheet.Cell("H" + row).Value = data.atmos_dav;
            worksheet.Cell("I" + row).Value = data.oblach;
            worksheet.Cell("J" + row).Value = data.nalichie_osad;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(@".\Output\" + FileName + ".xlsx");
    }


    private void FilterUser()
    {
        _filterMeteos = new List<filter_Meteo>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand("SELECT id, napravlenie FROM meteo_climat", _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new filter_Meteo()
            {
                id4 = reader.GetInt32("id"),
                napravl = reader.GetString("napravlenie")
            };
            _filterMeteos.Add(current);
        }

        _sqlConnection.Close();
        var comboBox = this.FindControl<ComboBox>("Box");
        comboBox.ItemsSource = _filterMeteos;
    }

    private void Search_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string sqlsearch = "select * from meteo_climat WHERE temper LIKE '%" + Search.Text +
                           "%' OR napravlenie LIKE '%" + Search.Text + "%'";
        Tables1(sqlsearch);
    }

    private void Box_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var current = comboBox.SelectedItem as filter_Meteo;
        var filter = _script
            .Where(x => x.id == current.id4)
            .ToList();
        Grid1.ItemsSource = filter;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    private void Button_Filter(object? sender, RoutedEventArgs e)
    {
        string sqlFilter = "select * from meteo_climat WHERE temper LIKE '+12' OR napravlenie LIKE 'Sever'";
        Tables1(sqlFilter);
    }
    //Публичный класс передаем значения
    public class UserRandom
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double Wallet { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActiveSubscriber { get; set; }
        public string Email { get; set; }

        //Генерация пользователей
        public static List<UserRandom> GenerateUsers(int count)
        {
            Random random = new Random();

            return Enumerable.Range(1, count).Select(_ => new UserRandom
            {
                Age = random.Next(1, 101),
                Wallet = random.Next(100, 1000001) * 1000,
                Email = GenerateEmail(),
                RegistrationDate = GenerateRandomRegistrationDate(),
                IsActiveSubscriber = random.Next(2) == 0
            }).ToList();
        }

        //Генерация почты
        private static string GenerateEmail()
        {
            Random random = new Random();
            string[] domains = { "gmail.com", "yahoo.com", "example.com" };
            return $"user{random.Next(1000)}@{domains[random.Next(domains.Length)]}";
        }

        //Генерация рандомной даты
        private static DateTime GenerateRandomRegistrationDate()
        {
            Random random = new Random();
            int range = (DateTime.Today - new DateTime(2010, 1, 1)).Days;
            return new DateTime(2010, 1, 1).AddDays(random.Next(range));
        }
    }
    
    //Нажатие на клавишу и обработка запросов с выводом результатов
    private void CreateClick(object? sender, RoutedEventArgs e)
    {
        List<UserRandom> users = UserRandom.GenerateUsers(10000);

        // LINQ запросы
        var richUsers = users.Where(u => u.Wallet > 100000);
        var youngRichUsers = users.Where(u => u.Age >= 18 && u.Age <= 25 && u.Wallet > 125000);
        var olderUsers = users.Where(u => u.Age > 50 && u.RegistrationDate >= new DateTime(2018, 1, 1) && u.RegistrationDate <= new DateTime(2023, 1, 1));
        var gmailUsers = users.Where(u => u.Email.EndsWith("@gmail.com") && u.Wallet > 50000 && u.IsActiveSubscriber);
        var youngYahooUsers = users.Where(u => u.Email.EndsWith("@yahoo.com") && u.Age == 18 && u.Wallet < 25000);
        var centenarianOnlineUsers = users.Where(u => u.Age > 100 && u.IsActiveSubscriber);
        var earlyRegistrantsRichUsers = users.OrderBy(u => u.RegistrationDate).Take(50).Where(u => u.Wallet > 100000);
        var birthdayCelebrants = users.Where(u => u.RegistrationDate.Month == DateTime.Now.Month && u.RegistrationDate.Day == DateTime.Now.Day && u.Age > 21);
        var topSpendingSubscribers = users.OrderByDescending(u => u.Wallet).Take(10).Where(u => u.IsActiveSubscriber && u.Wallet > 400000 && u.Age > 25).OrderBy(u => u.RegistrationDate);

        // Вывод результатов
        text1.Text = ($"Количество пользователей: {users.Count}"+
                      $"\nКоличество богатых пользователей (>100000 руб.): {richUsers.Count()}"+
                      $"\nКоличество пользователей (18-25 лет, >125000 руб.): {youngRichUsers.Count()}"+
                      $"\nКоличество пользователей чей возраст больше 50, дата регистрации(или последней активности) варьируется от 2018.1.1 до 2023.1.1: {olderUsers.Count()}"+
                      $"\nКоличество пользователей gmail, чей кошелек больше 50000 тыс. руб, с активной подпиской: {gmailUsers.Count()}"+
                      $"\nКоличество пользователей yaho 18 лет, с кошельком менее 25000 руб.: {youngYahooUsers.Count()}"+ 
                      $"\nКоличество пользователей старше 100 лет, кто был онлайн сегодня: {centenarianOnlineUsers.Count()}"+
                      $"\n50 пользователей которые были зарегистрированы с самого начала создания сервиса (первая дата в генерации), чей кошелек больше 100000 руб.: {earlyRegistrantsRichUsers.Count()}" +
                      $"\nКоличество пользователей у кого сегодня день рождения и кому больше 21 года: {birthdayCelebrants.Count()}" + 
                      $"\nТоп 10 пользователей с активной подпиской, кто вообщем потратил более 400000 руб., старше 25 лет, с самой ранней датой регистрации (для десяти человек): {topSpendingSubscribers.Count()}");
    }


    public void Tables5()
    {
        _seedings = new List<seeding>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        string sql = "SELECT * FROM seed_user";
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            seeding current = new seeding()
            {
                id9 = reader.GetInt32("id"),
                log = reader.GetString("Login"),
                pass = reader.GetString("Password")
            };
            _seedings.Add(current);
        }

        Grid6.ItemsSource = _seedings;
        _sqlConnection.Close();
    }
    private void Clean_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            MySqlCommand command = 
                new MySqlCommand($"truncate table seed_user", _sqlConnection);
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text = exception.Message);
        }
    }

    private void Generate_OnClick(object? sender, RoutedEventArgs e)
    {
        int first = 0;
        int last = 10;
        try
        {
            while (first<last)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);
                first++;
                _sqlConnection.Open();
                MySqlCommand command = 
                    new MySqlCommand($"Insert into seed_user  (Login, Password) Values ('{finalString}','{finalString}')", _sqlConnection);

                command.ExecuteNonQuery();
                _sqlConnection.Close();    
            }
            
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text = exception.Message);
        }
    }

    private void Update_OnClick(object? sender, RoutedEventArgs e)
    {
        _seedings = new List<seeding>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        string sql = "SELECT * FROM seed_user";
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            seeding current = new seeding()
            {
                id9 = reader.GetInt32("id"),
                log = reader.GetString("Login"),
                pass = reader.GetString("Password")
            };
            _seedings.Add(current);
        }

        Grid6.ItemsSource = _seedings;
        _sqlConnection.Close();
    }

    private void ProcessClientAsync()
    {
         // Устанавливаем для сокета локальную конечную точку
            IPHostEntry ipHost = Dns.GetHostEntry("localhost");
            System.Net.IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 11000);

            // Создаем сокет Tcp/Ip
            Socket sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Назначаем сокет локальной конечной точке и слушаем входящие сокеты
            try
            {
                sListener.Bind(ipEndPoint);
                sListener.Listen(10);

                // Начинаем слушать соединения
                while (true)
                {
                    Console.WriteLine("Ожидаем соединение через порт {0}", ipEndPoint);

                    // Программа приостанавливается, ожидая входящее соединение
                    Socket handler = sListener.Accept();
                    string data = null;

                    // Мы дождались клиента, пытающегося с нами соединиться
                    
                    byte[] bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    
                    // Показываем данные на консоли
                    Console.Write("Полученный текст: " + data + "\n\n");
                    
                    // Отправляем ответ клиенту\
                    string reply = "Спасибо за запрос в " + data.Length.ToString()
                            + " символов";
                    byte[] msg = Encoding.UTF8.GetBytes(reply);
                    handler.Send(msg);

                    if (data.IndexOf("<TheEnd>") > -1)
                    {
                        Console.WriteLine("Сервер завершил соединение с клиентом.");
                        break;
                    }
                    
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
    }

    private void ReadXML()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var stringpasword = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
            stringpasword[i] = chars[random.Next(chars.Length)];
        }
        var finalString = new String(stringChars);
        var passwords1 = new String(stringpasword);
        var z = new seeding { log = $"{finalString}", pass = $"{passwords1}"};
        var writer = new System.Xml.Serialization.XmlSerializer(typeof(seeding));
        var wfile = new System.IO.StreamWriter(@"./Output/serializationOverview.xml");
        writer.Serialize(wfile,z);
        wfile.Close();

    }

    private void OpenXml()
    {
        System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(seeding));
        System.IO.StreamReader file = new System.IO.StreamReader(@"./Output/serializationOverview.xml");
        seeding overview = (seeding)reader.Deserialize(file);
        file.Close();
        
        text4.Text = $"{(overview.log,overview.pass)})";
    }


    private void Xml_OnClick(object? sender, RoutedEventArgs e)
    {
        ReadXML();
    }

    private void Xml_Open_OnClick(object? sender, RoutedEventArgs e)
    {
        OpenXml();
    }
}