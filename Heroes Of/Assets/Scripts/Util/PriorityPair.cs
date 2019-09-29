using System;
using System.Collections;
using System.Collections.Generic;

public class PriorityPair<T> : IComparable<PriorityPair<T>> {

    T key;
    int priority;

    public PriorityPair(T key, int priority) {

        Key = key;
        Priority = priority;

    }


    public int CompareTo(PriorityPair<T> other) {

        if (other == null) {

            return 1;

        }

        int otherPriority = other.priority;

        if (Priority > otherPriority) {

            return 1;

        }
        else if (Priority < otherPriority) {

            return -1;

        }
        else {

            return 0;

        }
        
    }

    public int Priority {

        get { return priority; }
        set { priority = value; }

    }

    public T Key {

        get { return key; }
        private set { key = value; }

    }

}
