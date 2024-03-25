class Timer
{
    public ulong excuteTime = 0;
    protected ulong elapsedTime = 0;

    public delegate void Callback();
    public Callback callback;

    public Timer(ulong _excuteTime, Callback _callback)
    {
        SetTimer(_excuteTime, _callback);
    }

    public void SetTimer(ulong _excuteTime, Callback _callback)
    {
        excuteTime = _excuteTime;
        callback = _callback;
    }

    public void Update()
    {
        elapsedTime += Engine.GetInstance().deltaTime;
        if(elapsedTime >= excuteTime)
        {
            // 실행
            // 함수를 등록해서 그 함수 실행 되게
            callback();
            elapsedTime = 0;
        }
    }
}

