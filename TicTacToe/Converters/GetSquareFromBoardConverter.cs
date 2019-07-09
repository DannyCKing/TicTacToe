using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TicTacToe.ViewModels;

namespace TicTacToe.Converters
{
    public class GetSquareFromBoardConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (parameter == null)
                {
                    return new TicTacToeSqaure(new TicTacToeBoard(new TicTacToeGameModel(PlayerType.Human, PlayerType.Computer)), 0, 0);
                }
                int index = int.Parse(parameter.ToString());

                TicTacToeBoard board = (TicTacToeBoard)value;

                int row = index / 3;

                int column = index % 3;

                return board.Squares[row, column];
            }
            catch(Exception e)
            {
                return new TicTacToeSqaure(new TicTacToeBoard(new TicTacToeGameModel(PlayerType.Human, PlayerType.Computer)), 0, 0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
