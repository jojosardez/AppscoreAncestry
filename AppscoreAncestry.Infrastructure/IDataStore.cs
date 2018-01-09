namespace AppscoreAncestry.Infrastructure
{
    public interface IDataStore<T>
    {
        T Get();
    }
}
