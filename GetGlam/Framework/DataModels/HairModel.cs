using Microsoft.Xna.Framework.Graphics;

namespace GetGlam.Framework.DataModels
{
    /// <summary>Class the allows hairstyles to be loaded by content packs</summary>
    public class HairModel
    {
        //The hairs texture
        public Texture2D Texture;

        //The texture height
        public int TextureHeight;

        //The mod name where the hairstyles came from
        public string ModName;
    }
}
