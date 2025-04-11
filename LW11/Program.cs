using MusicalInstruments;
using System.Collections;
using System;
using System.Dynamic;


namespace LW11
{
    internal class Program
    {
        #region Queue Functions
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
        #endregion
        #region Stack thingies
        static void PrintStack(IEnumerable<MusicalInstrument> stack)
        {
            foreach (var item in stack)
            {
                Console.WriteLine(item);
            }
        }

        static Stack<MusicalInstrument> DeleteByNumStack(Stack<MusicalInstrument> stack, int toDelete)
        {
            if (toDelete <= 0)
            {
                throw new ArgumentException("Позиция должна быть больше 0.");
            }

            Stack<MusicalInstrument> tempStack = new Stack<MusicalInstrument>();
            int num = 1;

            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (num != toDelete)
                {
                    tempStack.Push(item);
                }
                num++;
            }

            // Восстанавливаем исходный порядок элементов
            while (tempStack.Count > 0)
            {
                stack.Push(tempStack.Pop());
            }

            return stack;
        }


        #endregion
        static void Main(string[] args)
        {
            //#region Part 1

            

<<<<<<< Updated upstream
            Queue instrumentsQueue = new Queue();
            
=======
            //Queue instrumentsQueue = new Queue();

>>>>>>> Stashed changes

            //int size = 5;

            //MusicalInstrument[] instruments = new MusicalInstrument[size];
            //Random rnd = new Random();


            //for (int i = 0; i < instruments.Length; i++)//initializing array of random stuff
            //{
            //    switch (rnd.Next(4))
            //    {
            //        case 0:
            //            instruments[i] = new MusicalInstrument();
            //            break;
            //        case 1:
            //            instruments[i] = new Guitar();
            //            break;
            //        case 2:
            //            instruments[i] = new ElectroGuitar();
            //            break;
            //        case 3:
            //            instruments[i] = new Piano();
            //            break;

            //    }
            //}

            //foreach (var instr in instruments) //making stuff random
            //{
            //    instr.RandomInit();
            //    instrumentsQueue.Enqueue(instr);
            //}

            //Console.WriteLine("Created queue:");
            //PrintQueue(instrumentsQueue);

<<<<<<< Updated upstream

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
            Console.WriteLine("Input number of elem to delete");
            int posToDelete = ValidInput.GetInt();
            instrumentsQueue = DeleteByPosition(instrumentsQueue, posToDelete);

            Console.WriteLine($"Queue after deleting {posToDelete} element");
            PrintQueue(instrumentsQueue);

            //1 zapros
            if (AverageNumberOfStrings(instrumentsQueue) < 0)
                Console.WriteLine("Array has no guitars");
            else
                Console.WriteLine($"Average number of string is {AverageNumberOfStrings(instrumentsQueue)}");
=======
            ////Adding to queue
            //Console.WriteLine("Adding");
            //instrumentsQueue = AddToQueue(instrumentsQueue);
            //PrintQueue(instrumentsQueue);
            //Console.ReadLine();
            //Console.Clear();

            ////Deleting
            //Console.WriteLine("Deleting");
            //PrintQueue(instrumentsQueue);
            //instrumentsQueue = DeleteInstrument(instrumentsQueue);
            //PrintQueue(instrumentsQueue);
            //Console.ReadLine();
            //Console.Clear();

            ////Zaprosi
            ////1 zapros
            //if (AverageNumberOfStrings(instrumentsQueue) < 0)
            //    Console.WriteLine("Array has no guitars");
            //else
            //    Console.WriteLine($"Average number of string is {AverageNumberOfStrings(instrumentsQueue)}");
>>>>>>> Stashed changes

            ////2 zaproa
            //if (NumberOfStringInElectroGuitarsWithFixedPower(instrumentsQueue) < 0)
            //    Console.WriteLine("There is no electroguitars with fixed source");

            //else
            //    Console.WriteLine($"Number of strings in e-guitars with fixed power: {NumberOfStringInElectroGuitarsWithFixedPower(instrumentsQueue)}");

            ////2 zapros
            //if (MaxNumberOfKeysOnOctave(instrumentsQueue) < 0)
            //    Console.WriteLine($"There were no pianos with octave keyboard layout");
            //else
            //    Console.WriteLine($"Max number of keys on octave keyboard is {MaxNumberOfKeysOnOctave(instrumentsQueue)}");

            ////Cloning queue
            //Queue cloneInstruments = new Queue();
            //cloneInstruments = DeepCopyQueue(instrumentsQueue);
            //Console.WriteLine("Cloned queue");
            //PrintQueue(cloneInstruments);

            //instrumentsQueue.Dequeue();
            //instrumentsQueue.Dequeue();
            //instrumentsQueue.Dequeue();
            //Console.WriteLine("Original queue after deleting some:");
            //PrintQueue(instrumentsQueue);
            //Console.WriteLine("Cloned queue after deleting some stuff from original queue:");
            //PrintQueue(cloneInstruments);
            //Console.ReadLine();
            //Console.Clear();

<<<<<<< Updated upstream

            //sorting 
            Queue sortedQueue = SortQueue(instrumentsQueue);
            Console.WriteLine("Unsorted queue");
            PrintQueue(instrumentsQueue);
            Console.WriteLine("Sorted queue");
            PrintQueue(sortedQueue);

            #endregion


            #region Part 2
            Console.WriteLine("End of part 1");
            Console.ReadLine();
            Console.Clear();
            Stack<MusicalInstrument> instrumentsStack = new Stack<MusicalInstrument>();
            Console.WriteLine("Input how much objects to add");
            sizeToAdd = ValidInput.GetInt();
            for (int i = 0; i < sizeToAdd; i++)
            {
                switch (rnd.Next(4))
                {
                    case 0:
                        MusicalInstrument instr = new MusicalInstrument();
                        instr.RandomInit();
                        instrumentsStack.Push(instr);
                        break;
                    case 1:
                        instr = new Guitar();
                        instr.RandomInit();
                        instrumentsStack.Push(instr);
                        break;
                    case 2:
                        instr = new ElectroGuitar();
                        instr.RandomInit();
                        instrumentsStack.Push(instr);
                        break;
                    case 3:
                        instr = new Piano();
                        instr.RandomInit();
                        instrumentsStack.Push(instr);
                        break;
                }
            }
            PrintStack(instrumentsStack);

            Console.WriteLine("Enter a number of element to delete");

            posToDelete = ValidInput.GetInt();

            instrumentsStack =DeleteByNumStack(instrumentsStack, posToDelete);
            PrintStack(instrumentsStack);
=======
            ////sorting 
            //Queue sortedQueue = SortQueue(instrumentsQueue);
            //Console.WriteLine("Unsorted queue");
            //PrintQueue(instrumentsQueue);
            //Console.WriteLine("Sorted queue");
            //PrintQueue(sortedQueue);

            ////Find elem
            //MusicalInstrument target;
            //target = FindInstrument(instrumentsQueue);
            //if (target == null)
            //    Console.WriteLine("item not found");
            //else
            //    Console.WriteLine(target = FindInstrument(instrumentsQueue));





            //#endregion

            //#region Part 2
            //Console.WriteLine("End of part 1");
            //Console.ReadLine();
            //Console.Clear();
            //Stack<MusicalInstrument> instrumentsStack = new Stack<MusicalInstrument>();
            //Console.WriteLine("Input how much objects to add");
            //SortStack(instrumentsStack);
            //DeleteInstrument(instrumentsStack);
            //FindInstrument(instrumentsStack);
            ////1 zapros
            //if (AverageNumberOfStrings(instrumentsStack) < 0)
            //    Console.WriteLine("Array has no guitars");
            //else
            //    Console.WriteLine($"Average number of string is {AverageNumberOfStrings(instrumentsStack)}");

            ////2 zaproa
            //if (NumberOfStringInElectroGuitarsWithFixedPower(instrumentsStack) < 0)
            //    Console.WriteLine("There is no electroguitars with fixed source");

            //else
            //    Console.WriteLine($"Number of strings in e-guitars with fixed power: {NumberOfStringInElectroGuitarsWithFixedPower(instrumentsStack)}");

            ////2 zapros
            //if (MaxNumberOfKeysOnOctave(instrumentsStack) < 0)
            //    Console.WriteLine($"There were no pianos with octave keyboard layout");
            //else
            //    Console.WriteLine($"Max number of keys on octave keyboard is {MaxNumberOfKeysOnOctave(instrumentsStack)}");
>>>>>>> Stashed changes






<<<<<<< Updated upstream
=======


            //#endregion

            #region Part 3
            // Создаем экземпляр с 1000 элементами
            TestCollections test = new TestCollections(1000);


            // Выводим коллекции (для проверки)
            Console.WriteLine("Collections initialized. Press Enter to start searches...");
            Console.ReadLine();
            int ticks = 0;
            
            
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
            
>>>>>>> Stashed changes
            #endregion
        }
    }
}
