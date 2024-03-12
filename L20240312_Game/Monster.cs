
internal class Monster : GameObject
{
    public Monster()
    {
        shape = 'M';
    }

    ~Monster()
    {

    }

    public Monster(int newX, int newY)
    {
        shape = 'M';

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

