using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GetDate
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                List<string> processedArgs = args.SelectMany(arg => arg.Split(' ')).ToList();

                if (processedArgs.Contains("--author"))
                {
                    Console.WriteLine("Davrisev Emrah Aslanovich 9/3-РПО-21/1 2023");
                }
                bool labelsFlag = processedArgs.Contains("--labels");
                bool dateTimeFlag = processedArgs.Any(arg => Regex.IsMatch(arg, @"^-[YMDdhms]+$"));

                if (dateTimeFlag)
                {
                    Dictionary<char, string> formatLabels = new Dictionary<char, string>(){
                        {'Y', "Year: "},
                        {'M', "Month: "},
                        {'D', "Day: "},
                        {'d', "Weekday: "},
                        {'h', "Hours: "},
                        {'m', "Minutes: "},
                        {'s', "Seconds: "}
                    };

                    string combinedFormat = string.Join("", processedArgs
                        .Where(arg => arg.Length > 1 && arg[0] == '-' && arg[1] != '-')
                        .SelectMany(arg => arg.Substring(1)));

                    foreach (char c in "YMDdhms")
                    {
                        if (combinedFormat.Contains(c))
                        {
                            DateTime currentDateTime = DateTime.Now;

                            if (labelsFlag)
                            {
                                Console.WriteLine($"{formatLabels[c]}{GetDateInfo(currentDateTime, c)}");
                            }
                            else
                            {
                                Console.WriteLine(GetDateInfo(currentDateTime, c));
                            }
                        }
                    }
                }
            }

            Console.WriteLine("\nДля завершения программы нажмите любую клавишу...");
            Console.ReadKey();
        }

        static string GetDateInfo(DateTime dt, char format)
        {
            switch (format)
            {
                case 'Y':
                    return dt.Year.ToString();
                case 'M':
                    return dt.Month.ToString();
                case 'D':
                    return dt.Day.ToString();
                case 'd':
                    return dt.DayOfWeek.ToString();
                case 'h':
                    return dt.Hour.ToString();
                case 'm':
                    return dt.Minute.ToString();
                case 's':
                    return dt.Second.ToString();
                default:
                    return string.Empty;
            }
        }
    }
}