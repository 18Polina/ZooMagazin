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

namespace ZooMagazin
{
    /// <summary>
    /// Логика взаимодействия для Kadrovik.xaml
    /// </summary>
    public partial class Kadrovik : Window
    {
        public Kadrovik()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Labor_book registration = new Labor_book();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Labor_dogovor registration = new Labor_dogovor();
            registration.Show();
            this.Close();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainWindow registration = new MainWindow();
            registration.Show();
            this.Close();
        }
    }
}
