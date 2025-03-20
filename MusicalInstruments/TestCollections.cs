using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicalInstruments
{
    public class TestCollections
    {
        private Queue<Piano> queuePianos;
        private Queue<string> queueStrings;
        private Dictionary<MusicalInstrument, Piano> dictionaryInstrumentToPiano;
        private Dictionary<string, Piano> dictionaryStringToPiano;

        public TestCollections(int count)
        {
            queuePianos = new Queue<Piano>();
            queueStrings = new Queue<string>();
            dictionaryInstrumentToPiano = new Dictionary<MusicalInstrument, Piano>();
            dictionaryStringToPiano = new Dictionary<string, Piano>();

            InitializeCollections(count);
        }

        private void InitializeCollections(int count)
        {
            Random rnd = new Random();
            HashSet<int> uniqueIDs = new HashSet<int>(); // Хранение уже использованных ID

            string[] names = { "Yamaha", "Roland", "Casio", "Korg", "Steinway", "Nord", "Kawai", "Bechstein", "Piano", "Cool Piano", "Saharoza", "Salt" };
            string[] layouts = { "Octave", "Scale", "Digital", "Acoustic", "C", "C-Sharp (D-Flat)", "Minor", "Major", "D", "D-Sharp (E-Flat)", "E", "F", "F-Sharp (G-Flat)" };

            for (int i = 0; i < count; i++)
            {
                // Генерация уникального ID
                int id;
                do
                {
                    id = rnd.Next(1, count * 10);
                } while (!uniqueIDs.Add(id));

                string name = names[rnd.Next(names.Length)] + i; // Уникальные имена
                string layout = layouts[rnd.Next(layouts.Length)];
                int keyCount = rnd.Next(25, 105);

                Piano piano = new Piano(name, id, layout, keyCount);

                queuePianos.Enqueue(piano);
                queueStrings.Enqueue(piano.ToString());

                if (!dictionaryInstrumentToPiano.ContainsKey(piano))
                    dictionaryInstrumentToPiano.Add(piano, piano);

                if (!dictionaryStringToPiano.ContainsKey(name))
                    dictionaryStringToPiano.Add(name, piano);
            }
        }

        public void PrintCollections()
        {
            Console.WriteLine("Queue<Piano>:");
            foreach (var item in queuePianos)
                Console.WriteLine(item);

            Console.WriteLine("\nQueue<string>:");
            foreach (var item in queueStrings)
                Console.WriteLine(item);

            Console.WriteLine("\nDictionary<MusicalInstrument, Piano>:");
            foreach (var pair in dictionaryInstrumentToPiano)
                Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");

            Console.WriteLine("\nDictionary<string, Piano>:");
            foreach (var pair in dictionaryStringToPiano)
                Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
        }

        public void MeasureSearchTime()
        {
            Console.WriteLine("\nМетод измерения времени поиска:");

            // Получаем элементы для поиска
            Piano firstPiano = queuePianos.Peek(); // Первый элемент
            Piano middlePiano = queuePianos.ElementAt(queuePianos.Count / 2); // Центральный элемент
            Piano lastPiano = queuePianos.Last(); // Последний элемент

            // Создаем "несуществующий" элемент с новой ссылкой, но с уникальными данными
            Piano nonExistingPiano = new Piano("NonExisting", 9999999, "Digital", 100);

            string firstName = queueStrings.Peek(); // Первый элемент
            string middleName = queueStrings.ElementAt(queueStrings.Count / 2); // Центральный элемент
            string lastName = queueStrings.Last(); // Последний элемент
            string nonExistingName = "NonExistingName"; // Не существующий элемент

            // Измеряем время поиска в очередях
            MeasureSearchTimeForQueue(queuePianos, firstPiano, middlePiano, lastPiano, nonExistingPiano);
            MeasureSearchTimeForQueue(queueStrings, firstName, middleName, lastName, nonExistingName);

            // Измеряем время поиска в словарях
            MeasureSearchTimeForDictionary(dictionaryInstrumentToPiano, firstPiano, middlePiano, lastPiano, nonExistingPiano);
            MeasureSearchTimeForDictionary(dictionaryStringToPiano, firstName, middleName, lastName, nonExistingName);
        }

        private void MeasureSearchTimeForQueue<T>(Queue<T> queue, T first, T middle, T last, T nonExisting)
        {
            Console.WriteLine($"\nИзмерение времени поиска в очереди типа {typeof(T)}:");
            Stopwatch stopwatch = new Stopwatch();

            // Поиск первого элемента
            stopwatch.Restart();
            bool containsFirst = queue.Contains(first);
            stopwatch.Stop();
            Console.WriteLine($"Поиск первого элемента: {(containsFirst ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            // Поиск центрального элемента
            stopwatch.Restart();
            bool containsMiddle = queue.Contains(middle);
            stopwatch.Stop();
            Console.WriteLine($"Поиск центрального элемента: {(containsMiddle ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            // Поиск последнего элемента
            stopwatch.Restart();
            bool containsLast = queue.Contains(last);
            stopwatch.Stop();
            Console.WriteLine($"Поиск последнего элемента: {(containsLast ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            // Поиск несуществующего элемента
            stopwatch.Restart();
            bool containsNonExisting = queue.Contains(nonExisting);
            stopwatch.Stop();
            Console.WriteLine($"Поиск несуществующего элемента: {(containsNonExisting ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");
        }

        private void MeasureSearchTimeForDictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey firstKey, TKey middleKey, TKey lastKey, TKey nonExistingKey)
        {
            Console.WriteLine($"\nИзмерение времени поиска в словаре с ключами типа {typeof(TKey)}:");
            Stopwatch stopwatch = new Stopwatch();

            // Поиск по ключу
            stopwatch.Restart();
            bool containsFirstKey = dictionary.ContainsKey(firstKey);
            stopwatch.Stop();
            Console.WriteLine($"Поиск по первому ключу: {(containsFirstKey ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            stopwatch.Restart();
            bool containsMiddleKey = dictionary.ContainsKey(middleKey);
            stopwatch.Stop();
            Console.WriteLine($"Поиск по центральному ключу: {(containsMiddleKey ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            stopwatch.Restart();
            bool containsLastKey = dictionary.ContainsKey(lastKey);
            stopwatch.Stop();
            Console.WriteLine($"Поиск по последнему ключу: {(containsLastKey ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            stopwatch.Restart();
            bool containsNonExistingKey = dictionary.ContainsKey(nonExistingKey);
            stopwatch.Stop();
            Console.WriteLine($"Поиск по несуществующему ключу: {(containsNonExistingKey ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");

            // Поиск по значению
            if (dictionary.ContainsKey(firstKey))
            {
                stopwatch.Restart();
                bool containsFirstValue = dictionary.ContainsValue(dictionary[firstKey]);
                stopwatch.Stop();
                Console.WriteLine($"Поиск по первому значению: {(containsFirstValue ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");
            }
            else
            {
                Console.WriteLine("Первый ключ не найден в словаре.");
            }

            if (dictionary.ContainsKey(middleKey))
            {
                stopwatch.Restart();
                bool containsMiddleValue = dictionary.ContainsValue(dictionary[middleKey]);
                stopwatch.Stop();
                Console.WriteLine($"Поиск по центральному значению: {(containsMiddleValue ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");
            }
            else
            {
                Console.WriteLine("Центральный ключ не найден в словаре.");
            }

            if (dictionary.ContainsKey(lastKey))
            {
                stopwatch.Restart();
                bool containsLastValue = dictionary.ContainsValue(dictionary[lastKey]);
                stopwatch.Stop();
                Console.WriteLine($"Поиск по последнему значению: {(containsLastValue ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");
            }
            else
            {
                Console.WriteLine("Последний ключ не найден в словаре.");
            }

            if (dictionary.ContainsKey(nonExistingKey))
            {
                stopwatch.Restart();
                bool containsNonExistingValue = dictionary.ContainsValue(dictionary[nonExistingKey]);
                stopwatch.Stop();
                Console.WriteLine($"Поиск по несуществующему значению: {(containsNonExistingValue ? "Найден" : "Не найден")}, Время: {stopwatch.ElapsedTicks} тиков");
            }
            else
            {
                Console.WriteLine("Несуществующий ключ не найден в словаре.");
            }
        }
    }
}
