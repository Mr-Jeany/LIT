namespace _3_4
{
    abstract class ArrayBase : IPrinter,IArrayBase
    {
        public decimal average { get; }
        public int max { get; set; }
        public int min { get; set; }
        
        public abstract void Regenerate(bool userGenerated = false);
        public abstract void Print();
        public abstract void CountAverage();
        public abstract void Randomed();
        public abstract void Usered();
    }
}
