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
        int jediSpeed = 3;
        int score = 0;
        Random ranNum = new Random();

        List<PictureBox> totaljedi = new List<PictureBox>();
        public Form1()
        {
            InitializeComponent();
            RestartGame();
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
                trooper.Image = Properties.Resources.deadtroop;
            }

            txtAmmo.Text = "Ammo: " + ammo;
            txtScore.Text = "Kills:" + score;

            if (goLeft == true && trooper.Left > 0)
            //that method checks for space from the container to an object
            //if there's space, 
            {
                trooper.Left -= speed;
            }
            if (goRight == true && trooper.Left + trooper.Width < this.ClientSize.Width)
            {
                trooper.Left += speed;
            }
            if (goUp == true && trooper.Top > 0)
            {
                trooper.Top -= speed;
            }
            if (goDown == true && trooper.Top + trooper.Height < this.ClientSize.Height)
            {
                trooper.Top += speed;
            }


            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "ammo")
                {
                    if (trooper.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        ((PictureBox)x).Dispose();
                        ammo += 5;
                    }
                }

                //getting rid of lazer beam from game

                if(x is PictureBox && (string)x.Tag == "lazer")
                {
                    
                }

                //trooper contact with jedi
                if(x is PictureBox && (string)x.Tag == "jedi")
                {
                    if (((PictureBox) x).Bounds.IntersectsWith(trooper.Bounds)){
                        trooperHealth -= 5;

                    }
                }


                if (x is PictureBox && (string)x.Tag == "jedi")
                {
                    if (x.Left > trooper.Left)
                    {
                        x.Left -= jediSpeed;
                        ((PictureBox)x).Image = Properties.Resources.jedileft;
                    }
                    if (x.Left < trooper.Left)
                    {
                        x.Left += jediSpeed;
                        ((PictureBox)x).Image = Properties.Resources.jedilright;
                    }
                    if (x.Top > trooper.Top)
                    {
                        x.Top -= jediSpeed;
                        ((PictureBox)x).Image = Properties.Resources.jediup;
                    }
                    if (x.Top < trooper.Top)
                    {
                        x.Top += jediSpeed;
                        ((PictureBox)x).Image = Properties.Resources.jedildown;
                    }

                }



                foreach (Control j in this.Controls)
                {
                    if ((j is PictureBox && (string)j.Tag == "lazer") && (x is PictureBox && (string)x.Tag == "jedi"))
                    {
                        if (x.Bounds.IntersectsWith(j.Bounds))
                        {
                            score++;

                            this.Controls.Remove(j);
                            j.Dispose();
                            this.Controls.Remove(x);
                            x.Dispose();
                           
                            CreateJedi();
                        }
                    }
                }


            }

        }
            private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
                facing = "left";
                trooper.Image = Properties.Resources.trooperleft;

            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
                facing = "right";
                trooper.Image = Properties.Resources.trooperright;

            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
                facing = "up";
                trooper.Image = Properties.Resources.trooperup;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
                facing = "down";
                trooper.Image = Properties.Resources.trooperdown;
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

                if (ammo > 0)
                {
                    ShootLazer(facing);
                    ammo--;
                }
                if (ammo<1)
                {
                    DropAmmo();
                }
               



            }


        }

        public void DropAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Image = Properties.Resources.ammo_Image;
            ammo.Tag = "ammo";
            ammo.Top = ranNum.Next(200, 700);
            ammo.Left = ranNum.Next(100, 1000);
            this.Controls.Add(ammo);
            this.BringToFront();
       
            
        }

        private void ShootLazer(string direction)
        {
            Lazer shootLazer = new Lazer();
            shootLazer.direction = direction;
            shootLazer.lazerLeft = trooper.Left + (trooper.Width / 2); //the beam originates from middle of trooper
            shootLazer.lazerTop = trooper.Top + (trooper.Height / 2);
            shootLazer.MakeLazer(this);
        }

        private void CreateJedi()
        {
            PictureBox jedi = new PictureBox();
            jedi.Image = Properties.Resources.jedileft;
            jedi.Tag = "jedi";
       
            jedi.Left = ranNum.Next(0, 900);
            jedi.Top = ranNum.Next(0, 800);
            jedi.SizeMode = PictureBoxSizeMode.AutoSize;
            totaljedi.Add(jedi);
            this.Controls.Add(jedi);
            trooper.BringToFront();

        }

        private void RestartGame()
        {
            trooper.Image = Properties.Resources.trooperright;

            foreach(PictureBox x in totaljedi){
                this.Controls.Remove(x);
            }

            totaljedi.Clear();

            for(int i = 0; i < 3; i++)
            {
                CreateJedi();
            }
            goDown = false;
            goUp = false;
            goLeft = false;
            goRight = false;

            trooperHealth = 100;
            score = 0;
            ammo = 10;
            GameTimer.Start();
        }

 


    }
}
