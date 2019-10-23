using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public class GoudKoorts
    {
        private PlacableObject Origin;
        // Is it allowed to switch?
        public GameState State { get; set; }
        // Collection of all the switches on the "Board"
        private List<Switch> _switches;

        private List<Instantiator> _instantiators;

        private PlacableObject[,] Grid;

        public GoudKoorts()
        {
            //TODO: Create board (hard coded, linked list model)
            LoadDemoList();
        }

        // Laad de standaard gedefiniëerde map.
        private void LoadDemoList()
        {
            /*Origin = new Track(Direction.LEFT, Orientation.STRAIGHT);

            // Generates 9 tracks and returns the last one.
            var tracks = GenerateTrack(ref Origin, 9, Direction.RIGHT, Direction.LEFT);

            var dock1 = new Dock();
            tracks.POAbove = dock1;

            var track = new Track(Direction.LEFT);
            tracks.PORight = track;

            var trackNext = new Track(Direction.UP);
            track.PORight = trackNext;

            track = new Track(Direction.UP);
            trackNext.POAbove = track;

            trackNext = new Track(Direction.UP);
            track.POAbove = trackNext;

            track = new Track(Direction.UP);
            trackNext.POAbove = track;*/

            // Initialize the data, so we can link them later on.
            List<List<PlacableObject>> columns = new List<List<PlacableObject>>
            {
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Dock(), new Empty(), new Empty() },
                new List<PlacableObject>{new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.UP)},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.UP) },
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.DOWN),new Empty(),new Track(Direction.UP),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.DOWN),new Empty(),new Track(Direction.UP)},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Switch(), new Track(Direction.RIGHT), new Switch(), new Empty(), new Empty(), new Empty(), new Switch(), new Track(Direction.RIGHT), new Track(Direction.UP) },
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.UP),new Empty(),new Track(Direction.DOWN), new Track(Direction.DOWN), new Empty(), new Track(Direction.UP), new Track(Direction.UP),new Empty(),new Empty()},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Switch(), new Track(Direction.RIGHT), new Switch(), new Empty(), new Empty(), new Empty()},
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.UP), new Empty(), new Track(Direction.DOWN), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.DOWN) },
                new List<PlacableObject>{ new Empty(), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Track(Direction.LEFT), new Track(Direction.LEFT), new Track(Direction.DOWN) },
            };


            //TODO: Link items (horizontally and vertically)


            Origin = columns[0][0];
            // Links items horizontally.

            PlacableObject prev = null;
            foreach(PlacableObject obj in columns[0])
            { 
                // Check if this is the first.
                if(prev == null)
                {
                    prev = obj;

                    if (Origin.PORight == null)
                    {
                        Origin.PORight = obj;
                    }
                }
                else
                {
                    obj.POLeft = prev;
                    prev.PORight = obj;
                }
            }

            Console.WriteLine(Origin.PORight.PORight.PORight.PORight.GetChar());
            Console.ReadLine();
        }

        private void LoadDemo()
        {
           /* Grid = new PlacableObject[10,10] {
                { new Track(Direction.LEFT, Orientation.STRAIGHT), new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Track(Direction.LEFT, Orientation.STRAIGHT), new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Track(Direction.LEFT, Orientation.STRAIGHT), new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Track(Direction.LEFT, Orientation.STRAIGHT), new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Warehouse(), new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Empty(), new Empty(),new Empty(),new Empty(),new Empty(),new Empty(),new Track(Direction.LEFT, Orientation.UP),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Warehouse(), new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Track(Direction.LEFT, Orientation.STRAIGHT), new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Warehouse(), new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.RIGHT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
                { new Empty(), new Empty(),new Empty(),new Empty(),new Empty(),new Empty(),new Track(Direction.LEFT, Orientation.UP),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT),new Track(Direction.LEFT, Orientation.STRAIGHT)},
            };
            */
        }

        // Returns the orientation the switch is switched to.
        public Orientation ToggleSwitch(int switchNum)
        {
            var selSwitch = _switches[switchNum];
            var orientation = selSwitch.GetOrientation() == Orientation.UP ? Orientation.DOWN : Orientation.UP;

            _switches[switchNum].SwitchTrack(orientation);
            return orientation;
        }

        private PlacableObject GenerateTrack(ref PlacableObject origin, int amount, Direction moveDir, Direction trackDir, Orientation or = Orientation.STRAIGHT)
        {
            var next = new Track(trackDir);

            if(moveDir == Direction.RIGHT)
            {
               origin.PORight = next;
            }
            else
            {
               origin.POLeft = next;
            }

            for (var i = 0; i < amount; i++)
            {
                var track = new Track(trackDir);

                switch (moveDir)
                {
                    case Direction.LEFT:
                        next.POLeft = track;
                        break;

                    case Direction.RIGHT:
                        next.PORight = track;
                        break;
                }
                next = track;
            }

            return next;
        }
    }
}
