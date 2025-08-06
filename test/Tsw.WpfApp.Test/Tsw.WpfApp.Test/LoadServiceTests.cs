using System;
using System.IO;
using System.Linq;
using Tsw.WpfApp.Model;
using Tsw.WpfApp.Services;
using Xunit;

namespace Tsw.WpfApp.Test
{
    public class LoadServiceTests
    {
        [Fact]
        public void Load_ValidXml_ReturnsAutoList()
        {
            string xml = """
            <Root>
                <Auto>
                    <NazevModelu>ModelA</NazevModelu>
                    <DatumProdeje>2024-06-08</DatumProdeje>
                    <Cena>100000</Cena>
                    <DPH>21</DPH>
                </Auto>
                <Auto>
                    <NazevModelu>ModelB</NazevModelu>
                    <DatumProdeje>2024-06-09</DatumProdeje>
                    <Cena>200000</Cena>
                    <DPH>21</DPH>
                </Auto>
            </Root>
            """;
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, xml);

            var result = LoadService.Load(tempFile);

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("ModelA", result[0].NazevModelu);
            Assert.Equal(new DateTime(2024, 6, 8), result[0].DatumProdeje);
            Assert.Equal(100000, result[0].Cena);
            Assert.Equal(21, result[0].DPH);

            Assert.Equal("ModelB", result[1].NazevModelu);
            Assert.Equal(new DateTime(2024, 6, 9), result[1].DatumProdeje);
            Assert.Equal(200000, result[1].Cena);
            Assert.Equal(21, result[1].DPH);

            File.Delete(tempFile);
        }

        [Fact]
        public void Load_MissingElement_ThrowsArgumentNullException()
        {
            string xml = """
            <Root>
                <Auto>
                    <NazevModelu>ModelC</NazevModelu>
                    <!-- Missing DatumProdeje -->
                    <Cena>300000</Cena>
                    <DPH>21</DPH>
                </Auto>
            </Root>
            """;
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, xml);

            Assert.Throws<ArgumentNullException>(() => LoadService.Load(tempFile));

            File.Delete(tempFile);
        }
    }
}