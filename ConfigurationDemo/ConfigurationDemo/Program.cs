using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using System;
using System.Collections.Generic;

namespace ConfigurationDemo
{
    class Program
    {
        static void Main2(string[] args)
        {
            // 配置框架
            // 以key-value字符串键值对的方式抽象了配置
            // 支持从各种不同的数据源读取配置

            // IConfigurationSource
            // IConfigurationProvider

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddInMemoryCollection(new Dictionary<string, string>()
            {
                { "key1", "value1"},
                { "key2", "value2"},
                { "section1:key3", "value3"},// 冒号
                { "section2:key4", "value4"}
            });

            IConfigurationRoot configurationRoot = builder.Build();

            IConfiguration config = configurationRoot;

            Console.WriteLine(configurationRoot["key1"]);
            Console.WriteLine(configurationRoot["key2"]);

            IConfigurationSection section = config.GetSection("section1");
            Console.WriteLine(section["key3"]);
        }


        static void Main(string[] args)
        {
            var source = new Dictionary<string, string>
            {
                ["longDatePattern"] = "dddd, MMMM d, yyyy",
                ["longTimePattern"] = "h:mm:ss tt",
                ["shortDatePattern"] = "M/d/yyyy",
                ["shortTimePattern"] = "h:mm tt",
            };

            var config = new ConfigurationBuilder()
            .Add(new MemoryConfigurationSource { InitialData = source })
            .Build();

            var options = new DateTimeFormatOptions(config);
            Console.WriteLine($"{options.LongDatePattern}");
        }
    }

    public class DateTimeFormatOptions
    {
        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }

        public DateTimeFormatOptions(IConfiguration config)
        {
            LongDatePattern = config["LongDatePattern"];
            LongTimePattern = config["LongTimePattern"];
            ShortDatePattern = config["ShortDatePattern"];
            ShortTimePattern = config["ShortTimePattern"];
        }
    }
}



/*
 配置系统支持多样化的数据源，
 既可以采用内存变量作为配置的数据源，也可以将配置定义在持久化的文件甚至是数据库中。
 
 读取的配置最终会转换为一个IConfiguration对象供应用程序使用。
 IConfigurationBuilder对象是Configuration对象的构造者，
 IConfigurationSource对象则代表配置数据最原始的来源。


 
 
 
 */
