using GetGlam.Framework.DataModels;
using StardewValley;
using System.IO;

namespace GetGlam.Framework
{
    /// <summary>Class that saves and loads character layouts</summary>
    public class CharacterLoader
    {
        //Instance of ModEntry
        private ModEntry Entry;

        //Instance of ContentPackHelper
        private ContentPackHelper PackHelper;

        //Instance of DresserHandler
        private DresserHandler Dresser;

        /// <summary>CharacterLoader's Constructor</summary>
        /// <param name="entry">The instance of ModEntry</param>
        /// <param name="packHelper">The instance of ContentPackHelper</param>
        /// <param name="dresser">The instance of DresserHandler</param>
        public CharacterLoader(ModEntry entry, ContentPackHelper packHelper, DresserHandler dresser)
        {
            //Set the fields to the instances
            Entry = entry;
            PackHelper = packHelper;
            Dresser = dresser;
        }

        /// <summary>Save the characters layout to a json file</summary>
        /// <param name="isMale">Whether the player is male</param>
        /// <param name="baseIndex">The base index</param>
        /// <param name="skinIndex">The skin index</param>
        /// <param name="hairIndex">The hair index</param>
        /// <param name="faceIndex">The face index</param>
        /// <param name="noseIndex">The nose index</param>
        /// <param name="shoesIndex">The shoes index</param>
        /// <param name="accessoryIndex">The accessory index</param>
        /// <param name="dresserIndex">The dresser index</param>
        /// <param name="isBald">Whether the player is bald</param>
        public void SaveCharacterLayout(bool isMale, int baseIndex, int skinIndex, int hairIndex, int faceIndex, int noseIndex, int shoesIndex, int accessoryIndex, int dresserIndex, bool isBald)
        {
            //Save all the current player index to the FavoriteModel
            FavoriteModel currentPlayerStyle = new FavoriteModel();
            currentPlayerStyle.IsMale = isMale;
            currentPlayerStyle.BaseIndex = baseIndex;
            currentPlayerStyle.SkinIndex = skinIndex;
            currentPlayerStyle.HairIndex = hairIndex;
            currentPlayerStyle.FaceIndex = faceIndex;
            currentPlayerStyle.NoseIndex = noseIndex;
            currentPlayerStyle.ShoesIndex = shoesIndex;
            currentPlayerStyle.AccessoryIndex = accessoryIndex;
            currentPlayerStyle.DresserIndex = dresserIndex;
            currentPlayerStyle.IsBald = isBald;

            //Write the favorite model to a json
            Entry.Helper.Data.WriteJsonFile<FavoriteModel>(Path.Combine("Saves", $"{Game1.player.name.Value}_current.json"), currentPlayerStyle);
        }

        /// <summary>Loads the character layout from a json file</summary>
        /// <param name="menu">The instance of Glam Menu used ti update indexes</param>
        public void LoadCharacterLayout(GlamMenu menu)
        {
            FavoriteModel currentPlayerStyle = Entry.Helper.Data.ReadJsonFile<FavoriteModel>(Path.Combine("Saves", $"{Game1.player.name.Value}_current.json"));

            //Don't try to load if it doesn't find the json
            if (currentPlayerStyle is null)
                return;

            //Update the dresser and Update the Menu Indexes
            Dresser.TextureSourceRect.Y = currentPlayerStyle.DresserIndex.Equals(1) ? 0 : currentPlayerStyle.DresserIndex * 32 - 32;
            Dresser.SetDresserTileSheetPoint(currentPlayerStyle.DresserIndex);
            menu.UpdateIndexes(currentPlayerStyle.BaseIndex, currentPlayerStyle.FaceIndex, currentPlayerStyle.NoseIndex, currentPlayerStyle.ShoesIndex, currentPlayerStyle.DresserIndex, currentPlayerStyle.IsBald);

            //Set the players gender, hair, accessory and skin
            Game1.player.changeGender(currentPlayerStyle.IsMale);
            Game1.player.hair.Set(currentPlayerStyle.HairIndex);
            Game1.player.accessory.Set(currentPlayerStyle.AccessoryIndex);
            Game1.player.skin.Set(currentPlayerStyle.SkinIndex);

            //Lastly, change the base, THIS NEEDS TO BE LAST OR ELSE IT WON'T LOAD THE PLAYER STYLE
            PackHelper.ChangePlayerBase(Game1.player.isMale, currentPlayerStyle.BaseIndex, currentPlayerStyle.FaceIndex, currentPlayerStyle.NoseIndex, currentPlayerStyle.ShoesIndex, currentPlayerStyle.IsBald);
        }
    }
}
