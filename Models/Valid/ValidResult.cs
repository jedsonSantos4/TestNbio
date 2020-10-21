namespace Models.Result
{
    public class ValidResult<T>
         where T : new()
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Value { get; set; }
    }
}

