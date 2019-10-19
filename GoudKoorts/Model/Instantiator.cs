using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public abstract class Instantiator : PlacableObject
    {
        public abstract override char GetChar();
        protected abstract void Instantiate(Vehicle vehicle);
    }
}
