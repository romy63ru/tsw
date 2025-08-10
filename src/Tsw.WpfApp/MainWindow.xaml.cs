using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Tsw.WpfApp.Model;
using Tsw.WpfApp.Services;

namespace Tsw.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Auto> _auta = new();
        public ObservableCollection<Auto> Auta
        {
            get => _auta;
            set { _auta = value; OnPropertyChanged(nameof(Auta)); }
        }

        private ObservableCollection<AutoSummary> _carSummaries = new();
        public ObservableCollection<AutoSummary> AutoSummaries
        {
            get => _carSummaries;
            set { _carSummaries = value; OnPropertyChanged(nameof(AutoSummaries)); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Auta.CollectionChanged += Cars_CollectionChanged;
        }

        private void Cars_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Auto car in e.NewItems)
                {
                    car.PropertyChanged += Car_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (Auto car in e.OldItems)
                {
                    car.PropertyChanged -= Car_PropertyChanged;
                }
            }

            RecalculateSummaries();
        }

        private void Car_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName is "Cena" or "DPH" or "DatumProdeje")
            {
                RecalculateSummaries();
            }
        }

        private void LoadXml_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    var loaded = LoadService.Load(ofd.FileName);

                    Auta.Clear();
                    foreach (var c in loaded)
                    {
                        Auta.Add(c);
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("Soubor nebyl nalezen.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Nemáte oprávnění číst tento soubor.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Chybný formát dat v XML (datum/cena/DPH).\n\n" + ex.Message,
                                    "Chyba formátu", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (System.Xml.XmlException ex)
                {
                    MessageBox.Show($"XML je poškozené nebo neplatné.\nŘádek {ex.LineNumber}, sloupec {ex.LinePosition}.\n\n{ex.Message}",
                                    "Chyba XML", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo k neočekávané chybě.\n\n" + ex.Message,
                                    "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RecalculateSummaries()
        {
            AutoSummaries.Clear();

            var summary = AggregationService.Aggregate(Auta);

            foreach (var item in summary)
            {
                AutoSummaries.Add(item);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}