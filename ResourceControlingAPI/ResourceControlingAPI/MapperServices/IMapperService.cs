namespace ResourceControlingAPI.MapperServices
{
    public interface IMapperService<T, V>
    {
        public List<T> AsDtoList(List<V> modelsList);

        public List<V> AsModelList(List<T> dtosList);

        public T AsDto(V model);

        public V AsModel(T dto);  
    }
}
