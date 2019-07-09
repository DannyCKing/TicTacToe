using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.ViewModels;

namespace TicTacToe.AI
{
    public enum GameState { InProgress, Win, Tie };

    public class TicTacToeDecisionNode
    {
        public List<TicTacToeDecisionNode> ChildNodes = new List<TicTacToeDecisionNode>();

        private TicTacToeGameModel ThisModel;

        public GameState GameState;

        public bool IsChildNode
        {
            get
            {
                return GameState != GameState.InProgress;
            }
        }

        public Player WinningPlayer = null;

        public bool IsOriginalPlayer = false;



        public bool CanWinNode = false;

        public int PreferredNodeForCurrentPlayerIndex = -1;

        public int LastRowMove = -1;
        public int LastColumnMove =- 1;

        public int Score = 0;

        private SquareLetter OriginalLetter;
        public SquareLetter CurrentPlayerLetter;


        /// <summary>
        /// Use this construtor for the "root" node
        /// </summary>
        /// <param name="currentGameModel"></param>
        /// <param name="currentPlayer"></param>
        /// <param name="currentPlayer"></param>
        public TicTacToeDecisionNode(TicTacToeGameModel currentGameModel, SquareLetter originalLetter, SquareLetter currentLetter)
        {
            OriginalLetter = originalLetter;
            CurrentPlayerLetter = currentLetter;
            ThisModel = currentGameModel;
            CreateChildNodes();
            SetNodeOutcome(this, true, originalLetter);
        } 

        public TicTacToeDecisionNode(TicTacToeGameModel currentGameModel, bool forOriginalPlayer, SquareLetter originalLetter, SquareLetter currentLetter, int i= -1, int j = -1)
        {
            LastRowMove = i;
            LastColumnMove = j;

            OriginalLetter = originalLetter;
            CurrentPlayerLetter = currentLetter;
            ThisModel = currentGameModel;
            CreateChildNodes();
            SetNodeOutcome(this, true, OriginalLetter);
        }

        private static void SetNodeOutcome(TicTacToeDecisionNode node, bool isForOriginalPlayer, SquareLetter originalLetter)
        {
            if(node.GameState == GameState.Win && node.WinningPlayer.Letter == originalLetter)
            {
                node.Score = 100;
                return;
            }
            else if (node.GameState == GameState.Win && node.WinningPlayer.Letter != originalLetter)
            {
                node.Score = -100;
                return;
            }
            else if (node.GameState == GameState.Tie)
            {
                node.Score = 0;
                return;
            }


            // check children
            if (isForOriginalPlayer)
            {
                // get max
                int max = int.MinValue;
                foreach (var childNode in node.ChildNodes)
                {
                    SetNodeOutcome(childNode, !isForOriginalPlayer, originalLetter);
                    max = Math.Max(max, childNode.Score);
                }
                node.Score = max;
                return;
            }
            else
            {
                // get min
                int min = int.MaxValue;
                foreach (var childNode in node.ChildNodes)
                {
                    SetNodeOutcome(childNode, !isForOriginalPlayer, originalLetter);
                    min = Math.Min(min, childNode.Score);
                }
                node.Score = min;
                return;
            }
        }

        private void SetNodeOutcomes()
        {
            //Set_Will_Win(this, OriginalPlayer);
            //Set_Will_Win_Or_Tie(this, OriginalPlayer);
            //Set_Will_Lose(this, OriginalPlayer);
            //Set_Can_Win(this, OriginalPlayer);
        }

        /*
        private void Set_Can_Win(TicTacToeDecisionNode ticTacToeDecisionNode, Player originalPlayer)
        {
            if(ticTacToeDecisionNode.IsWinForOriginalPlayer)
            {
                ticTacToeDecisionNode.CanWinNode = true;
                return;
            }

            if(ticTacToeDecisionNode.CurrentPlayer.Letter == originalPlayer.Letter)
            {
                // if I am making the next move and any children is a winning move or a "killer move" it is also a killer move
                foreach(var childNode in ChildNodes)
                {
                    if(childNode.IsWinForOriginalPlayer || childNode.CanWinNode)
                    {
                        ticTacToeDecisionNode.CanWinNode = true;
                        return;
                    }
                }
            }
            else
            {
                // if the other player is the next move and all of their children are killer moves, it is a killer move
                // if any of them are not killer moves, then it is not a killer move
                foreach (var childNode in ticTacToeDecisionNode.ChildNodes)
                {
                    Set_Can_Win(childNode, originalPlayer);
                    if (childNode.CanWinNode == false)
                    {
                        ticTacToeDecisionNode.CanWinNode = false;
                        return;
                    }
                }
                ticTacToeDecisionNode.CanWinNode = true;
                return;
            }
        }

        private void Set_Will_Lose(TicTacToeDecisionNode ticTacToeDecisionNode, Player playerForNode)
        {
            // No more child nodes, check these nodes
            if (ticTacToeDecisionNode.GameState == GameState.Tie)
            {
                ticTacToeDecisionNode.IsLosingNodeForOriginalPlayer = false;
                return;
            }
            else if (ticTacToeDecisionNode.GameState == GameState.Win && ticTacToeDecisionNode.WinningPlayer.Letter == playerForNode.Letter)
            {
                ticTacToeDecisionNode.IsLosingNodeForOriginalPlayer = false;
                return;
            }
            else if (ticTacToeDecisionNode.GameState == GameState.Win && ticTacToeDecisionNode.WinningPlayer.Letter != playerForNode.Letter)
            {
                ticTacToeDecisionNode.IsLosingNodeForOriginalPlayer = true;
                return;
            }

            if (ticTacToeDecisionNode.GameState != GameState.InProgress && ticTacToeDecisionNode.ChildNodes.Count > 0)
            {
                throw new InvalidOperationException("Invalid state");
            }

            foreach (var childNode in ticTacToeDecisionNode.ChildNodes)
            {
                Set_Will_Lose(childNode, playerForNode);

                if (childNode.IsLosingNodeForOriginalPlayer == false)
                {
                    ticTacToeDecisionNode.IsLosingNodeForOriginalPlayer = false;
                    return;
                }
            }

            ticTacToeDecisionNode.IsLosingNodeForOriginalPlayer = true;
        }

        private void Set_Will_Win_Or_Tie(TicTacToeDecisionNode ticTacToeDecisionNode, Player playerForNode)
        {
            // No more child nodes, check these nodes
            if (ChildNodes.Count == 0)
            {
                if (ticTacToeDecisionNode.GameState == GameState.Win && ticTacToeDecisionNode.WinningPlayer.Letter == playerForNode.Letter)
                {
                    ticTacToeDecisionNode.IsNonLosingNodeForOriginalPlayer = true;
                    return;
                }
                else if (ticTacToeDecisionNode.GameState == GameState.Win && ticTacToeDecisionNode.WinningPlayer.Letter != playerForNode.Letter)
                {
                    ticTacToeDecisionNode.IsNonLosingNodeForOriginalPlayer = false;
                    return;
                }
                else if (ticTacToeDecisionNode.GameState == GameState.Tie)
                {
                    ticTacToeDecisionNode.IsNonLosingNodeForOriginalPlayer = true;
                    return;
                }
            }

            if (ticTacToeDecisionNode.GameState != GameState.InProgress && ticTacToeDecisionNode.ChildNodes.Count > 0)
            {
                throw new InvalidOperationException("Invalid state");
            }

            foreach (var childNode in ticTacToeDecisionNode.ChildNodes)
            {
                Set_Will_Win_Or_Tie(childNode, playerForNode);
                if (childNode.IsNonLosingNodeForOriginalPlayer == false)
                {
                    ticTacToeDecisionNode.IsNonLosingNodeForOriginalPlayer = false;
                    return;
                }
            }

            ticTacToeDecisionNode.IsNonLosingNodeForOriginalPlayer = true;
        }

        private static void Set_Will_Win(TicTacToeDecisionNode ticTacToeDecisionNode, Player originalPlayer)
        {
            // No more child nodes, check these nodes
            if (ticTacToeDecisionNode.ChildNodes.Count == 0)
            {
                if (ticTacToeDecisionNode.GameState == GameState.Tie)
                {
                    ticTacToeDecisionNode.IsWinForOriginalPlayer = false;
                    return;
                }
                else if (ticTacToeDecisionNode.GameState == GameState.Win && ticTacToeDecisionNode.WinningPlayer.Letter != originalPlayer.Letter)
                {
                    ticTacToeDecisionNode.IsWinForOriginalPlayer = false;
                    return;
                }
                else if (ticTacToeDecisionNode.GameState == GameState.Win && ticTacToeDecisionNode.WinningPlayer.Letter == originalPlayer.Letter)
                {
                    ticTacToeDecisionNode.IsWinForOriginalPlayer = true;
                    return;
                }
            }

            if (ticTacToeDecisionNode.GameState != GameState.InProgress && ticTacToeDecisionNode.ChildNodes.Count > 0)
            {
                throw new InvalidOperationException("Invalid state");
            }

            foreach (var childNode in ticTacToeDecisionNode.ChildNodes)
            {
                Set_Will_Win(childNode, originalPlayer);
                if(childNode.IsWinForOriginalPlayer == false)
                {
                    ticTacToeDecisionNode.IsWinForOriginalPlayer = false;
                    return;
                }
            }

            ticTacToeDecisionNode.IsWinForOriginalPlayer = true;
        }
        */
        private void CreateChildNodes()
        {
            if(IsBoardInWinningState(ThisModel))
            {
                // don't add child nodes
                GameState = GameState.Win;
                //CurrentPlayer = null;
            }
            else if(IsBoardFull(ThisModel))
            {
                // don't add child nodes - game over with no winner
                GameState = GameState.Tie;
                //CurrentPlayer = null;
            }
            else
            {
                // there are still empty nodes
                GameState = GameState.InProgress;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (!ThisModel.Board.Squares[i, j].Played)
                        {
                            // Get the current player for the next turn
                            SquareLetter nextLetter = SquareLetter.O;
                            Player currentPlayer = ThisModel.Player1;
                            if(CurrentPlayerLetter == ThisModel.Player1.Letter)
                            {
                                nextLetter = this.ThisModel.Player2.Letter;
                                currentPlayer = this.ThisModel.Player2;
                            }
                            else
                            {
                                nextLetter = this.ThisModel.Player1.Letter;
                                currentPlayer = this.ThisModel.Player1;
                            }

                            // create a new game state for hypothetical move
                            var newGameState = new TicTacToeGameModel(ThisModel.Player1.PlayerType, ThisModel.Player2.PlayerType, ThisModel.Board, currentPlayer , i, j);

                            var childNode = new TicTacToeDecisionNode(newGameState, !IsOriginalPlayer, OriginalLetter, nextLetter, i, j);

                            ChildNodes.Add(childNode);
                        }
                    }
                }
            }
            
        }

        public static bool IsBoardFull(TicTacToeGameModel thisModel)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!thisModel.Board.Squares[i, j].Played)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        private bool IsBoardInWinningState(TicTacToeGameModel thisModel)
        {
            if(IsBoardInWinningStateForPlayer(ThisModel.Board, ThisModel.Player1.Letter))
            {
                WinningPlayer = new Player(this.ThisModel.Player1.Letter, this.ThisModel.Player1.PlayerType);
            }
            else if(IsBoardInWinningStateForPlayer(ThisModel.Board, ThisModel.Player2.Letter))
            {
                WinningPlayer = new Player(this.ThisModel.Player2.Letter, this.ThisModel.Player2.PlayerType);
            }
            else
            {
                return false;
            }
            return true;
        }

        public static bool IsBoardInWinningStateForPlayer(TicTacToeBoard board, SquareLetter letter)
        {
            // check for diagonal win from top left to bottom right
            if(board.Squares[0,0].CurrentStatus == letter && board.Squares[1, 1].CurrentStatus == letter && board.Squares[2, 2].CurrentStatus == letter)
            {
                return true;
            }

            // check for diagonal win form bottom left to top right
            if (board.Squares[2, 0].CurrentStatus == letter && board.Squares[1, 1].CurrentStatus == letter && board.Squares[0, 2].CurrentStatus == letter)
            {
                return true;
            }

            // check for top row win
            if (board.Squares[0, 0].CurrentStatus == letter && board.Squares[0, 1].CurrentStatus == letter && board.Squares[0, 2].CurrentStatus == letter)
            {
                return true;
            }

            // check for middle row win
            if (board.Squares[1, 0].CurrentStatus == letter && board.Squares[1, 1].CurrentStatus == letter && board.Squares[1, 2].CurrentStatus == letter)
            {
                return true;
            }

            // check for bottom row win
            if(board.Squares[2, 0].CurrentStatus == letter && board.Squares[2, 1].CurrentStatus == letter && board.Squares[2, 2].CurrentStatus == letter)
            {
                return true;
            }


            // check for left column win
            if (board.Squares[0, 0].CurrentStatus == letter && board.Squares[1, 0].CurrentStatus == letter && board.Squares[2, 0].CurrentStatus == letter)
            {
                return true;
            }


            // check for middle column win
            if (board.Squares[0, 1].CurrentStatus == letter && board.Squares[1, 1].CurrentStatus == letter && board.Squares[2, 1].CurrentStatus == letter)
            {
                return true;
            }

            // check for right column win
            if (board.Squares[0, 2].CurrentStatus == letter && board.Squares[1, 2].CurrentStatus == letter && board.Squares[2, 2].CurrentStatus == letter)
            {
                return true;
            }

            return false;
        }

        public MoveModel GetBestMove()
        {
            //get the best move for the current player 
            //var decisionNode = new TicTacToeDecisionNode(ticTacToeGameModel, ticTacToeGameModel.CurrentPlayer, ticTacToeGameModel.CurrentPlayer);

            List<TicTacToeDecisionNode> bestMoves = this.ChildNodes.OrderByDescending(node => node.Score).ToList();
            var result = bestMoves.First();

            return new MoveModel(result.LastRowMove, result.LastColumnMove);
        }

    }
}
