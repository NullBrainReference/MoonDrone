using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoonDrone
{
    public partial class Form1 : Form
    {
        private Drone drone;
        private PlanetCell[,] planetCells;
        private Button[,] cellButtons;
        private Random rnd = new Random();
        private TextBox[] leftBoxes;
        private TextBox[] rightBoxes;
        private TextBox[] topBoxes;
        private TextBox[] bottomBoxes;

        Timer timer;
        double time = 0;

        private int startPosX = 0;
        private int startPosY = 0;

        private int finishX = 10;
        private int finishY = 20;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private Button CreateCellButton(PlanetCell cell, int size, int x, int y)
        {
            Button button = new Button();

            button.Parent = this;

            switch (cell.cellType)
            {
                case CellType.Vulcane:
                    button.BackColor = Color.Red;
                    break;
                case CellType.Terrain:
                    button.BackColor = Color.Gray;
                    break;
            }

            button.Size = new Size(size, size);
            button.Location = new Point(30 + x * size, 30 + y * size);
            button.Text = cell.Temperature.ToString("0");

            return button;
        }
        private void CreateField()
        {
            int size = 30;
            planetCells = new PlanetCell[30, 30];
            cellButtons = new Button[30, 30];

            CreateTempFields(size);

            for (int i = 0; i < planetCells.GetLength(0); i++)
            {
                for(int j = 0; j < planetCells.GetLength(1); j++)
                {
                    int r = rnd.Next(0,30);
                    PlanetCell cell = new Terrain();

                    switch (r)
                    {
                        case 0:
                            if (i > 0 && i < planetCells.GetLength(0) - 1 && j > 0 && j < planetCells.GetLength(0) - 1) 
                                cell = new Vulcane(rnd.Next(15,60));
                            break;
                    }

                    planetCells[i, j] = cell;
                    cellButtons[i, j] = CreateCellButton(cell, size, i, j);
                    planetCells[i, j].parent = cellButtons[i, j];
                }
            }
        }
        private void CreateNewVulcanes()
        {
            for (int i = 0; i < planetCells.GetLength(0); i++)
            {
                for (int j = 0; j < planetCells.GetLength(1); j++)
                {
                    int r = rnd.Next(0, 90);
                    switch (r)
                    {
                        case 0:
                            if (cellButtons[i, j].BackColor != Color.Blue)
                            {
                                planetCells[i, j] = new Vulcane(rnd.Next(15, 80));
                                planetCells[i, j].parent = cellButtons[i, j];
                                cellButtons[i, j].BackColor = Color.Red;
                            }
                            break;
                    }
                }
            }
        }

        private void SetTemp()
        {
            for (int i = 0; i < planetCells.GetLength(0); i++)
            {
                planetCells[0, i].Temperature = Convert.ToInt32(leftBoxes[i].Text);
                cellButtons[0, i].Text = planetCells[0, i].Temperature.ToString("0");
            }
            for ( int i = 0;i < planetCells.GetLength(0); i++)
            {
                planetCells[i, 0].Temperature = Convert.ToInt32(topBoxes[i].Text);
                cellButtons[i, 0].Text = planetCells[0, i].Temperature.ToString("0");
            }
            for (int i = 0; i < planetCells.GetLength(0); i++)
            {
                planetCells[planetCells.GetLength(0) - 1, i].Temperature = Convert.ToInt32(rightBoxes[i].Text);
                cellButtons[planetCells.GetLength(0) - 1, i].Text = planetCells[planetCells.GetLength(0) - 1, i].Temperature.ToString("0");
            }
            for (int i = 0; i < planetCells.GetLength(0); i++)
            {
                planetCells[i, planetCells.GetLength(0) - 1].Temperature = Convert.ToInt32(topBoxes[i].Text);
                cellButtons[i, planetCells.GetLength(0) - 1].Text = planetCells[i, planetCells.GetLength(0) - 1].Temperature.ToString("0");
            }

            for (int i = 1; i < planetCells.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < planetCells.GetLength(1) - 1; j++)
                {
                    switch (planetCells[i, j].cellType)
                    {
                        case CellType.Terrain:
                            planetCells[i, j].EditTemp(
                                planetCells[i - 1, j].Temperature,
                                planetCells[i + 1, j].Temperature,
                                planetCells[i, j - 1].Temperature,
                                planetCells[i, j + 1].Temperature);
                            cellButtons[i, j].Text = planetCells[i, j].Temperature.ToString("0");
                            break;
                        case CellType.Vulcane:
                            cellButtons[i, j].Text = planetCells[i, j].Temperature.ToString("0");
                            break;
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((float)time % 60 == 0) CreateNewVulcanes();
            if(drone.PosX == finishX && drone.PosY == finishY) timer1.Stop();

            time++;
            label5.Text = time.ToString();

            for( int i = 1; i < planetCells.GetLength(0)-1; i++)
            {
                for (int j = 1; j < planetCells.GetLength(1) - 1; j++)
                {
                    if(planetCells[i,j].cellType == CellType.Vulcane)
                    {
                        Vulcane vulcane = (Vulcane)planetCells[i, j];
                        planetCells[i, j] = vulcane.ShutDown();
                    }
                    else
                    {
                        planetCells[i, j].EditTemp(
                            planetCells[i - 1, j].Temperature,
                            planetCells[i + 1, j].Temperature,
                            planetCells[i, j - 1].Temperature,
                            planetCells[i, j + 1].Temperature);
                        cellButtons[i, j].Text = planetCells[i, j].Temperature.ToString("0");
                    }
                }
            }
            int rangeX = 0;
            int rangeY = 0;
            bool lower = false;
            bool right = false;
            if(drone.PosX < finishX) right = true;
            if(drone.PosY < finishY) lower = true;

            BestDirection bestDirection = BestDirection.Top;
            BestDirection sideWay = BestDirection.Right;

            rangeX = Math.Abs(finishX-drone.PosX);
            rangeY = Math.Abs(finishY -drone.PosY);
            if(rangeX >= rangeY && right)
            {
                bestDirection = BestDirection.Right;
                if(lower) sideWay = BestDirection.Bottom;
                else sideWay = BestDirection.Top;
            }
            else if(rangeX >= rangeY && !right)
            {
                bestDirection = BestDirection.Left;
                if (lower) sideWay = BestDirection.Bottom;
                else sideWay = BestDirection.Top;
            }
            else if (rangeX <= rangeY && lower)
            {
                bestDirection = BestDirection.Bottom;
                if (right) sideWay = BestDirection.Right;
                else sideWay = BestDirection.Left;
            }
            else if (rangeX >= rangeY && !lower)
            {
                bestDirection = BestDirection.Top;
                if (right) sideWay = BestDirection.Right;
                else sideWay = BestDirection.Left;
            }

            int l0 = drone.PosX - 1;
            int r0 = drone.PosX + 1;
            int t0 = drone.PosY - 1;
            int b0 = drone.PosY + 1;

            if (l0 < 0) l0 = 0;
            if (r0 >= 30) r0 = 29;
            if (t0 < 0) t0 = 0;
            if (b0 >= 30) b0 = 29;

            PlanetCell pcLeft = planetCells[l0, drone.PosY];
            PlanetCell pcRight = planetCells[r0, drone.PosY];
            PlanetCell pcTop = planetCells[drone.PosX, t0];
            PlanetCell pcBottom = planetCells[drone.PosX, b0];
            drone.Move(pcLeft, pcRight, pcTop, pcBottom, bestDirection, sideWay);

            label5.Text+= bestDirection.ToString();
            label6.Text = drone.PosX.ToString();
            label7.Text = drone.PosY.ToString();

        }
        private void CreateTempFields(int size)
        {
            leftBoxes = new TextBox[30];
            rightBoxes = new TextBox[30];
            topBoxes = new TextBox[30];
            bottomBoxes = new TextBox[30];

            for(int i = 0;i < planetCells.GetLength(0); i++)
            {
                TextBox textBox = new TextBox();
                textBox.Parent = this;
                textBox.Size = new Size(size, size);
                textBox.Location = new Point(0, 30 + i * size);
                textBox.Text = 3.ToString();
                leftBoxes[i] = textBox;
            }
            for (int i = 0; i < planetCells.GetLength(1); i++)
            {
                TextBox textBox = new TextBox();
                textBox.Parent = this;
                textBox.Size = new Size(size, size);
                textBox.Location = new Point(30 + i * size, 0);
                textBox.Text = 5.ToString();
                topBoxes[i] = textBox;
            }
            for (int i = 0; i < planetCells.GetLength(1); i++)
            {
                TextBox textBox = new TextBox();
                textBox.Parent = this;
                textBox.Size = new Size(size, size);
                textBox.Location = new Point(30 + 30 * size, 30 + i * size);
                textBox.Text = 1.ToString();
                rightBoxes[i] = textBox;
            }
            for (int i = 0; i < planetCells.GetLength(1); i++)
            {
                TextBox textBox = new TextBox();
                textBox.Parent = this;
                textBox.Size = new Size(size, size);
                textBox.Location = new Point(30 + i * size, 30 + 30 * size);
                textBox.Text = 7.ToString();
                bottomBoxes[i] = textBox;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateField();
            SetTemp();
        }
        private void TimerSetUp()
        {
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void CreateDrone()
        {
            startPosX = Convert.ToInt32(textBox1.Text);
            startPosY = Convert.ToInt32(textBox2.Text);
            finishX = Convert.ToInt32(textBox3.Text);
            finishY = Convert.ToInt32(textBox4.Text);

            drone = new Drone(Convert.ToDouble(textBox5.Text));
            drone.PosX = startPosX;
            drone.PosY = startPosY;
            drone.parent = planetCells[drone.PosX, drone.PosY];
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cellButtons != null)
            {
                CreateDrone();
                TimerSetUp();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            finishX = Convert.ToInt32(textBox3.Text);
            finishY = Convert.ToInt32(textBox4.Text);

            TimerSetUp();
        }
    }
}
