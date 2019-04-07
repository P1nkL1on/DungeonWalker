using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class Obj : CDrawable
    {
        protected CIcon icon;
        protected int x;
        protected int y;
        public CIcon Icon { get { return icon; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public virtual bool Step(Level context) { return false; }
        public bool NeedRedraw = true;
    }
    public enum MoveDirection
    {
        None,
        Left,
        Right,
        Up,
        Down,
        LeftDown,
        LeftUp,
        RightDown,
        RightUp
    }
    public class MoveDiretionDecider : Obj
    {
        public int channel = 0;
        public MoveDirection direct;
        static CIcon directionDeciderDefailtIcon = CIcon.Unseen();


        public MoveDiretionDecider(int x, int y, MoveDirection direct, int channel)
        {
            this.x = x; this.y = y; this.direct = direct; this.channel = channel;
            this.icon = directionDeciderDefailtIcon;
        }
        public MoveDiretionDecider(int x, int y, MoveDirection direct)
        {
            this.x = x; this.y = y; this.direct = direct;
            this.icon = directionDeciderDefailtIcon;
        }
    }

    public class Tile : Obj
    {
        protected Tile() { }
        Tile(int x, int y, CIcon icon)
        {
            this.x = x;
            this.y = y;
            this.icon = icon;
        }

        static public Tile stoneTile(int x, int y)
        {
            return new Tile(x, y, CIcon.Tile(ConsoleColor.DarkBlue, ConsoleColor.Black, '.'));
        }

        static public Tile grassTile(int x, int y)
        {
            return new Tile(x, y, CIcon.Tile(ConsoleColor.DarkGreen, ConsoleColor.Black, ','));
        }

    }
    public class Door : Tile
    {
        static char openedChar = 'П';
        static char closedChar = '☒';
        public bool isOpened;
        Door(int x, int y, ConsoleColor fore, bool isOpened)
        {
            this.x = x;
            this.y = y;
            this.isOpened = isOpened;
            this.icon = CIcon.Tile(fore, ConsoleColor.Black, isOpened? openedChar : closedChar);
        }
        static public Door OpenedDoor(int x, int y, ConsoleColor fore){
            return new Door(x, y, fore, true);
        }
        //static public 
    }
    public enum Team
    {
        neutral,
        player,
        enemy,
        friend
        
    }
    public class Unit : Obj
    {
        UnitMind mind;
        public Team team;

        Unit(int x, int y, CIcon icon, UnitMind mind, Team team)
        {
            this.x = x;
            this.y = y;
            this.icon = icon;
            this.mind = mind;
            this.mind.host = this;
            this.team = team;
        }

        static public Unit player(int x, int y)
        {
            return new Unit(x, y, CIcon.CharacterPlayer(ConsoleColor.Red, 'ì'), new Controller(), Team.player);
        }

        static public Unit enemyGoblin(int x, int y)
        {
            return new Unit(x, y, CIcon.Character(ConsoleColor.DarkGreen, 'a'), new NoMind(), Team.enemy);
        }
        static public Unit enemyGoblinPatrol(int x, int y, MoveDirection defaultDirection)
        {
            return new Unit(x, y, CIcon.Character(ConsoleColor.DarkGreen, 'à'), new WalkByDirection(defaultDirection), Team.enemy);
        }

        static public Unit neutralGnome(int x, int y)
        {
            return new Unit(x, y, CIcon.Character(ConsoleColor.DarkGray, 'o'), new NoMind(), Team.neutral);
        }

        public ResultAction Move(Level context, int dx, int dy)
        {
            // want move to
            int x1 = x + dx;
            int y1 = y + dy;
            var all = context.everythingOnDot(x1, y1);

            //Obj possibleInteractiveObj = null;
            //ResultAction resAction = ResultAction.none;

            // if there is no tiles then you can not step there (wall)
            int tileCount = 0;
            foreach (Obj what in all) tileCount += (what as Tile != null)? 1 : 0;
            if (tileCount == 0)
            {
                context.Trace("A wall blocks you.", ConsoleColor.DarkGray);
                return ResultAction.blockedByWall;
            }

            foreach (Obj what in all)
                if (what as Unit != null)
                {
                    var res = isEnemyCompare(team, (what as Unit).team);
                    //context.Trace(" blocks you.", ConsoleColor.DarkGray);
                    return res;
                }
            // if want move then move
            if (dx != 0 || dy != 0)
            {
                x = x1;
                y = y1;
                return ResultAction.canMoveFreely;
            }
            // if do not move then do nothing
            return ResultAction.none;
        }

        // return Needs Repaint?
        public override bool Step(Level context)
        {
            UnitAction action = mind.readDesire(context);
            switch (action)
            {
                case UnitAction.moveLeft: return Move(context, -1, 0) != ResultAction.none;
                case UnitAction.moveRight: return Move(context, 1, 0) != ResultAction.none;
                case UnitAction.moveDown: return Move(context, 0, 1) != ResultAction.none;
                case UnitAction.moveUp: return Move(context, 0, -1) != ResultAction.none;
                default:
                    return false;
            }
        }

        static ResultAction isEnemyCompare(Team a, Team b)
        {
            if (a == Team.player || a == Team.friend)
                return (b == Team.enemy) ? ResultAction.blockedByEnemy : ResultAction.blockedByFriend;
            if (a == Team.enemy)
                return (b == Team.player || b == Team.friend) ? ResultAction.blockedByEnemy : ResultAction.blockedByFriend;
            // if a == neutral, then it is a friend to all)
            return ResultAction.blockedByFriend;
        }
    }

    public enum ResultAction
    {
        none,
        canMoveFreely,
        blockedByWall,
        blockedByClosedDoor,
        blockedByDoorThatCanBeOpened,
        blockedByEnemy,
        blockedByFriend
    }
}
