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

        static Queue DeletePiano(Queue queue, Piano pianoToDel)
        {
            Queue delQueue = new Queue();
            bool isFound = false;
            foreach (var item in queue)
            {
                if (item.GetType() == typeof(Piano))
                    if (item.Equals(pianoToDel))
                    {
                        Console.WriteLine("Piano deleted");
                        isFound = true;
                    }
                    else
                        delQueue.Enqueue(item);
                else
                    delQueue.Enqueue(item);
            }
            if (!isFound)
                Console.WriteLine("Piano not found");
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

        public static Stack<MusicalInstrument> DeletePiano(Stack<MusicalInstrument> stack, Piano pianoToDel)
        {
            Stack<MusicalInstrument> tempStack = new Stack<MusicalInstrument>();
            bool isFound = false;

            // all elems go to temp, until a piano is found
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                if (item is Piano piano && piano.Equals(pianoToDel))
                {
                    Console.WriteLine("Piano deleted");
                    isFound = true;
                    // dont put found item in stack
                }
                else
                {
                    tempStack.Push(item);
                }
            }

            // put elements bak in right order
            while (tempStack.Count > 0)
            {
                stack.Push(tempStack.Pop());
            }

            if (!isFound)
            {
                Console.WriteLine("Piano not found");
            }

            return stack;
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



        #endregion
        static void Main(string[] args)
        {
            //#region Part 1

            

            //Queue instrumentsQueue = new Queue();
            

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


            ////adding to queue
            //Console.WriteLine("Input how much objects to add");
            //int sizeToAdd = ValidInput.GetInt();
            //for (int i = 0; i < sizeToAdd; i++) 
            //{
            //    switch (rnd.Next(4))
            //    {
            //        case 0:
            //            MusicalInstrument instr = new MusicalInstrument();
            //            instr.RandomInit();
            //            instrumentsQueue.Enqueue(instr);
            //            break;
            //        case 1:
            //            instr = new Guitar();
            //            instr.RandomInit();
            //            instrumentsQueue.Enqueue(instr);
            //            break;
            //        case 2:
            //            instr = new ElectroGuitar();
            //            instr.RandomInit();
            //            instrumentsQueue.Enqueue(instr);
            //            break;
            //        case 3:
            //            instr = new Piano();
            //            instr.RandomInit();
            //            instrumentsQueue.Enqueue(instr);
            //            break;
            //    }
            //}
            //Console.WriteLine("Queue with added elements");
            //PrintQueue(instrumentsQueue);
            //Console.WriteLine("Enter a piano");
            //Piano newPiano = new Piano();
            //newPiano.Init();
            //instrumentsQueue.Enqueue(newPiano);

            ////Deleting
            //Console.WriteLine("Input number of elem to delete");
            //int posToDelete = ValidInput.GetInt();
            //instrumentsQueue = DeleteByPosition(instrumentsQueue, posToDelete);

            //Console.WriteLine($"Queue after deleting {posToDelete} element");
            //PrintQueue(instrumentsQueue);

            //Console.WriteLine("Enter a piano to delete (name, id, keycount, keylayout)");
            //Piano pianoToDel = new Piano();
            //pianoToDel.Init();
            //instrumentsQueue = DeletePiano(instrumentsQueue, pianoToDel);
            //Console.WriteLine("Queue after deleting (or not) piano");
            //PrintQueue(instrumentsQueue);
            //Console.WriteLine("Enter anything to proceed");
            //Console.ReadLine();
            //Console.Clear();
            //PrintQueue(instrumentsQueue);
            ////1 zapros
            //if (AverageNumberOfStrings(instrumentsQueue) < 0)
            //    Console.WriteLine("Array has no guitars");
            //else
            //    Console.WriteLine($"Average number of string is {AverageNumberOfStrings(instrumentsQueue)}");

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


            ////sorting 
            //Queue sortedQueue = SortQueue(instrumentsQueue);
            //Console.WriteLine("Unsorted queue");
            //PrintQueue(instrumentsQueue);
            //Console.WriteLine("Sorted queue");
            //PrintQueue(sortedQueue);

            //#endregion

            //#region Part 2
            //Console.WriteLine("End of part 1");
            //Console.ReadLine();
            //Console.Clear();
            //Stack<MusicalInstrument> instrumentsStack = new Stack<MusicalInstrument>();
            //Console.WriteLine("Input how much objects to add");
            //sizeToAdd = ValidInput.GetInt();
            //for (int i = 0; i < sizeToAdd; i++)
            //{
            //    switch (rnd.Next(4))
            //    {
            //        case 0:
            //            MusicalInstrument instr = new MusicalInstrument();
            //            instr.RandomInit();
            //            instrumentsStack.Push(instr);
            //            break;
            //        case 1:
            //            instr = new Guitar();
            //            instr.RandomInit();
            //            instrumentsStack.Push(instr);
            //            break;
            //        case 2:
            //            instr = new ElectroGuitar();
            //            instr.RandomInit();
            //            instrumentsStack.Push(instr);
            //            break;
            //        case 3:
            //            instr = new Piano();
            //            instr.RandomInit();
            //            instrumentsStack.Push(instr);
            //            break;
            //    }
            //}
            //PrintStack(instrumentsStack);

            //Console.WriteLine("Enter a piano to add");

            //Piano piano1 = new Piano();
            //piano1.Init();
            //instrumentsStack.Push(piano1);
            //PrintStack(instrumentsStack);
            ////delete
            //Console.WriteLine("Enter a piano to delete");

            //pianoToDel.Init();

            //instrumentsStack = DeletePiano(instrumentsStack, pianoToDel);

            //PrintStack(instrumentsStack);







            //#endregion

            #region Part 3
            TestCollections testCollections = new TestCollections(1000);
            testCollections.MeasureSearchTime();
            #endregion
        }
    }
}
