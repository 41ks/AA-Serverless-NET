namespace sample1;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

class ImageResize(string srcFilePath)
{
  public string srcFilePath = srcFilePath;

  public void Resize(string destFilePath, int width, int height)
  {
    // Load the image from the source file path
    using Image image = Image.Load(srcFilePath);
    // Resize the image to the specified width and height
    image.Mutate(x => x.Resize(width, height));
    // Save the resized image to the destination file path
    image.Save(destFilePath);
  }

  public void Resize(string destFilePath, int width)
  {
    // Load the image from the source file path
    using Image image = Image.Load(srcFilePath);
    // Resize the image to the specified width and height
    image.Mutate(x => x.Resize(width, width));
    // Save the resized image to the destination file path
    image.Save(destFilePath);
  }
  public void Resize(string destFilePath, float scale)
  {
    // Load the image from the source file path
    using Image image = Image.Load(srcFilePath);
    // Resize the image to the specified width and height
    image.Mutate(x => x.Resize((int)(image.Width * scale), (int)(image.Height * scale)));
    // Save the resized image to the destination file path
    image.Save(destFilePath);
  }
}
