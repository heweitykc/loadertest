using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILibs
{
    class BevNodePrioritySelector : BevNode
    {
        protected int _currentSelectedIndex = -1;
        protected int _lastSelectedIndex = -1;

        public BevNodePrioritySelector(string debugName)
            : base(debugName)
        {
            
        }

        override protected bool doEvaluate(BevNodeInputParam input)
        {
            _currentSelectedIndex = -1;
            int len = _children.Count;
            for (int i = 0; i < len; ++i) {
                if (_children[i].evaluate(input))
                {
                    _currentSelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        override protected void doTransition(BevNodeInputParam input)
        {
            if (checkIndex(_lastSelectedIndex)) {
                _children[_lastSelectedIndex].transition(input);
            }
            _lastSelectedIndex = -1;
        }

        override protected int doTick(BevNodeInputParam input, 
            BevNodeOutputParam output)
        {
            int isFinish = BRS_FINISH;
            if (checkIndex(_currentSelectedIndex))
            {
                if (_currentSelectedIndex != _lastSelectedIndex)
                {
                    if (checkIndex(_lastSelectedIndex))
                    {
                        _children[_lastSelectedIndex].transition(input);
                    }
                    _lastSelectedIndex = _currentSelectedIndex;
                }
            }

            if (checkIndex(_lastSelectedIndex))
            {
                isFinish = _children[_lastSelectedIndex].tick(input, output);
                if (isFinish == BRS_FINISH)
                    _lastSelectedIndex = -1;
            }
            return isFinish;
        }
    }
}
