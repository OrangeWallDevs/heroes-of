using System.Collections;
using System.Collections.Generic;

public class BinaryCell<T> {

    private BinaryCell<T> leftCell, rightCell;
    private T value;
    private int index;

    public BinaryCell() { }

    public BinaryCell(int index) {

        Index = index;

    }

    public BinaryCell(int index, T value) : this(null, null, index, value) { }

    public BinaryCell(BinaryCell<T> leftCell, BinaryCell<T> rightCell, int index, T value) {

        LeftCell = leftCell;
        RightCell = rightCell;
        Index = index;
        Value = value;

    }

    public BinaryCell<T> LeftCell {

        get { return leftCell; }
        set { leftCell = value; }

    }

    public BinaryCell<T> RightCell {

        get { return rightCell; }
        set { rightCell = value; }

    }

    public T Value {

        get { return value; }
        set { this.value = value; }

    }

    public int Index {

        get { return index; }
        set { index = value; }

    }

}
