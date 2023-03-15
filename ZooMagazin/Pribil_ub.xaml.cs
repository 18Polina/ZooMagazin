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
    /// Логика взаимодействия для Pribil_ub.xaml
    /// </summary>
    public partial class Pribil_ub : Window
    {
        DataSet1 dataSet;
        Otchet_orgTableAdapter otchet_Org;

        View_Otchet_orgTableAdapter view_Otchet_;
        public Pribil_ub()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            otchet_Org = new Otchet_orgTableAdapter();

            view_Otchet_ = new View_Otchet_orgTableAdapter();

            otchet_Org.Fill(dataSet.Otchet_org);

            view_Otchet_.Fill(dataSet.View_Otchet_org);


            DataGrid.ItemsSource = dataSet.View_Otchet_org.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Otchet";
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
                for (int i = 0; i < dataSet.View_Otchet_org.Rows.Count; i++)
                {

                    if (dataSet.View_Otchet_org.Rows[i][2].ToString().Contains(index))
                    {
                        DataGrid.SelectedIndex = i;
                        return;
                    }

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Buhgalter registration = new Buhgalter();
            registration.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(Surname.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(SalaryPrib.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool c = Regex.Match(Period.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;
            bool d = Regex.Match(SalaryUb.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;

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

                        if (!Surname.Text.Equals("") && b)
                        {
                            if (!SalaryPrib.Text.Equals("") && a)
                            {
                                if (!Period.Text.Equals("") && c)
                                {
                                    if (!SalaryUb.Text.Equals("") && d)
                                    {
                                        otchet_Org.Insert(Data.Text, Surname.Text, SalaryPrib.Text, Period.Text, SalaryUb.Text);
                                        view_Otchet_.Fill(dataSet.View_Otchet_org);

                                        Data.Text = "";
                                        Surname.Text = "";
                                        SalaryPrib.Text = "";
                                        Period.Text = "";
                                        SalaryUb.Text = "";
                                    }
                                    else
                                    {
                                        MessageBox.Show("Поле стоимость убытка введено неккоректно");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Поле период введено неккоректно");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Поле стоимость прибыли введено неккоректно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле фамилия сотрудника введено неккоректно");
                        }
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(Surname.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(SalaryPrib.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool c = Regex.Match(Period.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;
            bool d = Regex.Match(SalaryUb.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;

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

                        if (!Surname.Text.Equals("") && b)
                        {
                            if (!SalaryPrib.Text.Equals("") && a)
                            {
                                if (!Period.Text.Equals("") && c)
                                {
                                    if (!SalaryUb.Text.Equals("") && d)
                                    {
                                        otchet_Org.UpdateQuery(Data.Text, Surname.Text, SalaryPrib.Text, Period.Text, SalaryUb.Text, (int)DataGrid.SelectedValue);
                                        view_Otchet_.Fill(dataSet.View_Otchet_org);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Поле стоимость убытка введено неккоректно");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Поле период введено неккоректно");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Поле стоимость прибыли введено неккоректно");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле фамилия сотрудника введено неккоректно");
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
                Data.Text = dataRowView.Row.Field<String>("Дата_подписания");
                Surname.Text = dataRowView.Row.Field<String>("Фамилия_сотрудника");
                SalaryPrib.Text = dataRowView.Row.Field<String>("Стоимость_прибыли");
                Period.Text = dataRowView.Row.Field<String>("Период");
                SalaryUb.Text = dataRowView.Row.Field<String>("Стоимость_убытка");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                otchet_Org.DeleteQuery((int)DataGrid.SelectedValue);
                view_Otchet_.Fill(dataSet.View_Otchet_org);
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = "Otchet_o_pribili_ubitkah.txt";
                StreamWriter sw = new StreamWriter(path);
                DataTable dt_user = dataSet.View_Otchet_org;
                sw.WriteLine("Отчет о прибыли и убытках организации " + DateTime.Now.ToString() + " на текущий месяц");
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    sw.WriteLine("Дата подписания: " + dt_user.Rows[i][1].ToString());
                    sw.WriteLine("Фамилия сотрудника: " + dt_user.Rows[i][2].ToString());
                    sw.WriteLine("Стоимость прибыли: " + dt_user.Rows[i][3].ToString());
                    sw.WriteLine("Период: " + dt_user.Rows[i][4].ToString());
                    sw.WriteLine("Стоимость убытка: " + dt_user.Rows[i][5].ToString());


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
        }
    }
}
