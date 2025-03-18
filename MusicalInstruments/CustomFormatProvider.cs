using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalInstruments
{
    public class CustomFormatProvider: IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {

            if (arg is Guitar guitar)
            {
                return $"[Guitar] {guitar.Name} ({guitar.StringCount} strings)";
            }
            else if (arg is Piano piano)
            {
                return $"[Piano] {piano.Name} ({piano.KeyCount} keys)";
            }
            else if (arg is ElectroGuitar electroGuitar)
            {
                return $"[Electro Guitar] {electroGuitar.Name}, ({electroGuitar.StringCount} strings), ({electroGuitar.PowerSource} power source)";
            }
            return arg?.ToString();
        }
    }
}
