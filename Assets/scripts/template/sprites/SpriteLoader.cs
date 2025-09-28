using System.Linq;
using UnityEngine;

namespace template.sprites
{
    public class SpriteLoader
    {
        public static SpriteSheet load(string path, string origin)
        {
            var concatenatedPath = path;
            if (path.Length > 0 && path[^1] != '/')
            {
                concatenatedPath = path + "/";
            }
            var sprites = Resources.LoadAll<Sprite>(concatenatedPath + origin);
            return new SpriteSheet(origin, sprites);
        }

        public class SpriteSheet
        {
            private string origin;
            private Sprite[] sprites;

            public SpriteSheet(string origin, Sprite[] sprites)
            {
                this.origin = origin;
                this.sprites = sprites;
            }

            public Sprite getAtSuffix(string suffix)
            {
                return sprites.First(s => s.name == (origin + "_" + suffix));
            } 
        }
    }
}