using System;

namespace SingletonDemo
{
    public class WeatherForecastV2
    {
        // 定义一个静态变量来保存类的唯一实例
        private static WeatherForecastV2 uniqueInstance;
        // 定义一个锁，防止多线程
        private static readonly object locker = new object();

        // 定义私有构造函数，使外界不能创建该类实例
        private WeatherForecastV2()
        {
            Date = DateTime.Now;
        }

        public static WeatherForecastV2 GetInstance()
        {
            // 当第一个线程执行的时候，会对locker对象 "加锁"，
            // 当其他线程执行的时候，会等待 locker 执行完解锁
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new WeatherForecastV2();
                    }
                }
            }

            return uniqueInstance;
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
