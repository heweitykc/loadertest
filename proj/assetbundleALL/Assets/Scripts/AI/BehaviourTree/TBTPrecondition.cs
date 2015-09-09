using UnityEngine;
using System.Collections;

public abstract class TBTPrecondition : TBTreeNode {
    public TBTPrecondition(int maxChildCount)
        : base(maxChildCount)
    { }

    public abstract bool isTrue(TBTWorkingData wData);
}


