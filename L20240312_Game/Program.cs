
class Program
{
    static void Main(string[] args)
    {
        Engine engine = new Engine();
        engine.Init();
        engine.LoadScene("level01.map.txt");
        engine.Run();
        engine.Term();
    }
}

