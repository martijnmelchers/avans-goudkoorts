using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public abstract class PlacableObject
    {
        public PlacableObject POAbove {get; set; }
        public PlacableObject PORight { get; set; }
        public PlacableObject POLeft { get; set; }
        public PlacableObject PODown { get; set; }

        public abstract char GetChar();
    }
}
