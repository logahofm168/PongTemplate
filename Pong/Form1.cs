using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        //Global variables
        Rectangle player1 = new Rectangle(10, 130, 10, 60);
        Rectangle player2 = new Rectangle(10, 230, 10, 60);
        Rectangle ball = new Rectangle(295, 195, 10, 10);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 5;
        int ballXSpeed = 8;
        int ballYSpeed = -8;

        int playerTurn = 1;

        bool wPressed = false;
        bool sPressed = false;
        bool aPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;  

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        Pen whitepen = new Pen(Color.White);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move ball 
            ball.X += ballXSpeed;
            ball.Y += ballYSpeed;

            //move player 1 
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
            }

            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
            }

            if (aPressed == true && player1.X > 0)
            {
                player1.X -= playerSpeed;
            }

            if (dPressed == true && player1.X < this.Width - player1.Width)
            {
                player1.X += playerSpeed;
            }

            //move player2
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
            }

            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
            }

            if (leftPressed == true && player2.X > 0)
            {
                player2.X -= playerSpeed;
            }

            if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
            }

            //check if ball hit top or bottom wall and change direction if it does 
            if (ball.Y < 0 || ball.Y > this.Height - ball.Height)
            {
                ballYSpeed *= -1;  // or: ballYSpeed = -ballYSpeed; 
            }

            if (ball.X < 0 || ball.X > this.Width - ball.Width)
            {
                ballXSpeed *= -1;
            }

            //check if ball hits either player. If it does change the direction 
            //and place the ball in front of the player hit 
            if(player1.IntersectsWith(ball) )
            {
                ballXSpeed *= -1;
                ball.X = player1.X + player1.Width;
            }
            else if (player2.IntersectsWith(ball) )
            {
                ballXSpeed *= -1;
                ball.X = player2.X - ball.Width;
            }

            //check if a player missed the ball and if true add 1 to score of other player  
            if (ball.X < 0)
            {
                player2Score++;

                ball.X = 295;
                ball.Y = 195;

                player1.Y = 130;
                player2.Y = 230;
            }
            
            if(player1Score == 3)
            {
                gameTimer.Stop();

                Winnerlabel.Text = "Player 1 wins";

                ball.X = 1000;
            }

            if(player2Score == 3)
            {
                gameTimer.Stop();

                Winnerlabel.Text = "Player 2 wins";

                ball.X = 1000;
            }

                Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            p1ScoreLabel.Text = $"{player1Score}";
            p2ScoreLabel.Text = $"{player2Score}";

            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillEllipse(whiteBrush, ball);
        }
    }
}