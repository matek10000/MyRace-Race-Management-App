using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace MyRace
{
    /// <summary>
    /// Logika interakcji dla klasy PanelWindow.xaml
    /// </summary>
    public partial class PanelWindow : Window
    {
        public PanelWindow(string nickname)
        {
            InitializeComponent();
            welcome_text.Content = $"Witaj, {nickname}!";
            helper.Content = nickname;
            helper.Visibility = Visibility.Hidden;
            new_password.Visibility = Visibility.Hidden;
            new_password_text.Visibility = Visibility.Hidden;
            new_password_but.Visibility = Visibility.Hidden;

        }

        private void change_password_but_Click(object sender, RoutedEventArgs e)
        {
            new_password.Visibility = Visibility.Visible;
            new_password_text.Visibility = Visibility.Visible;
            new_password_but.Visibility = Visibility.Visible;
            change_password_but.IsEnabled = false;
        }

        private void new_password_but_Click(object sender, RoutedEventArgs e)
        {
            if (new_password.Text.Length > 10)
            {
                MessageBox.Show("Maksymalna długość hasła wynosi: 10 znaków!", "Błąd", MessageBoxButton.OK);
            }
            else if (new_password.Text == "password")
            {
                MessageBox.Show("Nie możesz ustawić domyślnego hasła!", "Błąd", MessageBoxButton.OK);
            }
            else if (new_password.Text.Length < 3)
            {
                MessageBox.Show("Minimalna długość hasła wynosi: 3 znaki!", "Błąd", MessageBoxButton.OK);
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

                SqlCommand cmd1 = new SqlCommand($"UPDATE Zawodnicy SET haslo = @Haslo WHERE login = @Login", connection);
                cmd1.Parameters.AddWithValue("@Haslo", new_password.Text);
                cmd1.Parameters.AddWithValue("@Login", helper.Content);

                int a = cmd1.ExecuteNonQuery();

                if (a == 1)
                {
                    MessageBox.Show($"Hasło zostało zmienione pomyślnie!\nZaloguj się ponownie.","Sukces!",MessageBoxButton.OK,MessageBoxImage.Information);
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
