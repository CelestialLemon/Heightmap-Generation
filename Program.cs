using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Heightmap_Generation
{
    class Program
    {
        // height and width of the image
        static int height = 1024, width = 1024;

        // random object for using throughout the program
        static Random random = new Random();
        static void Main(string[] args)
        {
            
            // PerlinNoise pn = new PerlinNoise();
            // var randomTexture = pn.RandomNoise(32, 32);
            // pn.Octave_Noise(2, randomTexture);

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            PerlinNoise pn = new PerlinNoise();
            //var randomNoise = pn.RandomNoise(width, height);
            //var smoothedTexture = pn.Octave_Noise(64, randomNoise);
            
            var heightmap = pn.Perlin(7, width, height);

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    int shade = (int)(heightmap[x, y] * 255);

                    bmp.SetPixel(x, y, Color.FromArgb(255, shade, shade, shade));
                }
            }
            bmp.Save("E:\\Repos\\Projects\\Heightmap Generation\\images\\heightmap.png", ImageFormat.Png);
            Console.ReadLine();
        }

        static Color BlendColor(float t, Color a, Color b)
        {
            return Color.FromArgb(
                (int)((b.A - a.A) * t + a.A),
                (int)((b.R - a.R) * t + a.R),
                (int)((b.G - a.G) * t + a.G),
                (int)((b.B - a.B) * t + a.B)
                );
        }
    }
}
