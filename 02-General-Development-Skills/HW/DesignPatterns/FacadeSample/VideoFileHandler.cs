using System;

namespace DesignPatterns.FacadeSample;

class VideoFileHandler
{
    public void Read(string file)
    {
        Console.WriteLine($"Reading file {file}...");
    }
}
