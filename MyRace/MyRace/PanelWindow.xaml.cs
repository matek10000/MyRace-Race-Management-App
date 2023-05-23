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
            //xxxxxxxxxxxxxx
            new_password.Visibility = Visibility.Hidden;
            new_password_text.Visibility = Visibility.Hidden;
            new_password_but.Visibility = Visibility.Hidden;
            //xxxxxxxxxxxxxx
            car_title.Visibility = Visibility.Hidden;
            car_text1.Visibility = Visibility.Hidden;
            car_text2.Visibility = Visibility.Hidden;
            car_text3.Visibility = Visibility.Hidden;
            car_text4.Visibility = Visibility.Hidden;
            input1.Visibility = Visibility.Hidden;
            input2.Visibility = Visibility.Hidden;
            input3.Visibility = Visibility.Hidden;
            input4.Visibility = Visibility.Hidden;
            car_add_but.Visibility = Visibility.Hidden;
            //xxxxxxxxxxxxxx
            team_text1.Visibility = Visibility.Hidden;
            team_text2.Visibility = Visibility.Hidden;
            team_add_but.Visibility = Visibility.Hidden;
            team_title.Visibility = Visibility.Hidden;
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

        private void logout_but_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Pomyślnie wylogowano!", "Sukces!", MessageBoxButton.OK,MessageBoxImage.Information);
            LoginWindow okno = new LoginWindow();
            okno.Show();
            this.Close();
        }

        private void car_but_Click(object sender, RoutedEventArgs e)
        {
            //xxxxxxxxxxxxxx
            car_title.Visibility = Visibility.Visible;
            car_text1.Visibility = Visibility.Visible;
            car_text2.Visibility = Visibility.Visible;
            car_text3.Visibility = Visibility.Visible;
            car_text4.Visibility = Visibility.Visible;
            input1.Visibility = Visibility.Visible;
            input2.Visibility = Visibility.Visible;
            input3.Visibility = Visibility.Visible;
            input4.Visibility = Visibility.Visible;
            car_add_but.Visibility = Visibility.Visible;
            //xxxxxxxxxxxxxx
            team_text1.Visibility = Visibility.Hidden;
            team_text2.Visibility = Visibility.Hidden;
            team_add_but.Visibility = Visibility.Hidden;
            team_title.Visibility = Visibility.Hidden;
        }

        private void team_but_Click(object sender, RoutedEventArgs e)
        {
            //xxxxxxxxxxxxxx
            car_title.Visibility = Visibility.Hidden;
            car_text1.Visibility = Visibility.Hidden;
            car_text2.Visibility = Visibility.Hidden;
            car_text3.Visibility = Visibility.Hidden;
            car_text4.Visibility = Visibility.Hidden;
            input3.Visibility = Visibility.Hidden;
            input4.Visibility = Visibility.Hidden;
            car_add_but.Visibility = Visibility.Hidden;
            //xxxxxxxxxxxxxx
            input1.Visibility = Visibility.Visible;
            input2.Visibility = Visibility.Visible;
            team_text1.Visibility = Visibility.Visible;
            team_text2.Visibility = Visibility.Visible;
            team_add_but.Visibility = Visibility.Visible;
            team_title.Visibility = Visibility.Visible;
        }

        private void team_add_but_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
