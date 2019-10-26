using GoudKoorts.Model.PlaceableObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public class GoudKoorts
    {
        public PlacableObject Origin { get; private set; }
        // Is it allowed to switch?
        public GameState State { get; set; }
        // Collection of all the switches on the "Board"
        private List<Switch> _switches = new List<Switch>();

        private List<Instantiator> _instantiators = new List<Instantiator>();

        private PlacableObject[,] Grid;
        public int Score { get; private set; }
        
        public GoudKoorts()
        {
            Score = 0;
            //TODO: Create board (hard coded, linked list model)
            LoadDemoList();
        }

        // Laad de standaard gedefiniëerde map.
        private void LoadDemoList()
        {
            // Initialize the data, so we can link them later on.
            List<List<PlacableObject>> columns = new List<List<PlacableObject>>
            {
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Water(), new Empty(), new Empty() },
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Dock(), new Empty(), new Empty() },
                new List<PlacableObject>{new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT),new Track(Direction.LEFT)},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.UP) },
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.DOWN),new Empty(),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.DOWN),new Empty(),new Track(Direction.UP)},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Switch(Direction.LEFT), new Track(Direction.RIGHT), new Switch(), new Empty(), new Empty(), new Empty(), new Switch(Direction.LEFT), new Track(Direction.RIGHT), new Track(Direction.UP) },
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT),new Track(Direction.RIGHT),new Track(Direction.UP),new Empty(),new Track(Direction.RIGHT), new Track(Direction.DOWN), new Empty(), new Track(Direction.RIGHT), new Track(Direction.UP),new Empty(),new Empty()},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.DOWN),new Empty() , new Track(Direction.UP), new Empty(), new Empty(), new Empty()},
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Switch(Direction.LEFT), new Track(Direction.RIGHT), new Switch(), new Empty(), new Empty(), new Empty()},
                new List<PlacableObject>{new Warehouse(), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.UP), new Empty(), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.RIGHT), new Track(Direction.DOWN) },
                new List<PlacableObject>{new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Track(Direction.DOWN) },
                new List<PlacableObject>{ new Empty(), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Brake(Direction.LEFT), new Track(Direction.LEFT), new Track(Direction.LEFT), new Track(Direction.LEFT) },
            };


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

                        if(columns[i][j] is Instantiator)
                        {
                            _instantiators.Add((Instantiator)columns[i][j]);
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


            Instantiate();
            //Render
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

        public bool MoveCartsFresh()
        {
            var origin = Origin;
            while(origin != null)
            {
                var next = origin;

                while (next != null)
                {


                    var moved = false;
                    // Move cart in the direction of the track.
                    if (next is Track)
                    {
                        var track = (Track)next;
                        if (track.HasCart())
                        {
                            var noCollision = track.MoveCart();

                            // When a collison happens on move return false.
                            if (!noCollision)
                            {
                                return false;
                            }

                            moved = true;
                        }
                    }

                    if (!moved)
                    {
                        next = next.PORight;
                    }
                    else
                    {
                        if(next.PORight == null)
                        {
                            next = null;
                        }
                        else
                        {
                            next = next.PORight.PORight;
                        }
                    }
                }

                origin = origin.PODown;
            }

            
            return true;
        }

        public int CalcScore()
        {
            int score = 0;
            foreach(var instantiator in _instantiators)
            {
                if(instantiator is Dock)
                {
                    var dock = (Dock)instantiator;
                    score += (dock.LoadsTransfered / 8) * 10;
                }
            }

            return score;
        }


        public void Instantiate()
        {
            foreach(var instantiator in _instantiators)
            {
                instantiator.Instantiate();
            }
        }


    }
}
