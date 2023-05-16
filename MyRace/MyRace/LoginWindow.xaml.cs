﻿using MySql.Data.MySqlClient;
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
    /// Logika interakcji dla klasy LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void login_but_Click(object sender, RoutedEventArgs e)
        {
            string nickname;
            string databaseFileName = "Baza.mdf";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            string databaseFilePath = Path.Combine(projectDirectory, databaseFileName);

            string conn = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={databaseFilePath};Integrated Security=True;Connect Timeout=30;";
            SqlConnection connection = new SqlConnection(conn);

            connection.Open();


                SqlDataAdapter cmd = new SqlDataAdapter($"SELECT * FROM Zawodnicy WHERE login = '{login.Text}' AND haslo = '{haslo.Text}'", connection);

                DataTable dataTable= new DataTable();
                cmd.Fill(dataTable);

                if(dataTable.Rows.Count > 0 )
                {
                    MessageBox.Show("Udało się!");
                    connection.Close();
                    nickname = login.Text;
                }
                else
                {
                    MessageBox.Show("Nieprawidłowy login lub hasło!!","BŁĄD LOGOWANIA",MessageBoxButton.OK,MessageBoxImage.Error);
                    connection.Close();
                }
        }

        private void back_but_Click(object sender, RoutedEventArgs e)
        {
            MainWindow okno = new MainWindow();
            this.Visibility = Visibility.Hidden;
            okno.Show();
        }
    }
}