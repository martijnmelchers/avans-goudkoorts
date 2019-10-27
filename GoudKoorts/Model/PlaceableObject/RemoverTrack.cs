using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public class RemoverTrack : Track
    {

        public RemoverTrack(Direction dir) : base(dir)
        {
        }


        public override bool SetCart(Cart cart)
        {
            return true;
        }

    }
}
