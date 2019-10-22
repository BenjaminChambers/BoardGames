using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGames
{
    public struct Run<T>
    {
        public int StartRow;
        public int StartColumn;
        public int EndRow;
        public int EndColumn;
        public T Value;
    }
}
