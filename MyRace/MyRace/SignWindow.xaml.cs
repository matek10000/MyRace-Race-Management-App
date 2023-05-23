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
            okno.Show();
            this.Close();
        }

        private void sign_but_Click(object sender, RoutedEventArgs e)
        {

            if(data_ur.Text == string.Empty)
            {
                MessageBox.Show("Nie mamy pewności, że masz 16 lat..\nUruchom program ponownie!", "Błąd", MessageBoxButton.OK,MessageBoxImage.Stop);
                SignWindow okno = new SignWindow();
                okno.Show();
                this.Close();
            }

            string[] dataur1 = data_ur.Text.Split(".");
            string dataur2 = dataur1[2] + "-" + dataur1[1] + "-" + dataur1[0];
            DateTime dataur3 = new DateTime(Convert.ToInt16(dataur1[2]), Convert.ToInt16(dataur1[1]), Convert.ToInt16(dataur1[0]));

            if (imie.Text == string.Empty || nazwisko.Text == string.Empty || kraj.Text == string.Empty)
            {
                MessageBox.Show("Jedno z Twoich danych zostało wprowadzone nieprawidłowo!", "Błąd", MessageBoxButton.OK);
                SignWindow okno = new SignWindow();
                okno.Show();
                this.Close();
            }
            else if (login.Text.Length > 10 || login.Text == string.Empty)
            {
                MessageBox.Show("Login został wprowadzony nieprawidłowo!", "Błąd", MessageBoxButton.OK);
                SignWindow okno = new SignWindow();
                okno.Show();
                this.Close();
            }
            else if (data_ur.Text == string.Empty || (DateTime.Now.Year - dataur3.Year <= 16))
            {
                MessageBox.Show("Data urodzenia została wprowadzona nieprawidłowo!", "Błąd", MessageBoxButton.OK);
                SignWindow okno = new SignWindow();
                okno.Show();
                this.Close();
            }
            else
            {
                string databaseFileName = "Baza.mdf";
                string currentDirectory = Directory.GetCurrentDirectory();
                string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
                string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

                string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
                SqlConnection connection = new SqlConnection(conn);

                connection.Open();

                SqlCommand cmd1 = new SqlCommand("INSERT INTO Zawodnicy (Imie, Nazwisko, Narodowosc, data_urodzenia, login, haslo) VALUES (@Imie, @Nazwisko, @Narodowosc, @DataUrodzenia, @Login, 'password')", connection);
                cmd1.Parameters.AddWithValue("@Imie", imie.Text);
                cmd1.Parameters.AddWithValue("@Nazwisko", nazwisko.Text);
                cmd1.Parameters.AddWithValue("@Narodowosc", kraj.Text);
                cmd1.Parameters.AddWithValue("@DataUrodzenia", DateTime.ParseExact(dataur2, "yyyy-MM-dd", CultureInfo.InvariantCulture));
                cmd1.Parameters.AddWithValue("@Login", login.Text);
                int a = cmd1.ExecuteNonQuery();

                if (a == 1)
                {
                    MessageBox.Show($"{imie.Text} {nazwisko.Text} został pomyślnie dodany do listy zawodników!");
                    connection.Close();
                    LoginWindow okno = new LoginWindow();
                    okno.Show();
                    this.Close();
                }
                else
                {
                    connection.Close();
                }
            }
        }
    }
}
