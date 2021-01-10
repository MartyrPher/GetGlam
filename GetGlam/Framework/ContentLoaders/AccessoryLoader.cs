using GetGlam.Framework.DataModels;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using System.IO;

namespace GetGlam.Framework.ContentLoaders
{
    public class AccessoryLoader
    {
        // Instance of ModEntry
        private ModEntry Entry;

        // Directory for Accessory files
        private DirectoryInfo AccessoriesDirectory;

        // Current Content Pack 
        private IContentPack CurrentContentPack;

        // Accessory Model
        private AccessoryModel Accessory;

        // Instance of ContentPackHelper
        private ContentPackHelper PackHelper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="modEntry">Instance of ModEntry</param>
        /// <param name="contentPack">Current Content Pack</param>
        public AccessoryLoader(ModEntry modEntry, IContentPack contentPack, ContentPackHelper packHelper)
        {
            AccessoriesDirectory = new DirectoryInfo(Path.Combine(contentPack.DirectoryPath, "Accessories"));
            Entry = modEntry;
            CurrentContentPack = contentPack;
            PackHelper = packHelper;
        }
        
        /// <summary>
        /// Loads an accessory from a Content Pack.
        /// </summary>
        public void LoadAccessory()
        {
            try
            {
                if (DoesAccessoryDirectoryExist())
                {
                    CreateAccessoryModel();
                    SetAccessoryModelVariables();
                    AddNumberOfAccessories();
                    AddAccessoryToAccessoryList();
                }
            }
            catch
            {
                Entry.Monitor.Log($"{CurrentContentPack.Manifest.Name} accessories is empty. This pack was not added.", LogLevel.Warn);
            }
        }

        /// <summary>
        /// Gets the Accessory Model.
        /// </summary>
        /// <returns>The Accessory Model</returns>
        public AccessoryModel GetAccessoryModel()
        {
            return Accessory;
        }

        /// <summary>
        /// Checks if the Accessory Directory Exists.
        /// </summary>
        /// <returns>Whether the directory exists</returns>
        private bool DoesAccessoryDirectoryExist()
        {
            return AccessoriesDirectory.Exists;
        }

        /// <summary>
        /// Creates the accessory model by loading files.
        /// </summary>
        private void CreateAccessoryModel()
        {
            Accessory = CurrentContentPack.ReadJsonFile<AccessoryModel>("Accessories/accessories.json");
            Accessory.Texture = CurrentContentPack.LoadAsset<Texture2D>("Accessories/accessories.png");
        }

        /// <summary>
        /// Sets texture height and mod name in the model.
        /// </summary>
        private void SetAccessoryModelVariables()
        {
            Accessory.TextureHeight = Accessory.Texture.Height;
            Accessory.ModName = CurrentContentPack.Manifest.Name;
        }

        /// <summary>
        /// Adds number of accessories from the Content Pack.
        /// </summary>
        private void AddNumberOfAccessories()
        {
            PackHelper.NumberOfHairstlyesAdded += Accessory.NumberOfAccessories;
        }

        /// <summary>
        /// Adds accessory model to the accessory list.
        /// </summary>
        private void AddAccessoryToAccessoryList()
        {
            PackHelper.AccessoryList.Add(Accessory);
        }
    }
}
