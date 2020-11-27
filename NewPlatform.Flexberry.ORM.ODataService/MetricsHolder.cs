namespace NewPlatform.Flexberry.ORM.ODataService
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Класс для сохранения метрик исполнения методов.
    /// </summary>
    public static class MetricsHolder
    {
        /// <summary>
        /// Словарь таймеров.
        /// </summary>
        private static Dictionary<Guid, Stopwatch> stopwatches = new Dictionary<Guid, Stopwatch>();

        private static Dictionary<Guid, string> methodNamesForStopwatches = new Dictionary<Guid, string>();

        /// <summary>
        /// Словарь для хранения метрик.
        /// </summary>
        internal static Dictionary<string, List<long>> Data = new Dictionary<string, List<long>>();

        /// <summary>
        /// Добавление метрики.
        /// </summary>
        /// <param name="methodName">Название метода.</param>
        /// <param name="ms">Время исполнения в мс.</param>
        internal static void AddMetric(string methodName, long ms)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            if (!Data.ContainsKey(methodName))
            {
                Data.Add(methodName, new List<long>());
            }

            Data[methodName].Add(ms);
        }

        /// <summary>
        /// Запустить таймер для замера метрики.
        /// </summary>
        /// <param name="methodName">Уникальное имя метрики.</param>
        /// <returns>Уникальный идентификатор таймера (нужен для его остановки).</returns>
        internal static Guid StartTimer(string methodName)
        {
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            Guid stopwatchId = Guid.NewGuid();
            methodNamesForStopwatches.Add(stopwatchId, methodName);
            Stopwatch stopwatch = new Stopwatch();
            stopwatches.Add(stopwatchId, stopwatch);
            stopwatch.Start();

            return stopwatchId;
        }

        /// <summary>
        /// Остановить и удалить таймер.
        /// </summary>
        /// <param name="stopwatchId">Уникальный идентификатор таймера.</param>
        /// <returns>Время на таймере.</returns>
        internal static long StopTimer(Guid stopwatchId)
        {
            if (!stopwatches.ContainsKey(stopwatchId))
            {
                throw new ArgumentException("stopwatchId does not exist");
            }

            stopwatches[stopwatchId].Stop();
            var ms = stopwatches[stopwatchId].ElapsedMilliseconds;
            string methodName = methodNamesForStopwatches[stopwatchId];
            AddMetric(methodName, ms);
            stopwatches.Remove(stopwatchId);
            methodNamesForStopwatches.Remove(stopwatchId);

            return ms;
        }

        /// <summary>
        /// Получить средние значения метрик.
        /// </summary>
        /// <returns>Средние значения метрик по методам.</returns>
        internal static Dictionary<string, long> GetAvgMs()
        {
            Dictionary<string, long> result = new Dictionary<string, long>();

            foreach (KeyValuePair<string, List<long>> item in Data)
            {
                long sum = 0;
                foreach (int itemList in item.Value)
                {
                    sum += itemList;
                }

                result.Add(item.Key, sum / item.Value.Count);
            }

            return result;
        }

        /// <summary>
        /// Получить количество вызовов метрик.
        /// </summary>
        /// <returns>Количество вызовов по методам.</returns>
        internal static Dictionary<string, long> GetCallsCount()
        {
            Dictionary<string, long> result = new Dictionary<string, long>();

            foreach (KeyValuePair<string, List<long>> item in Data)
            {
                result.Add(item.Key, item.Value.Count);
            }

            return result;
        }

        /// <summary>
        /// Очистить все метрики.
        /// </summary>
        internal static void Clear()
        {
            Data.Clear();
            foreach (var item in stopwatches)
            {
                item.Value.Stop();
            }

            stopwatches.Clear();
            methodNamesForStopwatches.Clear();
        }
    }
}
