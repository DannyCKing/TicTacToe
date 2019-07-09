using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.ViewModels
{
    public enum SquareLetter { Empty, X, O};

    public class TicTacToeSqaure : ViewModelBase
    {
        public TicTacToeBoard Parent;
        public int Row;
        public int Column;

        public string Letter
        {
            get
            {
                if(CurrentStatus == SquareLetter.Empty)
                {
                    return "";
                }
                else
                {
                    return CurrentStatus.ToString();
                }
            }
        }
        private SquareLetter _CurrentStatus;

        public SquareLetter CurrentStatus
        {
            get
            {
                return _CurrentStatus;
            }
            set
            {
                _CurrentStatus = value;
                RaisePropertyChanged();
                RaisePropertyChanged("Played");
                RaisePropertyChanged("Letter");
            }
        }

        public bool Played
        {
            get
            {
                return CurrentStatus != SquareLetter.Empty;
            }
        }

        internal bool SetLetter(Player player)
        {
            return Set(player.Letter);
        }


        public TicTacToeSqaure(TicTacToeBoard ticTacToeBoard, int myRow, int myColumn)
        {
            CurrentStatus = SquareLetter.Empty;
            this.Parent = ticTacToeBoard;
            Row = myRow;
            Column = myColumn;
        }

        private bool Set(SquareLetter status)
        {
            if(CurrentStatus == SquareLetter.Empty)
            {
                CurrentStatus = status;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
