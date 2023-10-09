using System.Diagnostics;

class MyList<T>
//Dynamic array, capacity doubles every time it's not enough capacity
{
    public T[] Data;
    public int Length = 0;
    public int Capacity;
    private int defaultCapacity = 1;

    public MyList()
    {
        Capacity = 0;
        Data = new T[Capacity];
    }

    public MyList(int capacity)
    {
        Capacity = capacity;
        Data = new T[Capacity];
    }

    public MyList(MyList<T> otherList)
    {
        Capacity = otherList.Length;
        Length = Capacity;
        Data = otherList.Data;
    }

    public T this[int index]
    {
        get { return Data[index]; }
        set { Data[index] = value; }
    }

    public void Add(T item)
    //Adds item to the end of the list
    {
        if (Length == Capacity) { Resize(); }
        Data[Length] = item;
        Length++;
    }

    public void Delete(T element)
    //Serches for an element and deletes the first match
    {
        int index = -1;
        for (int i = 0; i < Length; i++)
        {
            if (index == -1 && Data[i].Equals(element)) { index = i; break; }
        }
        if (index == -1) return;
        for (int i = index + 1; i < Length; i++)
        {
            Data[index] = Data[i];
            index++;
        }
        Length--;
    }
    
    public int IndexOf(T element)
    //Returns index of found element, else: -1
    {
        for (int i = 0; i < Length; i++)
        {
            if (Data[i].Equals(element))
                return i;
        }
        return -1;
    }

    public void Insert(T element, int index)
    {
        while(index > Capacity)
        {
            Resize();
        }
        if (Length == Capacity) { Resize(); }
        for (int i = Length + 1; i > index; i--)
        {
            Data[i] = Data[i - 1];
        }
        Data[index] = element;
        if (index > Length) Length = index + 1;
        else Length++;
    }

    public void Clear()
    {
        Data = new T[Capacity];
        Length = 0;
    }
    public void Reverse()
    {
        T[] tempData = new T[Length];
        for (int i = 0; i < Length; i++)
        {
            tempData[Length - i - 1] = Data[i];
        }
        Data = tempData;
    }

    public override string ToString()
    {
        string ans = "";
        for (int i = 0; i < Length; i++)
        {
            ans += Data[i].ToString();
            if (i < Length - 1) ans += ", ";
        }
        if (ans == "") return "empty";
        return ans;
    }

    private void Resize()
    //Doubles the size
    {
        if (Capacity == 0) Capacity = defaultCapacity;
        T[] tempData = new T[Capacity * 2];
        Capacity *= 2;
        for (int i = 0; i < Length; i++)
        {
            tempData[i] = Data[i];
        }
        Data = tempData;
    }

}


internal class Program
{
    static void Main()
    {
        MyList<int> list1 = new MyList<int>();
        Console.WriteLine($"Created list with default constructor, capacity = {list1.Capacity}");
        list1.Add(1);
        list1.Add(2);
        list1.Add(3);
        Console.WriteLine("Added 3 elements: " + list1);
        Console.WriteLine("Now capacity is " + list1.Capacity + "\n");
        Debug.Assert(list1.ToString() == "1, 2, 3");

        MyList<float> list2 = new MyList<float>(3);
        Console.WriteLine($"Created list with specified capacity = {list2.Capacity}\n");

        MyList<int> list3 = new MyList<int>(list1);
        Console.WriteLine($"Created list as a copy of the first, capacity: {list3.Capacity}\n");
        Debug.Assert(list3.ToString() == "1, 2, 3");

        list3.Delete(2);
        Console.WriteLine("List after deleting '2' : " + list3);
        Debug.Assert(list3.ToString() == "1, 3");

        list3.Clear();
        Console.WriteLine($"After list3.Clear() list: {list3}, capacity: {list3.Capacity}\n");
        Debug.Assert(list3.ToString() == "empty");

        list3.Add(1);
        list3.Add(2);
        list3.Add(3);
        list3.Reverse();
        Console.WriteLine($"Reversed {{1, 2, 3}} list: {list3}");
        Debug.Assert(list3.ToString() == "3, 2, 1");

        list3.Insert(-1, 3);
        list3.Insert(5, 5);
        Console.WriteLine(list3);
        Debug.Assert(list3.ToString() == "3, 2, 1, -1, 0, 5");

        Console.WriteLine("Index of 0 is " + list3.IndexOf(0));
        Debug.Assert(list3.IndexOf(0) == 4);

        Console.WriteLine("Element with index of 1 is " + list3[1]);
        Debug.Assert(list3[1] == 2);
    }
}