using GetGlam.Framework.DataModels;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using System.IO;

namespace GetGlam.Framework.ContentLoaders
{
    public class HairLoader
    {
        // Instance of ModEntry
        private ModEntry Entry;

        // Directory where the hair files are stored
        private DirectoryInfo HairDirectory;

        // The model of the hair
        private HairModel Hair;

        // Current content pack being looked at
        private IContentPack CurrentContentPack;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="modEntry">Instance of ModEntry</param>
        /// <param name="contentPack">Current content pack</param>
        public HairLoader(ModEntry modEntry, IContentPack contentPack)
        {
            HairDirectory = new DirectoryInfo(Path.Combine(contentPack.DirectoryPath, "Hairstyles"));
            Entry = modEntry;
            CurrentContentPack = contentPack;
        }
        
        /// <summary>
        /// Loads Hair from a Content Pack.
        /// </summary>
        public void LoadHair()
        {
            try
            {
                if (DoesHairDirectoryExists())
                {
                    CreateNewHairModel();
                    SetHairModelVariables();
                }
            }
            catch 
            {
                Entry.Monitor.Log($"{CurrentContentPack.Manifest.Name} hairstyles is empty. This pack was not added.", LogLevel.Warn);
            }
        }

        /// <summary>
        /// Get the current hair model.
        /// </summary>
        /// <returns>The hair model</returns>
        public HairModel GetHairModel()
        {
            return Hair;
        }

        /// <summary>
        /// Checks if the hair directory exists.
        /// </summary>
        /// <returns>If the hair directory exists</returns>
        private bool DoesHairDirectoryExists()
        {
            return HairDirectory.Exists;
        }

        /// <summary>
        /// Creates a new Hair Model.
        /// </summary>
        private void CreateNewHairModel()
        {
            Hair = CurrentContentPack.ReadJsonFile<HairModel>("Hairstyles/hairstyles.json");
            Hair.Texture = CurrentContentPack.LoadAsset<Texture2D>("Hairstyles/hairstyles.png");
        }

        /// <summary>
        /// Sets texture and mod name in the model.
        /// </summary>
        private void SetHairModelVariables()
        {
            Hair.TextureHeight = Hair.Texture.Height;
            Hair.ModName = CurrentContentPack.Manifest.Name;
        }
    }
}
