namespace BusinessObject.Common
{
    public class PagingModel<T>
    {
        public List<T> Items { get; set; } = new();

        public int TotalPages { get; set; } = 0;
    }
}
