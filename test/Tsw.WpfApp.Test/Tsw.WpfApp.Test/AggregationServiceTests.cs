using System.Collections.ObjectModel;
using Tsw.WpfApp.Model;
using Tsw.WpfApp.Services;

namespace Tsw.WpfApp.Test
{
    public class AggregationServiceTests
    {
        [Fact]
        public void Aggregate_WeekendSales_GroupsAndSumsCorrectly()
        {
            var auta = new ObservableCollection<Auto>
            {
                new Auto { NazevModelu = "ModelA", DatumProdeje = new DateTime(2024, 6, 8), Cena = 100000, DPH = 21 }, // Saturday
                new Auto { NazevModelu = "ModelA", DatumProdeje = new DateTime(2024, 6, 9), Cena = 200000, DPH = 21 }, // Sunday
                new Auto { NazevModelu = "ModelB", DatumProdeje = new DateTime(2024, 6, 10), Cena = 300000, DPH = 21 }, // Monday
            };

            var result = AggregationService.Aggregate(auta);

            Assert.Single(result.Where(r => r.NazevModelu == "ModelA"));
            var modelA = result.First(r => r.NazevModelu == "ModelA");
            Assert.Equal(300000, modelA.CenaBezDPH);
            Assert.Equal(300000 * 1.21, modelA.CenaSDPH, 2);

            Assert.DoesNotContain(result, r => r.NazevModelu == "ModelB");
        }

        [Fact]
        public void Aggregate_EmptyCollection_ReturnsEmptyList()
        {
            var auta = new ObservableCollection<Auto>();
            var result = AggregationService.Aggregate(auta);
            Assert.Empty(result);
        }

        [Fact]
        public void Aggregate_NoWeekendSales_ReturnsEmptyList()
        {
            var auta = new ObservableCollection<Auto>
            {
                new Auto { NazevModelu = "ModelC", DatumProdeje = new DateTime(2024, 6, 10), Cena = 50000, DPH = 21 }, // Monday
                new Auto { NazevModelu = "ModelD", DatumProdeje = new DateTime(2024, 6, 11), Cena = 60000, DPH = 21 }, // Tuesday
            };

            var result = AggregationService.Aggregate(auta);
            Assert.Empty(result);
        }
    }
}