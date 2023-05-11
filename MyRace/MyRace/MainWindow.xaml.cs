using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            string conn = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Wyscig;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(conn);

            //#

            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select nazwa,data from wydarzenia",connection);
            cmd1.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable("Wydarzenia");
            adapter.Fill(dt);
            najblizszew.ItemsSource = dt.DefaultView;
            adapter.Update(dt);
            connection.Close();
        }
    }
}
