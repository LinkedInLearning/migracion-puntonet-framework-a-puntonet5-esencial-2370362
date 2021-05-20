using MatchingGame.Logic;
using System;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using MatchingGame.Services;

namespace MatchingGame
{
	public partial class MainForm : MetroForm
	{
		private Game _game;

		private readonly ISoundService _soundService;

		public MainForm(ISoundService soundService)
		{
			_soundService = soundService;
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
			if (closeCardTimer.Enabled)
			{
				return;
			}

			if (sender is Button btn)
			{
				var column = tableLayoutPanel1.GetColumn(btn);
				var row = tableLayoutPanel1.GetRow(btn);

				if (_game.IsOpen(column, row))
				{
					return;
				}

				_game.OpenCard(column, row);
				_ = _soundService.PlaySound(SoundService.Sound.FlipCard);
				UpdateCards();

				if (_game.RemainingCardsInTurn > 0)
				{
					return;
				}

				CheckForWinner();

				if (_game.CompleteTurn())
				{
					_ = _soundService.PlaySound(SoundService.Sound.Match);
					return;
				}
				else
				{
					_ = _soundService.PlaySound(SoundService.Sound.NotMatch);
				}

				closeCardTimer.Start();
			}
		}

		private void CheckForWinner()
		{
			if (!_game.IsComplete())
			{
				return;
			}

			var bestScore = GameSettings.Instance.BestScore;
			var currentScore = _game.Turns;
			GameSettings.Instance.UpdateScore(_game.Turns);

			string text;
			if (bestScore == 0)
			{
				text = $"It took you {currentScore} turns to complete. Keep it up!";
			}
			else if (bestScore < currentScore)
			{
				text = $"It took you {currentScore - bestScore} more turns than your previous best. Try harder!";
			}
			else
			{
				text = $"You set a new best with only {currentScore} turns!";
			}

			MessageBox.Show(text, "Congratulations!");
			StartNewGame();
		}

		private void closeCardTimer_Tick(object sender, EventArgs e)
		{
			closeCardTimer.Stop();
			_game.CloseCards();
			_ = _soundService.PlaySound(SoundService.Sound.TimerFlip);

			UpdateCards();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			StartNewGame();
			btn1.Focus();
		}



		private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			_soundService.PlaySound(SoundService.Sound.Moving);
		}

		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (keyData == Keys.Right || keyData == Keys.Left || keyData == Keys.Up || keyData == Keys.Down)
			{
				return false;
			}
			return base.ProcessDialogKey(keyData);
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Left || e.KeyData == Keys.Right || e.KeyData == Keys.Up || e.KeyData == Keys.Down)
			{
				e.Handled = true;
				var control = this.ActiveControl;
				var column = tableLayoutPanel1.GetColumn(control);
				var row = tableLayoutPanel1.GetRow(control);

				if (e.KeyData == Keys.Left)
				{
					if (column== 0)
					{
						_soundService.PlaySound(SoundService.Sound.Edge);
						return;
					}
					else
					{
						column--;
					}
				}
				else if (e.KeyData == Keys.Right)
				{
					if (column== tableLayoutPanel1.ColumnCount- 1)
					{
						_soundService.PlaySound(SoundService.Sound.Edge);
						return;
					}
					else
					{
						column++;
					}
				}




				if (e.KeyData == Keys.Up)
				{
					if (row == 0)
					{
						_soundService.PlaySound(SoundService.Sound.Edge);
						return;
					}
					else
					{
						row--;
					}
				}
				else if (e.KeyData == Keys.Down)
				{
					if (row == tableLayoutPanel1.RowCount- 1)
					{
						_soundService.PlaySound(SoundService.Sound.Edge);
						return;
					}
					else
					{
						row++;
					}
				}

				var newControl = tableLayoutPanel1.GetControlFromPosition(column, row) as Button;
				newControl.Focus();
				_soundService.PlaySound(SoundService.Sound.Moving);
			}
		}
	}
}
