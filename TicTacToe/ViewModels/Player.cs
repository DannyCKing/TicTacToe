using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.ViewModels
{
    public enum PlayerType { Human, Computer}

    public class Player : ViewModelBase
    {
        private int _Wins;

        public int Wins
        {
            get
            {
                return _Wins;
            }
            set
            {
                _Wins = value;
                RaisePropertyChanged();
            }
        }

        private SquareLetter _Letter;

        public SquareLetter Letter
        {
            get
            {
                return _Letter;
            }
            private set
            {
                _Letter = value;
                RaisePropertyChanged();
            }
        }

        private PlayerType _PlayerType;

        public PlayerType PlayerType
        {
            get
            {
                return _PlayerType;
            }
            private set
            {
                _PlayerType = value;
                RaisePropertyChanged();
            }
        }


        public Player(SquareLetter playerLetter, PlayerType playerType)
        {
            Letter = playerLetter;
            this.PlayerType = playerType;
        }

        public void Play(TicTacToeGameModel ticTacToeGameModel)
        {
            if(PlayerType == PlayerType.Computer)
            {
                // don't allow keyboard inputs
                ticTacToeGameModel.AllowMouseInput = false;
                // do AI move
                MoveModel move = null;
                try
                {
                    move = AI.AIUtilities.GetBestMove(ticTacToeGameModel);
                }
                catch(Exception e)
                {
                    //Unable to get best move;
                    Console.WriteLine("Error in getting best move");
                }
                ticTacToeGameModel.PlaySquare(move.Row, move.Column);
            }
            else
            {
                // wait for human
            }
        }
    }
}
