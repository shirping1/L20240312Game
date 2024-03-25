class AIController : Component
{
    public AIController()
    {
        lastUpdate = 0;
        processTime = 500;
    }

    protected ulong lastUpdate;
    protected ulong processTime;

    public override void Start()
    {
    }
    public override void Update()
    {
        lastUpdate += Engine.GetInstance().deltaTime;
        if (lastUpdate >= processTime )
        {
            Random random = new Random();
            int nextDirection = random.Next(0, 4);

            int oldX = transform.x;
            int oldY = transform.y;

            if (nextDirection == 0)
            {
                transform.Translate(-1, 0);
            }
            if (nextDirection == 1)
            {
                transform.Translate(1, 0);
            }
            if (nextDirection == 2)
            {
                transform.Translate(0, -1);
            }
            if (nextDirection == 3)
            {
                transform.Translate(0, 1);
            }
            if (transform == null)
            {
                return;
            }


            transform.x = Math.Clamp(transform.x, 0, 80);
            transform.y = Math.Clamp(transform.y, 0, 80);

            // find new x, new y 해당 게임오브젝트 탐색
            // 찾은 게임 오브젝트에서 Collider2D 그리고 충돌 체크

            foreach (GameObject findGameObject in Engine.GetInstance().gameObjects)
            {
                if (findGameObject == gameObject)
                {
                    //자신 오브젝트 제외
                    continue;
                }

                Collider2D? findComponent = findGameObject.GetComponent<Collider2D>();
                if (findComponent != null)
                {
                    if (findComponent.Check(gameObject) && findComponent.isTrigger == false)
                    {
                        //충돌
                        transform.x = oldX;
                        transform.y = oldY;
                        break;
                    }
                }
            }
            lastUpdate = 0;
        }
    }
}

