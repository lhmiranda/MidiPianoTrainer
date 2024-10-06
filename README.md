# MIDI Keyboard Trainer

For now, this repository contains a simple C# console application that listens for MIDI input and visualizes the keys pressed on a piano keyboard in real time. The goal is to use this as the foundation for a piano trainer in the future.

## Features

- Displays a virtual piano keyboard layout in the console.
- Highlights keys in real time as they are pressed and released.
- Utilizes `NAudio.Midi` for capturing MIDI input from connected devices.

## Requirements

- .NET SDK 6.0 or later.
- `NAudio` library (included via NuGet).

## How to Run

1. Clone the repository.

2. Build and run the project:

   ```bash
   dotnet run
   ```

3. The program will detect your connected MIDI input device (default is `MidiIn(0)`).

4. Press any key on your MIDI keyboard, and the corresponding note will light up in blue on the console.

5. Press `[ESC]` to quit the application.

## Customization

- You can adjust the number of octaves and keyboard layout by modifying the `SetupKeyboard()` method.
- Change the colors for pressed and released keys by modifying `ConsoleColor` values in the `HighlightKey()` method.

## Dependencies

- [NAudio](https://github.com/naudio/NAudio) - A .NET audio and MIDI library used for capturing MIDI input.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## Contributing

This project is a basic example of handling MIDI events in C# and displaying interactive visual feedback in a console environment. Feel free to fork and extend the project!
