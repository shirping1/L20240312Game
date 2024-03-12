
internal class Goal : GameObject
{
    public Goal()
    {
        shape = 'G';
    }

    ~Goal()
    {

    }

    public Goal(int newX, int newY)
    {
        shape = 'G';


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
        base.Render();
    }
}

