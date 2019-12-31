using System.Drawing;
using System.IO;

namespace SplitCubeMap
{
    internal class Program
    {
        private static void Main()
        {
            using (var imageFactory = new ImageProcessor.ImageFactory())
            {
                imageFactory.Load("skybox.png");

                var size = imageFactory.Image.Width / 4;
                var directory = Directory.GetCurrentDirectory();

                imageFactory
                    .Crop(new Rectangle(size, 0, size, size))
                    .Save(Path.Combine(directory, "skybox-top.png"))

                    .Reset()
                    .Crop(new Rectangle(0, size, size, size))
                    .Save(Path.Combine(directory, "skybox-left.png"))

                    .Reset()
                    .Crop(new Rectangle(size, size, size, size))
                    .Save(Path.Combine(directory, "skybox-front.png"))

                    .Reset()
                    .Crop(new Rectangle(size * 2, size, size, size))
                    .Save(Path.Combine(directory, "skybox-right.png"))

                    .Reset()
                    .Crop(new Rectangle(size * 3, size, size, size))
                    .Save(Path.Combine(directory, "skybox-back.png"))

                    .Reset()
                    .Crop(new Rectangle(size, size * 2, size, size))
                    .Save(Path.Combine(directory, "skybox-bottom.png"));
            }
        }
    }
}
