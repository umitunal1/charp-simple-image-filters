using System.Drawing;

namespace _02_BitmapPlayground.Filters
{
    public class AverageFilter : IFilter
    {
        public Color[,] Apply(Color[,] input)
        {
            int width = input.GetLength(0);
            int height = input.GetLength(1);
            int A,R,G,B;
           
           //todo button bstayken check et
            Color[,] result = new Color[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x > 1 && x < width-1 && y > 1 && y < height-1)
                    {
                        A = (input[x, y - 1].A + input[x, y + 1].A + input[x - 1, y].A + input[x + 1, y].A) / 4;
                        R = (input[x, y - 1].R + input[x, y + 1].R + input[x - 1, y].R + input[x + 1, y].R) / 4;
                        G = (input[x, y - 1].G + input[x, y + 1].G + input[x - 1, y].G + input[x + 1, y].G) / 4;
                        B = (input[x, y - 1].B + input[x, y + 1].B + input[x - 1, y].B + input[x + 1, y].B) / 4;

                    }
                    else
                    {
                        A = input[x, y].A;
                        R = input[x, y].R;
                        G = input[x, y].G;
                        B = input[x, y].B;
                    }

                    result[x, y] = Color.FromArgb(A, R, G, B);
                }
            }

            return result;
        }

        public string Name => "Filter average component";

        public override string ToString()
            => Name;
    }
}
