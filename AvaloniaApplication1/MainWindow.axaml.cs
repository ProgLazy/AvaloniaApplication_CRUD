using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AvaloniaApplication1;

public partial class MainWindow : Window
{
    public class avtor
    {
        public string logins { get; set; }
        public string passwords { get; set; }
    }
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        string login = TextBox.Text;
        string password = TextBox1.Text;

        if (File.ReadAllText(path: @"./Output/avtor.json") != null && login == "admin")
        {
            AdminWorm admin = new AdminWorm();
            admin.Show();
            this.Close();
        }
        
        // Проверка логина и пароля
        else if (login == "admin" && password == "admin")
        {
                AdminWorm admin = new AdminWorm();
                admin.Show();
                this.Close();
        }
        
            // Проверка логина и пароля 
        else if (login == "client" && password == "client")
        {
                ClientWorm client = new ClientWorm();
                client.Show();
                this.Close();
        }

        avtor avtor = new avtor
        {
            logins = login,
            passwords = password
        };
        string jsonavtor = JsonConvert.SerializeObject(avtor, Formatting.Indented);
        
        File.WriteAllText(@"./Output/avtor.json",jsonavtor);
    }
}