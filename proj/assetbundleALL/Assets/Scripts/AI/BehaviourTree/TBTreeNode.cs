using UnityEngine;
using System.Collections.Generic;

public class TBTreeNode {
    private const int defaultChildCount = -1;
    private List<TBTreeNode> _children;
    private int _maxChildCount;

    public TBTreeNode(int maxChildCount = -1)
    {
        _children = new List<TBTreeNode>();
        if (maxChildCount >= 0)
            _children.Capacity = maxChildCount;
        _maxChildCount = maxChildCount;
    }

    public TBTreeNode()
        : this(defaultChildCount)
    { }

    ~TBTreeNode()
    {
        _children = null;
    }

    public TBTreeNode addChild(TBTreeNode node)
    {
        if (_maxChildCount >= 0 && _children.Count >= _maxChildCount) {
            TLogger.WARNING("exceeding child count");
            return this;
        }
        _children.Add(node);
        return this;
    }

    public int getChildCount()
    {
        return _children.Count;
    }

    public bool isIndexValid(int index)
    {
        return index >= 0 && index < _children.Count;
    }

    public T getChild<T>(int index) where T : TBTreeNode
    {
        if (index < 0 || index >= _children.Count)
            return null;
        return (T)_children[index];
    }
}
