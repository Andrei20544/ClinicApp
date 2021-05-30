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
    public partial class ServicesWindow : Window
    {
        private List<Services> services = new List<Services>();
        public ServicesWindow()
        {
            InitializeComponent();

            Update();

        }

        private void Update()
        {
            try
            {
                using (ModelBD model = new ModelBD())
                {

                    services.Clear();
                    GridClient.SelectedItem = null;
                    GridClient.ItemsSource = null;

                    var querry = from s in model.Services
                                 select s;

                    foreach (var item in querry)
                    {
                        services.Add(item);
                    }

                    GridClient.ItemsSource = services;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void AddClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = GridClient.SelectedItem as Services;

                using (ModelBD model = new ModelBD())
                {
                    Services services = new Services
                    {
                        name = nameText.Text,
                        id_pills = int.Parse(idPillsText.Text),
                        value = valueText.Text,
                        limit_age = int.Parse(limitText.Text),
                        description = descriptionText.Text
                    };

                    model.Services.Add(services);
                    model.SaveChanges();

                    MessageBox.Show("Услуга успешно добавлен");

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
                var selected = GridClient.SelectedItem as Services;

                using (ModelBD model = new ModelBD())
                {
                    Services services = model.Services.Where(n => n.id_service.Equals(selected.id_service)).FirstOrDefault();
                    services.name = nameText.Text;
                    services.limit_age = int.Parse(limitText.Text);
                    services.value = valueText.Text;
                    services.description = descriptionText.Text;
                    services.id_pills = int.Parse(idPillsText.Text);

                    model.Entry(services).State = System.Data.Entity.EntityState.Modified;
                    model.SaveChanges();

                    MessageBox.Show("Услуга успешно изменен");

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
                var selected = GridClient.SelectedItem as Services;

                using (ModelBD model = new ModelBD())
                {
                    if (selected != null)
                    {
                        Services services = model.Services.Where(n => n.id_service.Equals(selected.id_service)).FirstOrDefault();
                        model.Services.Remove(services);
                        model.SaveChanges();

                        MessageBox.Show("Услуга успешно удален");
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

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Services> items = new List<Services>();
                foreach (Services item in services)
                {
                    if (item.name.Contains(search.Text) || item.description.Contains(search.Text))
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
            nameText.Text = "";
            limitText.Text = "";
            valueText.Text = "";
            descriptionText.Text = "";
            idPillsText.Text = "";
        }

        private void GridClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (services.Count != 0)
            {
                var selected = GridClient.SelectedItem as Services;

                nameText.Text = selected.name;
                limitText.Text = selected.limit_age.ToString();
                valueText.Text = selected.value;
                descriptionText.Text = selected.description;
                idPillsText.Text = selected.id_pills.ToString();
            }
        }
    }
}
