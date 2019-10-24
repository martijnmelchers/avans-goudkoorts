using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Warehouse : Instantiator
    {
        public override char GetChar()
        {
            return 'W';
        }

        protected override void Instantiate(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }
    }
}
