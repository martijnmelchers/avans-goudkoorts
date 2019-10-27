using GoudKoorts.Model.PlaceableObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public class Dock : Instantiator
    {
        private Ship _ship;
        public int LoadsTransfered { get; private set; }

        public Dock()
        {
            LoadsTransfered = 0;
        }

        public override char ToChar()
        {
            return 'D';
        }

        public override void Instantiate()
        {
            var water = (Water)POAbove;
            water.Instantiate();
        }

        public bool HasShip()
        {
            var water = (Water)POAbove;
            return water.HasShip();
        }

        public void AddContents()
        {
            var water = (Water)POAbove;
            water.GetShip().AddContents();
            if(water.GetShip().FillState == FillState.FULL)
            {
                water.RemoveShip();
            }
            
            LoadsTransfered++;
        }
    }
}
