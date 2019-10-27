using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model.PlaceableObject
{
    class Water : Instantiator
    {
        protected Direction _direction;
        protected Ship _ship;

        public override char ToChar()
        {
            if(_ship != null)
            {
                return _ship.ToChar();
            }

            return ' ';
        }

        public bool HasShip()
        {
            return _ship != null;
        }

        public Ship GetShip()
        {
            return _ship;
        }

        public void RemoveShip()
        {
            _ship = null;
        }

        public override void Instantiate()
        {
            _ship = new Ship();
        }
    }
}
