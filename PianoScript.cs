using System;
using System.IO;

class PianoScript
{
    // Console key appearance lookup strings.
    const string upper_keyboard = "|   ###  ###   |   ###  ###  ###   ";
    const string lower_keyboard = "|    ";
    const string bottom_keyboard = "|____";

    // Print length characters from keyboard[start] wrapping around once the end is reached.
    static void PrintKeys(string keyboard, int start, int length)
    {
        for (int i = start; i < start + length; i++)
            Console.Write(keyboard[i % keyboard.Length]);
        Console.WriteLine();
    }

    // Print a keyboard of length numkeys (white keys only) starting at given key.
    static void Go(string key, int numkeys)
    {
        int length = numkeys * 5 + 1;

        int start = "CDEFGAB".IndexOf(key[0]) * 5;
        if (key.Length > 1 && key[1] == '#' && "CDFGA".Contains(key[0]))
        {
            start += 4;
            length += 1;
        }

        int i = 0;

        while (i++ < 5)
            PrintKeys(upper_keyboard, start, length);

        while (i++ < 8)
            PrintKeys(lower_keyboard, start, length);
        
        PrintKeys(bottom_keyboard, start, length);
    }

    // Parses and interprets instructions.
    //
    // Valid Grammar:
    //     .*go.* - Uses given command line arguments to write a piano.
    //
    // Note: there is no error checking on number and validitiy of arguments.
    //     Use at your own risk.
    static void Parse(string script, string[] args)
    {
        // Optimized regular expression parser for our grammar.
        if (script.ToLower().Contains("go"))
        {
            string key = args[1];
            int length = int.Parse(args[2]) * 5 + 1;
        }

        string key = args[1];
        int numkeys = int.Parse(args[2]);

        Go(key, numkeys);
    }
    
    // Parses a PianoScript file and interprets any instructions it contains.
    //
    // Usage:
    //     PianoScript <script>.ps [arguments]
    static void Main(string[] args)
    {
        string filename = args[0];
        string dir = System.Environment.CurrentDirectory;
        StreamReader file = new StreamReader(dir + "\\" + filename);

        string script = file.ReadToEnd();

        Parse(script, args);
    }
}