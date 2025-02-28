using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Catos.Cm.Mobile.Common.Item.Gps
{
    public class LocationItem
    {
        private string _blockName;
        private int _bayRowIndex;

        public string BlockName
        {
            get { return _blockName; }
            set { _blockName = value; }
        }

        public int BayRowIndex
        {
            get { return _bayRowIndex; }
            set { _bayRowIndex = value; }
        }
    }
}
