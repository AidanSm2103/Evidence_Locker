using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Centralizes console styling — colors, headers, status tags
// Every screen calls into here instead of setting Console.ForegroundColor directly, so the visual style lives in exactly one file 

namespace Evidence_Locker.UI
{
    public static class ConsoleTheme
    {
        public static void Header(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"   {text}");
            Console.WriteLine(new string('═', 60));
            // Always reset, or every later line stays this color
            Console.ResetColor();
        }

        public static void SubHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n-- {text} --");
            Console.ResetColor();
        }

        public static void Success(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[OK] {text}");
            Console.ResetColor();
        }

        public static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {text}");
            Console.ResetColor();
        }

        public static void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[!] {text}");
            Console.ResetColor();
        }

        public static void StatusTag(Evidence_Locker.Core.Enums.CaseStatus status)
        {
            // Maps each status to a color so a glance at "View All Cases" communicates state without reading the text
            Console.ForegroundColor = status switch
            {
                Core.Enums.CaseStatus.Open => ConsoleColor.Green,
                Core.Enums.CaseStatus.Cold => ConsoleColor.Cyan,
                Core.Enums.CaseStatus.Reopened => ConsoleColor.Yellow,
                Core.Enums.CaseStatus.Closed => ConsoleColor.DarkGray,
                _ => ConsoleColor.White
            };
            Console.Write($"[{status}]");
            Console.ResetColor();
        }

        public static void PrintBanner()
        {
            // ASCII art banner — purely cosmetic, shown once at startup
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
  _______     _____ _____  ______ _   _  _____ ______    _      ____   _____ _  ________ _____  
 |  ____\ \   / /_ _|  __ \|  ____| \ | |/ ____|  ____|  | |    / __ \ / ____| |/ /  ____|  __ \ 
 | |__   \ \ / / | || |  | | |__  |  \| | |    | |__     | |   | |  | | |    | ' /| |__  | |__) |
 |  __|   \ V /  | || |  | |  __| | . ` | |    |  __|    | |   | |  | | |    |  < |  __| |  _  / 
 | |____   | |  _| || |__| | |____| |\  | |____| |____   | |___| |__| | |____| . \| |____| | \ \ 
 |______|  |_| |___|_____/|______|_| \_|\_____|______|   |______\____/ \_____|_|\_\______|_|  \_\
");
            Console.ResetColor();
            Console.WriteLine("           C O L D   C A S E   M A N A G E M E N T   S Y S T E M");
            Console.WriteLine();
        }
    }
}

