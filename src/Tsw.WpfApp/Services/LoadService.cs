using System.Globalization;
using System.Xml.Linq;
using Tsw.WpfApp.Model;

namespace Tsw.WpfApp.Services
{
    public class LoadService
    {
        public static List<Auto> Load(string path)
        {
            var doc = XDocument.Load(path);
            return doc.Root.Elements("Auto").Select(static x => new Auto
            {
                NazevModelu = x.Element("NazevModelu")?.Value,
                DatumProdeje = DateTime.Parse(x.Element("DatumProdeje")?.Value, CultureInfo.InvariantCulture),
                Cena = double.Parse(x.Element("Cena")?.Value),
                DPH = double.Parse(x.Element("DPH")?.Value)
            }).ToList();
        }
    }
}
