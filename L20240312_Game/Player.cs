using System.Runtime.InteropServices;

class Player : GameObject
{
    public int preX;
    public int preY;



    public Player()
    {
        shape = 'P';
        x = 0;
        y = 0;
    }

    ~Player()
    {

    }

    public Player(int newX, int newY)
    {
        shape = 'P';

        x = newX;
        y = newY;
    }

    public override void Start()
    {

    }

    public override void Update()
    {

        if (Input.GetButton("Up"))
        {
            y--;
        }
        if (Input.GetButton("Left"))
        {
            x--;
        }
        if (Input.GetButton("Down"))
        {
            y++;
        }
        if (Input.GetButton("Right"))
        {
            x++;
        }
        if (Input.GetButton("Quit"))
        {
            // singleton pattern
            // Engine.stop();
        }

        x = Math.Clamp(x, 0, 80);
        y = Math.Clamp(y, 0, 80);

    }

}
