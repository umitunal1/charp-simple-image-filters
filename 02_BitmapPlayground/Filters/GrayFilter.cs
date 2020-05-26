using System;
using System.Drawing;

namespace _02_BitmapPlayground.Filters
{
    public class GrayFilter : IFilter
    {
        public Color[,] Apply(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            Color[,] result = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var p = input[x, y];
                    var gray = (p.R + p.G + p.B) / 3;
                   // var gray = Convert.ToInt32((p.R *0.3 + p.G * 0.59 + p.B * 0.11) / 3);
                    result[x, y] = Color.FromArgb(p.A, gray, gray, gray);
                }
            }

            return result;
        }

        public string Name => "Filter gray component";

        public override string ToString()
            => Name;
    }
}
