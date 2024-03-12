class Floor : GameObject
{
    public Floor()
    {
        shape = ' ';
    }

    ~Floor()
    {

    }
    public Floor(int newX, int newY)
    {
        shape = ' ';


        x = newX;
        y = newY;
    }

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public override void Render()
    {
        //base.Render();
    }
}

