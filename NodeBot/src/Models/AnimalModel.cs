namespace NodeBot.src.Models
{
    public class Animal
    {
        public Dog Dog { get; set; }
        public Cat Cat { get; set; }
    }

    public class Dog
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class Cat
    {
        public string Url { get; set; }
    }
}
