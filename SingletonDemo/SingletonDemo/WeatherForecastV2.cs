using System;

namespace SingletonDemo
{
    public class WeatherForecastV2
    {
        // ����һ����̬�������������Ψһʵ��
        private static WeatherForecastV2 uniqueInstance;
        // ����һ��������ֹ���߳�
        private static readonly object locker = new object();

        // ����˽�й��캯����ʹ��粻�ܴ�������ʵ��
        private WeatherForecastV2()
        {
            Date = DateTime.Now;
        }

        public static WeatherForecastV2 GetInstance()
        {
            // ����һ���߳�ִ�е�ʱ�򣬻��locker���� "����"��
            // �������߳�ִ�е�ʱ�򣬻�ȴ� locker ִ�������
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // ������ʵ���������򴴽�������ֱ�ӷ���
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
