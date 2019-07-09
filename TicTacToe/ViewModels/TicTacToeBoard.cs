using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.ViewModels
{
    public class TicTacToeBoard : ViewModelBase
    {
        public TicTacToeSqaure[,] _Squares;

        public TicTacToeSqaure[,] Squares
        {
            get
            {
                return _Squares;
            }
            set
            {
                _Squares = value;
                RaisePropertyChanged();
            }
        }

        public TicTacToeGameModel Parent;

        public TicTacToeBoard(TicTacToeGameModel parent)
        {
            Parent = parent;
            Squares = new TicTacToeSqaure[3, 3];
            for(int i = 0 ;  i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Squares[i, j] = new TicTacToeSqaure(this, i, j);
                }
            }
        }

        public TicTacToeBoard(TicTacToeGameModel parent, TicTacToeBoard board )
        {
            Parent = parent;
            Squares = new TicTacToeSqaure[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Squares[i, j] = new TicTacToeSqaure(this, i, j);
                    Squares[i, j].CurrentStatus = board.Squares[i, j].CurrentStatus;
                }
            }
        }

        internal bool SetSqaure(int row, int col, Player player)
        {
            return this.Squares[row, col].SetLetter(player);
        }
    }
}
