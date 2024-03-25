using SDL2;

class ResourceManager
{
    protected static Dictionary<string, IntPtr> Database = new Dictionary<string, IntPtr>();

    public static IntPtr Load(string _filename, SDL.SDL_Color _colorKey)
    {
        if (!Database.ContainsKey(_filename))
        {
            string dir = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.Parent.FullName;

            unsafe
            {
                SDL.SDL_Surface* mySurface = (SDL.SDL_Surface*)SDL.SDL_LoadBMP(dir + "/data/" + _filename);

                SDL.SDL_SetColorKey((IntPtr)mySurface, 1, SDL.SDL_MapRGBA(mySurface->format, _colorKey.r, _colorKey.g, _colorKey.b, _colorKey.a));
                IntPtr myTexture = SDL.SDL_CreateTextureFromSurface(Engine.GetInstance().myRenderer, (IntPtr)mySurface);

                Database[_filename] = myTexture;

                SDL.SDL_FreeSurface((IntPtr)mySurface);
            }
        }

        return Database[_filename];
    }

    public static IntPtr Find(string _filename)
    {
        return Database[_filename];
    }
}

