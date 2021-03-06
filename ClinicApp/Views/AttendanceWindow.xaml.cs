using ClinicApp.ViewModel;
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
using System.Windows.Shapes;

namespace ClinicApp.Views
{
    public partial class AttendanceWindow : Window
    {
        private List<DopAttendance> attendances = new List<DopAttendance>();
        public AttendanceWindow()
        {
            InitializeComponent();

            Update();
        }

        private void Update()
        {
            try
            {
                string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Hospital;Integrated Security=True";
                string sqlExpression = "TalonNa_3";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nameClient = reader.GetString(1);
                            string nameService = reader.GetString(2);
                            string itogo = reader.GetInt32(3).ToString();
                            DopAttendance attendance = new DopAttendance(id, nameClient, nameService, double.Parse(itogo));
                            attendances.Add(attendance);
                            //MessageBox.Show($"{id} \t{nameClient} \t{nameService} \t{itogo}");
                        }
                    }
                    reader.Close();
                }
                GridClient.ItemsSource = attendances;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<DopAttendance> items = new List<DopAttendance>();
                foreach (DopAttendance item in attendances)
                {
                    if (item.NameClient.Contains(search.Text) || item.NameService.Contains(search.Text))
                    {
                        items.Add(item);
                    }
                }
                GridClient.ItemsSource = items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearTextBox()
        {
            nameClientText.Text = "";
            nameServiceText.Text = "";
            itogText.Text = "";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (attendances.Count != 0)
            {
                var selected = GridClient.SelectedItem as DopAttendance;

                if (selected != null)
                {
                    try
                    {
                        nameClientText.Text = selected.NameClient;
                        nameServiceText.Text = selected.NameService;
                        itogText.Text = selected.Itogo.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
