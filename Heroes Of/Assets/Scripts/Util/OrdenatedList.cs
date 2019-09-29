using System;
using System.Collections;
using System.Collections.Generic;

public abstract class OrdenatedList<T> where T : IComparable<T> {

    private int count = 0;

    public abstract void Add(T data);

    public abstract T Remove(T data);

    public abstract T Remove(int index);

    public abstract void Clear();

    public abstract int IndexOf(T data);

    public abstract bool Contains(T data);

    public abstract T GetLast();

    public abstract T GetFirst();

    public abstract T Find(int index);

    public abstract List<T> ToList();

    public int Count {

        get { return count; }
        protected set { count = value; }

    }

}
