using System.Text.Json;

namespace misFunciones
{
    public class Json
    {
        public static bool IsValidJson(string message, out JsonDocument jsonDoc)
        {
            try
            {
                jsonDoc = JsonDocument.Parse(message);
                return jsonDoc.RootElement.TryGetProperty("instruccion", out _) &&
                       jsonDoc.RootElement.TryGetProperty("datos", out _);
            }
            catch
            {
                jsonDoc = null;
                return false;
            }
        }
        public static bool IsValidPrintData(string datos, out string printerName, out string datosImprimir)
        {
            try
            {
                var jsonDoc = JsonDocument.Parse(datos);
                return IsValidPrintData(jsonDoc.RootElement, out printerName, out datosImprimir);
            }
            catch
            {
                printerName = null;
                datosImprimir = null;
                return false;
            }
        }
        public static bool IsValidPrintData(JsonElement datosElement, out string printerName, out string datosImprimir)
        {
            if (datosElement.TryGetProperty("impresora", out JsonElement printerElement) &&
                datosElement.TryGetProperty("datosAImprimir", out JsonElement datosImprimirElement))
            {
                printerName = printerElement.GetString();
                datosImprimir = datosImprimirElement.GetString();
                return true;
            }
            else
            {
                printerName = null;
                datosImprimir = null;
                return false;
            }
        }
    }
    public class Mensajes
    {
        public static string GenerateMessaje(string mensaje)
        {
            var mensajeJson = new
            {
                instruccion = "mensaje",
                datos = mensaje
            };

            string mensajeString = JsonSerializer.Serialize(mensajeJson);
            return mensajeString;
        }
        public static string GeneratePrinterListRequest(string mensaje = "")
        {
            var mensajeJson = new
            {
                instruccion = "impresoras",
                datos = mensaje
            };

            string mensajeString = JsonSerializer.Serialize(mensajeJson);
            return mensajeString;
        }
        public static string GeneratePrinterRequest(string datosAImprimir, string impresora)
        {
            var mensajeJson = new
            {
                instruccion = "imprimir",
                datos = new
                {
                    datosAImprimir = datosAImprimir,
                    impresora = impresora
                }
            };

            string mensajeString = JsonSerializer.Serialize(mensajeJson);
            return mensajeString;
        }
      
    }
    public class Utilidades
    {
        public static void CenterText(string text)
        {
            int windowWidth = Console.WindowWidth; // Ancho de la consola
            int textPosition = (windowWidth / 2) - (text.Length / 2); // Calcula la posición inicial

            if (textPosition < 0)
                textPosition = 0; // Asegura que no se desborde si el texto es más ancho que la consola

            Console.SetCursorPosition(textPosition, Console.CursorTop); // Posiciona el cursor
            Console.WriteLine(text); // Imprime el texto
        }
    }
}
