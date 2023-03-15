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
    /// Логика взаимодействия для Labor_dogovor.xaml
    /// </summary>
    public partial class Labor_dogovor : Window
    {
        DataSet1 dataSet;
        Labor_dogovorTableAdapter labor_DogovorTable;
        PersonalsTableAdapter personalsTableAdapter;
      
        View_LaborDogovorTableAdapter view_LaborDogovor;
        public Labor_dogovor()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            labor_DogovorTable = new Labor_dogovorTableAdapter();
            personalsTableAdapter = new PersonalsTableAdapter();

            view_LaborDogovor = new View_LaborDogovorTableAdapter();

            labor_DogovorTable.Fill(dataSet.Labor_dogovor);
            personalsTableAdapter.Fill(dataSet.Personals);
            
            view_LaborDogovor.Fill(dataSet.View_LaborDogovor);


            CBPersonal.ItemsSource = dataSet.Personals.DefaultView;
            CBPersonal.DisplayMemberPath = "Surname";
            CBPersonal.SelectedValuePath = "ID_Personal";
            CBPersonal.SelectedIndex = 0;

            DataGrid.ItemsSource = dataSet.View_LaborDogovor.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Labor_dogovor";
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
                for (int i = 0; i < dataSet.View_LaborDogovor.Rows.Count; i++)
                {

                    if (dataSet.View_LaborDogovor.Rows[i][4].ToString().Contains(index))
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
            bool b = Regex.Match(INN.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool c = Regex.Match(Rejim.Text, "^[0-9][ ][а-яА-Я]*$").Success;

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
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {


                        if (!INN.Text.Equals("") && b)
            {
                if (!Rejim.Text.Equals("") && c)
                {
                    
                        
                            labor_DogovorTable.Insert(INN.Text, Data.Text, Rejim.Text, (int)CBPersonal.SelectedValue);
                            view_LaborDogovor.Fill(dataSet.View_LaborDogovor);

                        INN.Text = "";
                        Rejim.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Поле стоимость введено неккоректно");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Поле ИНН введено неккоректно");
                        }
                    }
                }
            }
        }


        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(INN.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool c = Regex.Match(Rejim.Text, "^[0-9][ ][а-яА-Я]*$").Success;

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
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {

                        if (!INN.Text.Equals("") && b)
                        {
                            if (!Rejim.Text.Equals("") && c)
                            {
                                labor_DogovorTable.UpdateQuery(INN.Text, Data.Text, Rejim.Text, (int)CBPersonal.SelectedValue, (int)DataGrid.SelectedValue);
                                view_LaborDogovor.Fill(dataSet.View_LaborDogovor);
                            }
                            else
                            {
                                MessageBox.Show("Поле стоимость введено неккоректно");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Поле ИНН введено неккоректно");
                        }
                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                labor_DogovorTable.DeleteQuery((int)DataGrid.SelectedValue);
                view_LaborDogovor.Fill(dataSet.View_LaborDogovor);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                INN.Text = dataRowView.Row.Field<String>("ИНН");
                Data.Text = dataRowView.Row.Field<String>("Дата_начала_договора");
                Rejim.Text = dataRowView.Row.Field<String>("Режим_работы");

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            
        }
    }
}
