using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicTacToe.ViewModels;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeWindow.xaml
    /// </summary>
    public partial class TicTacToeWindow : Window
    {
        public TicTacToeWindow()
        {
            DataContext = new TicTacToeGameModel(PlayerType.Human, PlayerType.Human);

            InitializeComponent();
        }

        public TicTacToeWindow(PlayerType player1Type, PlayerType player2Type)
        {
            DataContext = new TicTacToeGameModel(player1Type, player2Type);

            InitializeComponent();

            this.Loaded += TicTacToeWindow_Loaded;
        }

        private void TicTacToeWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
