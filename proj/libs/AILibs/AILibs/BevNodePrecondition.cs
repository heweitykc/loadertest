using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILibs
{
    class BevNodePrecondition
    {
        public BevNodePrecondition()
        {
 
        }

        public bool evaluate(BevNodeInputParam input)
        {
            throw new Exception("This is an abstract method. You need to implement yourself.");
        }
    }
}
