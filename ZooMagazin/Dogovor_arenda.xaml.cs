using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
    /// Логика взаимодействия для Dogovor_arenda.xaml
    /// </summary>
    public partial class Dogovor_arenda : Window
    {
        DataSet1 dataSet;
        DogovorArendaTableAdapter dogovorArendaTableAdapter;
        PersonalsTableAdapter personalsTableAdapter;

        View_DogovorArendsTableAdapter view_DogovorArends;
        public Dogovor_arenda()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            dogovorArendaTableAdapter = new DogovorArendaTableAdapter();
            personalsTableAdapter = new PersonalsTableAdapter();

            view_DogovorArends = new View_DogovorArendsTableAdapter();

            dogovorArendaTableAdapter.Fill(dataSet.DogovorArenda);
            personalsTableAdapter.Fill(dataSet.Personals);

            view_DogovorArends.Fill(dataSet.View_DogovorArends);


            CBSurnameSotr.ItemsSource = dataSet.Personals.DefaultView;
            CBSurnameSotr.DisplayMemberPath = "Surname";
            CBSurnameSotr.SelectedValuePath = "ID_Personal";
            CBSurnameSotr.SelectedIndex = 0;

            DataGrid.ItemsSource = dataSet.View_DogovorArends.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Invoice_for_the_lease";
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
                for (int i = 0; i < dataSet.View_DogovorArends.Rows.Count; i++)
                {

                    if (dataSet.View_DogovorArends.Rows[i][3].ToString().Contains(index))
                    {
                        DataGrid.SelectedIndex = i;
                        return;
                    }

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Menedjer_po_nedv registration = new Menedjer_po_nedv();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(Salary.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool c = Regex.Match(Street.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(SurnameArend.Text, "^[А-Я][а-яА-Я]*$").Success;

            int age = DateNach.SelectedDate.Value.Year;
            int month = DateNach.SelectedDate.Value.Month;
            int day = DateNach.SelectedDate.Value.Day;


            int age2 = DateKon.SelectedDate.Value.Year;
            int month2 = DateKon.SelectedDate.Value.Month;
            int day2 = DateKon.SelectedDate.Value.Day;

            if (age > age2)
            {
                MessageBox.Show("Выберите правильные значения ");
            }
            else
            {
                if ((month > DateKon.SelectedDate.Value.Month) || (month2 > DateKon.SelectedDate.Value.Month))
                {
                    MessageBox.Show("Выберите правильные ");
                }
                else
                {
                    if ((day > DateKon.SelectedDate.Value.Day) || (day2 > DateKon.SelectedDate.Value.Day))
                    {
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {

                        if (!Salary.Text.Equals("") && b)
                        {
                            if (!Street.Text.Equals("") && c)
                            {
                                if (!SurnameArend.Text.Equals("") && a)
                                {

                                    dogovorArendaTableAdapter.Insert(DateNach.Text, Street.Text,
                                        SurnameArend.Text, Salary.Text, (int)CBSurnameSotr.SelectedValue, DateKon.Text);
                                    view_DogovorArends.Fill(dataSet.View_DogovorArends);

                                    SurnameArend.Text = "";
                                    Street.Text = "";
                                    Salary.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Поле фамилия исполнителя введено неккоректно");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Поле улица введено неккоректно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле стоимость введено неккоректно");
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(Salary.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool c = Regex.Match(Street.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(SurnameArend.Text, "^[А-Я][а-яА-Я]*$").Success;

            int age = DateNach.SelectedDate.Value.Year;
            int month = DateNach.SelectedDate.Value.Month;
            int day = DateNach.SelectedDate.Value.Day;


            int age2 = DateKon.SelectedDate.Value.Year;
            int month2 = DateKon.SelectedDate.Value.Month;
            int day2 = DateKon.SelectedDate.Value.Day;

            if (age > age2)
            {
                MessageBox.Show("Выберите правильные значения ");
            }
            else
            {
                if ((month > DateKon.SelectedDate.Value.Month) || (month2 > DateKon.SelectedDate.Value.Month))
                {
                    MessageBox.Show("Выберите правильные ");
                }
                else
                {
                    if ((day > DateKon.SelectedDate.Value.Day) || (day2 > DateKon.SelectedDate.Value.Day))
                    {
                        MessageBox.Show("Выберите правильные значения");
                    }
                    else
                    {

                        if (!Salary.Text.Equals("") && b)
                        {
                            if (!Street.Text.Equals("") && c)
                            {
                                if (!SurnameArend.Text.Equals("") && a)
                                {
                                    dogovorArendaTableAdapter.UpdateQuery(DateNach.Text, Street.Text,
                                        SurnameArend.Text, Salary.Text, (int)CBSurnameSotr.SelectedValue, DateKon.Text,
                                        (int)DataGrid.SelectedValue);
                            view_DogovorArends.Fill(dataSet.View_DogovorArends);
                                }
                                else
                                {
                                    MessageBox.Show("Поле фамилия исполнителя введено неккоректно");
                                }

                            }
                            else
                            {
                                MessageBox.Show("Поле улица введено неккоректно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле стоимость введено неккоректно");
                        }
                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                dogovorArendaTableAdapter.DeleteQuery((int)DataGrid.SelectedValue);
                view_DogovorArends.Fill(dataSet.View_DogovorArends);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                DateNach.Text = dataRowView.Row.Field<String>("Дата_начала_аренды");
                Street.Text = dataRowView.Row.Field<String>("Улица");
                SurnameArend.Text = dataRowView.Row.Field<String>("Фамилия_арендодателя");
                Salary.Text = dataRowView.Row.Field<String>("Стоимость");
                DateKon.Text = dataRowView.Row.Field<String>("Дата_окончания_договора");
                CBSurnameSotr.Text = dataRowView.Row.Field<String>("Фамилия_сотрудника");

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = "Otchet_o_dogovore_tehnicheskoe_jbclujivanie.txt";
                StreamWriter sw = new StreamWriter(path);
                DataTable dt_user = dataSet.View_DogovorArends;
                sw.WriteLine("Отчет о договоре об аренде: " + DateTime.Now.ToString() + " на текущий месяц");
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    sw.WriteLine("Дата начала аренды: " + dt_user.Rows[i][1].ToString());
                    sw.WriteLine("Улица: " + dt_user.Rows[i][2].ToString());
                    sw.WriteLine("Фамилия арендодателя: " + dt_user.Rows[i][3].ToString());
                    sw.WriteLine("Стоимость: " + dt_user.Rows[i][4].ToString());
                    sw.WriteLine("Дата окончания договора: " + dt_user.Rows[i][6].ToString());
                    sw.WriteLine("Фамилия сотрудника: " + dt_user.Rows[i][7].ToString());


                    sw.WriteLine("");
                }

                sw.Close();
                Process.Start("notepad.exe", path);
            }
            catch
            {
                MessageBox.Show("Выберите значения, которые должны экспортироваться");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;
            DataGrid.Columns[5].Visibility = Visibility.Hidden;
        }
    }
}
