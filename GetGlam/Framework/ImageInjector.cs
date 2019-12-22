using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using System;
using System.IO;

namespace GetGlam.Framework
{
    /// <summary>Class used to inject new textures into the games content.</summary>
    public class ImageInjector : IAssetEditor
    {
        //Instance of ModEntry
        private ModEntry Entry;

        //Instance of ContentPackHelper
        private ContentPackHelper PackHelper;

        //The HairTexture height, default: 672
        private int HairTextureHeight = 672;

        //The AccessoryTexture Height, default: 96
        private static int AccessoryTextureHeight = 96;

        //The DresserTexture Height, default: 32
        private int DresserTextureHeight = 32;

        //Was the Dresser image already edited???
        private bool WasDresserImageEdited = false;

        /// <summary>Image Injectors Constructor</summary>
        /// <param name="entry"></param>
        /// <param name="packHelper"></param>
        public ImageInjector(ModEntry entry, ContentPackHelper packHelper)
        {
            //Set the vars to the instances
            Entry = entry;
            PackHelper = packHelper;
        }

        /// <summary>Wether SMAPI's Asset Editor can edit a specific asset.</summary>
        /// <typeparam name="T">The Type of asset</typeparam>
        /// <param name="asset">The asset in question</param>
        /// <returns>Whether it can load the specific asset</returns>
        public bool CanEdit<T>(IAssetInfo asset)
        {
            if (asset.AssetNameEquals("Characters\\Farmer\\hairstyles"))
                return true;
            else if (asset.AssetNameEquals("Characters\\Farmer\\accessories"))
                return true;
            else if (asset.AssetNameEquals($"Mods/{Entry.ModManifest.UniqueID}/dresser.png"))
                return true;

            return false;
        }

        /// <summary>SMAPI's Asset Editor tries to edit a specific asset.</summary>
        /// <typeparam name="T">The Type of asset</typeparam>
        /// <param name="asset">The asset in question</param>
        public void Edit<T>(IAssetData asset)
        {
            //If the asset is hairstyles
            if (asset.AssetNameEquals("Characters\\Farmer\\hairstyles"))
            {
                //Create a new texture and set it as the old one
                Texture2D oldTexture = asset.AsImage().Data;
                Texture2D newTexture = new Texture2D(Game1.graphics.GraphicsDevice, oldTexture.Width, Math.Max(oldTexture.Height, 4096));
                asset.ReplaceWith(newTexture);
                asset.AsImage().PatchImage(oldTexture);

                //Loop through each hair loaded and extend the image
                foreach (var hair in PackHelper.HairList)
                {
                    if ((hair.TextureHeight + HairTextureHeight) > 4096)
                    {
                        Entry.Monitor.Log($"{hair.ModName} hairstyles cannot be added to the game, the texture is too big.", LogLevel.Error);
                        return;
                    }

                    //Patch the hair texture and change the hair texture height
                    asset.AsImage().PatchImage(hair.Texture, null, new Rectangle(0, HairTextureHeight, 128, hair.Texture.Height));
                    HairTextureHeight += hair.TextureHeight;
                }

                //Cut the blank image from the image
                CutEmptyImage(asset, HairTextureHeight, 128);
            }

            //If the asset is accessories
            if (asset.AssetNameEquals("Characters\\Farmer\\accessories"))
            {
                //Create a new texture and set it as the old one
                Texture2D oldTexture = asset.AsImage().Data;
                Texture2D newTexture = new Texture2D(Game1.graphics.GraphicsDevice, oldTexture.Width, Math.Max(oldTexture.Height, 4096));
                asset.ReplaceWith(newTexture);
                asset.AsImage().PatchImage(oldTexture);
    
                //Loop through each accessory loaded and extend the image
                foreach (var accessory in PackHelper.AccessoryList)
                {
                    if ((accessory.TextureHeight + AccessoryTextureHeight) > 4096)
                    {
                        Entry.Monitor.Log($"{accessory.ModName} accessories cannot be added to the game, the texture is too big.", LogLevel.Warn);
                        return;
                    }

                    //Patch the accessory texture and change the accessory texture height
                    asset.AsImage().PatchImage(accessory.Texture, null, new Rectangle(0, AccessoryTextureHeight, 128, accessory.Texture.Height));
                    AccessoryTextureHeight += accessory.TextureHeight;
                }

                //Cut the blank image from the image
                CutEmptyImage(asset, AccessoryTextureHeight, 128);
            }

            //If the asset is the dresser
            if (asset.AssetNameEquals($"Mods/{Entry.ModManifest.UniqueID}/dresser.png"))
            {
                //Break if the image was already edited
                if (WasDresserImageEdited)
                    return;

                //Set dresser was edited and replace the old tecture with a new one
                WasDresserImageEdited = true;
                Texture2D oldTexture = asset.AsImage().Data;
                Texture2D newTexture = new Texture2D(Game1.graphics.GraphicsDevice, 16, Math.Max(oldTexture.Height, 4096));
                asset.ReplaceWith(newTexture);
                asset.AsImage().PatchImage(oldTexture);

                //Loop through each dresser and patch the image
                foreach (var dresser in PackHelper.DresserList)
                {
                    if ((dresser.TextureHeight + DresserTextureHeight) > 4096)
                    {
                        Entry.Monitor.Log($"{dresser.ModName} dressers cannot be added to the game, the texture is too big.", LogLevel.Warn);
                        return;
                    }

                    //Patch the dresser.png and adjust the height
                    asset.AsImage().PatchImage(dresser.Texture, null, new Rectangle(0, DresserTextureHeight, 16, dresser.TextureHeight));
                    DresserTextureHeight += dresser.TextureHeight;
                }

                Entry.Monitor.Log($"Dresser Image height is now: {DresserTextureHeight}", LogLevel.Trace);

                //Cut the empty image from the dresser texture
                CutEmptyImage(asset, DresserTextureHeight, 16);

                //Save the dresser to the mod folder so it can be used to create a TileSheet for the Farmhouse
                FileStream stream = new FileStream(Path.Combine(Entry.Helper.DirectoryPath, "assets", "dresser.png"), FileMode.Create);
                asset.AsImage().Data.SaveAsPng(stream, 16, DresserTextureHeight);
                stream.Close();
            }
        }

        /// <summary>Get the number of accessories, used in AccessoryPatch</summary>
        /// <returns>The number of Accessories</returns>
        public static int GetNumberOfAccessories()
        {
            return AccessoryTextureHeight / 32 * 8 / 2;
        }

        /// <summary>Get the number of accessories minus one, used in AccessoryPatch</summary>
        /// <returns>The number of Accessories minus one</returns>
        public static int GetNumberOfAccessoriesMinusOne()
        {
            return AccessoryTextureHeight / 32 * 8 / 2 - 1;
        }

        /// <summary>Cuts the empty image from the texture.</summary>
        /// <param name="asset">The asset to cut</param>
        /// <param name="newHeight">The assets new height</param>
        /// <param name="newWidth">The assets new width</param>
        private void CutEmptyImage(IAssetData asset, int newHeight, int newWidth)
        {
            Texture2D oldTexture = asset.AsImage().Data;
            Texture2D cutTexture = new Texture2D(Game1.graphics.GraphicsDevice, oldTexture.Width, newHeight);

            asset.ReplaceWith(cutTexture);
            asset.AsImage().PatchImage(oldTexture, new Rectangle(0, 0, newWidth, newHeight), new Rectangle(0, 0, newWidth, newHeight));
        }
    }
}
