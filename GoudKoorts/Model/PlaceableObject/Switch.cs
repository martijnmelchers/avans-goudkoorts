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

        public override char GetChar()
        {


            /* char icon = ' ';
             switch (_direction)
             {
                 case Direction.LEFT:
                 case Direction.RIGHT:
                     icon = ' ';
                     break;

                 case Direction.UP:
                     {

                         if (HasCart())
                         {
                             return '8';
                         }
                         if (POLeft is Track)
                         {
                             var po = (Track)POLeft;

                             if (po.GetDirection() == Direction.RIGHT)
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
                             return '/';
                         }
                     }
                 case Direction.DOWN:
                     {
                         if (HasCart())
                         {
                             return '8';
                         }

                         if (POLeft is Track)
                         {
                             var po = (Track)POLeft;

                             if (po.GetDirection() == Direction.RIGHT)
                             {
                                 return '\\';
                             }
                             else
                             {
                                 return '/';
                             }
                         }
                         else
                         {
                             return '\\';
                         }
                     }

                 default:
                     break;
             }


             return icon;*/

            if (HasCart())
            {
                return '8';
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
