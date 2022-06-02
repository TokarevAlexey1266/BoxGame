using BoxGame.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoxGame.Entites
{
    public class Chest : MapEntity
    {
        public Chest(Point pos, Size size) : base(pos, size)
        {
            this.position = pos;
            this.size = size;
            Collide = new Rectangle(position, size);
        }

        public static void DrawChests(Graphics g)
        {
            foreach(var chest in mapController.chests)
            {
                chest.DrawChest(g);
            }
        }

        public void DrawChest(Graphics g)
        {
            if(type == MapEntityType.Chest)
                g.DrawImage(mapController.spriteSheet, Collide, 1005, 400, 72, 68, GraphicsUnit.Pixel);
            if(type == MapEntityType.OpenedChest)
                g.DrawImage(mapController.spriteSheet, Collide, 1085, 400, 72, 68, GraphicsUnit.Pixel);
        }
    }
}
