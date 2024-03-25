public class Component
{
    public Component()
    {
        // = new GameObject();
        //transform = new Transform();
    }

    ~Component()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    // 내가 어디 속해 있는지 확인하는 용도
    public GameObject gameObject;

    // 내가 어디
    public Transform transform;

}

