namespace Main.Scripts.Saving
{
    [System.Serializable]
    public class Wrapper<T>
    {
        public T Value;
        public Wrapper(T value)
        {
            Value = value;
        }
    }
}