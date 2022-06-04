using BoxGame.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGame.Entites
{
    public class MapEntity
    {
        public Point position;
        public Size size;
        public MapEntityType type;
        public Rectangle Collide;

        public MapEntity(Point pos, Size size)
        {
            position = pos;
            this.size = size;
            Collide = new Rectangle(position, size);
        }

        public void MoveChest(int dirX, int dirY)
        {
            Collide.Location += new Size(dirX, dirY);
            foreach (MapEntity mapEntity in mapController.mapObjects.Where(x => x.position != position))
            {
                if (Collide.IntersectsWith(mapEntity.Collide) && mapEntity.type != MapEntityType.Chest && mapEntity.type != MapEntityType.Place)
                {
                    Collide.Location -= new Size(dirX, dirY);
                    dirX = 0;
                    dirY = 0;
                    break;
                }
                if (Collide.IntersectsWith(mapEntity.Collide) && mapEntity.type == MapEntityType.Place)
                {
                    Collide.Location = mapEntity.Collide.Location;
                    position = mapEntity.position;
                    type = MapEntityType.OpenedChest;
                    Entity.score++;
                    if (Entity.score == 6)
                        Form1.ShowExitPanel();
                    break;
                }
            }
            position.X += dirX;
            position.Y += dirY;
        }
    }   

    public enum MapEntityType
    {
        Chest, 
        Wall,
        Player,
        Place,
        OpenedChest
    }
}
