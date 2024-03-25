
using System.Reflection;

class Program
{

    static void Main(string[] args)
    {

        


        
        Engine engine = Engine.GetInstance();
        engine.Init();
        engine.LoadScene("level01.map.txt");
        engine.Run();
        engine.Term();

        
    }
}

