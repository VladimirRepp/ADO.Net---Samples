using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_2
{
    /// <summary>
    /// Полезные ссылки: https://metanit.com/sharp/tutorial/13.3.php
    /// 
    /// === async метод ===
    /// - Большую часть работы по написанию ассинхроного кода компилятор делает самостоятельно
    /// - Стркуктура написанного ассинхронного кода внешне выглядит как синхронная 
    /// 
    /// Async метод возвращает управление в двух случая: 
    /// 1) он завершил работу 
    /// 2) в нем происходит какой нибудь await вызов
    /// 
    /// Принцип работы: async метод работает НЕ параллельно и НЕ блокирует поток из которого вызван,
    /// происходит очередность вызова => его можно приостанавливать, а затем возобнавлять его 
    /// исполнение. Точками приостановки/возобновления являются await вызовы в методе.
    /// На самом деле, async метод не является асинхронным по определению. Если в async 
    /// методе нет await вызовов, то такой метод будет выполняться, как синхронный.
    /// 
    /// Асинхронность не означает выполнения в отдельном потоке. Асинхронность означает НЕ синхронность,
    /// т.е.отсутствие блокировки. Выполнение в отдельном потоке — один из способов избежать
    /// блокирующих вызовов. Но не единственный способ.
    /// 
    /// async методы встраивают в свой рабочий поток передачу управления в те моменты, 
    /// когда может возникнуть блокировка
    /// 
    /// === Класс Task ===
    /// Для многопоточности нужно использовать класс Task.
    /// В этом классе есть метод Task.Run<T>(), который 
    /// ставит в очередь на выполнение в ThreadPool, переданные
    /// ему строки кода.
    /// </summary>
    public static class SampleAsync
    {
        private static string _txtResult;

        async private static Task GetDataAsync(string filename)
        {
            // Готовим массив для приема прочитанных данных
            byte[] data = null;

            // Создаем поток для чтения
            using (FileStream fs = File.Open(filename,
            FileMode.Open))
            {
                // Создаем массив для чтения
                data = new byte[fs.Length];

                // Вызываем встроенный в класс FileStream async
                // метод ReadAsync по имени этого метода ясно,
                // что он асинхронный, значит, вызывать его
                // надо со спецификатором await
                await fs.ReadAsync(data, 0, (int)fs.Length);
            }

            // Преобразовываем прочитанные данные из байтового 
            // массива в строку и отображаем в текстовом поле
            _txtResult = System.Text.Encoding.UTF8.
            GetString(data);
        }

        private static string SlowMethod(string file)
        {
            Thread.Sleep(3000);

            // reading file
            return string.Format("File {0} is read", file);
        }

        /// <summary>
        /// Пример 1: обернуть SlowMethod асинхронным методом,
        /// чтобы избежать блокировки
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static Task<string> SlowMethodAsync(string file)
        {
            // Вызываем SlowMethod в доп. потоке, чтобы избежать блокировки
            return Task.Run<string>(() =>
            {
                return SlowMethod(file);
            });
        }

        private static async void CallMyAsync()
        {
            string result = await SlowMethodAsync("BigFile.txt");

            // Сюда можно добавить и другие вызовы нашего метода
            // string result1 = await SlowMethodAsync("BigFile1.txt");
            // string result2 = await SlowMethodAsync("BigFile2.txt");
            Console.WriteLine(result);
        }

        /// <summary>
        /// Для того, чтобы остановить запущенную задачу, надо познакомится с классом  CancellationTokenSource.
        /// </summary>
        private async static void CallMyAsync1()
        {
            try
            {
                var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(3));
                Task<string> t1 = SlowMethodAsync1("BigFile.txt", cts.Token);
                string result = await t1;
                Console.WriteLine(result);
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string SlowMethod1(string file, CancellationToken token)
        {
            Thread.Sleep(3000);
            //reading file
            token.ThrowIfCancellationRequested();
            return string.Format("File {0} is read", file);
        }

        static Task<string> SlowMethodAsync1(string file, CancellationToken token)
        {
            return Task.Run<string>(() =>
            {
                return SlowMethod1(file, token);
            });
        }

        // === Пример 1 ===
        // Время ожидания > 9 сек
        public async static void SampleAsync_1()
        {
            await PrintNameAsync("Tom");
            await PrintNameAsync("Bob");
            await PrintNameAsync("Sam");
        }

        // === Пример 2 ===
        // Время ожидания < 9, но > 3 сек
        // Используем возможности асинхроннсоти 
        public async static void SampleAsync_2()
        {
            // Запускаются во время определения 
            var tomTask = PrintNameAsync("Tom");
            var bobTask = PrintNameAsync("Bob");
            var samTask = PrintNameAsync("Sam");

            // Асинхронно ожидается 
            await tomTask;
            await bobTask;
            await samTask;
        }

        // Oпределение асинхронного метода
        private async static Task PrintNameAsync(string name)
        {
            await Task.Delay(3000);     // имитация продолжительной работы
            Console.WriteLine(name);
        }
    }
}
