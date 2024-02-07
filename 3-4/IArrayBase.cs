namespace _3_4
{
    public interface IArrayBase
    {
        decimal average { get; }
        int max { get; set; }
        int min { get; set; }

        void Regenerate(bool userGenerated = false);
        void CountAverage();
        void Randomed();
        void Usered();
    }
}