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
    /// Логика взаимодействия для Dogovor_obsluj.xaml
    /// </summary>
    public partial class Dogovor_obsluj : Window
    {
        DataSet1 dataSet;
        Dogovor_obsluzivaniyaTableAdapter dogovor_ObsluzivaniyaTable;
        PersonalsTableAdapter personalsTableAdapter;

        View_DogovorObcluzTableAdapter view_DogovorObcluz;
        public Dogovor_obsluj()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            dogovor_ObsluzivaniyaTable = new Dogovor_obsluzivaniyaTableAdapter();
            personalsTableAdapter = new PersonalsTableAdapter();

            view_DogovorObcluz = new View_DogovorObcluzTableAdapter();

            dogovor_ObsluzivaniyaTable.Fill(dataSet.Dogovor_obsluzivaniya);
            personalsTableAdapter.Fill(dataSet.Personals);

            view_DogovorObcluz.Fill(dataSet.View_DogovorObcluz);


            CBSurnameSotr.ItemsSource = dataSet.Personals.DefaultView;
            CBSurnameSotr.DisplayMemberPath = "Surname";
            CBSurnameSotr.SelectedValuePath = "ID_Personal";
            CBSurnameSotr.SelectedIndex = 0;

            DataGrid.ItemsSource = dataSet.View_DogovorObcluz.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Technical_Maintenance_Contr";
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
                for (int i = 0; i < dataSet.View_DogovorObcluz.Rows.Count; i++)
                {

                    if (dataSet.View_DogovorObcluz.Rows[i][1].ToString().Contains(index))
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
            bool b = Regex.Match(SurnameIspoln.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool c = Regex.Match(Street.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(Salary.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]*$").Success;
            bool d = Regex.Match(SrokDogovora.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;


            if (!SurnameIspoln.Text.Equals("") && b)
            {
                if (!Street.Text.Equals("") && c)
                {
                    if (!Salary.Text.Equals("") && a)
                    {
                        if (!SrokDogovora.Text.Equals("") && d)
                        {
                            dogovor_ObsluzivaniyaTable.Insert(SurnameIspoln.Text, (int)CBSurnameSotr.SelectedValue, Street.Text, Salary.Text, SrokDogovora.Text);
                    view_DogovorObcluz.Fill(dataSet.View_DogovorObcluz);

                            SurnameIspoln.Text = "";
                            Street.Text = "";
                            Salary.Text = "";
                            SrokDogovora.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Поле срок договора введено неккоректно");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Поле ежемесячная стоиомсть введено неккоректно");
                    }
                }
                else
                {
                    MessageBox.Show("Поле улица введено неккоректно");
                }
            }
            else
            {
                MessageBox.Show("Поле фамилия исполнителя введено неккоректно");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem != null)
            {
                dogovor_ObsluzivaniyaTable.DeleteQuery((int)DataGrid.SelectedValue);
                view_DogovorObcluz.Fill(dataSet.View_DogovorObcluz);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            bool b = Regex.Match(SurnameIspoln.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool c = Regex.Match(Street.Text, "^[А-Я][а-яА-Я]*$").Success;
            bool a = Regex.Match(Salary.Text, "^[0-9][0-9][0-9][0-9][0-9][0-9]]*$").Success;
            bool d = Regex.Match(SrokDogovora.Text, "^[0-9][0-9][ ][а-яА-Я]*$").Success;


            if (!SurnameIspoln.Text.Equals("") && b)
            {
                if (!Street.Text.Equals("") && c)
                {
                    if (!Salary.Text.Equals("") && a)
                    {
                        if (!SrokDogovora.Text.Equals("") && d)
                        {
                            dogovor_ObsluzivaniyaTable.UpdateQuery(SurnameIspoln.Text, (int)CBSurnameSotr.SelectedValue, Street.Text, Salary.Text, SrokDogovora.Text, (int)DataGrid.SelectedValue);
                            view_DogovorObcluz.Fill(dataSet.View_DogovorObcluz);
                        }
                        else
                        {
                            MessageBox.Show("Поле срок договора введено неккоректно");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Поле ежемесячная стоиомсть введено неккоректно");
                    }
                }
                else
                {
                    MessageBox.Show("Поле улица введено неккоректно");
                }
            }
            else
            {
                MessageBox.Show("Поле фамилия исполнителя введено неккоректно");
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataGrid.SelectedItem;
            if (dataRowView != null)
            {
                SurnameIspoln.Text = dataRowView.Row.Field<String>("Фамилия_исполнителяя");
                CBSurnameSotr.Text = dataRowView.Row.Field<String>("Фамилия_сотрудника");
                Street.Text = dataRowView.Row.Field<String>("Улица");
                Salary.Text = dataRowView.Row.Field<String>("Ежемесечная_стоимость");
                SrokDogovora.Text = dataRowView.Row.Field<String>("Срок_договора");

            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = "Otchet_o_dogovore_tehnicheskoe_jbclujivanie.txt";
                StreamWriter sw = new StreamWriter(path);
                DataTable dt_user = dataSet.View_DogovorObcluz;
                sw.WriteLine("Отчет о договоре на техническое обслуживание объектов: " + DateTime.Now.ToString() + " на текущий месяц");
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    sw.WriteLine("Фамилия исполнителя: " + dt_user.Rows[i][1].ToString());
                    sw.WriteLine("Улица: " + dt_user.Rows[i][3].ToString());
                    sw.WriteLine("Ежемесечная стоимость: " + dt_user.Rows[i][4].ToString());
                    sw.WriteLine("Фамилия сотрудника: " + dt_user.Rows[i][5].ToString());
                    sw.WriteLine("Срок договора: " + dt_user.Rows[i][6].ToString());


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
            DataGrid.Columns[2].Visibility = Visibility.Hidden;
        }
    }
}
