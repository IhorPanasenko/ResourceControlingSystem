namespace ResourceControlingAPI.Services
{
    public interface IUpdateService<T,V>
    {
        public void Update(T model, V dtoUpdate);
    }
}
