using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;


namespace StormTrooperShooter
{
    class Lazer
    {
        public string direction;
        public int lazerLeft;
        public int lazerTop;

        private int speed = 20;
        private PictureBox lazer = new PictureBox();
        private Timer lazerTimer = new Timer();


        public void MakeLazer(Form form)
            //takes in form(our game)
        {
            lazer.BackColor = Color.Red;
            lazer.Top = lazerTop;
            lazer.Left = lazerLeft;
            lazer.Size = new Size(50, 5);
            lazer.BringToFront();
            form.Controls.Add(lazer);
            lazer.Tag = "lazer";

            lazerTimer.Interval = speed;
            lazerTimer.Tick += new EventHandler(LazerTimerEvent);
            lazerTimer.Start();


        }

        private void LazerTimerEvent(object sender, EventArgs e)
        {

            //determines direciton bullet will travel
            if (direction == "left")
            {
                lazer.Left -= speed;
            }
            if (direction == "right")
            {
                lazer.Left += speed;
            }
            if (direction == "up")
            {
                lazer.Size = new Size(5, 50);
                lazer.Top -= speed;
            }
            if (direction == "down")
            {
                lazer.Size = new Size(5, 50);
                lazer.Top += speed;
            }

            if(lazer.Left<10|| lazer.Left > 1000|| lazer.Top<10||lazer.Top>1000)
            {
                lazerTimer.Stop();
                lazerTimer.Dispose();
                lazer.Dispose();
                lazer = null;
                lazerTimer = null;


            }

            

        }
    }
}
