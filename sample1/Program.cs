namespace sample1;

using System.Diagnostics;
using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Image resizing started...");
        var numbers = Enumerable.Range(1, 5);
        // Compare time taken by Parallel.ForEach and for loop

        var watchForParallel = Stopwatch.StartNew();
        Parallel.ForEach(numbers, i =>
        {
            string srcFilePath = $"images/raw/dogs-{i}.png";
            string destFilePath = $"images/edited/dogs-{i}.png";

            // Create an instance of the ImageResize class
            ImageResize imageResize = new(srcFilePath);
            // Resize the image to half its original size
            imageResize.Resize(destFilePath, 0.5f);
        });
        watchForParallel.Stop();
        Console.WriteLine($"Parallel.ForEach took: {watchForParallel.ElapsedMilliseconds} ms");

        var watchForLoop = Stopwatch.StartNew();
        for (int i = 1; i < 6; i++)
        {
            string srcFilePath = $"images/raw/dogs-{i}.png";
            string destFilePath = $"images/edited/dogs-{i}.png";

            // Create an instance of the ImageResize class
            ImageResize imageResize = new(srcFilePath);
            // Resize the image to half its original size
            imageResize.Resize(destFilePath, 0.5f);
        }
        watchForLoop.Stop();

        Console.WriteLine($"for loop took: {watchForLoop.ElapsedMilliseconds} ms");
    }
}

