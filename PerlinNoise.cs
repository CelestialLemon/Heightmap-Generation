﻿using System;
using System.Drawing;
using System.Numerics;
using System.Collections.Generic;

namespace Heightmap_Generation
{
    class PerlinNoise
    {
        public float[,] RandomNoise(int width, int height)
        {
            float[,] texture = new float[width, height];
            Random random = new Random();
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    texture[x, y] = (float)random.NextDouble();
                }
            }
            return texture;
        }

        public float[,] Octave_Noise(int stride, ref float[,] baseNoise)
        {
            int width = baseNoise.GetLength(0);
            int height = baseNoise.GetLength(1);
            int exponent = stride;
            
            for(int i = 0; i < Math.Ceiling((float)height / exponent); i++)
            {
                for(int j = 0; j < Math.Ceiling((float)width / exponent); j++)
                {
                    int x = j * exponent > width - 1 ? width - 1: j * exponent;
                    int y = i * exponent > height - 1 ? height - 1: i * exponent;

                    int x2 = x + exponent > width - 1? width - 1: x + exponent;
                    int y2 = y + exponent > height - 1? height - 1: y + exponent;

                    Lerp_Vertical(x, y, y2, ref baseNoise);
                }
            }

            // lerp last vertical line
            for(int j = 0; j < Math.Ceiling((float)height / exponent); j++)
            {
                int x = width - 1;
                int y = j * exponent;
                int y2 = y + exponent > height - 1 ? height - 1 : y + exponent;

                Lerp_Vertical(x, y, y2, ref baseNoise);
            }

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x += exponent)
                {
                    int x2 = x + exponent > width - 1 ? width - 1 : x + exponent;
                    Lerp_Horizontal(x, x2, y, ref baseNoise);
                }
            }
            return baseNoise;
        }

        void Lerp_Vertical(int x, int y1, int y2, ref float[,] noiseMap)
        {
            // y2 > y1
            // each pixel in noisemap should contain a float value from 0 - 1(exlusive)
            for(int y = y1; y <= y2; y++)
            {
                float lerpFactor = (float)(y - y1) / (y2 - y1);

                // lerp formula value = (b - a) * t + a
                noiseMap[x, y] = (noiseMap[x, y2] - noiseMap[x, y1]) * lerpFactor + noiseMap[x, y1];
            }
        }

        void Lerp_Horizontal(int x1, int x2, int y, ref float[,] noiseMap)
        {
            // x2 > x1
            // each pixel in noisemap should contain a float value from 0 - 1(exlusive)

            for(int x = x1; x <= x2; x++)
            {
                float lerpFactor = (float)(x - x1) / (x2 - x1);

                noiseMap[x, y] = (noiseMap[x2, y] - noiseMap[x1, y]) * lerpFactor + noiseMap[x1, y];
            }
        }
    
        public float[,] Perlin(int k, int width, int height)
        {
            float[,] noise = RandomNoise(width, height);

            List<float[,]> noiseMap_arr = new List<float[,]>();
            for(int i = 1; i <= k; i++)
            {
                var newNoiseMap = Octave_Noise((int)(Math.Pow(2, i)), ref noise);
                noiseMap_arr.Add(newNoiseMap);
            }

            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    float avg_val = 0.0f;
                    for(int i = 0; i < noiseMap_arr.Count; i++)
                    {
                        avg_val += noiseMap_arr[i][x, y];
                    }

                    avg_val /= noiseMap_arr.Count;
                    noise[x, y] = avg_val;
                }
            }


            return noise;
        }
    }
}
