using BoxGame.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGame.Entites
{
    public class Entity
    {
        public int posX;
        public int posY;
        public Rectangle collide;
        public int dirX;
        public int dirY;
        public bool isMoving;
        public MapEntityType type;
        public static int score;

        public int currentAnimation;
        public int currentFrame;
        public int currentLimit;

        public int runBackFrames;
        public int runForwardFrames;
        public int runLeftFrames;
        public int runRightFrames;
        public int idleFrames;

        public int size;

        public Image spiriteSheet;

        public Entity(int posX, int posY, int runBackFrames, int idleFrames, int runForwardFrames, int runLeftFrames, int runRightFrames, Image spiriteSheet)
        {
            this.posX=posX;
            this.posY=posY;
            this.runBackFrames=runBackFrames;
            this.runForwardFrames=runForwardFrames;
            this.runLeftFrames=runLeftFrames;
            this.runRightFrames=runRightFrames;
            this.spiriteSheet=spiriteSheet;
            size = 55;
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = idleFrames;
            collide = new Rectangle(new Point(posX, posY), new Size(size, size));
        }
        public void Move()
        {
            collide.Location += new Size(dirX, dirY);
            foreach (MapEntity mapEntity in mapController.mapObjects)
            {
                if(collide.IntersectsWith(mapEntity.Collide) && mapEntity.type != MapEntityType.Chest && mapEntity.type != MapEntityType.Place)
                {
                    collide.Location -= new Size(dirX, dirY);
                    dirX = 0;
                    dirY = 0;
                    break;
                }
                if(collide.IntersectsWith(mapEntity.Collide) && mapEntity.type == MapEntityType.Chest)
                {
                    mapEntity.MoveChest(dirX, dirY);
                    collide.Location -= new Size(dirX, dirY);
                    dirX = 0;
                    dirY = 0;
                    break;
                }
            }
            posX += dirX;
            posY += dirY;

        }
        
        public void PlayAnimation(Graphics g)
        {
            

            if (currentFrame < currentLimit - 1)
                currentFrame++;
            else currentFrame = 0;

            g.DrawImage(spiriteSheet, new Rectangle(new Point(posX, posY), new Size(size, size)), 237*currentFrame, 288*currentAnimation, 237, 288, GraphicsUnit.Pixel);
        }

        public void SetAnimationConfiguration(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;

            switch(currentAnimation)
            {
                case 0:
                    currentLimit = idleFrames;
                    break;
                case 1:
                    currentLimit = runBackFrames;
                    break;
                case 2:
                    currentLimit = runForwardFrames;
                    break;
                case 3:
                    currentLimit = runLeftFrames;
                    break;
                case 4:
                    currentLimit = runRightFrames;
                    break;
            }
        }
    }
}
