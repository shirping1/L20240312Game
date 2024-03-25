using SDL2;
using System.Globalization;

public enum RenderOrder
{
    None = 0,
    Floor = 100,
    Wall = 200,
    Goal = 300,
    Player = 400,
    Monster = 500,
}



class SpriteRenderer : Renderer
{
    public string textureName;

    public SDL.SDL_Color colorKey;

    public bool isMultipe = false;

    public int spriteCount = 0;

    protected int currentIndexX = 0;
    public int currentIndexY = 0;

    public ulong currentTime = 0;
    public ulong executeTime = 250;

    public SpriteRenderer()
    {
        renderOrder = RenderOrder.None;
        textureName = "";
        colorKey.r = 255;
        colorKey.g = 255;
        colorKey.b = 255;
        colorKey.a = 255;
    }

    public void Load(string _textureName)
    {
        textureName = _textureName;

        ResourceManager.Load(textureName, colorKey);
    }

    public char Shape;
    public RenderOrder renderOrder;

    public override void Update()
    {
        if (isMultipe)
        {
            currentTime += Engine.GetInstance().deltaTime;
            if (currentTime >= executeTime)
            {
                currentIndexX++;
                currentIndexX %= spriteCount;
                currentTime = 0;
            }
        }
    }

    public override void Render()
    {
        SDL.SDL_GetTicks64();
        if (transform != null)
        {
            //Console.SetCursorPosition(transform.x, transform.y);
            //Console.Write(Shape);


            Engine myEngine = Engine.GetInstance();

            SDL.SDL_Rect destRect = new SDL.SDL_Rect();
            destRect.x = transform.x * 30;
            destRect.y = transform.y * 30;
            destRect.h = 30;
            destRect.w = 30;


            //unsafe // c, c++
            //{
            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            uint format = 0;
            int access = 0;

            SDL.SDL_QueryTexture(ResourceManager.Find(textureName), out format, out access, out rect.w, out rect.h);

            if(isMultipe)
            {
                //animation
                int spriteWidth = rect.w / spriteCount;
                int spriteHeight = rect.h / spriteCount;

                rect.x = spriteWidth * currentIndexX;
                rect.y = spriteHeight * currentIndexY;
                rect.w = spriteWidth;
                rect.h = spriteHeight;
            }
            else
            {
            rect.x = 0;
            rect.y = 0;

            }


            SDL.SDL_RenderCopy(myEngine.myRenderer, ResourceManager.Find(textureName), ref rect, ref destRect);
            //}

            //SDL.SDL_RenderFillRect(myEngine.myRenderer, ref myRect);
        }
    }
}

