namespace AppscoreAncestry.Infrastructure
{
    public interface IDataStore
    {
        T Get<T>();
    }
}
