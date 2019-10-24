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
        private List<Switch> _switches = new List<Switch>();

        private List<Instantiator> _instantiators = new List<Instantiator>();

        private PlacableObject[,] Grid;


        public GoudKoorts()
        {
            //TODO: Create board (hard coded, linked list model)
            LoadDemoList();
        }

        // Laad de standaard gedefiniëerde map.
        private void LoadDemoList()
        {
            // Initialize the data, so we can link them later on.
            List<List<PlacableObject>> columns = new List<List<PlacableObject>>
            {
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Dock(), new Empty(), new Empty() },
                new List<PlacableObject>{new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT)},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.UP) },
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.DOWN),new Empty(),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.DOWN),new Empty(),new Track(Direction.UP)},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Switch(Direction.LEFT), new Track(Direction.RIGHT), new Switch(), new Empty(), new Empty(), new Empty(), new Switch(Direction.LEFT), new Track(Direction.RIGHT), new Track(Direction.UP) },
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.UP),new Empty(),new Track(Direction.DOWN), new Empty(), new Empty(), new Empty(), new Track(Direction.UP),new Empty(),new Empty()},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.DOWN),new Empty() , new Track(Direction.UP), new Empty(), new Empty(), new Empty()},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Switch(Direction.LEFT), new Track(Direction.RIGHT), new Switch(), new Empty(), new Empty(), new Empty()},
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.UP), new Empty(), new Track(Direction.DOWN), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.DOWN) },
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.DOWN) },
                new List<PlacableObject>{ new Empty(), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Track(Direction.LEFT), new Track(Direction.LEFT), new Track(Direction.DOWN) },
            };

            var testTrack = (Track)columns[8][1];
            //TODO: Link items (horizontally and vertically)
            testTrack.SetCart(new Cart());

            Origin = columns[0][0];
            // Links items horizontally.


            for(var i = 0; i < columns.Count; i++)
            {
                PlacableObject prev = null;

                for (var j = 0; j < columns[i].Count; j++)
                {

                    if(i != 0)
                    {
                        var prevI = i - 1;
                        columns[i][j].POAbove = columns[prevI][j];
                        columns[prevI][j].PODown = columns[i][j];

                        if(columns[i][j] is Switch)
                        {
                            _switches.Add((Switch) columns[i][j]);
                        }

                        if(columns[i][j] is Dock)
                        {
                            _instantiators.Add((Dock)columns[i][j]);
                        }

                        if (columns[i][j] is Warehouse)
                        {
                            _instantiators.Add((Warehouse)columns[i][j]);
                        }
                    }
                }


                for (var j = 0; j < columns[i].Count; j++)
                {
                    if(prev == null)
                    {
                        prev = columns[i][j];

                        if(Origin.PORight == null)
                        {
                            Origin.PORight = columns[i][j];
                        }
                    }
                    else
                    {
                        columns[i][j].POLeft = prev;
                        prev.PORight = columns[i][j];
                        prev = columns[i][j];


                        //Link the rows.
                    }
                }
            }

            foreach(List<PlacableObject> column in columns)
            {
                PlacableObject prev = null;
                foreach (PlacableObject obj in column)
                {
                    // Check if this is the first.
                    if (prev == null)
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

                        prev = obj;
                    }
                }
            }



            Render();

        }

        // Returns the orientation the switch is switched to.
        public Orientation ToggleSwitch(int switchNum)
        {
             var selSwitch = _switches[switchNum];
             var direction = selSwitch.GetOrientation() == Orientation.UP ? Orientation.DOWN : Orientation.UP;

             _switches[switchNum].SwitchTrack(direction);
            return direction; 
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

        public void MoveCarts()
        {
            int height = 11;
            int width = 12;
            PlacableObject originTile = Origin;
            PlacableObject fieldBelow = originTile.PODown;
            for (int index1 = 0; index1 < height; ++index1)
            {

                var prevMove = false;
                for (int index2 = 0; index2 < width; ++index2)
                {
                    // Move cart in the direction of the track.
                    if(originTile is Track)
                    {
                        var track = (Track)originTile;
                        if (track.HasCart() && !prevMove)
                        {
                            track.MoveCart();
                            prevMove = true;
                        }
                        if (prevMove)
                        {
                            prevMove = false;
                        }
                    }

                    originTile = originTile.PORight;
                }

                originTile = fieldBelow;
                if (fieldBelow != null)
                    fieldBelow = originTile.PODown;
            }
        }

        public void Render()
        {
            Console.Clear();
            int height = 11;
            int width = 12;
            PlacableObject originTile = Origin;
            PlacableObject fieldBelow = originTile.PODown;
            for (int index1 = 0; index1 < height; ++index1)
            {
                for (int index2 = 0; index2 < width; ++index2)
                {
    
                    if(originTile is Brake)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    if(originTile is Switch)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(originTile.GetChar());
                    Console.ResetColor();
                    originTile = originTile.PORight;
                }
                originTile = fieldBelow;
                if (fieldBelow != null)
                    fieldBelow = originTile.PODown;
                Console.WriteLine();
            }
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────");
        }
    }
}
