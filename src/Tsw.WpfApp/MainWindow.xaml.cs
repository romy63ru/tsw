using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tsw.WpfApp.Model;
using Tsw.WpfApp.Services;

namespace Tsw.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Car> _cars = new();
        public ObservableCollection<Car> Cars
        {
            get => _cars;
            set { _cars = value; OnPropertyChanged(nameof(Cars)); }
        }

        private ObservableCollection<CarSummary> _carSummaries = new();
        public ObservableCollection<CarSummary> CarSummaries
        {
            get => _carSummaries;
            set { _carSummaries = value; OnPropertyChanged(nameof(CarSummaries)); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Cars.CollectionChanged += Cars_CollectionChanged;
        }

        private void Cars_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Car car in e.NewItems)
                    car.PropertyChanged += Car_PropertyChanged;

            if (e.OldItems != null)
                foreach (Car car in e.OldItems)
                    car.PropertyChanged -= Car_PropertyChanged;

            RecalculateSummaries();
        }

        private void Car_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "Cena" or "DPH" or "DatumProdeje")
                RecalculateSummaries();
        }

        private void LoadXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
            {
                var loaded = LoadService.Load(ofd.FileName);

                Cars.Clear();
                foreach (var c in loaded)
                    Cars.Add(c);
            }
        }

        private void RecalculateSummaries()
        {
            CarSummaries.Clear();

            var summary = AggregationService.Aggregate(Cars);

            foreach (var item in summary)
                CarSummaries.Add(item);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}