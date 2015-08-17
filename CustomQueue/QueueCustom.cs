using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomQueue
{
    public class QueueCustom<T> 
    {
        private T[] container;
        private int count;
        private int capacity;
        private int backIndex;
        private int frontIndex;

        public int FrontIndex { get { return frontIndex; } set { frontIndex = value; } }
        public int BackIndex { get { return (frontIndex + count) % capacity; } }
        public int Capacity { get { return capacity; } set { capacity = value; } }
        public int Count { get { return count; } set { count = value; } }

        public T this[int i]
        {
            get
            {
                return container[i+FrontIndex];
            }
            set
            {
                container[i+FrontIndex] = value;
            }
        }


        public QueueCustom()
        {
           container = new T[Capacity];
        }

        public QueueCustom(int capacity)
        {
            Capacity = capacity;
            container = new T[Capacity];
        }

        public void Enqueue(T element)
        {
            if(Count == Capacity)
            {
                IncreaseCapacity();
            }
            container[BackIndex] = element;
            Count++;
        }

        private void IncreaseCapacity()
        {
            this.Capacity++;
            QueueCustom<T> tempQueue = new QueueCustom<T>(this.Capacity);
            while (this.Count > 0)
            {
                tempQueue.Enqueue(Dequeue());
            }
            this.container = tempQueue.container;
            this.Count = tempQueue.Count;
            this.FrontIndex = tempQueue.FrontIndex;
        }

        public T Peek()
        {
            return container[0];
        }

        public T Dequeue()
        {
            if(this.Count < 1)
            {
                throw new InvalidOperationException("Queue i sempty");
            }
            T element = container[FrontIndex];
            container[FrontIndex] = default(T);
            Count--;
            FrontIndex = (FrontIndex + 1) ;
            return element;
        }

        public CustomIterator GetEnumerator()
        {
            return new CustomIterator(this);
        }


        public class CustomIterator
        {
            private readonly QueueCustom<T> container;
            private int currentIndex;

            public CustomIterator(QueueCustom<T> container)
            {
                this.currentIndex = -1;
                this.container = container;
            }

            public T Current
            {
                get
                {
                    if(currentIndex == -1 || currentIndex == container.Count)
                    {
                        throw new InvalidOperationException();
                    }
                    return container[currentIndex];
                }
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public bool MoveNext()
            {
                return ++currentIndex < container.Count;
            }

            public void Dispose()
            { }
        }

    }
}
