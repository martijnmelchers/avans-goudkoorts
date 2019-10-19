using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Dock : Instantiator
    {
        public override char GetChar()
        {
            return 'D';
        }

        protected override void Instantiate(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
