using System;
using System.Collections.Generic;
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
using MessagesApp.ViewModels;

namespace MessagesApp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainViewModel();

            //try
            //{
            //    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                
            //    builder.DataSource = @"DESKTOP-J75DUOD\SQLEXPRESS";
            //    //builder.UserID = "bartek";
            //    //builder.Password = "xxx";
            //    builder.IntegratedSecurity = true;
            //    builder.InitialCatalog = "messages_app_db";

            //    using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            //    {
            //        //Console.WriteLine("\nQuery data example:");
            //        //Console.WriteLine("=========================================\n");

            //        connection.Open();
            //        StringBuilder sb = new StringBuilder();
            //        sb.Append("select * from [user]");
            //        String sql = sb.ToString();

            //        using (SqlCommand command = new SqlCommand(sql, connection))
            //        {
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    //Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
            //                    //Field.Text += String.Format("{0} {1}", reader.GetString(0), reader.GetString(1));
            //                    //Field.Text = String.Format("{0}", reader.GetString(0).ToString()).ToString();
            //                    //Field.Text += reader.GetValue(4);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (SqlException e)
            //{
            //    //Console.WriteLine(e.ToString());
            //    //Field.Text += e.ToString();
            //}
            //Console.ReadLine();
        }
    }
}
