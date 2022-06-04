using BoxGame.Controllers;
using BoxGame.Entites;
using BoxGame.Mogels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxGame
{
    public partial class Form1 : Form
    {
        public Image hero;
        public Entity player;
        private static Stopwatch gameTimer;




        public Form1()
        {
            gameTimer = new Stopwatch();

            Button ButtonIn = new Button();
            ButtonIn.Width = 641;
            ButtonIn.Height = 170;
            ButtonIn.BackgroundImage = Resource1.ButtonIn;
            ButtonIn.FlatStyle = FlatStyle.Flat;
            ButtonIn.ForeColor = Color.Transparent;
            ButtonIn.BackgroundImageLayout = ImageLayout.Stretch;
            ButtonIn.BackColor = Color.Transparent;
            ButtonIn.FlatAppearance.BorderSize = 0;
            ButtonIn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ButtonIn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ButtonIn.Location = new Point(704, 335);
            ButtonIn.Click += (sender, e) =>
            {
                this.Paint += (s, g) =>
                {
                    mapController.DrawMap(g.Graphics);
                    Chest.DrawChests(g.Graphics);
                    player.PlayAnimation(g.Graphics);
                    gameTimer.Start();
                };
                Controls.Clear();
                BackgroundImage = Properties.Resources.grass1;
            };

            Button ButtonOut = new Button();
            ButtonOut.Width = 641;
            ButtonOut.Height = 170;
            ButtonOut.BackgroundImage = Resource1.ButtonOut;
            ButtonOut.FlatStyle = FlatStyle.Flat;
            ButtonOut.ForeColor = Color.Transparent;
            ButtonOut.BackgroundImageLayout = ImageLayout.Stretch;
            ButtonOut.BackColor = Color.Transparent;
            ButtonOut.FlatAppearance.BorderSize = 0;
            ButtonOut.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ButtonOut.FlatAppearance.MouseOverBackColor = Color.Transparent;
            ButtonOut.Location = new Point(704, 615);
            ButtonOut.Click += (s,e) => Application.Exit();
            Controls.Add(ButtonIn);
            Controls.Add(ButtonOut);

            InitializeComponent();

            timer1.Interval = 20;
            timer1.Tick += new EventHandler(Update);
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);


            Init();

        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = 0;
                    break;
                case Keys.S:
                    player.dirY = 0;
                    break;
                case Keys.A:
                    player.dirX = 0;
                    break;
                case Keys.D:
                    player.dirX = 0;
                    break;
            }
            if (player.dirX == 0 && player.dirY == 0)
            {
                player.isMoving = false;
                player.SetAnimationConfiguration(0);
            }
        }

        public void OnPress(object sender,KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.W:
                    player.dirY = -25;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(2);
                    break;
                case Keys.S:
                    player.dirY = 25;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(1);
                    break;
                case Keys.A:
                    player.dirX = -25;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(3);
                    break;
                case Keys.D:
                    player.dirX = 25;
                    player.isMoving = true;
                    player.SetAnimationConfiguration(4);
                    break;
                default:
                    player.SetAnimationConfiguration(0);
                    break;
            }
            
        }

        public void Init()
        {
            this.Width = mapController.GetWidth();
            this.Width = mapController.GetHeight();
            mapController.Init();
            hero = Properties.Resources.moveHero12;
            player = new Entity(100, 100, Hero.idleFrames, Hero.runBackFrames, Hero.runForwardFrames,Hero.runLeftFrames,Hero.runRightFrames, hero);
            player.type = MapEntityType.Player;
            timer1.Start();
        }
        public void Update(object sender, EventArgs e)
        {
            if (player.isMoving)
            player.Move();
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        }

        public static void ShowExitPanel()
        {
            gameTimer.Stop();
            var time = string.Format("Ваше время составило {0} секунд", gameTimer.ElapsedMilliseconds / 1000);
            var endMessage = MessageBox.Show(time, "Вы выиграли", MessageBoxButtons.OK);
            if(endMessage == DialogResult.OK)
                Application.Exit();
        }
    }
}