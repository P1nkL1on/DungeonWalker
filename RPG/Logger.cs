using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public class CLogger
    {
        ConsoleColor loggerBackground = ConsoleColor.Black;
        int startX = 6;
        int startY = 3;
        int wid;
        int hei;
        int usedSpace = 0;

        public CLogger(int wid, int hei)
        {
            this.wid = wid;
            this.hei = hei;
            usedSpace = 0;
        }

        public void AddOffset(int xOff, int yOff)
        {
            startX += xOff;
            startY += yOff;
        }

        public void AddLog(string message, ConsoleColor fore)
        {
            int approximateLength = message.Length / wid + 1;
            if (usedSpace + approximateLength > hei)
                ClearLogWindow(loggerBackground);

            Console.ForegroundColor = fore;
            Console.BackgroundColor = loggerBackground;
            while (message.Length > wid)
            {
                string wr = message.Substring(0, wid);
                message = message.Substring(wid);
                Console.SetCursorPosition(startX, startY + usedSpace);
                Console.Write(wr);
                usedSpace++;
            }
            Console.SetCursorPosition(startX, startY + usedSpace);
            Console.Write(message);
            usedSpace++;
            Console.ResetColor();
        }
        public void ClearLogWindow(ConsoleColor back)
        {
            Console.BackgroundColor = back;
            for (int i = 0; i < hei; ++i)
            {
                Console.SetCursorPosition(startX, startY + i);
                Console.Write("".PadLeft(wid));
            }
            usedSpace = 0;
        }
        
    }  
}
