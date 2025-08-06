using System.ComponentModel;

namespace Tsw.WpfApp.Model
{
    public class Auto : INotifyPropertyChanged
    {
        public string NazevModelu { get; set; }

        private DateTime _datumProdeje;
        public DateTime DatumProdeje
        {
            get => _datumProdeje;
            set { _datumProdeje = value; OnPropertyChanged(nameof(DatumProdeje)); }
        }

        private double _cena;
        public double Cena
        {
            get => _cena;
            set { _cena = value; OnPropertyChanged(nameof(Cena)); }
        }

        private double _dph;
        public double DPH
        {
            get => _dph;
            set { _dph = value; OnPropertyChanged(nameof(DPH)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
