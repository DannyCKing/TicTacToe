﻿using System;
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
    /// Interaction logic for SquareUserControl.xaml
    /// </summary>
    public partial class SquareUserControl : UserControl
    {
        public SquareUserControl()
        {
            InitializeComponent();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Grid_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            TicTacToeSqaure ticTacToeSqaure = (TicTacToeSqaure)DataContext;
            ticTacToeSqaure.Parent.Parent.PlaySquare(ticTacToeSqaure.Row, ticTacToeSqaure.Column);
        }
    }
}
