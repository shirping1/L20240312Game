using SDL2;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

class Engine
{
    protected Engine()
    {
        gameObjects = new List<GameObject>();
        isRunning = true;
    }

    ~Engine()
    {

    }

    public static Engine GetInstance()
    {
        if (instance == null)
        {
            instance = new Engine();
        }
        return instance;
        // return instance ?? (instance = new Engine());
    }

    private static Engine? instance;

    public List<GameObject> gameObjects;
    public bool isRunning;

    public bool isNextLoading = false;
    public string nextSceneName = string.Empty;

    public void NextLoadScene(string _nextSceneName)
    {
        isNextLoading = true;
        nextSceneName = _nextSceneName;
    }

    public IntPtr myWindow;
    public IntPtr myRenderer;
    public SDL.SDL_Event myEvent;

    public ulong deltaTime;
    protected ulong lastTime;


    public void Init()
    {

        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0)
        {
            Console.WriteLine("Init Fail.");
            return;
        }

        myWindow = SDL.SDL_CreateWindow("2D Engine", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        myRenderer = SDL.SDL_CreateRenderer(myWindow, -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
            SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC |
            SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);

        Input.Init();

        lastTime = SDL.SDL_GetTicks64();

        // Load();
    }

    public void Stop()
    {
        isRunning = false;
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

        GameObject newGameObject;

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (map[y][x] == '*')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Wall";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = '*';
                    renderer.Load("wall.bmp");
                    renderer.renderOrder = RenderOrder.Wall;
                    newGameObject.AddCompoment<Collider2D>();

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = ' ';
                    renderer.Load("floor.bmp");
                    renderer.renderOrder = RenderOrder.Floor;

                }
                if (map[y][x] == 'P')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Player";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = 'P';
                    renderer.colorKey.g = 0;
                    renderer.Load("test.bmp");
                    renderer.isMultipe = true;
                    renderer.spriteCount = 5;
                    renderer.renderOrder = RenderOrder.Player;
                    PlayerController playerController = newGameObject.AddCompoment<PlayerController>();
                    playerController.Start();
                    Collider2D collider2D = newGameObject.AddCompoment<Collider2D>();
                    collider2D.isTrigger = true;

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = ' ';
                    renderer.Load("floor.bmp");
                    renderer.renderOrder = RenderOrder.Floor;


                    //Instantiate(new Floor(x, y));
                    //Instantiate(new Player(x, y));
                }
                if (map[y][x] == 'M')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Monster";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = 'M';
                    renderer.Load("slime.bmp");
                    renderer.renderOrder = RenderOrder.Monster;
                    Collider2D collider2D = newGameObject.AddCompoment<Collider2D>();
                    collider2D.isTrigger = true;
                    newGameObject.AddCompoment<AIController>();


                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = ' ';
                    renderer.Load("floor.bmp");
                    renderer.renderOrder = RenderOrder.Floor;
                }
                if (map[y][x] == 'G')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Goal";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = 'G';
                    renderer.Load("coin.bmp");
                    renderer.renderOrder = RenderOrder.Goal;
                    Collider2D collider2D = newGameObject.AddCompoment<Collider2D>();
                    collider2D.isTrigger = true;


                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = ' ';
                    renderer.Load("floor.bmp");
                    renderer.renderOrder = RenderOrder.Floor;
                }
                if (map[y][x] == ' ')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpriteRenderer renderer = newGameObject.AddCompoment<SpriteRenderer>();
                    renderer.Shape = ' ';
                    renderer.Load("floor.bmp");
                    renderer.renderOrder = RenderOrder.Floor;
                }

            }
        }

        newGameObject = Instantiate<GameObject>();
        newGameObject.name = "GameManager";
        newGameObject.AddCompoment<GameManager>();

        RenderSort();
    }

    public void RenderSort()
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = i + 1; j < gameObjects.Count; j++)
            {
                GameObject preObject = gameObjects[i];
                GameObject nextObject = gameObjects[j];

                SpriteRenderer? preRender = preObject.GetComponent<SpriteRenderer>();
                SpriteRenderer? nextRender = nextObject.GetComponent<SpriteRenderer>();

                if (preRender != null && nextRender != null)
                {
                    if ((int)preRender.renderOrder > (int)nextRender.renderOrder)
                    {
                        GameObject temp = gameObjects[i];
                        gameObjects[i] = gameObjects[j];
                        gameObjects[j] = temp;
                    }

                }


            }
        }
    }

    protected void StartInAllComponents()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            foreach (Component component in gameObject.components)
            {
                component.Start();
            }

        }
    }

    public void Run()
    {
        bool isFirst = true;
        while (isRunning)
        {
            if (isFirst)
            {
                StartInAllComponents();
                isFirst = false;
            }

            ProcessInput();
            Update();
            Render();
            if (isNextLoading)
            {
                gameObjects.Clear();
                LoadScene(nextSceneName);
                isNextLoading = false;
                nextSceneName = string.Empty;
            }


        } // frame
    }

    public void Term()
    {
        gameObjects.Clear();

        SDL.SDL_DestroyRenderer(myRenderer);
        SDL.SDL_DestroyWindow(myWindow);
        SDL.SDL_Quit();

    }

    public GameObject Instantiate<T>() where T : GameObject, new()
    {
        T newObject = new T();
        gameObjects.Add(newObject);

        return newObject;
    }

    //public GameObject Instantiate(GameObject newGameObject)
    //{
    //    gameObjects.Add(newGameObject);

    //    return newGameObject;
    //}

    protected void ProcessInput()
    {
        SDL.SDL_PollEvent(out myEvent);
        //Input.keyInfo = Console.ReadKey();
    }

    protected void Update()
    {
        deltaTime = SDL.SDL_GetTicks64() - lastTime;
        foreach (GameObject gameObject in gameObjects)
        {
            foreach (Component component in gameObject.components)
            {
                component.Update();
            }

        }
        lastTime = SDL.SDL_GetTicks64();
    }



    protected void Render()
    {
        //gameObjects.Sort();

        //for(int i = 0; i < gameObjects.Count; i++)
        //{
        //    gameObjects[i].Render();
        //}
        Console.Clear();
        //gameObjects[1] == Player()
        foreach (GameObject gameObject in gameObjects)
        {
            Renderer? renderer = gameObject.GetComponent<Renderer>();

            if (renderer != null)
            {
                renderer.Render();
            }

        }
        SDL.SDL_RenderPresent(Engine.GetInstance().myRenderer);
    }

    public GameObject? Find(string name)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == name)
            {
                return gameObject;
            }
        }

        return null;
    }


}

