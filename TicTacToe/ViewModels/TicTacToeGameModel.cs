using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicTacToe.AI;

namespace TicTacToe.ViewModels
{
    public enum PlayerTurn { X, O };

    public class TicTacToeGameModel : ViewModelBase
    {
        private bool _IsOver = false;

        public bool IsOver
        {
            get
            {
                return _IsOver;
            }
            set
            {
                _IsOver = value;
                RaisePropertyChanged();
            }
        }

        private Player _Player1;

        public Player Player1
        {
            get
            {
                return _Player1;
            }
            set
            {
                _Player1 = value;
                RaisePropertyChanged();
            }
        }

        private Player _Player2;

        public Player Player2
        {
            get
            {
                return _Player2;
            }
            set
            {
                _Player2 = value;
                RaisePropertyChanged();
            }
        }

        private Player _CurrentPlayer;

        public Player CurrentPlayer
        {
            get
            {
                return _CurrentPlayer;
            }
            set
            {
                _CurrentPlayer = value;
                RaisePropertyChanged();
            }
        }

        private TicTacToeBoard _Board;
        internal bool AllowMouseInput = true;
        private int j;

        public TicTacToeBoard Board
        {
            get
            {
                return _Board;
            }
            set
            {
                _Board = value;
                RaisePropertyChanged();
            }
        }

        public TicTacToeGameModel(PlayerType player1Type, PlayerType player2Type)
        {
            Board = new TicTacToeBoard(this);
            Player1 = new Player(SquareLetter.X, player1Type);
            Player2 = new Player(SquareLetter.O, player2Type);
            CurrentPlayer = Player1;
            CurrentPlayer.Play(this);
        }

        public void Reset()
        {
            Board = new TicTacToeBoard(this);
            CurrentPlayer = Player1;
            CurrentPlayer.Play(this);
        }

        public TicTacToeGameModel(PlayerType player1Type, PlayerType player2Type, TicTacToeBoard board, Player currentPlayer)
        {
            // this is simulation
            Board = new TicTacToeBoard(this, board);
            CurrentPlayer = currentPlayer;
            Player1 = new Player(SquareLetter.X, player1Type);
            Player2 = new Player(SquareLetter.O, player2Type);
        }

        public TicTacToeGameModel(PlayerType player1Type, PlayerType player2Type, TicTacToeBoard board, Player currentPlayer, int i, int j)
        {
            // this is simulation
            Board = new TicTacToeBoard(this, board);
            CurrentPlayer = currentPlayer;
            Player1 = new Player(SquareLetter.X, player1Type);
            Player2 = new Player(SquareLetter.O, player2Type);
            SetSquare(i, j, currentPlayer);
        }

        private void SetSquare(int i, int j, Player currentPlayer)
        {
            Board.SetSqaure(i, j, currentPlayer);
        }

        public void StartGame()
        {
        }

        public void PlaySquare(int row, int col, bool IsSimulation = false)
        {
            //if(this.AllowMouseInput == false)
            //{
            //    return;
            //}

            bool result = Board.SetSqaure(row, col, CurrentPlayer);
            if(result)
            {
                // check for end of game
                if(TicTacToeDecisionNode.IsBoardInWinningStateForPlayer(Board, CurrentPlayer.Letter))
                {
                    if(CurrentPlayer == Player1)
                    {
                        MessageBox.Show("Player 1 wins");
                        Player1.Wins = Player1.Wins + 1;
                        Reset();
                    }
                    else
                    {
                        MessageBox.Show("Player 2 wins");
                        Player2.Wins = Player2.Wins + 1;
                        Reset();
                    }
                    return;
                }

                if(TicTacToeDecisionNode.IsBoardFull(this))
                {
                    MessageBox.Show("Tie. No winner.");
                    Reset();
                    return;
                }

                // Move to next player
                if (CurrentPlayer == Player1)
                {
                    CurrentPlayer = Player2;
                }
                else
                {
                    CurrentPlayer = Player1;
                }
                if(IsSimulation == false)
                {
                    CurrentPlayer.Play(this);
                }
            }
        }
    }
}
