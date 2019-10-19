using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Switch: Track
    {
        public override char GetChar()
        {
            if (_direction == Direction.LEFT)
            {
                if (_orientation == Orientation.UP)
                    return '\\';

                return '/';
            }
            else
            {
                if (_orientation == Orientation.UP)
                    return '/';
                return '\\';
            }
        }
    }
}
