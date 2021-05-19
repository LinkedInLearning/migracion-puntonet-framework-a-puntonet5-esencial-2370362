﻿using MatchingGame.Logic;
using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MatchingGame
{
    public partial class MainForm : MetroForm
    {
        private Game _game;

        public MainForm()
        {
            InitializeComponent();
        }

        private void StartNewGame()
        {
            _game = Game.Create();

            for (var i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                var btn = (Button)tableLayoutPanel1.Controls[i];
                var column = tableLayoutPanel1.GetColumn(btn);
                var row = tableLayoutPanel1.GetRow(btn);

                btn.Text = _game.GetCard(column, row).ToString();
                btn.ForeColor = btn.BackColor;
            }

            UpdateCards();
        }

        private void UpdateCards()
        {
            for (var w = 0; w < _game.Width; w++)
            {
                for (var h = 0; h < _game.Height; h++)
                {
                    var btn = tableLayoutPanel1.GetControlFromPosition(w, h);
                    btn.ForeColor = _game.IsOpen(w, h) ? Color.Black : btn.BackColor;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (closeCardTimer.Enabled == true)
                return;

            if (sender is Button btn)
            {
                var column = tableLayoutPanel1.GetColumn(btn);
                var row = tableLayoutPanel1.GetRow(btn);

                if (_game.IsOpen(column, row))
                    return;

                _game.OpenCard(column, row);
                UpdateCards();

                if (_game.RemainingCardsInTurn > 0)
                    return;

                CheckForWinner();

                if (_game.CompleteTurn())
                    return;

                closeCardTimer.Start();
            }
        }

        private void CheckForWinner()
        {
            if (!_game.IsComplete())
                return;

            var bestScore = GameSettings.Instance.BestScore;
            var currentScore = _game.Turns;
            GameSettings.Instance.UpdateScore(_game.Turns);

            string text;
            if (bestScore == 0)
                text = $"It took you {currentScore} turns to complete. Keep it up!";
            else if (bestScore < currentScore)
                text = $"It took you {currentScore - bestScore} more turns than your previous best. Try harder!";
            else
                text = $"You set a new best with only {currentScore} turns!";

            MessageBox.Show(text, "Congratulations!");
            StartNewGame();
        }

        private void closeCardTimer_Tick(object sender, EventArgs e)
        {
            closeCardTimer.Stop();
            _game.CloseCards();
            UpdateCards();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }
    }
}
