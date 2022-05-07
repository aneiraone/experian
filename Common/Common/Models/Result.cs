namespace Common
{
    public class Result
    {
        public Result(string propiedad, string message, int value)
        {
            Property = propiedad;
            Message = message;
            Value = value;
        }
        public string Property { get; set; }
        public string Message { get; set; }
        public int Value { get; set; }
    }
}
