using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Switch: Track
    {

        public Switch():base(Direction.RIGHT) { }
        public void SwitchTrack(Orientation orientation)
        {
            return;
        }

        public override char GetChar()
        {
            return '|';
        }
    }
}
