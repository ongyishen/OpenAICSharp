namespace OpenAIDemo.Models
{
    public class TData
    {
        /// <summary>
        /// 1 : Success ; 0: Failed
        /// </summary>
        public int Tag { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }
    }

    public class TData<T> : TData
    {
        public int Total { get; set; }

        public T Data { get; set; }


    }
}
