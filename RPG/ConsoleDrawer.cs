using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public interface CDrawable
    {
        CIcon Icon { get; }
        int X { get; }
        int Y { get; }
    }

    public class CDrawer
    {
        int startX = 6;
        int startY = 3;
        int wid;
        int hei;

        public List<CDrawable> objects;
        CIcon defaultTile = CIcon.DefaultTile(Level.defaultTerrainColor);

        public CDrawer(int wid, int hei)
        {
            this.wid = wid;
            this.hei = hei;
            objects = new List<CDrawable>();
        }

        CIcon[][] zeroPriorities
        {
            get
            {
                CIcon[][] pris = new CIcon[wid][];
                for (int i = 0; i < wid; ++i)
                {
                    pris[i] = new CIcon[hei];
                    for (int j = 0; j < hei; ++j)
                        pris[i][j] = defaultTile;
                }
                return pris;
            }
        }

        bool isIn(int x, int y)
        {
            return (x >= 0 && x < wid && y >=0 && y < hei);
        }

        public void repaint(List<Dot> updateOnly)
        {
            var p = zeroPriorities;
            foreach (CDrawable cd in objects){
                int x = cd.X, y = cd.Y;
                //if (!dotNeedsUpdate(updateOnly, x, y))
                //    continue;
                if (!isIn(x, y))
                    continue;
                CIcon ic = cd.Icon;
                if (p[x][y].priority > ic.priority)
                {
                    // if this is background then draw a color
                    if (ic.isBackround && !p[x][y].isBackround)
                        p[x][y].back = ic.back;
                    // get a color of prioritet background
                    continue;
                }
                if (p[x][y].priority == -1)
                { // if default
                    p[x][y] = ic;
                    continue;
                }
                // if there is anything behind this new symbol
                if (ic.isBackround && p[x][y].isBackround)
                {
                    p[x][y] = ic;
                    continue;
                }
                p[x][y].fore = ic.fore;
                p[x][y].c = ic.c;
                p[x][y].priority = ic.priority;
            }
            for (int i = 0; i < wid; ++i)
                for (int j = 0; j < hei; ++j)
                {
                    bool needRepaint = dotNeedsUpdate(updateOnly, i, j);
                    Console.SetCursorPosition(0, 0);
                    
                    //Console.SetCursorPosition(startX + i, startY + hei + 4 + j);
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.Write(needRepaint? 'X':'.');

                    if (!needRepaint)
                        continue;
                    Console.SetCursorPosition(startX + i, startY + j);
                    p[i][j].Repaint();
                }
            Console.ResetColor();
        }
        // if none arguments then ALL
        public void repaint()
        {
            repaint(new List<Dot>(){new Dot(-1, -1)});
        }

        bool dotNeedsUpdate(List<Dot> updates, int X, int Y)
        {
            // special kostil for drawing eveything existed
            if (updates.Count == 1 && updates[0].X == -1)
                return true;
            foreach (Dot d in updates)
                if (d.X == X && d.Y == Y)
                    return true;
            return false;
        }
    }
}
