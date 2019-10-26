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

        public override char ToChar()
        {

            if (HasCart())
            {
                return _cart.ToChar();
            }

            switch (_direction)
            {
                case Direction.LEFT:
                case Direction.RIGHT:
                    {
            
                        return '-';
                    }
                case Direction.UP:
                    { 
                    
                        if(POLeft is Track)
                        {
                            var po = (Track)POLeft;

                            if (po.GetDirection()== Direction.RIGHT)
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
                            return '|';
                        }
                    }
                case Direction.DOWN:
                    {

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
                            return '|';
                        }
                    }
                default:
                    return ' ';
            }
        }


        public Direction GetDirection()
        {
            return _direction ;
        }

        public bool SetCart(Cart cart)
        {
            if(_cart != null)
            {
                return false;
            }
            else
            {
                _cart = cart;
            }

            return true;
        }

        public bool HasCart()
        {
            return _cart != null;
        }

        public virtual bool MoveCart()
        {

            // Move the cart to the next track.
            
            Track nextTrack = null;

            switch (_direction)
            {
                case Direction.UP:
                    if (!(POAbove is Empty))
                        nextTrack = (Track) POAbove;
                    break;

                case Direction.DOWN:
                    if (!(PODown is Empty))
                        nextTrack = (Track)PODown;
                    break;

                case Direction.LEFT:
                    if (!(POLeft is Empty))
                        nextTrack = (Track)POLeft;
                    break;

                case Direction.RIGHT:
                    if (!(PORight is Empty))
                        nextTrack = (Track)PORight;
                    break;
            }

            if(nextTrack == null)
            {
                return true;
            }

            if (nextTrack.POAbove is Dock)
            {
                var dock = (Dock)nextTrack.POAbove;
                _cart.TransferContents(dock);
            }


            if (nextTrack is Switch)
            {
                // The next item is a switch, check if we can move through it.
                var switchTrack = (Switch)nextTrack;

                var or = switchTrack.GetOrientation();

                if(_direction == Direction.UP && or == Orientation.DOWN)
                {
                    return true;
                }
                else if(_direction == Direction.DOWN && or == Orientation.UP)
                {
                    return true;
                }
            }

   
          
            var cart = _cart;
            var noCollision = nextTrack.SetCart(cart);

            _cart = null;

            return noCollision;        
        }
    }
}
