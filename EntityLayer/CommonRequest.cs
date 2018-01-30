namespace EntityLayer
{
    public class CommonRequest<T>
    {
        public T Data { get; set; }

        public override string ToString()
        {          
            return Data.ToString();
        }
    }
}
