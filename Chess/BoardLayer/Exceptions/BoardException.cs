using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.BoardLayer.Exceptions
{
    class BoardException : ApplicationException
    {
        public BoardException(string e) : base(e)
        {
        }
    }
}
