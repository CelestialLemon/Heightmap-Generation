using System;
using System.Drawing;

namespace Heightmap_Generation
{
    class Program
    {
        // height and width of the image
        static int height = 1080, width = 1920;

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
            
            var smoothedTexture = pn.Perlin(6, width, height);

            Color red = Color.FromArgb(255, 255, 0, 0);
            Color orange = Color.FromArgb(255, 255, 127, 0);
            Color yellow = Color.FromArgb(255, 255, 255, 0);
            Color green = Color.FromArgb(255, 0, 255, 0);
            Color blue = Color.FromArgb(255, 0, 0, 255);
            Color indigo = Color.FromArgb(255, 75, 0, 130);
            Color violet = Color.FromArgb(255, 148, 0, 211);


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float height = smoothedTexture[x, y];
                    if(height < 0.16f && height > 0.0f)
                    {
                        //bmp.SetPixel(x, y, BlendColor(height / (1.0f / 5.9f), violet, indigo));
                        bmp.SetPixel(x, y, indigo);
                    }
                    else if (height < 0.33f && height > 0.16f)
                    {
                        //bmp.SetPixel(x, y, BlendColor((height - 0.16f) / (1.0f / 5.9f), indigo, blue));
                        bmp.SetPixel(x, y, blue);
                    }
                    else if (height < 0.50f && height > 0.33f)
                    {
                        //bmp.SetPixel(x, y, BlendColor((height - 0.33f) / (1.0f / 5.9f), blue, green));
                        bmp.SetPixel(x, y, green);
                    }
                    else if (height < 0.66f && height > 0.50f)
                    {
                        //bmp.SetPixel(x, y, BlendColor((height - 0.50f) / (1.0f / 5.9f), green, yellow));
                        bmp.SetPixel(x, y, yellow);
                    }
                    else if (height < 0.83f && height > 0.66f)
                    {
                        //bmp.SetPixel(x, y, BlendColor((height - 0.66f) / (1.0f / 5.9f), yellow, orange));
                        bmp.SetPixel(x, y, orange);
                    }
                    else if (height < 1.0f && height > 0.83f)
                    {
                        //bmp.SetPixel(x, y, BlendColor((height - 0.83f) / (1.0f / 5.9f), orange, red));
                        bmp.SetPixel(x, y, red);
                    }
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
