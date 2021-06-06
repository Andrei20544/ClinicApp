﻿using ClinicApp.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Clients clients = new Clients();
            clients.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DoctorsWindows doctorsWindows = new DoctorsWindows();
            doctorsWindows.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MedConsWindow medConsWindow = new MedConsWindow();
            medConsWindow.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PillsWindow pillsWindow = PillsWindow.GetInstance();
            pillsWindow.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ServicesWindow servicesWindow = new ServicesWindow();
            servicesWindow.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }
    }
}
