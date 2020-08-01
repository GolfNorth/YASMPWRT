namespace GeekBrainsInternship.Interfaces
{
    public interface IInitializable
    {
        void Initialize();
    }
    
    public interface IInitializable<T>
    {
        void Initialize(T context);
    }
}