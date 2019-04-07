using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public enum UnitAction
    {
        moveLeft,
        moveRight,
        moveUp,
        moveDown,
        none
    }
    public class UnitMind
    {
        Unit _host = null;
        public virtual Unit host { get { return _host; } set { _host = value; } }
        public virtual UnitAction readDesire(Level context) { return UnitAction.none; }
    }

    public class Controller : UnitMind
    {
        public Controller()
        {

        }
        public override UnitAction readDesire(Level context)
        {
            ConsoleKeyInfo get = Console.ReadKey();
            switch (get.Key)
            {
                case ConsoleKey.LeftArrow: return UnitAction.moveLeft;
                case ConsoleKey.RightArrow: return UnitAction.moveRight;
                case ConsoleKey.UpArrow: return UnitAction.moveUp;
                case ConsoleKey.DownArrow: return UnitAction.moveDown;
                default:
                    return UnitAction.none;
            }
        }
    }

    public class NoMind : UnitMind
    {
    }

    public class WalkByDirection : UnitMind
    {
        MoveDirection currentDirection;
        int phase = 1;
        public WalkByDirection(MoveDirection defaultDirection)
        {
            currentDirection = defaultDirection;
        }
        public override UnitAction readDesire(Level context)
        {
            var standingOn = context.everythingOnDot(host);
            foreach (Obj o in standingOn)
            if (o as MoveDiretionDecider != null){
                currentDirection = (o as MoveDiretionDecider).direct;
                break;
            }
            phase *= -1;
            switch (currentDirection)
            {
                //← ↑ → ↓  ↖ ↗ ↘ ↙
                case MoveDirection.Down: return UnitAction.moveDown;
                case MoveDirection.Left: return UnitAction.moveLeft;
                case MoveDirection.LeftDown: return (phase > 0) ? UnitAction.moveLeft : UnitAction.moveDown;
                case MoveDirection.LeftUp: return (phase > 0) ? UnitAction.moveLeft : UnitAction.moveUp;
                case MoveDirection.Right: return UnitAction.moveRight;
                case MoveDirection.RightDown: return (phase > 0) ? UnitAction.moveRight : UnitAction.moveDown;
                case MoveDirection.RightUp: return (phase > 0) ? UnitAction.moveRight : UnitAction.moveUp;
                case MoveDirection.Up: return UnitAction.moveUp;
                default: return UnitAction.none;
            }
        }
    }
}
