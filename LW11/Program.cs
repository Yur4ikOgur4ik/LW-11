using MusicalInstruments;
using System.Collections;
using System;


namespace LW11
{
    internal class Program
    {
        public static double AverageNumberOfStrings(Queue instruments)
        {
            double count = 0;
            double numberOfStrings = 0;
            foreach (MusicalInstrument instr in instruments)
            {
                if (instr is Guitar guitar)//rassmotr kak v seredine ierarxii vedut => Guitar != EGuitar
                {
                    numberOfStrings += guitar.StringCount;
                    count++;
                }
            }

            if (count != 0)
                return numberOfStrings / count;
            else
                return -1;

        }

        public static int NumberOfStringInElectroGuitarsWithFixedPower(Queue instruments)
        {
            int numberOfStrings = 0;

            foreach (MusicalInstrument instr in instruments)
            {
                ElectroGuitar eGuitar = instr as ElectroGuitar;
                if (eGuitar != null)
                {
                    if (eGuitar.PowerSource.Equals("Fixed power", StringComparison.OrdinalIgnoreCase))
                        numberOfStrings += eGuitar.StringCount;
                }
            }
            if (numberOfStrings != 0)
                return numberOfStrings;
            else return -1;
        }

        public static int MaxNumberOfKeysOnOctave(Queue instruments)
        {

            int maxNumberOfKeys = -1;

            foreach (MusicalInstrument instr in instruments)
            {
                if (instr.GetType() == typeof(Piano))
                {
                    Piano piano = (Piano)instr;
                    if (piano.KeyLayout.Equals("Octave", StringComparison.OrdinalIgnoreCase))
                    {
                        maxNumberOfKeys = Math.Max(maxNumberOfKeys, piano.KeyCount);//if max<p.keys
                    }
                }
            }
            return maxNumberOfKeys;
        }
        static void PrintQueue(Queue queue)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("Queue is empty.");
                return;
            }

            int order = 1;

            foreach (var item in queue)
            {
                Console.WriteLine($"{order}) {item}");
                order++;
            }
        }

        static Queue DeepCopyQueue(Queue originalQueue)
        {
            Queue deepCopy = new Queue();

            foreach (var item in originalQueue)
            {
                if (item is ICloneable cloneableItem)
                {
                    deepCopy.Enqueue(cloneableItem.Clone());
                    Console.WriteLine(cloneableItem.Clone());
                }
                else
                {
                    throw new InvalidOperationException("Element does not support cloning");
                }
            }

            return deepCopy;
        }
        static void Main(string[] args)
        {
            #region Part 1

            Queue instrumentsQueue = new Queue();

            int size = 5;

            MusicalInstrument[] instruments = new MusicalInstrument[size];
            Random rnd = new Random();


            for (int i = 0; i < instruments.Length; i++)//initializing array of random stuff
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        instruments[i] = new MusicalInstrument();
                        break;
                    case 1:
                        instruments[i] = new Guitar();
                        break;
                    case 2:
                        instruments[i] = new ElectroGuitar();
                        break;
                    case 3:
                        instruments[i] = new Piano();
                        break;

                }
            }

            foreach (var instr in instruments) //making stuff random
            {
                instr.RandomInit();
                instrumentsQueue.Enqueue(instr);
            }

            Console.WriteLine("Created queue:");
            PrintQueue(instrumentsQueue);


            //adding to queue
            Console.WriteLine("Input how much objects to add");
            int sizeToAdd = ValidInput.GetInt();
            for (int i = 0; i < sizeToAdd; i++) 
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        MusicalInstrument instr = new MusicalInstrument();
                        instr.RandomInit();
                        instrumentsQueue.Enqueue(instr);
                        break;
                    case 1:
                        instr = new Guitar();
                        instr.RandomInit();
                        instrumentsQueue.Enqueue(instr);
                        break;
                    case 2:
                        instr = new ElectroGuitar();
                        instr.RandomInit();
                        instrumentsQueue.Enqueue(instr);
                        break;
                    case 3:
                        instr = new Piano();
                        instr.RandomInit();
                        instrumentsQueue.Enqueue(instr);
                        break;
                }
            }
            Console.WriteLine("Queue with added elements");
            PrintQueue(instrumentsQueue);


            //Deleting
            Console.WriteLine("Input number of items to delete from queue");
            int sizeToDelete = ValidInput.GetInt();
            while (sizeToDelete > instrumentsQueue.Count) 
            {
                Console.WriteLine("Can't delete more objects than in queue");
                sizeToDelete = ValidInput.GetInt();
            }

            for (int i = 0; i < sizeToDelete; i++)
            { 
                instrumentsQueue.Dequeue();
            }

            Console.WriteLine($"Queue after deleting {sizeToDelete} elements");
            PrintQueue(instrumentsQueue);

            //1 zapros
            if (AverageNumberOfStrings(instrumentsQueue) < 0)
                Console.WriteLine("Array has no guitars");
            else
                Console.WriteLine($"Average number of string is {AverageNumberOfStrings(instrumentsQueue)}");

            //2 zaproa
            if (NumberOfStringInElectroGuitarsWithFixedPower(instrumentsQueue) < 0)
                Console.WriteLine("There is no electroguitars with fixed source");

            else
                Console.WriteLine($"Number of strings in e-guitars with fixed power: {NumberOfStringInElectroGuitarsWithFixedPower(instrumentsQueue)}");

            //2 zapros
            if (MaxNumberOfKeysOnOctave(instrumentsQueue) < 0)
                Console.WriteLine($"There were no pianos with octave keyboard layout");
            else
                Console.WriteLine($"Max number of keys on octave keyboard is {MaxNumberOfKeysOnOctave(instrumentsQueue)}");

            //Cloning queue
            Queue cloneInstruments = new Queue();
            cloneInstruments = DeepCopyQueue(instrumentsQueue);
            Console.WriteLine("Cloned queue");
            PrintQueue(cloneInstruments);

            instrumentsQueue.Dequeue();
            instrumentsQueue.Dequeue();
            instrumentsQueue.Dequeue();
            Console.WriteLine("Original queue after deleting some:");
            PrintQueue(instrumentsQueue);
            Console.WriteLine("Cloned queue after deleting some stuff from original queue:");
            PrintQueue(cloneInstruments);


            #endregion
        }
    }
}
