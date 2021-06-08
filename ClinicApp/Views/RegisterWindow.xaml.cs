﻿using ClinicApp.ViewModel;
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

namespace ClinicApp.Views
{
    public partial class RegisterWindow : Window
    {
        private List<DopRegister> register = new List<DopRegister>();
        public RegisterWindow()
        {
            InitializeComponent();

            SetComboBox();
            Update();
        }

        private void SetComboBox()
        {
            using (ModelBD model = new ModelBD())
            {
                var services = from s in model.Services
                               select new
                               {
                                   nameService = s.name + "(id: " + s.id_service + " )"
                               };

                var clients = from c in model.Client
                               select new
                               {
                                   nameClient = c.name + " " + c.middle_name,
                                   policyNum = c.policy_number
                               };

                var doctors = from d in model.Doctor
                              select new
                              {
                                  nameDoctor = d.name + " " + d.middle_name + "(id: " + d.id_doctor + " )",
                                  role = d.role
                              };

                foreach (var item in clients)
                {
                    nameClientsCombo.Items.Add(item.nameClient + "( " + item.policyNum + " )");                  
                }
                foreach (var item in services.Distinct())
                {
                    nameServicesCombo.Items.Add(item.nameService);
                }
                foreach (var item in doctors)
                {
                    nameDoctorCombo.Items.Add(item.nameDoctor + "( " + item.role + " )");
                }
            }
        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    register.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var querry = from r in model.Register
                                 join c in model.Client on r.id_client equals c.id_client
                                 join s in model.Services on r.id_service equals s.id_service
                                 join d in model.Doctor on r.id_doctor equals d.id_doctor
                                 select new
                                 {
                                     nameService = s.name,
                                     nameClient = c.name + " " + c.middle_name,
                                     nameDoctor = d.name + " " + d.middle_name,
                                     Date = r.date_register,
                                     role = d.role,
                                     policyNum = c.policy_number,
                                     idServices = s.id_service,
                                     idDoctor = d.id_doctor,
                                     idRegister = r.id_register
                                 };

                    foreach (var item in querry)
                    {
                        DopRegister DopRegister = new DopRegister(item.nameClient, item.nameService, item.nameDoctor, 
                            item.Date, item.role, item.policyNum, item.idServices, item.idDoctor, item.idRegister);
                        register.Add(DopRegister);
                    }

                    GridClient.ItemsSource = register;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {
                    var policyNum = nameClientsCombo.SelectedItem.ToString().Split('(')[1].Split(')')[0].Replace(" ", "");
                    var DoctorID = nameDoctorCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", "");
                    var ServiceID = nameServicesCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", "");
                    //error

                    var Id_client = model.Client.Where(p => p.policy_number.Equals(policyNum)).FirstOrDefault();

                    Register register = new Register
                    {
                        id_client = Id_client.id_client,
                        id_doctor = int.Parse(DoctorID),
                        id_service = int.Parse(ServiceID),
                        date_register = Date.SelectedDate
                    };

                    model.Register.Add(register);
                    model.SaveChanges();

                    MessageBox.Show("Запись успешно добавлен");

                    Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возможно не все поля были заполненны!");
            }
        }

        private void ChangeClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as DopRegister;

                using (ModelBD model = new ModelBD())
                {
                    var policyNum = nameClientsCombo.SelectedItem.ToString().Split('(')[1].Split(')')[0].Replace(" ", "");

                    var DoctorID = nameDoctorCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", "");
                    var ServiceID = nameServicesCombo.SelectedItem.ToString().Split(':')[1].Split(')')[0].Replace(" ", "");
                    var Id_client = model.Client.Where(p => p.policy_number.Equals(policyNum)).FirstOrDefault();

                    Register register = model.Register.Where(n => n.id_register.Equals(selected.IdRegister)).FirstOrDefault();
                    register.id_client = Id_client.id_client;
                    register.id_doctor = int.Parse(DoctorID);
                    register.id_service = int.Parse(ServiceID);
                    register.date_register = Date.SelectedDate;

                    model.Entry(register).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Запись успешно изменен");

                    Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возможно не все поля были заполненны!");
            }
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as DopRegister;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Register register = model.Register.Where(n => n.id_register.Equals(selected.IdRegister)).FirstOrDefault();
                        model.Register.Remove(register);
                        model.SaveChanges();

                        MessageBox.Show("Запись успешно удален");
                        ClearTextBox();

                        Update();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ClearTextBox();
            GridClient.SelectedItem = null;
            try
            {
                List<DopRegister> items = new List<DopRegister>();
                foreach (DopRegister item in register)
                {
                    if (item.NameClient.Contains(search.Text) || item.NameDoctor.Contains(search.Text) || item.NameServices.Contains(search.Text))
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
            nameClientsCombo.SelectedItem = null;
            nameDoctorCombo.SelectedItem = null;
            nameServicesCombo.SelectedItem = null;
            Date.SelectedDate = null;
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (register.Count != 0)
            {
                var selected = GridClient.SelectedItem as DopRegister;

                if (selected != null)
                {
                    try
                    {
                        nameClientsCombo.SelectedItem = selected.NameClient + "( " + selected.polNum + " )";
                        nameDoctorCombo.SelectedItem = selected.NameDoctor + "(id: " + selected.IdDoctor + " )" + "( " + selected.role + " )";
                        nameServicesCombo.SelectedItem = selected.NameServices + "(id: " + selected.IdServices + " )";
                        Date.Text = selected.Date.ToString();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
