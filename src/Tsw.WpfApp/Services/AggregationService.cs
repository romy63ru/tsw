using System.Collections.ObjectModel;
using Tsw.WpfApp.Model;

namespace Tsw.WpfApp.Services
{
    public class AggregationService
    {
        public static List<AutoSummary> Aggregate(ObservableCollection<Auto> cars)
        {
            return cars
                .Where(c => c.DatumProdeje.DayOfWeek == DayOfWeek.Saturday || c.DatumProdeje.DayOfWeek == DayOfWeek.Sunday)
                .GroupBy(c => c.NazevModelu)
                .Select(g => new AutoSummary
                {
                    NazevModelu = g.Key,
                    CenaBezDPH = g.Sum(c => c.Cena),
                    CenaSDPH = g.Sum(c => c.Cena * (1 + c.DPH / 100))
                })
                .ToList();
        }
    }
}
