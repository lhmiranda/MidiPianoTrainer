using NAudio.Midi;


class Program
{
    static string[] notes = new[] { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    static Dictionary<int, (string k, int x)> keyboardLayout = new();

    static void Main(string[] args)
    {
        MidiIn? midiIn = null;

        if (MidiIn.NumberOfDevices > 0)
        {
            midiIn = new MidiIn(0);
            midiIn.MessageReceived += OnMidiMessageReceived;
            midiIn.Start();
            Console.Clear();
            SetupKeyboard();
            DrawKeyboard();
            Console.WriteLine($"Using MIDI device: {MidiIn.DeviceInfo(0).ProductName}");
        }
        else
        {
            Console.WriteLine("No MIDI device found.");
        }

        Console.WriteLine("\nPress [ESC] to quit...");
        while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }

        // Só chama Stop se midiIn não for nulo
        midiIn?.Stop();
    }

    static void SetupKeyboard()
    {
        var cursonXPos = 0;
        var firstOctave = 3;
        var noteNumber = 36;
        var octaves = 5;
        for (var octave = 0 + firstOctave; octave < octaves + firstOctave; octave++)
        {
            for (var note = 0; note < 12; note++)
            {
                var noteStr = notes[note] + octave;
                keyboardLayout.Add(noteNumber++, (noteStr, cursonXPos++));
            }
        }

        keyboardLayout.Add(noteNumber++, (notes[0], cursonXPos++));
    }

    static void DrawKeyboard()
    {
        Console.Clear();
        foreach (var entry in keyboardLayout)
        {
            HighlightKey(entry.Key, false);
        }
    }

    static void OnMidiMessageReceived(object sender, MidiInMessageEventArgs e)
    {
        if (e.MidiEvent is NoteOnEvent noteOn && noteOn.Velocity > 0)
        {
            HighlightKey(noteOn.NoteNumber, true);
        }
        else if (e.MidiEvent is NoteEvent noteOff && noteOff.CommandCode == MidiCommandCode.NoteOff)
        {
            HighlightKey(noteOff.NoteNumber, false);
        }
    }

    static void HighlightKey(int noteNumber, bool isPressed)
    {
        var ch = "█";
        var (ox, oy) = Console.GetCursorPosition();
        var cursonYPos = 10;
        if (keyboardLayout.ContainsKey(noteNumber))
        {
            var (k, x) = keyboardLayout[noteNumber];

            Console.ForegroundColor = isPressed ? ConsoleColor.Blue : ConsoleColor.White;

            for (var i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, cursonYPos + i);


                if (k.Length == 3) // black key
                {
                    Console.ForegroundColor = isPressed ? ConsoleColor.DarkBlue : ConsoleColor.Black;

                    if (i == 2)
                    {
                        //Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.White;
                        //ch = "│";
                    }
                }

                Console.Write(ch);
            }

            Console.ResetColor();
            Console.SetCursorPosition(ox, oy);
        }
    }
}
