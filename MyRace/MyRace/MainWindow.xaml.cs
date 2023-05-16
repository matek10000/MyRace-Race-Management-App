using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string databaseFileName = "Baza.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);
            // Łączenie się z bazą dla tabeli "Ostatnie wyścigi"

            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select Nazwa as 'Nazwa wyścigu', Data as 'Data wyścigu' from wydarzenia order by data",connection);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable("Wydarzenia");
            adapter.Fill(dt);
            najblizszew.ItemsSource = dt.DefaultView;
            adapter.Update(dt);
            connection.Close();


        }

        // Łączenie się z bazą po kliknięciu "Informacje o zawodnikach"

        private void zawodnicy_but_Click(object sender, RoutedEventArgs e)
        {
            string databaseFileName = "Baza.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select imie as Imię, nazwisko as Nazwisko ,narodowosc as Narodowość,data_urodzenia as 'Data urodzenia' from zawodnicy order by IDzawodnik DESC", connection);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable("Zawodnicy");
            adapter.Fill(dt);
            infogrid.ItemsSource = dt.DefaultView;
            adapter.Update(dt);
            connection.Close();
        }

        // Łączenie się z bazą po kliknięciu "Informacje o zespołach"

        private void zespoly_but_Click(object sender, RoutedEventArgs e)
        {
            string databaseFileName = "Baza.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select nazwa as 'Nazwa zespołu' ,kraj as Narodowość from zespoly", connection);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable("Zespoly");
            adapter.Fill(dt);
            infogrid.ItemsSource = dt.DefaultView;
            adapter.Update(dt);
            connection.Close();
        }

        // Łączenie z bazą po kliknięciu "Informacje o sponsorach"

        private void sponsorzy_but_Click(object sender, RoutedEventArgs e)
        {
            string databaseFileName = "Baza.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select nazwa as Nazwa,branza as Branża from sponsorzy", connection);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable("Sponsorzy");
            adapter.Fill(dt);
            infogrid.ItemsSource = dt.DefaultView;
            adapter.Update(dt);
            connection.Close();
        }

        // Przeniesienie na nowe okno "Zapisanie się"
        private void signin_but_Click(object sender, RoutedEventArgs e)
        {
            SignWindow okno = new SignWindow();
            okno.Show();
            this.Close();
        }

        private void login_but_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow okno = new LoginWindow();
            okno.Show();
            this.Close();
        }
    }
}
