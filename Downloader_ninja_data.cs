// descarga_data
using System.IO;

namespace NinjaTrader.NinjaScript.Strategies
{
    public class ExportadorCSV : Strategy
    {
        private StreamWriter sw;
        private string filePath;

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Name = "ExportadorCSV";
                Calculate = Calculate.OnBarClose;
                IsOverlay = false;
				BarsRequiredToTrade = 0;
            }
            else if (State == State.DataLoaded)
            {
                // Ruta local en tu escritorio (ajustada desde la ruta compartida)
                filePath = @"C:\Users\ferra\Desktop\Data\export.csv";

                try
                {
                    sw = new StreamWriter(filePath, false);
                    sw.WriteLine("FechaHora,Open,High,Low,Close,Volumen");
                }
                catch (System.Exception e)
                {
                    Print("Error creando archivo: " + e.Message);
                }
            }
            else if (State == State.Terminated)
            {
                if (sw != null)
                    sw.Close();
            }
        }

        protected override void OnBarUpdate()
        {
            if (sw == null || CurrentBar < BarsRequiredToTrade)
                return;

            string linea = Time[0].ToString("yyyy-MM-dd HH:mm:ss") + ";" +
                           Open[0] + ";" +
                           High[0] + ";" +
                           Low[0] + ";" +
                           Close[0] + ";" +
                           Volume[0];

            sw.WriteLine(linea);
        }
    }
}

