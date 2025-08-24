using System;

namespace DesignPatterns.FacadeSample;

class Program
{
    public static void Main()
    {
        Console.WriteLine("FacadeSample demo:");

        var facade = new VideoConversionFacade();
        facade.ConvertVideo("example.mp4");
    }
}
