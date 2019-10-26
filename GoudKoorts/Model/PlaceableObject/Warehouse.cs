using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Warehouse : Instantiator
    {
        public override char ToChar()
        {
            return 'W';
        }

        public override void Instantiate()
        {
            if(PORight is Track)
            {
                var track = (Track)PORight;

                track.SetCart(new Cart());
            }
            else
            {
                return;
            }
        }
    }
}
