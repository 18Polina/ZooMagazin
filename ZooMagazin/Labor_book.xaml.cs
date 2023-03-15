using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZooMagazin.DataSet1TableAdapters;

namespace ZooMagazin
{
    /// <summary>
    /// Логика взаимодействия для Labor_book.xaml
    /// </summary>
    public partial class Labor_book : Window
    {
        DataSet1 dataSet;
        Labor_bookTableAdapter labor_BookTable;
        PostTableAdapter postTableAdapter;

        View_LaborBookTableAdapter view_LaborBook;
        public Labor_book()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            labor_BookTable = new Labor_bookTableAdapter();
            postTableAdapter = new PostTableAdapter();

            view_LaborBook = new View_LaborBookTableAdapter();

            labor_BookTable.Fill(dataSet.Labor_book);
            postTableAdapter.Fill(dataSet.Post);

            view_LaborBook.Fill(dataSet.View_LaborBook);


            Post.ItemsSource = dataSet.Post.DefaultView;
            Post.DisplayMemberPath = "Post_name";
            Post.SelectedValuePath = "ID_Post";
            Post.SelectedIndex = 0;

            DataGrid.ItemsSource = dataSet.View_LaborBook.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Labor_book";
            DataGrid.CanUserAddRows = false;
            DataGrid.CanUserDeleteRows = false;
            DataGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string index = Poisk.Text;

            if (String.IsNullOrWhiteSpace(index))
            {
                return;
            }
            else
            {
                for (int i = 0; i < dataSet.View_LaborBook.Rows.Count; i++)
                {

                    if (dataSet.View_LaborBook.Rows[i][1].ToString().Contains(index))
                    {
                        DataGrid.SelectedIndex = i;
                        return;
                    }

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Kadrovik registration = new Kadrovik();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool d = Regex.Match(Number.Text, "^[0-9][0-9]*$").Success;

            int age = DateTime.Today.Year - Data.SelectedDate.Value.Year;
            int month = DateTime.Today.Month - Data.SelectedDate.Value.Month;
            int day = DateTime.Today.Day - Data.SelectedDate.Value.Day;

            if (age < 0)
            {
                MessageBox.Show("Выберите правильные ");
            }
            else
            {
                if (month < 0)
                {
                    MessageBox.Show("Выберите  значения");
                }
                else
                {
                    if (day < 0)
                    {
                        MessageBox.Show("Выберите  значения");
                    }
                    else
                    {

                        if (!Number.Text.Equals("") && d)
                        {
                            labor_BookTable.Insert(Number.Text, Data.Text, (int)Post.SelectedValue);
                            view_LaborBook.Fill(dataSet.View_LaborBook);

                            Number.Text = "";
                            Data.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Поле номер трудовой книжки введено неккоректно");
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool d = Regex.Match(Number.Text, "^[0-9][0-9]*$").Success;

            int age = DateTime.Today.Year - Data.SelectedDate.Value.Year;
            int month = DateTime.Today.Month - Data.SelectedDate.Value.Month;
            int day = DateTime.Today.Day - Data.SelectedDate.Value.Day;

            if (age < 0)
            {
                MessageBox.Show("Выберите правильные ");
            }
            else
            {
                if (month < 0)
                {
                    MessageBox.Show("Выберите  значения");
                }
                else
                {
                    if (day < 0)
                    {
                        MessageBox.Show("Выберите  значения");
                    }
                    else
                    {

                        if (!Number.Text.Equals("") && d)
                        {
                          

                        labor_BookTable.UpdateQuery(Number.Text, Data.Text, (int)Post.SelectedValue, (int)DataGrid.SelectedValue);
                        view_LaborBook.Fill(dataSet.View_LaborBook);
                        }
                        else
                        {
                            MessageBox.Show("Поле номер трудовой книжки у введено неккоректно");
                        }
                    }
                }
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                Number.Text = dataRowView.Row.Field<String>("Номер_трудовой_книжки");
                Data.Text = dataRowView.Row.Field<String>("Дата_принятия_на_работу");
                Post.Text = dataRowView.Row.Field<String>("Должность");


            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                labor_BookTable.DeleteQuery((int)DataGrid.SelectedValue);
                view_LaborBook.Fill(dataSet.View_LaborBook);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            DataGrid.Columns[3].Visibility = Visibility.Hidden;
        }
    }
}
