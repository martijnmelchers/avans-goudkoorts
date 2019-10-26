using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Ship: Vehicle
    {
        public int Contents { get;  set; }

        public Ship()
        {
            FillState = FillState.EMPTY;
            Contents = 0;
        }

        public void AddContents()
        {
            Contents += 1;

            if(Contents > 2 && Contents < 8)
            {
                FillState = FillState.FILLED;
            }
            else if (Contents >= 8)
            {
                FillState = FillState.FULL;
            }
            else
            {
                FillState = FillState.EMPTY;
            }
        }

        public char ToChar()
        {
            if (FillState == FillState.EMPTY)
            {
                return 'E';
            }
            else if (FillState == FillState.FILLED)
            {
                return 'P';
            }
            else
            {
                return 'S';
            }
        }
    }
}
