using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StormTrooperShooter
{
    public partial class Form1 : Form
    {
        bool goLeft;
        bool goRight;
        bool goUp;
        bool goDown;
        bool gameOver;
        string facing = "right";
        int trooperHealth = 100;
        int speed = 10;
        int ammo = 10;
        int zombieSpeed = 3;
        int score = 0;
        Random ranNum = new Random();

        List<PictureBox> jediHitBoxes = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
        }


     

        private void MainTimerEvent(object sender, EventArgs e)
        {
            if (trooperHealth > 1)
            {
                healthBar.Value = trooperHealth;
            }
            else
            {
                gameOver = true;
            }

            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text="Kills:" + score;

            if(goLeft==true && trooper.Left > 0)
                //that method checks for space from the container to an object
                //if there's space, 
            {
                trooper.Left -= speed;
            }
            if(goRight==true && trooper.Left + trooper.Width < this.ClientSize.Width)
            {
                trooper.Left += speed;
            }
            if(goUp==true && trooper.Top > 0)
            {
                trooper.Top -= speed;
            }
            if (goDown == true && trooper.Top + trooper.Height < this.ClientSize.Height)
            {
                trooper.Top += speed;
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                trooper.Image = Properties.Resources.troopleft;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                trooper.Image = Properties.Resources.troopright;

            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                trooper.Image = Properties.Resources.troopup;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                trooper.Image = Properties.Resources.troopdown;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
             


            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
                

            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
        
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
             
            }

            if(e.KeyCode == Keys.Space){

                ShootLazer(facing);

            }


        }

        private void ShootLazer(string direction)
        {

        }

        private void CreateJedi()
        {

        }

        private void RestartGame()
        {

        }
    }
}
