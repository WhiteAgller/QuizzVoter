using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Quizz;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .Build();

var groups = config.Get<GroupList>();

//For padding
var maxCharName = groups!.Groups.Max(x => String.Join("", x.Participants).Length);
var numberOfMostParticipants = groups!.Groups.MaxBy(x => String.Join("", x.Participants).Length)!.Participants.Length;
var padding = maxCharName + numberOfMostParticipants;

var alreadyPressed = new List<char>();
var isFirst = true;

Stopwatch sw = new Stopwatch();

Console.WriteLine("Start");
do {
    while (! Console.KeyAvailable)
    {
        var key = Console.ReadKey(true);

        foreach (var group in groups!.Groups)
        {
            if (key.KeyChar == group.Key && !alreadyPressed.Contains(key.KeyChar))
            {
                if (isFirst)
                {
                    sw.Start();
                    isFirst = false;
                }

                var ts = sw.Elapsed;
                alreadyPressed.Add(key.KeyChar);
                Console.WriteLine(String.Join("/", group.Participants).PadRight(padding) + "| " + String.Format("{0:00}:{1:00}", ts.Seconds, ts.Milliseconds));
            }
        }
        
        if (key.KeyChar == 'r')
        {
            sw.Stop();
            sw.Reset();
            alreadyPressed.Clear();
            isFirst = true;
            Console.Clear();
            Console.WriteLine("Next round:");
        }

    }       
} while (Console.ReadKey(true).Key != ConsoleKey.Escape);

