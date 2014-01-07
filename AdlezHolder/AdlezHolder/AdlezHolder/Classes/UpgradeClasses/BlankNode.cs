using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdlezHolder
{
    class BlankNode
    {
        public BlankNode()
        {
        }

        public void upgrade(bool state)
        {
            upgradeBlankNode(state, this);
        }
    }
}
