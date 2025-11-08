using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowToDo
{
    public class Tools
    {
        public static string Timestamp() {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static string FontToString(Font font)
        {
            return $"{font.Name},{font.Size},{font.Style}";
        }

        public static Font StringToFont(string str)
        {
            string[] parts = str.Split(',');
            string name = parts[0];
            float size = float.Parse(parts[1], CultureInfo.InvariantCulture);
            FontStyle style = (FontStyle)Enum.Parse(typeof(FontStyle), parts[2]);
            return new Font(name, size, style);
        }

        public static string ColorToString(Color color)
        {
            return $"{color.A},{color.R},{color.G},{color.B}";
        }

        public static Color StringToColor(string str, Color defaultColor)
        {
            try
            {
                string[] parts = str.Split(',');
                int a = int.Parse(parts[0]);
                int r = int.Parse(parts[1]);
                int g = int.Parse(parts[2]);
                int b = int.Parse(parts[3]);
                return Color.FromArgb(a, r, g, b);
            }
            catch (Exception)
            {

                
            }
            
            return defaultColor;
        }
    }
}
