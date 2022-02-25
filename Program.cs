using System;
using System.Drawing;

namespace Heightmap_Generation
{
    class Program
    {
        // height and width of the image
        static int height = 512, width = 512;

        // random object for using throughout the program
        static Random random = new Random();
        static void Main(string[] args)
        {

            // PerlinNoise pn = new PerlinNoise();
            // var randomTexture = pn.RandomNoise(32, 32);
            // pn.Octave_Noise(2, randomTexture);

            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            PerlinNoise pn = new PerlinNoise();
            //var randomNoise = pn.RandomNoise(width, height);
            //var smoothedTexture = pn.Octave_Noise(32, ref randomNoise);
            
            var smoothedTexture = pn.Perlin(5, width, height);

            Color a = Color.FromArgb(255, 255, 255, 255);
            Color b = Color.FromArgb(255, 255, 0, 0);

            int redPixels = 0, whitePixels = 0; 
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    bmp.SetPixel(x, y, BlendColor(smoothedTexture[x, y], a, b));
                }
            }
            bmp.Save("E:\\Repos\\Projects\\Heightmap Generation\\images\\sample_image.bmp");
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
