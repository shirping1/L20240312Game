using System.Data;
using static System.Net.Mime.MediaTypeNames;

class Engine
{
    public Engine()
    {
        gameObjects = new List<GameObject>();
    }

    ~Engine()
    {

    }

    public static List<GameObject> gameObjects;
    public bool isRunning = true;

    public void Init()
    {
        Input.Init();

        // Load();
    }

    public void LoadScene(string sceneName)
    {
#if DEBUG
        string dir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.Parent.FullName;
        //string[] map = File.ReadAllLines("C:/Users/lee/GitTest/Test/L20240312_Game/L20240312_Game/obj/Debug/net8.0/data/" + sceneName);
        string[] map = File.ReadAllLines(dir + "/data/" + sceneName);
#else
string dir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.Parent.FullName;
        //string[] map = File.ReadAllLines("C:/Users/lee/GitTest/Test/L20240312_Game/L20240312_Game/obj/Debug/net8.0/data/" + sceneName);
        string[] map = File.ReadAllLines(dir + "/data/" + sceneName);
#endif

        //string[] map = new string[10];
        //map[0] = "**********";
        //map[1] = "*P       *";
        //map[2] = "*        *";
        //map[3] = "*        *";
        //map[4] = "*   M    *";
        //map[5] = "*        *";
        //map[6] = "*        *";
        //map[7] = "*        *";
        //map[8] = "*       G*";
        //map[9] = "**********";

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (map[y][x] == '*')
                {
                    Instantiate(new Floor(x, y));
                    Instantiate(new Wall(x, y));
                    //newGameObject.x = x;
                    //newGameObject.y = y;
                }
                if (map[y][x] == 'P')
                {
                    Instantiate(new Floor(x, y));
                    Instantiate(new Player(x, y));
                }
                if (map[y][x] == 'M')
                {
                    Instantiate(new Floor(x, y));
                    Instantiate(new Monster(x, y));
                }
                if (map[y][x] == 'G')
                {
                    Instantiate(new Floor(x, y));
                    Instantiate(new Goal(x, y));
                }
                if (map[y][x] == ' ')
                {
                    Instantiate(new Floor(x, y));
                }

            }
        }
        //gameObjects.Sort();
    }

    public void Run()
    {
        while (isRunning)
        {
            ProcessInput();
            Update();
            Render();
        } // frame
    }

    public void Term()
    {
        gameObjects.Clear();
    }

    //public GameObject Instantiate<T>() where T : GameObject
    //{
    //    return new T();
    //}

    public GameObject Instantiate(GameObject newGameObject)
    {
        gameObjects.Add(newGameObject);

        return newGameObject;
    }

    protected void ProcessInput()
    {
        Input.keyInfo = Console.ReadKey();
    }

    protected void Update()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.Update();
        }
    }

    protected void Render()
    {
        //for(int i = 0; i < gameObjects.Count; i++)
        //{
        //    gameObjects[i].Render();
        //}
        Console.Clear();
        //gameObjects[1] == Player()
        foreach(GameObject gameObject in gameObjects)
        {
            gameObject.Render();
            
        }
    }
}

