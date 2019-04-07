using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    // a struct of drawing in console
    public struct CIcon
    {
        public ConsoleColor fore;
        public ConsoleColor back;
        public char c;
        public bool isBackround;    // if true then background
        public int priority;

        CIcon(ConsoleColor fore, ConsoleColor back, char c, bool isBackround, int priority)
        {
            this.fore = fore;
            this.back = back;
            this.isBackround = isBackround;
            this.priority = priority;
            this.c = c;
        }

        //public CIcon Item (){

        //}
        static public CIcon CharacterPlayer(ConsoleColor fore, Char c)
        {
            return new CIcon(fore, ConsoleColor.Black, c, false, 11);
        }
        static public CIcon Character(ConsoleColor fore, Char c)
        {
            return new CIcon(fore, ConsoleColor.Black, c, false, 10);
        }
        static public CIcon Tile(ConsoleColor fore, ConsoleColor back, Char c)
        {
            return new CIcon(fore, back, c, true, 0);
        }
        static public CIcon Tile(ConsoleColor back)
        {
            return new CIcon(back, back, ' ', true, 0);
        }
        static public CIcon DefaultTile(ConsoleColor back)
        {
            return new CIcon(back, back, ' ', true, -1);
        }
        static public CIcon Unseen()
        {
            return new CIcon(ConsoleColor.Red, ConsoleColor.White, '*', true, -2);
        }
        public void Repaint()
        {
            Console.ForegroundColor = fore;
            Console.BackgroundColor = back;
            Console.Write(c);
        }
    }
}
