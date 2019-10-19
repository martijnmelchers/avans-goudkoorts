using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Track : PlacableObject
    {
        protected Orientation _orientation;
        protected Direction _direction;

        protected Cart _cart;
        public override char GetChar()
        {
            return '-';
        }

        public Orientation GetOrientation()
        {
            return _orientation;
        }

        private bool HasCart()
        {
            return _cart == null;
        }
    }
}
