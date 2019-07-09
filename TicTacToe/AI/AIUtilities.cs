using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.ViewModels;

namespace TicTacToe.AI
{
    public class AIUtilities
    {
        public static MoveModel GetBestMove(TicTacToeGameModel model)
        {
            //get the best move for the current player 
            TicTacToeGameModel fakeGameModel = null;
            try
            {
                fakeGameModel = new TicTacToeGameModel(model.Player1.PlayerType, model.Player2.PlayerType, model.Board, model.CurrentPlayer);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to create game model");
                throw e;
            }

            TicTacToeDecisionNode decisionNode = null;
            try
            {
                Player lastPlayer = fakeGameModel.Player1;
                //use last player as current player
                if(fakeGameModel.CurrentPlayer == fakeGameModel.Player1)
                {
                    lastPlayer = fakeGameModel.Player2;
                }
                decisionNode = new TicTacToeDecisionNode(fakeGameModel, fakeGameModel.CurrentPlayer.Letter, lastPlayer.Letter);
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to create decision node");
                throw e;
            }

            MoveModel bestMove = null;

            try
            {
                bestMove = decisionNode.GetBestMove();
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to get best move");
                throw e;
            }

            return bestMove;
        }
    }
}
