using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class TerrainGenerator
    {
        public static List<Obj> testLevelTerrain()
        {
            List<Obj> objs = new List<Obj>();
            for (int i = 4; i < 10; ++i)
                for (int j = 2; j < 15; ++j)
                    objs.Add(Tile.stoneTile(i, j));
            for (int i = 11; i < 15; ++i)
                for (int j = 2; j < 8; ++j)
                    objs.Add(Tile.grassTile(i, j));
            objs.Add(Door.OpenedDoor(10, 5, Level.defaultTerrainColor));

            objs.Add(new MoveDiretionDecider(4, 2, MoveDirection.Right));
            objs.Add(new MoveDiretionDecider(9, 2, MoveDirection.Down));
            objs.Add(new MoveDiretionDecider(9, 14, MoveDirection.Left));
            objs.Add(new MoveDiretionDecider(4, 14, MoveDirection.Up));

            objs.Add(Unit.player(4, 4));
            objs.Add(Unit.enemyGoblin(8, 12));
            objs.Add(Unit.enemyGoblinPatrol(8, 14, MoveDirection.Left));
            objs.Add(Unit.neutralGnome(7, 7));

            return objs;
        }
    }
}
