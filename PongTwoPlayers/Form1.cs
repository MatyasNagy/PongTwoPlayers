using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PongTwoPlayers
{
    public partial class Form1 : Form
    {

        int BallSpeed = 5;
        int score = 0;

        bool racketLeftUP= false;
        bool racketLeftDOWN = false;
        bool racketRightUP = false;
        bool racketRightDOWN = false;
        

        //Balls direction on the PlayGround
        bool ballDOWN = true;
        bool ballRIGHT = true;

        public Form1()
        {
            InitializeComponent();

            Cursor.Hide();
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            Bounds = Screen.PrimaryScreen.Bounds;

            ResetGame();
            
        }

        private void ResetGame()
        {
            GameOverLabel.Visible = false;
            RacketLeft.Left = PlayGround.Left + 20;
            RacketLeft.Top = PlayGround.Bottom / 2-RacketLeft.Height/2;
            RacketRight.Left = PlayGround.Width - (20+RacketRight.Width);
            RacketRight.Top = PlayGround.Bottom / 2-RacketRight.Height/2;
            BallStartPosition();
            timer.Enabled = true;
            score = 0;
            Scoring(0);

        }

        private void BallStartPosition()
        {
            Random r = new Random();
            Ball.Left = PlayGround.Width / 2;
            Ball.Top = r.Next(0, PlayGround.Bottom);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // To much keys down at a sametime, it must recognize for the pressed buttons
            //


            if (e.KeyCode == Keys.Escape) { Close(); }
            if (e.KeyCode == Keys.F1) { ResetGame(); }

            if (e.KeyCode == Keys.Q) { racketLeftUP = true; }
            if (e.KeyCode == Keys.A) { racketLeftDOWN = true; }
            if (e.KeyCode == Keys.O) { racketRightUP = true; }
            if (e.KeyCode == Keys.L) { racketRightDOWN= true; }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q) { racketLeftUP = false; }
            if (e.KeyCode == Keys.A) { racketLeftDOWN = false; }
            if (e.KeyCode == Keys.O) { racketRightUP = false; }
            if (e.KeyCode == Keys.L) { racketRightDOWN = false; }
        }

        private void RocketsMove()
        {
            //
            // Separeted Method this move the rackets, not problem with multiple keys down
            //

            if (racketLeftUP) { RacketLeft.Top -= 10; }
            if (racketLeftDOWN) { RacketLeft.Top += 10; }
            if (racketRightUP) { RacketRight.Top -= 10; }
            if (racketRightDOWN) { RacketRight.Top += 10; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = 10;
            RocketsMove();
            BallMove();
        }


        private void BallMove()
        {
            //From ball position -> To ball direction

            if (Ball.Top <= PlayGround.Top) { ballDOWN = true; }
            if (Ball.Bottom >= PlayGround.Bottom) { ballDOWN = false; }

            if (Ball.Left >= RacketLeft.Right && Ball.Right >= RacketRight.Left && Ball.Top>= RacketRight.Top &&Ball.Bottom<=RacketRight.Bottom) { ballRIGHT = false; Scoring(1); }
            if (Ball.Right > RacketRight.Right) { GameOver(2); }
            if (Ball.Left<=RacketLeft.Right && Ball.Right <= RacketRight.Left&& Ball.Top >= RacketLeft.Top && Ball.Bottom <= RacketLeft.Bottom) { ballRIGHT = true; Scoring(1); }
            if (Ball.Left<RacketLeft.Left) { GameOver(1); }

            //Ball move to a chosed direction.
            if      (ballDOWN) { Ball.Top += BallSpeed; }
            else    { Ball.Top -= BallSpeed; }
            if      (ballRIGHT) { Ball.Left += BallSpeed; }
            else    { Ball.Left -= BallSpeed; }

            

        }

        private void GameOver(int loser_playerID)
        {
            if (loser_playerID == 2) { GameOverLabel.Text = "GAME OVER\nWinner: Player1\nScore: "+ score.ToString()+"\nRestart: F1\nExit: Esc"; }
            if (loser_playerID == 1) { GameOverLabel.Text = "GAME OVER\nWinner: Player2\nScore: "+score.ToString()+"\nRestart: F1\nExit: Esc"; }

            GameOverLabel.Left = (PlayGround.Width / 2) - (GameOverLabel.Width / 2);
            GameOverLabel.Top= (PlayGround.Height / 2) - (GameOverLabel.Height / 2);
            GameOverLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            GameOverLabel.Visible=true;
            timer.Enabled = false;
        }

        private void Scoring(int sco)
        {
            score += sco;
            scoreLabel.Text = "Score: "+score.ToString();
            scoreLabel.Left = (PlayGround.Width / 2)-(scoreLabel.Width/2);
        }
    }
}
