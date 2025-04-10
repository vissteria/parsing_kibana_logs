using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Parsing_kibana_logs;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

using (var reader = new StreamReader("D:/Untitled discover search.csv"))
{
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        var records = csv.GetRecords<LogItem>();

        TimeSpan? firstElementTime = null;// 
        TimeSpan? lastElementTime = null;// TimeSpan.Parse(Regex.Match(records.Last().DateStamp, "\\d{2}\\:\\d{2}\\:\\d{2}\\.\\d{3}$").Value);

        var processTimes = new List<decimal>();

        foreach (var record in records)
        {
            if (firstElementTime is null)
            {
                firstElementTime = TimeSpan.Parse(Regex.Match(record.DateStamp, "\\d{2}\\:\\d{2}\\:\\d{2}\\.\\d{3}$").Value);
            }

            var time = Regex.Match(record.Message, "\\d*\\.?\\d*ms$");
            if (time.Success)
            {
                var parsedTime = time.Value.Replace("ms", "").Replace(".",",");
                processTimes.Add(decimal.Parse(parsedTime));
            }

            lastElementTime = TimeSpan.Parse(Regex.Match(record.DateStamp, "\\d{2}\\:\\d{2}\\:\\d{2}\\.\\d{3}$").Value);
        }

        var timeDiff = firstElementTime - lastElementTime;
        var timeDiffInSecs = timeDiff.Value.TotalSeconds;

        Console.WriteLine($"11.12.2023 average request time - {processTimes.Average()}");
        var serverDelay = ((decimal)timeDiffInSecs - (processTimes.Sum() / 1000))/(decimal)timeDiffInSecs * 100 ;
        Console.WriteLine($"11.12.2023 server delay - {serverDelay} percents");
    }
}

Console.WriteLine("");

using (var reader = new StreamReader("D:/Untitled discover search (2).csv"))
{
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
        var records = csv.GetRecords<LogItem>();

        TimeSpan? firstElementTime = null;// 
        TimeSpan? lastElementTime = null;// TimeSpan.Parse(Regex.Match(records.Last().DateStamp, "\\d{2}\\:\\d{2}\\:\\d{2}\\.\\d{3}$").Value);

        var processTimes = new List<decimal>();

        foreach (var record in records)
        {
            if (firstElementTime is null)
            {
                firstElementTime = TimeSpan.Parse(Regex.Match(record.DateStamp, "\\d{2}\\:\\d{2}\\:\\d{2}\\.\\d{3}$").Value);
            }

            var time = Regex.Match(record.Message, "\\d*\\.?\\d*ms$");
            if (time.Success)
            {
                var parsedTime = time.Value.Replace("ms", "").Replace(".", ",");
                processTimes.Add(decimal.Parse(parsedTime));
            }

            lastElementTime = TimeSpan.Parse(Regex.Match(record.DateStamp, "\\d{2}\\:\\d{2}\\:\\d{2}\\.\\d{3}$").Value);
        }

        var timeDiff = firstElementTime - lastElementTime;
        var timeDiffInSecs = timeDiff.Value.TotalSeconds;

        Console.WriteLine($"average request time - {processTimes.Average()}");
        var serverDelay = ((decimal)timeDiffInSecs - (processTimes.Sum() / 1000)) / (decimal)timeDiffInSecs * 100;
        Console.WriteLine($"server delay - {serverDelay} percents");
    }
}

Console.ReadKey();