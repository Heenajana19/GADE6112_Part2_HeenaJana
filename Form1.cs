using System;
using System.Windows.Forms;

namespace GADE_POE_HEENA_JANA
{
    public partial class Form1 : Form
    {
        private GameEngine engine;

        public Form1()
        {
            InitializeComponent();
            // Initialize 10x10 map grid
            engine = new GameEngine(10, 10);
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
            UpdateUI();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    engine.MoveHero(Direction.Up);
                    break;
                case Keys.S:
                case Keys.Down:
                    engine.MoveHero(Direction.Down);
                    break;
                case Keys.A:
                case Keys.Left:
                    engine.MoveHero(Direction.Left);
                    break;
                case Keys.D:
                case Keys.Right:
                    engine.MoveHero(Direction.Right);
                    break;
            }

            UpdateUI();
        }

        private void UpdateUI()
        {
            // Render map grid to RichTextBox or Label (assuming control name: rtbMap)
            rtbMap.Clear();
            for (int y = 0; y < engine.CurrentLevel.Height; y++)
            {
                for (int x = 0; x < engine.CurrentLevel.Width; x++)
                {
                    Tile tile = engine.CurrentLevel.Grid[x, y];
                    rtbMap.AppendText(tile.ToString() + " ");
                }
                rtbMap.AppendText("\n");
            }

            // Display Hero Health (assuming control name: lblStats)
            if (lblStats != null)
            {
                lblStats.Text = $"Hero HP: {engine.CurrentLevel.Hero.CurrentHealth} / {engine.CurrentLevel.Hero.MaxHealth}";
                if (engine.CurrentLevel.Hero.IsDead)
                {
                    lblStats.Text += " | GAME OVER!";
                }
            }
        }
    }
}
