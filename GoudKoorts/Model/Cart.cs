using System;
using System.Collections.Generic;
using System.Text;

namespace GoudKoorts.Model
{
    class Cart : Vehicle
    {

        public char ToChar()
        {   
            if(FillState == FillState.FULL)
            {
                return '8';
            }

            return 'o';
        }

        public Cart()
        {
            FillState = FillState.FULL;
        }

        public bool TransferContents(Dock dock)
        {
            if (dock.HasShip())
            {
                dock.AddContents();
                FillState = FillState.EMPTY;
                return true; 
            }

            return false;
        }
    }
}
