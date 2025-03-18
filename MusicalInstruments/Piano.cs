using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalInstruments
{
    public class Piano : MusicalInstrument, IInit
    {
        private string keyLayout;
        private int keyCount;

        public string KeyLayout
        {
            get {return keyLayout;}
            set
            {
                string[] validLayouts = { "Octave", "Scale", "Digital" };
                if (!Array.Exists(validLayouts, s => s.Equals(value, StringComparison.OrdinalIgnoreCase)))
                    throw new ArgumentException("Invalid keyboard layout.");
                keyLayout = value;
            }
        }

        public int KeyCount
        {
            get { return keyCount; }
            set 
            {
                if (value < 25 || value > 104)
                    throw new ArgumentException("Number of keys must be between 25 and 104");
                keyCount = value;
            }
        }

        public Piano() : base()
        {
            KeyLayout = "Octave";
            KeyCount = 88;
        }

        public Piano(string name, int id, string keyLayout,  int keyCount) : base(name, id)
        {
            KeyLayout = keyLayout;
            KeyCount = keyCount;
        }

        //public override void ShowVirtual()
        //{
        //    base.ShowVirtual();
        //    Console.WriteLine($"Number of keys {KeyCount}");
        //    Console.WriteLine($"Key layout: {KeyLayout}");
        //}
        //public void Show()
        //{
        //    Console.WriteLine($"Name: {Name}");
        //    Console.WriteLine($"Key layout: {KeyLayout}");
        //    Console.WriteLine($"Number of keys: {KeyCount}");
        //}


        public override string ToString() 
        {
            return $"{base.ToString()}, key layout: {KeyLayout}, number of keys: {KeyCount}";
        }

        //public override void Init()
        //{
        //    base.Init();
        //    bool isValid = false;
        //    while (!isValid)
        //    {
        //        try
        //        {
        //            KeyLayout = Console.ReadLine().Trim();
        //            isValid = true;
        //        }
        //        catch (ArgumentException ex)
        //        {
        //            Console.WriteLine($"Error: {ex.Message}");
        //            Console.WriteLine("Try again.");
        //        }
        //    }
        //    try
        //    {
        //        KeyCount = ValidInput.GetInt();
        //    }
        //    catch (Exception ex) when (ex is ArgumentException)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        Console.WriteLine("Because of error, making a standart number of keys (88)");
        //        KeyCount = 81;


        //    }
        //    //Console.Write("Enter keyboard layout (Octave/Scale/Digital): ");
        //    KeyLayout = Console.ReadLine();
        //    //Console.Write("Enter number of keys: ");
            
        //}

        public override void RandomInit()
        {
            base.RandomInit();
            string[] layouts = { "Octave", "Scale", "Digital" };
            KeyLayout = layouts[rnd.Next(layouts.Length)];
            KeyCount = rnd.Next(25, 105);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            Piano other = (Piano)obj;
            return string.Equals(KeyLayout, other.KeyLayout, StringComparison.OrdinalIgnoreCase) && KeyCount == other.KeyCount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ KeyLayout.GetHashCode() ^ KeyCount.GetHashCode();
        }

        public override object Clone()
        {
            return new Piano(this.Name, this.ID.id, this.KeyLayout, this.KeyCount);
        }

    }
}
