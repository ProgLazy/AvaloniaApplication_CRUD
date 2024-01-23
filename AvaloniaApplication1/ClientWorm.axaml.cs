using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Script_C_;
using MySql.Data.MySqlClient;

namespace AvaloniaApplication1;

public partial class ClientWorm : Window
{
    private List<Scripts> _script;
    private List<Svoystva_vesh> _svoystvaVeshes;
    private List<monitor> _monitors;
    private List<stream_vbr> _vbrs;
    private string _connString = "server=localhost;Port=3301;Database=metrology;UserID=root;password=Ghost45)";
    private MySqlConnection _sqlConnection;
    public ClientWorm()
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
                pdk_rab= reader.GetInt32("pdk_v_rab"),
                pdk_poch = reader.GetString("pdk_poch"),
                cl_opasnos = reader.GetInt32("class_dang"),
                plots= reader.GetString("plot"),
                temperature= reader.GetString("temper")
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

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }
}