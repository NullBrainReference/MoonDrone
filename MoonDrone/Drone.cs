using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MoonDrone
{
    public enum BestDirection { Left, Right, Top, Bottom }
    internal class Drone
    {
        public double maxTemp;
        public PlanetCell parent;
        public int PosX
        {
            get { return posX; }
            set
            {
                if (value < 0) posX = 0;
                else if (value > 29) posX = 29;
                else posX = value;
            }
        }
        private int posX;
        public int PosY
        {
            get { return posY; }
            set {
                if (value < 0) posY = 0;
                else if (value > 29) posY = 29;
                else posY = value; 
            }
        }
        private int posY;
        private BestDirection lastMove;
        public Drone(double maxTemp)
        {
            this.maxTemp = maxTemp;
        }
        public void Move(PlanetCell cellL, PlanetCell cellR, PlanetCell cellT, PlanetCell cellB, BestDirection bestDirection, BestDirection sideWay)
        {
            switch(bestDirection)
            {
                case BestDirection.Left:
                    if (isMovable(cellL) && lastMove != BestDirection.Right)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellL;
                        parent.parent.BackColor = Color.Blue;
                        PosX -= 1;
                        lastMove = BestDirection.Left;
                    }
                    else if (sideWay == BestDirection.Top && isMovable(cellT) && lastMove != BestDirection.Bottom)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellT;
                        parent.parent.BackColor = Color.Blue;
                        PosY -= 1;
                        lastMove = BestDirection.Top;
                    }
                    else if (sideWay == BestDirection.Bottom && isMovable(cellB) && lastMove != BestDirection.Top)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosY += 1;
                        lastMove = BestDirection.Bottom;
                    }
                    else if (isMovable(cellT))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellT;
                        parent.parent.BackColor = Color.Blue;
                        PosY -= 1;
                        lastMove = BestDirection.Top;
                    }
                    else if (isMovable(cellB))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosY += 1;
                        lastMove = BestDirection.Bottom;
                    }
                    else if (isMovable(cellR))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosX += 1;
                        lastMove = BestDirection.Right;
                    }


                    break;
                case BestDirection.Right:
                    if (isMovable(cellR) && lastMove != BestDirection.Left)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellR;
                        parent.parent.BackColor = Color.Blue;
                        PosX += 1;
                        lastMove = BestDirection.Right;
                    }
                    else if (sideWay == BestDirection.Top && isMovable(cellT) && lastMove != BestDirection.Bottom)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellT;
                        parent.parent.BackColor = Color.Blue;
                        PosY -= 1;
                        lastMove = BestDirection.Top;
                    }
                    else if (sideWay == BestDirection.Bottom && isMovable(cellB) && lastMove != BestDirection.Top)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosY += 1;
                        lastMove = BestDirection.Bottom;
                    }
                    else if (isMovable(cellT))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellT;
                        parent.parent.BackColor = Color.Blue;
                        PosY -= 1;
                        lastMove = BestDirection.Top;
                    }
                    else if (isMovable(cellB))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosY += 1;
                        lastMove = BestDirection.Bottom;
                    }
                    else if (isMovable(cellL))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellL;
                        parent.parent.BackColor = Color.Blue;
                        PosX -= 1;
                        lastMove = BestDirection.Left;
                    }
                    break;
                case BestDirection.Top:
                    if (isMovable(cellT) && lastMove != BestDirection.Bottom)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellT;
                        parent.parent.BackColor = Color.Blue; 
                        PosY -= 1;
                        lastMove = BestDirection.Top;
                    }
                    else if (sideWay == BestDirection.Right && isMovable(cellR) && lastMove != BestDirection.Left)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellR;
                        parent.parent.BackColor = Color.Blue;
                        PosX += 1;
                        lastMove = BestDirection.Right;
                    }
                    else if (sideWay == BestDirection.Left && isMovable(cellL) && lastMove != BestDirection.Right)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellL;
                        parent.parent.BackColor = Color.Blue;
                        PosX -= 1;
                        lastMove = BestDirection.Left;
                    }
                    else if (isMovable(cellL))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellL;
                        parent.parent.BackColor = Color.Blue;
                        PosX -= 1;
                        lastMove = BestDirection.Left;
                    }
                    else if (isMovable(cellR))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellR;
                        parent.parent.BackColor = Color.Blue;
                        PosX += 1;
                        lastMove = BestDirection.Right;
                    }
                    else if (isMovable(cellB))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosY += 1;
                        lastMove = BestDirection.Bottom;
                    }
                    break;
                    
                case BestDirection.Bottom:
                    if (isMovable(cellB) && lastMove != BestDirection.Top)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellB;
                        parent.parent.BackColor = Color.Blue;
                        PosY += 1;
                        lastMove = BestDirection.Bottom;
                    }
                    else if (sideWay == BestDirection.Right && isMovable(cellR) && lastMove != BestDirection.Left)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellR;
                        parent.parent.BackColor = Color.Blue;
                        PosX += 1;
                        lastMove = BestDirection.Right;
                    }
                    else if (sideWay == BestDirection.Left && isMovable(cellL) && lastMove != BestDirection.Right)
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellL;
                        parent.parent.BackColor = Color.Blue;
                        PosX -= 1;
                        lastMove = BestDirection.Left;
                    }
                    else if (isMovable(cellL))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellL;
                        parent.parent.BackColor = Color.Blue;
                        PosX -= 1;
                        lastMove = BestDirection.Left;
                    }
                    else if (isMovable(cellR))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellR;
                        parent.parent.BackColor = Color.Blue;
                        PosX += 1;
                        lastMove = BestDirection.Right;
                    }
                    else if (isMovable(cellT))
                    {
                        parent.parent.BackColor = Color.Green;
                        parent = cellT;
                        parent.parent.BackColor = Color.Blue;
                        PosY -= 1;
                        lastMove = BestDirection.Top;
                    }
                    break;
                    
            }    

        }
        private bool isMovable(PlanetCell cell)
        {
            bool movable = false;
            if(cell.Temperature < maxTemp)
                movable = true;
            return movable;

        }
    }
}
