using System;
using System.Collections.Generic;
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
            new_password.Visibility = Visibility.Hidden;
            new_password_text.Visibility = Visibility.Hidden;
            new_password_but.Visibility = Visibility.Hidden;

        }

        private void change_password_but_Click(object sender, RoutedEventArgs e)
        {
            new_password.Visibility = Visibility.Visible;
            new_password_text.Visibility= Visibility.Visible;
            new_password_but.Visibility = Visibility.Visible;
        }

        private void new_password_but_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
