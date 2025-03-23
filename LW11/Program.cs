using MusicalInstruments;
using System.Collections;
using System;
using System.Dynamic;
using System.Transactions;
using System.Collections.Generic;
using System.Reflection;


namespace LW11
{
    internal class Program
    {
        #region Helper func
        static string InputInstrumentType()
        {
            string instrType;
            Console.WriteLine("Choose an instrument: \n1) - Musical instrument (name, id)\n2) - Guitar (name, id, num of strings)\n3) - Electroguitar (guitar + power source)\n4) - Piano (name, id, key layout, number of keys)");
            int typeInt;
            typeInt = ValidInput.GetPositiveInt();
            while (typeInt > 5) 
            {
                Console.WriteLine("Enter a number between 1 and 4");
                typeInt = ValidInput.GetPositiveInt();
            }
            switch (typeInt)
            {
                case 1:
                    instrType = "MusicalInstrument";
                    break;
                case 2:
                    instrType = "Guitar";
                    break;
                case 3:
                    instrType = "Eguitar";
                    break;
                case 4:
                    instrType = "Piano";
                    break;
                default:
                    instrType = "Aboba";
                    break;
            }
            return instrType;
        }
        
        static MusicalInstrument GetMusicalInstrument()
        {
            string instrType;
            instrType = InputInstrumentType();
            MusicalInstrument instr = new();
            switch (instrType)
            {
                case "MusicalInstrument":
                    instr = new MusicalInstrument();
                    instr.Init();
                    break;
                case "Guitar":
                    instr = new Guitar();
                    instr.Init();
                    break;
                case "Eguitar":
                    instr = new ElectroGuitar();
                    instr.Init();
                    break;
                case "Piano":
                    instr = new Piano();
                    instr.Init();
                    break;
                default:
                    Console.WriteLine("An unexpected error ocurred");
                    break;

            }
            return instr;
        }
        #endregion
        #region Queue Functions
        static Queue AddToQueue(Queue queue)
        {
            MusicalInstrument instrument;
            instrument = GetMusicalInstrument();
            queue.Enqueue(instrument);
            return queue;
        }
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
        static Queue SortQueue(Queue queue)
        {
            int i = 0;
            Queue sortedQueue = new Queue();

            MusicalInstrument[] helperArray = new MusicalInstrument[queue.Count];
            MusicalInstrument[] sortedArr = new MusicalInstrument[queue.Count];

            queue.CopyTo(helperArray, 0);

            for (i = 0; i < helperArray.Length; i++) 
            {
                if (helperArray[i] is ICloneable itemToClone)
                {
                    sortedArr[i] = (MusicalInstrument)itemToClone.Clone();//cloniruem every elem in array
                }
                else
                    throw new InvalidOperationException("Element does not support cloning");
            }


            Array.Sort(sortedArr);//sorting by name 

            foreach (var item in sortedArr)
            {
                sortedQueue.Enqueue(item);
            }

            return sortedQueue;
        }
        static Queue DeleteByPosition(Queue queue, int toDelete)
        {
            Queue delQueue = new Queue();
            int num = 1;
            foreach (var item in queue) 
            {
                if (num == toDelete)
                    num++;
                else
                { 
                    delQueue.Enqueue(item);
                    num++;
                }

            }
            return delQueue;
        }
        static Queue DeleteInstrument(Queue queue)
        {
            MusicalInstrument instrument = null;
            instrument = GetMusicalInstrument();
            Queue delQueue = new Queue();
            bool isFound = false;
            foreach (var item in queue)
            {
                if (item.GetType() == instrument.GetType())
                    if (item.Equals(instrument))
                    {
                        Console.WriteLine("Instrument deleted");
                        isFound = true;
                    }
                    else
                        delQueue.Enqueue(item);
                else
                    delQueue.Enqueue(item);
            }
            if (!isFound)
                Console.WriteLine("Instrument not found");
            return delQueue;
        }
        static MusicalInstrument FindInstrument(Queue queue)
        {
            MusicalInstrument instr = new MusicalInstrument();
            instr = GetMusicalInstrument();
            int count = 0;
            if (queue.Count == 0 || queue == null)
            {
                Console.WriteLine("Queue is empty");
                return default;
            }
            else if (!queue.Contains(instr))
            {
                Console.WriteLine("Element not found");
            }
            else 
            {
                foreach (var item in queue)
                {
                    count++;
                    if (item.GetType() == instr.GetType())
                        if (item.Equals(instr))
                        {
                            Console.WriteLine($"Item found after {count} comparisons");
                            return instr;
                        }
                            
                }
            }
            return default;
        }
        #endregion
        #region Stack thingies
        static void PrintStack(IEnumerable<MusicalInstrument> stack)
        {
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }

        static Stack<MusicalInstrument> AddToStack(Stack<MusicalInstrument> stack)
        {
            MusicalInstrument instrument = GetMusicalInstrument();
            stack.Push(instrument);
            return stack;
        }
        static Stack<MusicalInstrument> DeleteInstrument(Stack<MusicalInstrument> stack)
        {
            MusicalInstrument instrument = GetMusicalInstrument();
            Stack<MusicalInstrument> tempStack = new Stack<MusicalInstrument>();
            bool isFound = false;

            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item.Equals(instrument))
                {
                    Console.WriteLine("Instrument deleted");
                    isFound = true;
                }
                else
                {
                    tempStack.Push(item);
                }
            }

            // Восстанавливаем стек
            while (tempStack.Count > 0)
            {
                stack.Push(tempStack.Pop());
            }

            if (!isFound)
            {
                Console.WriteLine("Instrument not found");
            }

            return stack;
        }
        static MusicalInstrument FindInstrument(Stack<MusicalInstrument> stack)
        {
            Console.WriteLine("Enter the instrument to search for:");
            MusicalInstrument target = GetMusicalInstrument();
            int count = 0;

            foreach (var item in stack)
            {
                count++;
                // Проверяем, что типы объектов совпадают
                if (item.GetType() == target.GetType())
                {
                    // Сравниваем объекты только если они одного типа
                    if (item.Equals(target))
                    {
                        Console.WriteLine($"Item found after {count} comparisons.");
                        return item;
                    }
                }
            }

            // Если элемент не найден
            Console.WriteLine("Item not found in the stack.");
            return null;
        }
        static Stack<MusicalInstrument> DeepCopyStack(Stack<MusicalInstrument> originalStack)
        {
            Stack<MusicalInstrument> deepCopy = new Stack<MusicalInstrument>();
            MusicalInstrument[] helperArray = originalStack.ToArray();

            foreach (var item in helperArray)
            {
                if (item is ICloneable cloneableItem)
                {
                    deepCopy.Push((MusicalInstrument)cloneableItem.Clone());
                }
                else
                {
                    throw new InvalidOperationException("Element does not support cloning");
                }
            }

            return deepCopy;
        }
        static Stack<MusicalInstrument> SortStack(Stack<MusicalInstrument> stack)
        {
            MusicalInstrument[] helperArray = stack.ToArray();
            Array.Sort(helperArray, (x, y) => x.Name.CompareTo(y.Name)); // Сортировка по имени

            Stack<MusicalInstrument> sortedStack = new Stack<MusicalInstrument>();
            foreach (var item in helperArray)
            {
                sortedStack.Push(item);
            }

            return sortedStack;
        }
        public static double AverageNumberOfStrings(Stack<MusicalInstrument> instruments)
        {
            Stack<MusicalInstrument> tempStack = new Stack<MusicalInstrument>();
            double count = 0;
            double numberOfStrings = 0;

            // Перекладываем элементы во временный стек, проверяя их
            while (instruments.Count > 0)
            {
                var instr = instruments.Pop();
                if (instr is Guitar guitar) // Проверяем, является ли инструмент гитарой
                {
                    numberOfStrings += guitar.StringCount;
                    count++;
                }
                tempStack.Push(instr); // Сохраняем элемент во временном стеке
            }

            // Восстанавливаем исходный порядок элементов
            while (tempStack.Count > 0)
            {
                instruments.Push(tempStack.Pop());
            }

            // Вычисляем среднее значение
            if (count != 0)
                return numberOfStrings / count;
            else
                return -1;
        }
        public static int MaxNumberOfKeysOnOctave(Stack<MusicalInstrument> instruments)
        {
            Stack<MusicalInstrument> tempStack = new Stack<MusicalInstrument>();
            int maxNumberOfKeys = -1;

            while (instruments.Count > 0)
            {
                var instr = instruments.Pop();
                if (instr is Piano piano && piano.KeyLayout.Equals("Octave", StringComparison.OrdinalIgnoreCase))
                {
                    maxNumberOfKeys = Math.Max(maxNumberOfKeys, piano.KeyCount);
                }
                tempStack.Push(instr);
            }

            while (tempStack.Count > 0)
            {
                instruments.Push(tempStack.Pop());
            }

            return maxNumberOfKeys;
        }
        public static int NumberOfStringInElectroGuitarsWithFixedPower(Stack<MusicalInstrument> instruments)
        {
            Stack<MusicalInstrument> tempStack = new Stack<MusicalInstrument>();
            int numberOfStrings = 0;

            while (instruments.Count > 0)
            {
                var instr = instruments.Pop();
                if (instr is ElectroGuitar eGuitar && eGuitar.PowerSource.Equals("Fixed power", StringComparison.OrdinalIgnoreCase))
                {
                    numberOfStrings += eGuitar.StringCount;
                }
                tempStack.Push(instr);
            }

            while (tempStack.Count > 0)
            {
                instruments.Push(tempStack.Pop());
            }

            return numberOfStrings;
        }


        #endregion
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

            //Adding to queue
            Console.WriteLine("Adding");
            instrumentsQueue = AddToQueue(instrumentsQueue);
            PrintQueue(instrumentsQueue);
            Console.ReadLine();
            Console.Clear();

            //Deleting
            Console.WriteLine("Deleting");
            PrintQueue(instrumentsQueue);
            instrumentsQueue = DeleteInstrument(instrumentsQueue);
            PrintQueue(instrumentsQueue);
            Console.ReadLine();
            Console.Clear();

            //Zaprosi
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
            Console.ReadLine();
            Console.Clear();

            //sorting 
            Queue sortedQueue = SortQueue(instrumentsQueue);
            Console.WriteLine("Unsorted queue");
            PrintQueue(instrumentsQueue);
            Console.WriteLine("Sorted queue");
            PrintQueue(sortedQueue);

            //Find elem
            MusicalInstrument target;
            target = FindInstrument(instrumentsQueue);
            if (target == null)
                Console.WriteLine("item not found");
            else
                Console.WriteLine(target = FindInstrument(instrumentsQueue));





            #endregion

            #region Part 2
            Console.WriteLine("End of part 1");
            Console.ReadLine();
            Console.Clear();
            Stack<MusicalInstrument> instrumentsStack = new Stack<MusicalInstrument>();
            Console.WriteLine("Input how much objects to add");
            SortStack(instrumentsStack);
            DeleteInstrument(instrumentsStack);
            FindInstrument(instrumentsStack);
            //1 zapros
            if (AverageNumberOfStrings(instrumentsStack) < 0)
                Console.WriteLine("Array has no guitars");
            else
                Console.WriteLine($"Average number of string is {AverageNumberOfStrings(instrumentsStack)}");

            //2 zaproa
            if (NumberOfStringInElectroGuitarsWithFixedPower(instrumentsStack) < 0)
                Console.WriteLine("There is no electroguitars with fixed source");

            else
                Console.WriteLine($"Number of strings in e-guitars with fixed power: {NumberOfStringInElectroGuitarsWithFixedPower(instrumentsStack)}");

            //2 zapros
            if (MaxNumberOfKeysOnOctave(instrumentsStack) < 0)
                Console.WriteLine($"There were no pianos with octave keyboard layout");
            else
                Console.WriteLine($"Max number of keys on octave keyboard is {MaxNumberOfKeysOnOctave(instrumentsStack)}");








            //#endregion

            #region Part 3
            // Создаем экземпляр с 1000 элементами
            TestCollections test = new TestCollections(1000);

            // Выводим коллекции (для проверки)
            Console.WriteLine("Collections initialized. Press Enter to start searches...");
            Console.ReadLine();

            // Поиск в очереди Piano
            Console.WriteLine("\nSearching in Queue<Piano>:");
            test.FindItemInQueue(test.first, "first");
            test.FindItemInQueue(test.middle, "middle");
            test.FindItemInQueue(test.last, "last");
            test.FindItemInQueue(test.noexist, "non-existent");

            // Поиск в очереди строк
            Console.WriteLine("\nSearching in Queue<string>:");
            test.FindItemInStringQueue(test.first.ToString(), "first");
            test.FindItemInStringQueue(test.middle.ToString(), "middle");
            test.FindItemInStringQueue(test.last.ToString(), "last");
            test.FindItemInStringQueue("NonExistingName", "non-existent");

            // Поиск в словаре с ключами MusicalInstrument
            Console.WriteLine("\nSearching in Dictionary<MusicalInstrument, Piano> (keys):");
            test.FindItemInInstrumentDictionary(test.first.GetBase, "first");
            test.FindItemInInstrumentDictionary(test.middle.GetBase, "middle");
            test.FindItemInInstrumentDictionary(test.last.GetBase, "last");
            test.FindItemInInstrumentDictionary(test.noexist.GetBase, "non-existent");

            // Поиск в словаре с ключами string
            Console.WriteLine("\nSearching in Dictionary<string, Piano> (keys):");
            test.FindItemInStringDictionary(test.first.GetBase.ToString(), "first");
            test.FindItemInStringDictionary(test.middle.GetBase.ToString(), "middle");
            test.FindItemInStringDictionary(test.last.GetBase.ToString(), "last");
            test.FindItemInStringDictionary("NonExistingKey", "non-existent");

            // Поиск по значениям в словарях (медленно)
            Console.WriteLine("\nSearching by VALUES in dictionaries (slower):");
            test.FindValueInInstrumentDictionary(test.first, "first");
            test.FindValueInInstrumentDictionary(test.middle, "middle");
            test.FindValueInInstrumentDictionary(test.last, "last");
            test.FindValueInInstrumentDictionary(test.noexist, "non-existent");

            test.FindValueInStringDictionary(test.first, "first");
            test.FindValueInStringDictionary(test.middle, "middle");
            test.FindValueInStringDictionary(test.last, "last");
            test.FindValueInStringDictionary(test.noexist, "non-existent");
            #endregion
        }
    }
}
