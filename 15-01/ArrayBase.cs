namespace BaseArrays
{
    abstract class ArrayBase
    {
        public decimal average;
        public int min = 0;
        public int max = 10;

        public abstract void Regenerate(bool userGenerated = false);
        public abstract void Print();
        public abstract void CountAverage();
        public abstract void Randomed();
        public abstract void Usered();
    }
}
