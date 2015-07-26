using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILibs
{
    class BevNodeNonePrioritySelector : BevNodePrioritySelector
    {
        public BevNodeNonePrioritySelector(string debugName)
            : base(debugName)
        {
                        
        }

        override protected bool doEvaluate(BevNodeInputParam input)
        {
            if (checkIndex(_currentSelectedIndex))
            {
                if (_children[_currentSelectedIndex].evaluate(input))
                    return true;
            }
            return base.doEvaluate(input);
        }
    }
}
