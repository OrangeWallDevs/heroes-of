using System.Collections.Generic;

public class PriorityList<T> : OrdenatedList<PriorityPair<T>> {

    private BinaryCell<PriorityPair<T>> startCell, finalCell;

    public PriorityList() {

        startCell = new BinaryCell<PriorityPair<T>>();
        finalCell = new BinaryCell<PriorityPair<T>>();

        startCell.LeftCell = null;
        startCell.RightCell = finalCell;

        finalCell.LeftCell = startCell;
        finalCell.RightCell = null;

    }

    public override void Add(PriorityPair<T> data) {

        if (Count == 0) {

            BinaryCell<PriorityPair<T>> newCell = new BinaryCell<PriorityPair<T>>(Count, data);

            finalCell.LeftCell = newCell;
            startCell.RightCell = newCell;

            newCell.LeftCell = startCell;
            newCell.RightCell = finalCell;

        }
        else {

            BinaryCell<PriorityPair<T>> newCell = new BinaryCell<PriorityPair<T>>(Count, data);
            BinaryCell<PriorityPair<T>> graterCell = finalCell.LeftCell;

            while (!(graterCell.Value.CompareTo(newCell.Value) > 0)) {

                graterCell.Index++;
                graterCell = graterCell.LeftCell;

                if (graterCell == startCell) {

                    break;

                }

            }

            BinaryCell<PriorityPair<T>> smallerCell = graterCell.RightCell;

            graterCell.RightCell = newCell;
            smallerCell.LeftCell = newCell;

            newCell.RightCell = smallerCell;
            newCell.LeftCell = graterCell;

            if (smallerCell != finalCell) {

                newCell.Index = smallerCell.Index - 1;

            }

        }

        Count++;

    }

    public override void Clear() {
        
        if (Count <= 0) {

            return;

        }

        BinaryCell<PriorityPair<T>> cell = startCell.RightCell;

        while (cell != finalCell) {

            BinaryCell<PriorityPair<T>> nextCell = cell.RightCell;

            cell.LeftCell = null;
            cell.RightCell = null;
            cell.Index = 0;
            cell.Value = null;

            cell = nextCell;

        }

        startCell.RightCell = finalCell;
        finalCell.LeftCell = startCell;
        Count = 0;

    }

    public override bool Contains(PriorityPair<T> data) {
        
        if (Count <= 0) {

            return false;

        }

        BinaryCell<PriorityPair<T>> cell = finalCell.LeftCell;

        while (cell.Value != data) {

            cell = cell.LeftCell;

            if (cell == startCell) {

                return false;

            }

        }

        return (cell.Value == data);

    }

    public override PriorityPair<T> Find(int index) {

        if (Count <= 0 || index >= Count || index < 0) {

            return null;

        }

        BinaryCell<PriorityPair<T>> cell = finalCell.LeftCell;

        while (cell.Index != index) {

            cell = cell.LeftCell;
            
        }

        return cell.Value;

    }

    public override PriorityPair<T> GetFirst() {

        if (Count <= 0) {

            return null;

        }

        return startCell.RightCell.Value;

    }

    public override PriorityPair<T> GetLast() {

        if (Count <= 0) {

            return null;

        }

        return finalCell.LeftCell.Value;

    }

    public override int IndexOf(PriorityPair<T> data) {

        if (Count <= 0) {

            return -1;

        }

        BinaryCell<PriorityPair<T>> cell = finalCell.LeftCell;

        while (cell.Value != data) {

            cell = cell.LeftCell;

            if (cell == startCell) {

                return -1;

            }

        }

        return cell.Index;

    }

    public override PriorityPair<T> Remove(PriorityPair<T> data) {

        if (Count <= 0 || !Contains(data)) {

            return null;

        }
        
        BinaryCell<PriorityPair<T>> cell = finalCell.LeftCell;

        while (cell.Value != data) {

            cell.Index--;
            cell = cell.LeftCell;

        }

        BinaryCell<PriorityPair<T>> leftCell = cell.LeftCell;
        BinaryCell<PriorityPair<T>> rightCell = cell.RightCell;

        leftCell.RightCell = rightCell;
        rightCell.LeftCell = leftCell;

        cell.RightCell = null;
        cell.LeftCell = null;
        cell.Index = 0;

        Count--;

        return cell.Value;

    }

    public override PriorityPair<T> Remove(int index) {
        
        if (Count <= 0 || index >= Count || index < 0) {

            return null;

        }

        PriorityPair<T> searchedData = Find(index);

        if (searchedData == null) {

            return null;

        }

        return Remove(searchedData);

    }

    public override List<PriorityPair<T>> ToList() {

        List<PriorityPair<T>> returnList = new List<PriorityPair<T>>();
        BinaryCell<PriorityPair<T>> cell = startCell.RightCell;

        while (cell != finalCell) {

            returnList.Add(cell.Value);
            cell = cell.RightCell;

        }

        return returnList;

    }

    public override string ToString() {

        string listString = "Values: [";

        BinaryCell<PriorityPair<T>> cell = startCell.RightCell;

        while (cell.RightCell != null) {

            listString += " (" + cell.Index + ")";
            listString += " Key: \"" + cell.Value.Key + "\"";
            listString += " Priority: " + cell.Value.Priority + ";";

            cell = cell.RightCell;

        }

        listString += " ]";

        return listString;

    }

}
