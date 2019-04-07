using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public struct Dot
    {
        public int X;
        public int Y;
        public Dot(int x, int y) { X = x; Y = y; }
    }
    public class Level
    {
        public static void FormateWindow()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(60, 60);
            Console.SetBufferSize(60, 60);
        }

        void clearScreen()
        {
            Console.Clear();
            Console.ResetColor();
        }

        public static ConsoleColor defaultTerrainColor = ConsoleColor.DarkCyan;

        CDrawer cdr = null;
        CLogger clg = null;

        List<Obj> objs = new List<Obj>();
        List<Dot> needUpdate = new List<Dot>();

        public Level(CDrawer cdr, CLogger clg, List<Obj> terrain)
        {
            this.cdr = cdr;
            this.clg = clg;
            this.objs = terrain;
            foreach (Obj o in objs)
                cdr.objects.Add(o as CDrawable);
        }
        public void Paint()
        {
            clearScreen();
            cdr.repaint();
        }
        public void Repaint()
        {
            cdr.repaint(needUpdate);
        }

        public void Step()
        {
            needUpdate.Clear();
            foreach (Obj o in objs)
                if (o.Step(this))
                    needUpdate.AddRange(nearDotsFor(o.X, o.Y));
        }
        List<Dot> nearDotsFor(int X, int Y, int Rad)
        {
            List<Dot> res = new List<Dot>();
            for (int i = -Rad; i <= Rad; ++i)
                for (int j = -Rad; j <= Rad; ++j)
                    res.Add(new Dot(X + i, Y + j));
            return res;
        }
        List<Dot> nearDotsFor(int X, int Y)
        {
            return nearDotsFor(X, Y, 1);
        }
        public List<Obj> everythingOnDot(int X, int Y)
        {
            List<Obj> res = new List<Obj>();
            foreach (Obj o in objs)
                if (o.X == X && o.Y == Y)
                    res.Add(o);
            return res;
        }
        public List<Obj> everythingOnDot(Dot dot) { return everythingOnDot(dot.X, dot.Y); }
        public List<Obj> everythingOnDot(Unit unit) { return everythingOnDot(unit.X, unit.Y); }

        public void Trace(string message, ConsoleColor fore)
        {
            if (clg == null)
                return;
            clg.AddLog(message, fore);
        }
        public void Trace(string message)
        {
            Trace(message, ConsoleColor.Gray);
        }
    }
}
