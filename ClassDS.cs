using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DSAA1  //Data Structures And Algorithms
{
    public class ClassDataProcessing
    {
        private Random rand = new Random(); //рандом - переменная для генерации случайных элементов массива
        private string path = Directory.GetCurrentDirectory() + "/array.txt"; //путь к файлу для записи







        //todo static методы
        //в сравнениях исользовать equals и поискатьметод для сравнения > <
        //дополнить test, проверка issorted, больше проверок поиска(вариант когда ищется первое/последнее значение)
        // начать добавлять графики по времени выполнения операций
        //добавить git/github





        //Создает и возвращает целочисленный массив длины length чисел в диапазоне min - max
        public int[] createIntArray(uint length, int min, int max) {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(min, max+1);
            }

            return array;
        }


        //Создает и возвращает вещественный массив длины length чисел в диапазоне min - max
        public double[] createDoubleArray(uint length, int min, int max)
        {
            double[] array = new double[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(min, max+1) + Math.Round(rand.NextDouble(), 3);
            }

            return array;
        }

        //Создает и возвращает массив символов длины length символов в диапазоне min - max
        public char[] createCharArray(uint length, char min, char max)
        {
            char[] array = new char[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = (char)rand.Next(min, max + 1);
            }

            return array;
        }

        //Создает и возвращает массив строк длины length с длиной строк в диапазоне min - max
        public string[] createStringArray(uint length, int minStrLen, int maxStrLen)
        {
            int lenstr; //длина конкрутной строки
            string[] array = new string[length];
            for (int i = 0; i < array.Length; i++)
            {
                lenstr = rand.Next(minStrLen, maxStrLen);
                for (int j = 0; j < lenstr; j++) {
                    array[i] += (char)rand.Next('a', 'z' + 1);
                }
            }

            return array;
        }

        //Создает и возвращает массив булевых значений длины length
        public bool[] createBoolArray(uint length)
        {
            int rndBool;
            bool[] array = new bool[length]; //целочисленная переменная для случ. значений 
            for (int i = 0; i < array.Length; i++)
            {

                    rndBool = rand.Next(0, 2);
                if (rndBool == 0)
                {
                    array[i] = false;
                }
                else
                {
                    array[i] = true;
                }
            }

            return array;
        }


        //сохранение массива любого стандартного типа в файл
        public void SaveInFile<anyType>(anyType[] array) {
            //в параметры передаем путь к файлу и булевское значение - то, нужно ли перезаписать файл или дозаписать в него данные
            StreamWriter file = new StreamWriter(this.path, true); //создаем поток для записи

            for (int i = 0; i < array.Length; i++) {
                file.WriteLine(array[i]);
            }
            file.Close(); 
        }

        //вывод на экран массива любого стандартного типа
        public void PrintArray<anytype>(anytype[] array) {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }

        //сортировка массива любого численного стандартного типа методом расчески
        public anytype[] CombSortArray<anytype>(anytype[] array) {
            double gap = array.Length; //берем изначальный шаг за длину массива
            bool swaps = true;          //для определения необходимости дальнейшей сортировки
            while (gap > 1 || swaps)
            {
                gap /= 1.247330950103979;  //уменьшаем шаг(это число считается оптимальным для деления на него пред. шага)
                if (gap < 1) { gap = 1; }  //не даем шагу быть < 1
                //по достижению gap = 1 становится пузырьковым методом
                int i = 0;
                swaps = false; //перед проверкой массива на сортировку делаем лог. значение ложным
                while (i + gap < array.Length) //двигаемся указателем прибавляя шаг(gap)
                {

                    var igap = i + (int)gap; //меняем индекс сравниваемого элемента на каждомшаге
                    if (Convert.ToDouble(array[i]) > Convert.ToDouble(array[igap])) //если число перед больше числа после, то меняем их местами
                    {
                        var swap = array[i];
                        array[i] = array[igap];
                        array[igap] = swap;
                        swaps = true;
                    }
                    i++;
                }
            }
            return array;
        }

        //сортировка массива любого численного стандартного типа пузырьковым методом
        public anytype[] BubbleSortArray<anytype>(anytype[] array) {

            bool swaps = true;
            while (swaps) {

                swaps = false;
                for (int i = 0; i < array.Length-1; i++) {
                    if (Convert.ToDouble(array[i]) > Convert.ToDouble(array[i + 1])) {
                        swaps = true;
                        var swap = array[i];
                        array[i] = array[i+1];
                        array[i+1] = swap;
                    }
                }
            }
            return array;
        }

        //быстрая сортировка массива любого численного стандартного типа
        public anytype[] FastSortArray<anytype>(anytype[] array, int leftIndex, int rightIndex)
        {

            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (Convert.ToInt32(array[i]) > Convert.ToInt32(pivot))
                {
                    i++;
                }

                while (Convert.ToInt32(array[j]) > Convert.ToInt32(pivot))
                {
                    j--;
                }
                if (i <= j)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                FastSortArray(array, leftIndex, j);
            if (i < rightIndex)
                FastSortArray(array, i, rightIndex);
            return array;
        }

        //метод возвращающий индекс опорного элемента
        static int Partition<anytype>(anytype[] array, int minIndex, int maxIndex)
        {
            var temp = array[0]; //объявление переменной для обмена
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (Convert.ToDouble(array[i]) < Convert.ToDouble(array[maxIndex]))
                {
                    pivot++;
                    temp = array[pivot];
                    array[pivot] = array[i];
                    array[i] = temp;
                }
            }

            pivot++;
            temp = array[pivot];
            array[pivot] = array[maxIndex];
            array[maxIndex] = temp;
            return pivot;
        }

        //быстрая сортировка
        static anytype[] QuickSort<anytype>(anytype[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        //метод для работы RadixSort поразрядной сортировки
        public static void CountingSort<anytype>(anytype[] array, int exponent)
        {
            var outputArr = new anytype[array.Length];
            var occurences = new int[10];
            for (int i = 0; i < 10; i++)
                occurences[i] = 0;
            for (int i = 0; i < array.Length; i++)
                occurences[(Convert.ToInt32(array[i]) / exponent) % 10]++;
            for (int i = 1; i < 10; i++)
                occurences[i] += occurences[i - 1];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                outputArr[occurences[(Convert.ToInt32(array[i].ToString()) / exponent) % 10] - 1] = array[i];
                occurences[(Convert.ToInt32(array[i]) / exponent) % 10]--;
            }
            for (int i = 0; i < array.Length; i++)
                array[i] = outputArr[i];
        }

        //поразрядная сортировка целочисленного типа
        public anytype[] RadixSortArray<anytype>(anytype[] array)
        {
            var maxVal = GetMaxVal(array);
            for (int exponent = 1; Convert.ToDouble(maxVal) / exponent > 0; exponent *= 10)
                CountingSort(array, exponent);
            return array;
        }

        //получение max элемента из массива(нужно для RadixSort)
        public static anytype GetMaxVal<anytype>(anytype[] array)
        {
            var maxVal = array[0];
            for (int i = 1; i < array.Length; i++)
                if (Convert.ToDouble(array[i]) > Convert.ToDouble(maxVal))
                    maxVal = array[i];
            return maxVal;
        }

        //проверка сортировки массива
        public bool isSorted<anytype>(anytype[] array) {
            bool sort = true;
            for (int i = 0; i < array.Length-1; i++) {
                if (Convert.ToDouble(array[i]) > Convert.ToDouble(array[i + 1])) { 
                    sort = false; break;
                }
            }
            return sort;
        }

        //метод тестирующий все остальные методы в классе
        public void Test(){
            //тест создания массивов разных стандартных типов
            int[] arrint = createIntArray(10, 1, 10);
            double[] arrdouble = createDoubleArray(10, 1, 10);
            char[] arrchar = createCharArray(10, 'A', 'C');
            string[] arrstr = createStringArray(10, 2, 5);
            bool[] arrbool = createBoolArray(10);
            for (int i = 0; i < arrint.Length; i++) {
                Debug.Assert(arrint[i] > 0 && arrint[i] < 11);
                Debug.Assert(arrdouble[i] > 0 && arrdouble[i] < 11);
                Debug.Assert(arrchar[i] >= 'A' && arrint[i] <= 'C');
                Debug.Assert(arrstr[i].Length >= 2 && arrstr[i].Length <= 5);
                Debug.Assert(arrbool[i] == true || arrbool[i] == false);
            }

            //тест верного сохранения в файл массивов
            SaveInFile(arrint); //сохраняем
            StreamReader sr = new StreamReader(path); //создаем поток чтения файла
            for (int i = 0; i < arrint.Length; i++) {
                //Debug.Assert((arrint[i] == Convert.ToInt32(sr.ReadLine().ToString()))); //в цикле сравниваем значения массива и значение из файла
            }
            sr.Close(); //закрыли файл


            int[] arrint2 = createIntArray(10, 1, 10);
            double[] arrdouble2 = createDoubleArray(10, 1, 10); //создаем доп. массивы для теста сортировки

            arrint = CombSortArray(arrint); //проверка сортировки методом расчески
            arrdouble = BubbleSortArray(arrdouble);//проверка сортировки пузырьковым методом
            arrint2 = RadixSortArray(arrint2);    //проверка поразрядной сортировки
            arrdouble2 = QuickSort(arrdouble2, 0, arrint2.Length - 1);//проверка быстрой сортировки
           

            //проверка коректности всех алгоритмов поиска
            int[] testarr = new int[100];
            for (int i = 0; i < testarr.Length; i++)
            {
                testarr[i] = i;
            }
            int search = 55;
            Debug.Assert(sequentialSearch(testarr, search) == 55);
            Debug.Assert(binarySearch(testarr, search, 0, testarr.Length) == 55);
            Debug.Assert(interpolationSearch(testarr, search) == 55);
        }


        //последовательный поиск элемента в массиве, на выходе индекс на котором впервые встретился элемент
        public int sequentialSearch<anytype>(anytype[] array, anytype search) {
            for (int i = 0; i < array.Length; i++) {
                if (array[i].Equals(search)) {
                    return i;
                }
            }
            return -1;
        }

        //метод для рекурсивного бинарного поиска
        public int binarySearch<anytype>(anytype[] array, int searchedValue, int first, int last)
        {
            //границы сошлись
            if (first > last)
            {
                //элемент не найден
                return -1;
            }

            //средний индекс подмассива
            var middle = (first + last) / 2;
            //значение в средине подмассива
            var middleValue = array[middle];

            if (Convert.ToDouble(middleValue) == Convert.ToDouble(searchedValue))
            {
                return middle;
            }
            else
            {
                if (Convert.ToDouble(middleValue) > Convert.ToDouble(searchedValue))
                {
                    //рекурсивный вызов поиска для левого подмассива
                    return binarySearch(array, searchedValue, first, middle - 1);
                }
                else
                {
                    //рекурсивный вызов поиска для правого подмассива
                    return binarySearch(array, searchedValue, middle + 1, last);
                }
            }
        }


        //интерполяционный поиск элемента в массиве
        public int interpolationSearch<anytype>(anytype[] array, anytype search)
        {
            int mid, left = 0, right = array.Length - 1;
            while (Convert.ToDouble(array[left]) <= Convert.ToDouble(search) && Convert.ToDouble(array[right]) >= Convert.ToDouble(search))
            {
                mid = left + (int)(((Convert.ToDouble(search) - Convert.ToDouble(array[left])) * (right - left)) / (Convert.ToDouble(array[right]) - Convert.ToDouble(array[left])));
                if (Convert.ToDouble(array[mid]) < Convert.ToDouble(search)) left = mid + 1;
                else if (Convert.ToDouble(array[mid]) > Convert.ToDouble(search)) right = mid - 1;
                else return mid;
            }

            return -1;
        }


    }
 }
