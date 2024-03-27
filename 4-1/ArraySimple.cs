using System;

namespace _4_1
{
    public class ArraySimple<T>
    {
        private int _size = 10;
        private int _elements = 0;
        private T[] _selfArray;
        private T _emptySlot;

        public ArraySimple(int size = 10)
        {
            _size = size;
            _emptySlot = (T)Convert.ChangeType(Int32.MinValue, typeof(T));
            RegenerateArray();
        }

        public void RegenerateArray()
        {
            Console.Write("Write elements of your array separated by spaces: ");
            string[] input = Console.ReadLine().Split();
            
            while (input.Length > _size)
            {
                _size = _size * 2 + 1;
            }
            
            _selfArray = new T[_size];
            
            for (int i = 0; i < _size; i++)
            {
                _selfArray[i] = (T)Convert.ChangeType(_emptySlot, typeof(T));
            }
            
            for (int i = 0; i < input.Length; i++)
            {
                _selfArray[i] = (T)Convert.ChangeType(input[i], typeof(T));
                _elements++;
            }
        }

        public void Print()
        {
            foreach (var element in _selfArray)
            {
                if (!element.Equals(_emptySlot))
                {
                    Console.Write(element + " ");
                }
            }
            Console.WriteLine();
        }

        public void Add(Object newElement)
        {
            int newSize = _elements + 1;
            if (newSize >= _size)
            {
                while (newSize >= _size)
                {
                    _size = _size * 2 + 1;
                }
                
                Array.Resize(ref _selfArray, _size);

                for (int i = _elements; i < _size; i++)
                {
                    _selfArray[i] = _emptySlot;
                }
            }
            
            _selfArray[newSize - 1] = (T)Convert.ChangeType(newElement, typeof(T));
        }

        public void Remove(int elementIndex)
        {
            _selfArray[elementIndex] = _emptySlot;
            MakeitBetter();
        }

        private void MakeitBetter()
        {
            for (int i = 0; i < _selfArray.Length - 1; i++)
            {
                if (_selfArray[i].Equals(_emptySlot))
                {
                    (_selfArray[i], _selfArray[i + 1]) = (_selfArray[i + 1], _selfArray[i]);
                }
            }
        }
        

        public void Sort()
        {
            Array.Sort(_selfArray);
            MakeitBetter();
        }
    }
}
