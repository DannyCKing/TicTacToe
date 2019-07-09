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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicTacToe.ViewModels;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class HighlightLabel : Window
    {
        private SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);
        private SolidColorBrush BlueBrush = new SolidColorBrush(Colors.LightBlue);
        private SolidColorBrush BlackBrush = new SolidColorBrush(Colors.Black);

        private Thickness WithBorder = new Thickness(5);
        private Thickness NoBorder = new Thickness(0);

        private bool IsPlayer1Human = true;
        private bool IsPlayer2Human = false;
        public HighlightLabel()
        {
            InitializeComponent();
        }

        private void Player1HumanButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowSelectedLabels(Player1HumanButton, Player1ComputerButton);
            IsPlayer1Human = true;
        }

        private void Player1ComputerButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowSelectedLabels(Player1ComputerButton, Player1HumanButton);
            IsPlayer1Human = false;
        }

        private void Player2HumanButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowSelectedLabels(Player2HumanButton, Player2ComputerButton);
            IsPlayer2Human = true;
        }

        private void Player2ComputerButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowSelectedLabels(Player2ComputerButton, Player2HumanButton);
            IsPlayer2Human = false;
        }

        private void ShowSelectedLabels(Label selectedLabel, Label notSelectedLabel)
        {
            selectedLabel.Background = BlueBrush;
            selectedLabel.BorderBrush = BlackBrush;
            selectedLabel.BorderThickness = WithBorder;

            notSelectedLabel.Background = TransparentBrush;
            notSelectedLabel.BorderBrush = TransparentBrush;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            PlayerType player1Type = PlayerType.Computer;
            if(IsPlayer1Human)
            {
                player1Type = PlayerType.Human;
            }

            PlayerType player2Type = PlayerType.Computer;
            if (IsPlayer2Human)
            {
                player2Type = PlayerType.Human;
            }

            TicTacToeWindow gameWindow = new TicTacToeWindow(player1Type, player2Type);
            gameWindow.ShowDialog();
            this.Show();
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
