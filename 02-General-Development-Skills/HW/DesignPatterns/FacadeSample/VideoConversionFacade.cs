using System;

namespace DesignPatterns.FacadeSample;

public class VideoConversionFacade
{
    private readonly VideoFileHandler _fileHandler = new();
    private readonly AudioProcessor _audioProcessor = new();
    private readonly VideoProcessor _videoProcessor = new();

    public void ConvertVideo(string fileName)
    {
        Console.WriteLine($"Starting conversion for {fileName}");
        _fileHandler.Read(fileName);
        _audioProcessor.Process();
        _videoProcessor.Process();
        Console.WriteLine("Encoding and finishing...");
        Console.WriteLine("Conversion finished.");
    }
}
