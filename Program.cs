using DSAA1;//подключаем пространство имен с классом
using System.Diagnostics;   //подключаем библиотеку для использования таймера
Stopwatch timer = new Stopwatch();  //создаем таймер для отслеживания времени выполнения кода


ClassDataProcessing testObject = new ClassDataProcessing(); //объект класса для работы с массивами

testObject.Test(); //тест всех методов класса

Console.WriteLine("Введите длину массива");  
uint length = uint.Parse(Console.ReadLine());  //считываем длину массива

int[] arr = testObject.createIntArray(length, 10, 100); //создаем целочисленный массив



timer.Start();//запускаем таймер
testObject.SaveInFile(arr); //сохраняем массив в файл
timer.Stop();//останавливаем таймер
Console.WriteLine("Сохранение в файл заняло " + (double)timer.ElapsedMilliseconds/1000 + " секунд"); //выводим результат



//int[] arr2 = arr;  //создаем доп. массив равный изначальному чтобы все тесты скорости сортировки были честными
//timer.Restart();  //перезапускаем таймер
//arr2 = testObject.BubbleSortArray(arr2);
//timer.Stop();//останавливаем таймер
//Console.WriteLine("Сортировка Пузырьковым методом заняла " + (double)timer.ElapsedMilliseconds / 1000 + " секунд"); //выводим результат
//testObject.SaveInFile(arr2);

int[] arr3 = arr;
timer.Restart();  //перезапускаем таймер
arr3 = testObject.CombSortArray(arr3);
timer.Stop();//останавливаем таймер
Console.WriteLine("Сортировка методом расчески заняла " + (double)timer.ElapsedMilliseconds / 1000 + " секунд"); //выводим результат
testObject.SaveInFile(arr3);

//int[] arr4 = arr;
//timer.Restart();  //перезапускаем таймер
//arr4 = testObject.FastSortArray(arr4, arr4.Length - (int)arr4.Length / 3, arr4.Length - (int)arr4.Length / 5);
//timer.Stop();//останавливаем таймер
//Console.WriteLine("Сортировка Быстрым методом заняла " + (double)timer.ElapsedMilliseconds / 1000 + " секунд"); //выводим результат
//testObject.SaveInFile(arr4);

int[] arr5 = arr;
timer.Restart();
arr5 = testObject.RadixSortArray(arr5);
timer.Stop();//останавливаем таймер
Console.WriteLine("Сортировка Поразрядным методом заняла " + (double)timer.ElapsedMilliseconds / 1000 + " секунд"); //выводим результат
testObject.SaveInFile(arr5);

int search = 97;
int result = 0;
//замер времени работы последовательного поиска
if (testObject.isSorted(arr5))
{
    timer.Restart();
    result = testObject.sequentialSearch(arr5, search);
    timer.Stop();
    Console.WriteLine("Последовательный поиск. Позиция искомого элемента: " + result + ". Занятое время: " + (double)timer.ElapsedMilliseconds/1000 + " секунд");
}

//замер времени выполнения бинарного поиска
if (testObject.isSorted(arr5))
{
    timer.Restart();
    result = testObject.binarySearch(arr5, search, 0, arr5.Length);
    timer.Stop();
    Console.WriteLine("Позиция искомого элемента: " + result + ". Занятое время: " + (double)timer.ElapsedMilliseconds / 1000 + " секунд");
}
if (testObject.isSorted(arr5))
{
    timer.Restart();
    result = testObject.interpolationSearch(arr5, search);
    timer.Stop();
    Console.WriteLine("Позиция искомого элемента: " + result + ". Занятое время: " + (double)timer.ElapsedMilliseconds / 1000 + " секунд");
}



//int[] array = testObject.createArray<int>(length, 1, 10);



