using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    public abstract class Instantiator : PlacableObject
    {
        public abstract override char ToChar();
        public abstract void Instantiate();

        public Instantiator() {
          

            // Start timer.
            
        }
    }
}
