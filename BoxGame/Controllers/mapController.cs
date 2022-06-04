using BoxGame.Entites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxGame.Controllers
{
    public static class mapController
    {
        public const int mapHeigh = 9;
        public const int mapWidth = 17;

        public static int cellSize = 89;

        public static int[,] map = new int[mapHeigh, mapWidth];
        public static Image spriteSheet;
        public static List<MapEntity> mapObjects;
        public static List<Chest> chests;
        private static bool isFirstDrawingMap;

        public static void Init()
        {
            map = GetMap();
            spriteSheet = Properties.Resources.Jungle_Platformer;
            mapObjects = new List<MapEntity>();
            chests = new List<Chest>();
            isFirstDrawingMap = true;
        }
        
        public static int[,] GetMap()
        {
            return new int[,] {
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                { 8,5,4,1,1,1,5,1,1,1,1,3,6,1,7,5,2},
                { 2,1,4,1,1,44,7,4,3,4,4,4,44,1,4,7,2},
                { 2,1,1,1,1,3,1,4,1,1,1,1,1,1,1,1,2},
                { 2,7,4,44,4,4,1,1,1,7,44,4,5,1,44,1,2},
                { 2,5,1,1,4,7,1,1,6,1,1,1,1,1,1,1,2},
                { 2,3,4,1,4,1,4,3,4,1,4,3,4,4,4,1,2},
                { 2,1,1,1,1,1,44,1,1,1,4,1,1,6,1,1,2},
                { 2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
                };
        }

        public static void DrawObject(Graphics g)
        {
            for (var i = 0; i < mapHeigh; i++)
            {
                for (var j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 3 && isFirstDrawingMap)//сундукзакрытый
                    {
                        Chest mapEntity = new Chest(new Point(j*cellSize, i*cellSize), new Size(45, 45));
                        mapEntity.type = MapEntityType.Chest;
                        mapObjects.Add(mapEntity);
                        chests.Add(mapEntity);
                    }
                    if (map[i, j] == 2) //Деревья
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(cellSize, cellSize)), 116, 677, 280, 220, GraphicsUnit.Pixel);
                        MapEntity mapEntity = new MapEntity(new Point(j*cellSize, i*cellSize), new Size(cellSize, cellSize));
                        mapEntity.type = MapEntityType.Wall;
                        if(isFirstDrawingMap)
                            mapObjects.Add(mapEntity);
                    }
                    
                    if (map[i, j] == 4)//камень
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(cellSize*3/4, cellSize*3/4)), 557, 47, 68, 76, GraphicsUnit.Pixel);
                        MapEntity mapEntity = new MapEntity(new Point(j*cellSize, i*cellSize), new Size(cellSize*3/4, cellSize*3/4));
                        mapEntity.type = MapEntityType.Wall;
                        if(isFirstDrawingMap)
                            mapObjects.Add(mapEntity);
                    }
                    
                    if (map[i, j] == 44)//камни
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(cellSize*3/4, cellSize*3/4)), 550, 134, 82, 78, GraphicsUnit.Pixel);
                        MapEntity mapEntity = new MapEntity(new Point(j*cellSize, i*cellSize), new Size(cellSize*3/4, cellSize*3/4));
                        mapEntity.type = MapEntityType.Wall;
                        if(isFirstDrawingMap)
                            mapObjects.Add(mapEntity);
                    }
                    if (map[i, j] == 33)//сундукоткрырый
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(45, 45)), 1088, 398, 79, 68, GraphicsUnit.Pixel);
                        MapEntity mapEntity = new MapEntity(new Point(j*cellSize, i*cellSize), new Size(45, 45));
                        if(isFirstDrawingMap)
                            mapObjects.Add(mapEntity);
                    }
                    if (map[i, j] == 7)//ящик?
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(45, 45)), 845, 464, 80, 80, GraphicsUnit.Pixel);
                        MapEntity mapEntity = new MapEntity(new Point(j*cellSize, i*cellSize), new Size(45, 45));
                        mapEntity.type = MapEntityType.Place;
                        if (isFirstDrawingMap)
                            mapObjects.Add(mapEntity);
                    }
                }
            }
            isFirstDrawingMap = false;
        }
                    
         public static void DrawMap(Graphics g)
        {
            for (var i = 0; i < mapHeigh; i++)
            {
                for (var j = 0; j < mapWidth; j++)
                {
                    if (map[i, j] == 5)//трава1
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(cellSize*1/3, cellSize*1/3)), 847, 55, 78, 72, GraphicsUnit.Pixel);
                        
                    }else
                    if (map[i, j] == 6)//трава2
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(cellSize*1/3, cellSize*1/3)), 930, 50, 76, 37, GraphicsUnit.Pixel);
                        
                    }else
                    if (map[i, j] == 8)//табличка
                    {
                        g.DrawImage(spriteSheet, new Rectangle(new Point(j*cellSize, i*cellSize), new Size(cellSize*3/4, cellSize*3/4)), 1008, 224, 77, 78, GraphicsUnit.Pixel);
                        
                    }
                }
            }
            mapController.DrawObject(g);
        }
        
        public static int GetWidth()
        {
            return cellSize * mapWidth;
        }
        public static int GetHeight()
        {
            return cellSize * mapHeigh*2;
        }
    }
}
