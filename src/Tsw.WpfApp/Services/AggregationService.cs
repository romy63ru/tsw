using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsw.WpfApp.Model;

namespace Tsw.WpfApp.Services
{
    public class AggregationService
    {
        public static List<CarSummary> Aggregate(ObservableCollection<Car> cars)
        {
            return cars
                .Where(c => c.DatumProdeje.DayOfWeek == DayOfWeek.Saturday || c.DatumProdeje.DayOfWeek == DayOfWeek.Sunday)
                .GroupBy(c => c.NazevModelu)
                .Select(g => new CarSummary
                {
                    NazevModelu = g.Key,
                    CenaBezDPH = g.Sum(c => c.Cena),
                    CenaSDPH = g.Sum(c => c.Cena * (1 + c.DPH / 100))
                })
                .ToList();
        }
    }
}
