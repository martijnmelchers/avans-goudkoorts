using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public class GoudKoorts
    {
        // Is it allowed to switch?
        public GameState State { get; set; }

        // Collection of all the switches on the "Board"
        private List<Switch> _switches;

        public GoudKoorts()
        {
            //TODO: Create board (hard coded, linked list model)
            
        }

        // Returns the orientation the switch is switched to.
        public Orientation ToggleSwitch(int switchNum)
        {
            var selSwitch = _switches[switchNum];
            var orientation = selSwitch.GetOrientation() == Orientation.UP ? Orientation.DOWN : Orientation.UP;

            _switches[switchNum].SwitchTrack(orientation);
            return orientation;
        }
    }
}
