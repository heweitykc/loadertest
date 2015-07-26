using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILibs
{
    class BevNodeLoop : BevNode
    {
        int _loopCount = -1;
        int _currentLoop;

        public BevNodeLoop(string debugName):base(debugName)
        {
            
        }

        public BevNodeLoop setLoopCount(int n)
        {
            _loopCount = n;
            return this;
        }

        override protected bool doEvaluate(BevNodeInputParam input)
        {
            if (_loopCount < 0) return false;
            if (_currentLoop < _loopCount) return false;

            if (checkIndex(0))
                if (_children[0].evaluate(input))
                    return true;
            return false;
        }

        override protected int doTick(BevNodeInputParam input, 
            BevNodeOutputParam output)
        {
            int isFinish = BRS_FINISH;
            if (checkIndex(0)) {
                isFinish = _children[0].tick(input, output);
                if (isFinish == BRS_FINISH)
                {
                    if (_loopCount == -1)
                        isFinish = BRS_EXECUTING;
                    else {
                        ++_currentLoop;
                        if (_currentLoop < _loopCount)
                            isFinish = BRS_EXECUTING;
                    }
                }
            }

            if (isFinish == BRS_FINISH)
                _currentLoop = 0;
            return isFinish;
        }

        override protected void doTransition(BevNodeInputParam input)
        {
            if (checkIndex(0))
                _children[0].transition(input);
            _currentLoop = 0;
        }
    }
}
