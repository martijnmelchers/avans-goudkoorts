using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Empty : PlacableObject
    {
        public override char GetChar()
        {
            return ' ';
        }
    }
}
