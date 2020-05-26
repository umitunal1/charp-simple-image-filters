using System.Drawing;
using System.Drawing.Text;

namespace FilterLib
{
    public interface IFilterImage
    {
        Color[,] Apply(Color[,] input);

        string Name { get; }
    }
}
