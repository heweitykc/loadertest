using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AILibs
{
    class BevNode
    {        
        public static const int BRS_EXECUTING = 0;
        public static const int BRS_FINISH = 1;
        public static const int BRS_ERROR_TRANSITION = -1;
        public static const int MAX_CHILDREN = 16;

        protected String _debugName;
        protected BevNodePrecondition _precondition;
        protected List<BevNode> _children;
        protected BevNode _parent;
        
        public string debugName{
            get {
                return _debugName;
            }
        }        

        public BevNode(string debugName = null)
        {
            if (String.IsNullOrEmpty(debugName)) {
                _debugName = "";
            } else {
                _debugName = debugName;
            }
        }

        public BevNode addChild(BevNode node)
        {
            if (!_children)
                _children = new List<BevNode>();
            if (_children.Count == MAX_CHILDREN)
                throw new Exception(this + "overflow, max children number is " + MAX_CHILDREN);

            _children.Add(node);
            node._parent = this;
            return this;
        }

        public BevNode AddChildAt(BevNode node, int index)
        {
            this.addChild(node);
            if (index < 0)
                index = 0;
            else if (index > _children.Count - 1)
                index = _children.Count;

            for (int i = _children.Count - 1; i > index; --i) { 
                _children[i] = _children[i-1];
            }

            _children[index] = node;

            return this;
        }

        public BevNode setPrecondition(BevNodePrecondition precondition)
        {
            _precondition = precondition;
            return this;
        }


    }
}
