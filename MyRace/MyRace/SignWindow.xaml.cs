using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MyRace
{
    /// <summary>
    /// Logika interakcji dla klasy SignWindow.xaml
    /// </summary>
    public partial class SignWindow : Window
    {
        public SignWindow()
        {
            InitializeComponent();
        }

        private void back_but_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            this.Visibility = Visibility.Hidden;
            okno.Show();
        }

        private void sign_but_Click(object sender, RoutedEventArgs e)
        {
            string databaseFileName = "Baza.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();

            string[] dataur1 = data_ur.Text.Split(".");
            string dataur2 = dataur1[2] + "-" + dataur1[1] + "-" + dataur1[0];

            SqlCommand cmd1 = new SqlCommand("INSERT INTO Zawodnicy (Imie, Nazwisko, Narodowosc, data_urodzenia, login, haslo) VALUES (@Imie, @Nazwisko, @Narodowosc, @DataUrodzenia, @Login, 'password')", connection);
            cmd1.Parameters.AddWithValue("@Imie", imie.Text);
            cmd1.Parameters.AddWithValue("@Nazwisko", nazwisko.Text);
            cmd1.Parameters.AddWithValue("@Narodowosc", kraj.Text);
            cmd1.Parameters.AddWithValue("@DataUrodzenia", DateTime.ParseExact(dataur2, "yyyy-MM-dd", CultureInfo.InvariantCulture));
            cmd1.Parameters.AddWithValue("@Login", login.Text);
            int a = cmd1.ExecuteNonQuery();

            if (a == 1)
            {
                MessageBox.Show($"Zawodnik {imie.Text} {nazwisko.Text} został pomyślnie dodany do listy zawodników!");
                LoginWindow okno = new LoginWindow();
                this.Visibility = Visibility.Hidden;
                okno.Show();
            }
        }
    }
}
