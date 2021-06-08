using ClinicApp.Views;
using System.Windows;
using System.Windows.Input;

namespace ClinicApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Clients clients = Clients.GetInstance();
            clients.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DoctorsWindows doctorsWindows = DoctorsWindows.GetInstance();
            doctorsWindows.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MedConsWindow medConsWindow = MedConsWindow.GetInstance();
            medConsWindow.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PillsWindow pillsWindow = PillsWindow.GetInstance();
            pillsWindow.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ServicesWindow servicesWindow = ServicesWindow.GetInstance();
            servicesWindow.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = RegisterWindow.GetInstance();
            registerWindow.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ScheduleWindow scheduleWindow = ScheduleWindow.GetInstance();
            scheduleWindow.Show();
        }
    }
}
