using System;
using System.Drawing;

namespace FilterLib
{
    public class FilterImage : IFilterImage
    {
        public string name => throw new NotImplementedException();

        public Color[,] Apply(Color[,] input)
        {
            throw new NotImplementedException();
        }
    }
}
