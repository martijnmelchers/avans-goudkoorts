using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Switch: Track
    {

        public Switch(Direction dir = Direction.RIGHT):base(dir) { }

        private Orientation _orientation = Orientation.UP;


        public void SwitchTrack(Orientation orientation)
        {
            _orientation = orientation;
        }

        public Orientation GetOrientation()
        {
            return _orientation;
        }

        public override char ToChar()
        {
            if (HasCart())
            {
                return _cart.ToChar();
            }

            if(_direction == Direction.RIGHT)
            {
                if(_orientation == Orientation.UP)
                {
                    return '/';
                }
                else
                {
                    return '\\';
                }
            }
            else
            {
                if (_orientation == Orientation.UP)
                {
                    return '/';
                }
                else
                {
                    return '\\';
                }
            }
        }

        public override bool MoveCart()
        {
            Track nextTrack = null;

            if(_direction == Direction.LEFT)
            {
                //Move cart to right

                nextTrack = (Track)PORight;
            }

            if(_direction == Direction.RIGHT)
            {
                // Move cart based on orientation.

                if(_orientation == Orientation.UP)
                {
                    nextTrack = (Track)POAbove;
                }
                else
                {
                    nextTrack = (Track)PODown;
                }
            }


            if (nextTrack == null || nextTrack is Empty)
            {
                return true;
            }

            var cart = _cart;
            var noCollision = nextTrack.SetCart(cart);

            _cart = null;

            return noCollision;
        }
    }
}
