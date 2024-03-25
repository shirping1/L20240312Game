using SDL2;
using System.Security.Cryptography.X509Certificates;

class PlayerController : Component
{
    SpriteRenderer renderer;
    public PlayerController()
    {

    }
    public PlayerController(int newX, int newY)
    {
        transform.x = newX;
        transform.y = newY;
    }
    ~PlayerController()
    {
    }
    public override void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public override void Update()
    {
        if (transform == null)
        {
            return;
        }

        int oldX = transform.x;
        int oldY = transform.y;

        if (Input.GetKey(SDL.SDL_Keycode.SDLK_a))
        {
            transform.Translate(-1, 0);
            renderer.currentIndexY = 0;
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_d))
        {
            transform.Translate(1, 0);
            renderer.currentIndexY = 1;

        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_w))
        {
            transform.Translate(0, -1);
            renderer.currentIndexY = 2;

        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_s))
        {
            transform.Translate(0, 1);
            renderer.currentIndexY = 3;

        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_ESCAPE))
        {
            //singleton pattern
            Engine.GetInstance().Stop();
        }

        transform.x = Math.Clamp(transform.x, 0, 80);
        transform.y = Math.Clamp(transform.y, 0, 80);

        // find new x, new y 해당 게임오브젝트 탐색
        // 찾은 게임 오브젝트에서 Collider2D 그리고 충돌 체크

        
        foreach (GameObject findGameObject in Engine.GetInstance().gameObjects)
        {
            if(findGameObject == gameObject)
            {
                continue;
            }
            Collider2D? findComponent = findGameObject.GetComponent<Collider2D>();
            if (findComponent != null)
            {
                if (findComponent.Check(gameObject) && findComponent.isTrigger == false)
                {
                    // 충돌
                    transform.x = oldX;
                    transform.y = oldY;
                    break;

                }
                if(findComponent.Check(gameObject) && findComponent.isTrigger == true)
                {
                    OnTrigger(findGameObject);
                }
            }
        }

       



        /* 내가 작성한코드
        for (int i = 0; i < Engine.GetInstance().gameObjects.Count; i++)
        {
            GameObject checkGameObjct = Engine.GetInstance().gameObjects[i];

            if (Engine.GetInstance().gameObjects[i].GetComponent<Collider2D>() != null && Engine.GetInstance().gameObjects[i] != gameObject)
            {
                if (Engine.GetInstance().gameObjects[i].GetComponent<Collider2D>().Check(gameObject))
                {
                    transform.x = oldX;
                    transform.y = oldY;
                    break;
                }
            }

        }
        */
    }
    public void OnTrigger(GameObject other)
    {
        // 겹쳤을때 처리 할 로릭
        if(other.name == "Monster")
        {
            Engine.GetInstance().Find("GameManager").GetComponent<GameManager>().isGameOver = true;
            // GameOver
        }
        else if(other.name == "Goal")
        {
            Engine.GetInstance().Find("GameManager").GetComponent<GameManager>().isNextStage = true;
            // 다음판

        }
    }
}