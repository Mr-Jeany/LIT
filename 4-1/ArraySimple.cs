using System;

namespace _4_1
{
    public class ArraySimple<T>
    {
        private int _capacity = 10;
        private int _size = 0;
        private T[] _selfArray;
        private T _emptySlot;

        public ArraySimple(int size = 10)
        {
            _capacity = size;
            _emptySlot = (T)Convert.ChangeType(Int32.MinValue, typeof(T));
            RegenerateArray();
        }

        public void RegenerateArray()
        {
            Console.Write("Write elements of your array separated by spaces: ");
            string[] input = Console.ReadLine().Split();
            
            while (input.Length > _capacity)
            {
                _capacity = _capacity * 2 + 1;
            }
            
            _selfArray = new T[_capacity];
            
            for (int i = 0; i < _capacity; i++)
            {
                _selfArray[i] = (T)Convert.ChangeType(_emptySlot, typeof(T));
            }
            
            for (int i = 0; i < input.Length; i++)
            {
                _selfArray[i] = (T)Convert.ChangeType(input[i], typeof(T));
                _size++;
            }
        }

        public void Print()
        {
            MakeForEachElement((x) => Console.Write(x + " "));
            Console.WriteLine();
        }

        public void Add(Object newElement)
        {
            int newSize = _size + 1;
            if (newSize >= _capacity)
            {
                while (newSize >= _capacity)
                {
                    _capacity = _capacity * 2 + 1;
                }
                
                Array.Resize(ref _selfArray, _capacity);

                for (int i = _size; i < _capacity; i++)
                {
                    _selfArray[i] = _emptySlot;
                }
            }
            
            _selfArray[newSize - 1] = (T)Convert.ChangeType(newElement, typeof(T));
        }

        public void Remove(int elementIndex)
        {
            _selfArray[elementIndex] = _emptySlot;
            MakeItBetter();
        }

        private void MakeItBetter()
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
            MakeItBetter();
        }

        public int Count()
        {
            return _size;
        }

        public int CountWithCondition(Func<T, bool> condition)
        {
            short counter = 0;
            foreach (var element in _selfArray)
            {
                if (condition(element))
                {
                    counter++;
                }
            }

            return counter;
        }

        public bool CheckIfOneIsTrue(Func<T, bool> condition)
        {
            foreach (var element in _selfArray)
            {
                if (condition(element) && !element.Equals(_emptySlot))
                {
                    return true;
                }
            }

            return false;
        }
        
        public bool CheckIfAllAreTrue(Func<T, bool> condition)
        {
            foreach (var element in _selfArray)
            {
                if (!condition(element) && !element.Equals(_emptySlot))
                {
                    return false;
                }
            }

            return true;
        }

        public T FindFirstElementByCondition(Func<T, bool> condition)
        {
            foreach (var element in _selfArray)
            {
                if (condition(element))
                {
                    return element;
                }
            }
            Console.WriteLine("No element found. Returning -1.");
            return (T)Convert.ChangeType(-1, typeof(T));
        }

        public void Reverse()
        {
            Array.Reverse(_selfArray);
            MakeItBetter();
        }
        
        public bool Contains(T element)
        {
            return CheckIfOneIsTrue((x) => x.Equals(element));
        }

        public void MakeForEachElement(Action<T> action)
        {
            foreach (var element in _selfArray)
            {
                if (!element.Equals(_emptySlot))
                {
                    action(element);
                }
            }
        }
    }
}