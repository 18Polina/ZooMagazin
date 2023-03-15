using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZooMagazin.DataSet1TableAdapters;
using LiveCharts.Definitions.Charts;

namespace ZooMagazin
{
    /// <summary>
    /// Логика взаимодействия для Grafik.xaml
    /// </summary>
    public partial class Grafik : Window
    {
        DataSet1 dataSet;
        GrafikTableAdapter grafikTableAdapter;

        View_GrafikTableAdapter view_GrafikTable;
        public Grafik()
        {
            InitializeComponent();
            dataSet = new DataSet1();

            grafikTableAdapter = new GrafikTableAdapter();

            view_GrafikTable = new View_GrafikTableAdapter();

            grafikTableAdapter.Fill(dataSet.Grafik);

            view_GrafikTable.Fill(dataSet.View_Grafik);


            DataGrid.ItemsSource = dataSet.View_Grafik.DefaultView;
            DataGrid.SelectionMode = DataGridSelectionMode.Single;
            DataGrid.SelectedValuePath = "ID_Grafik";
            DataGrid.CanUserAddRows = false;
            DataGrid.CanUserDeleteRows = false;
            DataGrid.IsReadOnly = true;
        }

        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.Columns[0].Visibility = Visibility.Hidden;

            graf.LegendLocation = LegendLocation.Bottom;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Administrator registration = new Administrator();
            registration.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SeriesCollection series = new SeriesCollection();
            ChartValues<int> sportValues = new ChartValues<int>();
            List<string> dates = new List<string>();
            foreach (var sportRow in dataSet.Grafik)
            {
                sportValues.Add(sportRow.Month);
                sportValues.Add(sportRow.Kolichestvo_dogovorov);
            }
            graf.AxisX.Clear();
            graf.AxisX.Add(new Axis()
            {
                Title = "Месяц",
                Labels = dates
            });
            LineSeries sportLine = new LineSeries();
            
            sportLine.Values = sportValues;

            series.Add(sportLine);

            graf.Series = series;
        }
    }
}
