using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Track : PlacableObject
    {
        protected Direction _direction;

        protected Cart _cart;

        public Track(Direction dir)
        {
            _direction = dir;
        }

        public override char GetChar()
        {
            return '-';
        }

        public Orientation GetOrientation()
        {
            return Orientation.UP;
        }

        private bool HasCart()
        {
            return _cart == null;
        }
    }
}
