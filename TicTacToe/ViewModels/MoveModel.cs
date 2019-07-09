using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.ViewModels
{
    public class MoveModel
    {
        public int Row;
        public int Column;

        public MoveModel(int row, int col)
        {
            Row = row;
            Column = col;
        }
    }
}
