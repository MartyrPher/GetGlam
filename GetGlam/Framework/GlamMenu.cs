using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Menus;
using System;
using System.Collections.Generic;

namespace GetGlam.Framework
{
    /// <summary>Class that draws the custom menu and allows the player to change appearance.<summary>
    public class GlamMenu : CharacterCustomization
    {
        //Instance of ModEntry
        private ModEntry Entry;

        //The mods config
        private ModConfig Config;

        //Instance of ContentPackHelper
        private ContentPackHelper PackHelper;

        //Instance of DresserHandler
        private DresserHandler Dresser;

        //Instanc of PlayerLoader
        private CharacterLoader PlayerLoader;

        //Instance of Pet, used to check to disable the pet button
        private Pet Pet;

        //List of new left buttons added to the menu
        private List<ClickableTextureComponent> NewLeftButtonsList = new List<ClickableTextureComponent>();

        //List of new right buttons added to the menu
        private List<ClickableTextureComponent> NewRightButtonsList = new List<ClickableTextureComponent>();

        //List of new labels added to the menu
        private List<ClickableComponent> NewLabels = new List<ClickableComponent>();

        //Button for the Hat Hair Fix
        private ClickableTextureComponent HatCoversHairButton;

        //Label for the Hait Hair Fix
        private ClickableComponent HatFixLabel;

        //Whether the button is selected
        private bool IsHatFixSelected = false;

        //The index of the nose
        private int NoseIndex = 0;

        //The index of the face
        private int FaceIndex = 0;

        //The index of the base
        private int BaseIndex = 0;

        //The index of the base, there is always going to be 1 dresser
        private int DresserIndex = 1;

        //The indes of the shoe
        private int ShoeIndex = 0;

        //Whether the player is bald
        private bool IsBald = false;

        /// <summary>Glam Menu's Conrstructor</summary>
        /// <param name="entry">Instance of <see cref="ModEntry"/></param>
        /// <param name="packHelper">Instance of <see cref="ContentPackHelper"/></param>
        /// <param name="dresser">Instance of <see cref="DresserHandler"/></param>
        /// <param name="playerLoader">Instance of <seealso cref="CharacterLoader"/></param>
        public GlamMenu(ModEntry entry, ModConfig config, ContentPackHelper packHelper, DresserHandler dresser, CharacterLoader playerLoader)
            : base(Source.Wizard)
        {
            //Set the vars to the Instances
            Entry = entry;
            Config = config;
            PackHelper = packHelper;
            Dresser = dresser;
            PlayerLoader = playerLoader;

            //Grab the players pet
            Pet = Game1.getCharacterFromName<Pet>(Game1.player.getPetName(), false);

            //Check if they're wearing a hat
            if (Game1.player.hat.Value != null)
            {
                //Get the draw type value and change the HairFix button as selected
                if (Game1.player.hat.Value.hairDrawType.Get() == 0)
                    IsHatFixSelected = true;
            }

            //Set the positions of each item on the menu
            SetUpPositions();
        }

        /// <summary>Update the indexes on the menu when the player loads a layout</summary>
        /// <param name="baseindex">The base index</param>
        /// <param name="faceIndex">The face index</param>
        /// <param name="noseIndex">The nose index</param>
        /// <param name="shoeIndex">The shoe index</param>
        /// <param name="dresserIndex">The dresser index</param>
        /// <param name="isBald">Whether the player is bald</param>
        /// <remarks>This is only used when the player loads a layout from a json file</remarks>
        public void UpdateIndexes(int baseindex, int faceIndex, int noseIndex, int shoeIndex, int dresserIndex, bool isBald)
        {
            BaseIndex = baseindex;
            FaceIndex = faceIndex;
            NoseIndex = noseIndex;
            ShoeIndex = shoeIndex;
            DresserIndex = dresserIndex;
            IsBald = isBald;
        }

        /// <summary>Sets the position for all the UI elements in the menu</summary>
        private void SetUpPositions()
        {
            //Clear the lists incase of window size change
            NewLeftButtonsList.Clear();
            NewRightButtonsList.Clear();
            NewLabels.Clear();

            //Add all the new left buttons to the list
            NewLeftButtonsList.Add(new ClickableTextureComponent("LeftBase", new Rectangle(this.xPositionOnScreen + 44, this.yPositionOnScreen + 128, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 44, -1, -1), 1f, false));
            NewLeftButtonsList.Add(new ClickableTextureComponent("LeftFace", new Rectangle(this.xPositionOnScreen + 44, this.yPositionOnScreen + 400, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 44, -1, -1), 1f, false));
            NewLeftButtonsList.Add(new ClickableTextureComponent("LeftNose", new Rectangle(this.xPositionOnScreen + 44, this.yPositionOnScreen + 464, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 44, -1, -1), 1f, false));
            NewLeftButtonsList.Add(new ClickableTextureComponent("LeftShoe", new Rectangle(this.xPositionOnScreen + 44, this.yPositionOnScreen + 528, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 44, -1, -1), 1f, false));
            NewLeftButtonsList.Add(new ClickableTextureComponent("LeftDresser", new Rectangle(this.xPositionOnScreen + this.width / 2 - 114, this.yPositionOnScreen + 200, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 44, -1, -1), 1f, false));

            //Add all the new right buttons to the list
            NewRightButtonsList.Add(new ClickableTextureComponent("RightBase", new Rectangle(this.xPositionOnScreen + 170, this.yPositionOnScreen + 128, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 33, -1, -1), 1f, false));
            NewRightButtonsList.Add(new ClickableTextureComponent("RightFace", new Rectangle(this.xPositionOnScreen + 170, this.yPositionOnScreen + 400, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 33, -1, -1), 1f, false));
            NewRightButtonsList.Add(new ClickableTextureComponent("RightNose", new Rectangle(this.xPositionOnScreen + 170, this.yPositionOnScreen + 464, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 33, -1, -1), 1f, false));
            NewRightButtonsList.Add(new ClickableTextureComponent("RightShoe", new Rectangle(this.xPositionOnScreen + 170, this.yPositionOnScreen + 528, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 33, -1, -1), 1f, false));
            NewRightButtonsList.Add(new ClickableTextureComponent("RightDresser", new Rectangle(this.xPositionOnScreen + this.width / 2 + 48, this.yPositionOnScreen + 200, 64, 64), null, "", Game1.mouseCursors, Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 33, -1, -1), 1f, false));

            //Create hair fix button and add new labels to the list
            HatCoversHairButton = new ClickableTextureComponent("HatFix", new Rectangle(this.xPositionOnScreen + this.width / 2 - 114, this.yPositionOnScreen + 128, 36, 36), null, "Hat Hair Fix", Game1.mouseCursors, new Rectangle(227, 425, 9, 9), 4f, false);
            NewLabels.Add(new ClickableComponent(new Rectangle(NewLeftButtonsList[0].bounds.X + 70, NewLeftButtonsList[0].bounds.Y + 16, 1, 1), "Base", "Base"));
            NewLabels.Add(new ClickableComponent(new Rectangle(NewLeftButtonsList[1].bounds.X + 70, NewLeftButtonsList[1].bounds.Y + 16, 1, 1), "Face", "Face"));
            NewLabels.Add(new ClickableComponent(new Rectangle(NewLeftButtonsList[2].bounds.X + 70, NewLeftButtonsList[2].bounds.Y + 16, 1, 1), "Nose", "Nose"));
            NewLabels.Add(new ClickableComponent(new Rectangle(NewLeftButtonsList[3].bounds.X + 70, NewLeftButtonsList[3].bounds.Y + 16, 1, 1), "Shoe", "Shoe"));
            NewLabels.Add(new ClickableComponent(new Rectangle(NewLeftButtonsList[4].bounds.X + 70, NewLeftButtonsList[4].bounds.Y + 16, 1, 1), "Dresser", "Dresser"));
            HatFixLabel = new ClickableComponent(new Rectangle(HatCoversHairButton.bounds.X + 48, HatCoversHairButton.bounds.Y, 1, 1), "Hat Ignores Hair", "Hat Ignores Hair");

            //Change the bounds in the already created left selection button list
            foreach (ClickableTextureComponent buttonComponent in this.leftSelectionButtons)
            {
                if (buttonComponent.name.Contains("Dir"))
                {
                    buttonComponent.bounds.X -= 16;
                    buttonComponent.bounds.Y += 16;
                }
                else
                {
                    buttonComponent.bounds.X -= 30;
                    buttonComponent.bounds.Y -= 128;
                }
            }

            //Change the bounds in the already created right selection button list
            foreach (ClickableTextureComponent buttonComponent in this.rightSelectionButtons)
            {
                if (buttonComponent.name.Contains("Dir"))
                {
                    buttonComponent.bounds.X += 16;
                    buttonComponent.bounds.Y += 16;
                }
                else
                {
                    buttonComponent.bounds.X -= 30;
                    buttonComponent.bounds.Y -= 128;
                }
            }

            //Change the bounds and set color pickers for the labels
            foreach (ClickableComponent component in this.labels)
            {
                if (component.name.Contains("Color"))
                {
                    component.bounds.X += 230;
                    component.bounds.Y -= 64;

                    if (component.name.Contains("Eye"))
                    {
                        //Set the eye color picker to new instance and set the color
                        this.eyeColorPicker = new ColorPicker("Eyes", component.bounds.X, component.bounds.Y + 36);
                        this.eyeColorPicker.setColor(Game1.player.newEyeColor.Value);
                    }
                    else if (component.name.Contains("Hair"))
                    {
                        //Sets the hair color picker to new instance and set the color
                        component.bounds.Y += 64;
                        this.hairColorPicker = new ColorPicker("Hair", component.bounds.X, component.bounds.Y + 36);
                        this.hairColorPicker.setColor(Game1.player.hairstyleColor.Value);
                    }
                }
                else
                {
                    component.bounds.X -= 30;
                    component.bounds.Y -= 128;
                }
            }

            //Add male gender button
            this.genderButtons.Add(new ClickableTextureComponent(
                "Male",
                new Rectangle(xPositionOnScreen + this.width - IClickableMenu.spaceToClearSideBorder - IClickableMenu.borderWidth - 128, this.yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder, 64, 64),
                null,
                "Male",
                Game1.mouseCursors,
                new Rectangle(128, 192, 16, 16),
                4f,
                false)
            );

            //Add female gender button
            this.genderButtons.Add(new ClickableTextureComponent(
                "Female",
                new Rectangle(xPositionOnScreen + this.width - IClickableMenu.spaceToClearSideBorder - IClickableMenu.borderWidth - 64, this.yPositionOnScreen + IClickableMenu.borderWidth + IClickableMenu.spaceToClearTopBorder, 64, 64),
                null,
                "Female",
                Game1.mouseCursors,
                new Rectangle(144, 192, 16, 16),
                4f,
                false)
            );

            //disbale the random button
            this.randomButton.visible = false;

            //Change the bounds on the ok button
            this.okButton.bounds.Y += 8;
            this.okButton.bounds.X += 8;
        }

        /// <summary>Override to change the menu when the window size changes</summary>
        /// <param name="oldBounds">The old bounds</param>
        /// <param name="newBounds">The new bounds</param>
        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            //Call the base version and update the x and y position of the menu
            base.gameWindowSizeChanged(oldBounds, newBounds);
            this.xPositionOnScreen = Game1.viewport.Width / 2 - (632 + IClickableMenu.borderWidth * 2) / 2;
            this.yPositionOnScreen = Game1.viewport.Height / 2 - (600 + IClickableMenu.borderWidth * 2) / 2 - 64;

            //Set up the UI elements again
            SetUpPositions();
        }

        /// <summary>Override that checks if the mouse is above a certain elememt</summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        public override void performHoverAction(int x, int y)
        {
            //Call the base version
            base.performHoverAction(x, y);

            //Change the scale of the new left buttons
            foreach (ClickableTextureComponent leftButton in NewLeftButtonsList)
            {
                if (leftButton.containsPoint(x, y))
                    leftButton.scale = Math.Min(leftButton.scale + 0.02f, leftButton.baseScale + 0.1f);
                else
                    leftButton.scale = Math.Max(leftButton.scale - 0.02f, leftButton.baseScale);
            }

            //Change the scale of the new right buttons
            foreach (ClickableTextureComponent rightButton in NewRightButtonsList)
            {
                if (rightButton.containsPoint(x, y))
                    rightButton.scale = Math.Min(rightButton.scale + 0.02f, rightButton.baseScale + 0.1f);
                else
                    rightButton.scale = Math.Max(rightButton.scale - 0.02f, rightButton.baseScale);
            }

            //Change the scale of the gender buttons
            foreach (ClickableTextureComponent genderButton in this.genderButtons)
            {
                if (genderButton.containsPoint(x, y))
                    genderButton.scale = Math.Min(genderButton.scale + 0.05f, genderButton.baseScale + 0.5f);
                else
                    genderButton.scale = Math.Min(genderButton.scale - 0.05f, genderButton.baseScale);
            }
        }

        /// <summary>Override that handles recieving a left click</summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <param name="playSound">Whether to play the sounds</param>
        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            //Call the base version
            base.receiveLeftClick(x, y, playSound);

            //Check if the Hat Hair fix button is pressed and change accordingly
            if (HatCoversHairButton.bounds.Contains(x, y) && !IsHatFixSelected)
            {
                IsHatFixSelected = true;
                Game1.player.hat.Value.hairDrawType.Set(0);
                HatCoversHairButton.sourceRect.X = 236;
            }
            else if (HatCoversHairButton.bounds.Contains(x, y) && IsHatFixSelected)
            {
                IsHatFixSelected = false;
                Game1.player.hat.Value.hairDrawType.Set(1);
                HatCoversHairButton.sourceRect.X = 227;
            }

            //Check if any of the new left buttons were clicked
            foreach (ClickableTextureComponent component in NewLeftButtonsList)
            {
                if (component.bounds.Contains(x, y))
                {
                    SelectionClick(component.name, -1);
                    if (component.scale != 0f)
                    {
                        component.scale -= 0.25f;
                        component.scale = Math.Max(0.75f, component.scale);
                    }
                    Game1.playSound("grassyStep");
                }
            }

            //Check if any of the right buttons were clicked
            foreach (ClickableTextureComponent component in NewRightButtonsList)
            {
                if (component.bounds.Contains(x, y))
                {
                    SelectionClick(component.name, 1);
                    if (component.scale != 0f)
                    {
                        component.scale -= 0.25f;
                        component.scale = Math.Max(0.75f, component.scale);
                    }
                    Game1.playSound("grassyStep");
                }
            }

            //Check if any of the gender buttons are clicked
            foreach (ClickableTextureComponent genderButton in this.genderButtons)
            {
                if (genderButton.containsPoint(x, y))
                {
                    SelectionClick(genderButton.name, 0);
                    Game1.playSound("yoba");
                }
            }

            //Check if the left Hair style button is pressed, used to calculate baldness
            foreach (ClickableTextureComponent hairButton in this.leftSelectionButtons)
            {
                if (hairButton.containsPoint(x, y) && hairButton.name.Contains("Hair"))
                {
                    if (Game1.player.FarmerRenderer.textureName.Value.Contains("bald"))
                    {
                        IsBald = true;
                        PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    }
                    else if (IsBald && !Game1.player.FarmerRenderer.textureName.Value.Contains("bald"))
                    {
                        IsBald = false;
                        PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    }
                }
            }

            //Chech if the right Hair style button is pressed, used to calculate baldness
            foreach (ClickableTextureComponent hairButton in this.rightSelectionButtons)
            {
                if (hairButton.containsPoint(x, y) && hairButton.name.Contains("Hair"))
                {
                    if (Game1.player.FarmerRenderer.textureName.Value.Contains("bald"))
                    {
                        IsBald = true;
                        PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    }
                    else if (IsBald && !Game1.player.FarmerRenderer.textureName.Value.Contains("bald"))
                    {
                        IsBald = false;
                        PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    }
                }
            }

            //Check the okbutton again to save the player when the menu closes
            if (this.okButton.containsPoint(x, y))
                PlayerLoader.SaveCharacterLayout(Game1.player.isMale, BaseIndex, Game1.player.skin.Value, Game1.player.hair.Value, FaceIndex, NoseIndex, ShoeIndex, Game1.player.accessory.Value, DresserIndex, IsBald);
        }

        /// <summary>Update the buttons for changing the face and nose.</summary>
        /// <param name="isFaceAndNoseDrawing">Wether the face and nose buttons are drawing</param>
        private void UpdateFaceAndNoseButtonsPositions(bool isFaceAndNoseDrawing)
        {
            if (isFaceAndNoseDrawing)
            {
                NewLeftButtonsList[1].visible = true;
                NewLeftButtonsList[2].visible = true;
                NewRightButtonsList[1].visible = true;
                NewRightButtonsList[2].visible = true;

                NewLeftButtonsList[3].bounds.Y = this.yPositionOnScreen + 528;
                NewRightButtonsList[3].bounds.Y = this.yPositionOnScreen + 528;
                NewLabels[3].bounds.Y = NewLeftButtonsList[3].bounds.Y + 16;
            }
            else
            {
                NewLeftButtonsList[1].visible = false;
                NewLeftButtonsList[2].visible = false;
                NewRightButtonsList[1].visible = false;
                NewRightButtonsList[2].visible = false;

                NewLeftButtonsList[3].bounds.Y = this.yPositionOnScreen + 400;
                NewRightButtonsList[3].bounds.Y = this.yPositionOnScreen + 400;
                NewLabels[3].bounds.Y = NewLeftButtonsList[3].bounds.Y + 16;
            }
        }

        /// <summary>Handles which index to move and by what direction</summary>
        /// <param name="name">The name of the button</param>
        /// <param name="direction">Which direction to move the indexes</param>
        private void SelectionClick(string name, int direction)
        {
            //Switch statement of the different button names
            switch (name)
            {
                case "LeftBase":
                    if (BaseIndex + direction > -1)
                        BaseIndex += direction;
                    else
                        BaseIndex = Game1.player.isMale ? PackHelper.MaleBaseTextureList.Count : PackHelper.FemaleBaseTextureList.Count;

                    FaceIndex = 0;
                    NoseIndex = 0;
                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "RightBase":
                    if (BaseIndex + direction > (Game1.player.isMale ? PackHelper.MaleBaseTextureList.Count : PackHelper.FemaleBaseTextureList.Count))
                        BaseIndex = 0;
                    else
                        BaseIndex += direction;

                    FaceIndex = 0;
                    NoseIndex = 0;
                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "LeftNose":
                    if (NoseIndex + direction > -1)
                        NoseIndex += direction;
                    else
                        NoseIndex = PackHelper.GetNumberOfFacesAndNoses(Game1.player.isMale, BaseIndex, false);

                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "RightNose":
                    if (NoseIndex + direction > PackHelper.GetNumberOfFacesAndNoses(Game1.player.isMale, BaseIndex, false))
                        NoseIndex = 0;
                    else
                        NoseIndex += direction;

                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "LeftFace":
                    if (FaceIndex + direction > -1)
                        FaceIndex += direction;
                    else
                        FaceIndex = PackHelper.GetNumberOfFacesAndNoses(Game1.player.isMale, BaseIndex, true);

                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "RightFace":
                    if (FaceIndex + direction > PackHelper.GetNumberOfFacesAndNoses(Game1.player.isMale, BaseIndex, true))
                        FaceIndex = 0;
                    else
                        FaceIndex += direction;

                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "LeftShoe":
                    if (ShoeIndex + direction > -1)
                        ShoeIndex += direction;
                    else
                        ShoeIndex = Game1.player.isMale ? PackHelper.MaleShoeTextureList.Count: PackHelper.FemaleShoeTextureList.Count;

                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "RightShoe":
                    if (ShoeIndex + direction > (Game1.player.isMale ? PackHelper.MaleShoeTextureList.Count : PackHelper.FemaleShoeTextureList.Count))
                        ShoeIndex = 0;
                    else
                        ShoeIndex += direction;

                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "LeftDresser":
                    if (DresserIndex + direction > 0)
                        DresserIndex += direction;
                    else
                        DresserIndex = Dresser.GetNumberOfDressers();

                    //Change the Dressers source rect and update the dresser in the farmhouse
                    Dresser.TextureSourceRect.Y = DresserIndex.Equals(1) ? 0 : DresserIndex * 32 - 32;
                    Dresser.SetDresserTileSheetPoint(DresserIndex);
                    Dresser.UpdateDresserInFarmHouse();
                    break;
                case "RightDresser":
                    if (DresserIndex + direction <= Dresser.GetNumberOfDressers())
                        DresserIndex += direction;
                    else
                        DresserIndex = 1;

                    //Change the Dressers source rect and update the dresser in the farmhouse
                    Dresser.TextureSourceRect.Y = DresserIndex.Equals(1) ? 0 : DresserIndex * 32- 32; 
                    Dresser.SetDresserTileSheetPoint(DresserIndex);
                    Dresser.UpdateDresserInFarmHouse();
                    break;
                case "Male":
                    Game1.player.changeGender(true);

                    //Reset the BaseIndex, ShoeIndex to prevent crashing
                    BaseIndex = 0;
                    ShoeIndex = 0;
                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
                case "Female":
                    Game1.player.changeGender(false);
                    
                    //Reset the BaseIndex, ShoeIndex to prevent crashing
                    BaseIndex = 0;
                    ShoeIndex = 0;
                    PackHelper.ChangePlayerBase(Game1.player.isMale, BaseIndex, FaceIndex, NoseIndex, ShoeIndex, IsBald);
                    break;
            }
        }

        /// <summary>Override to draw the different menu parts</summary>
        /// <param name="b">The games spritebatch</param>
        public override void draw(SpriteBatch b)
        {
            //bools whether to draw Base and Dresser buttons
            bool shouldDrawBaseButtons = false;
            bool shouldDrawDresserButtons = false;
            bool shouldDrawNosesAndFaceButtons = false;

            //Draw the dialogue box or else Minerva will haunt my dreams
            Game1.drawDialogueBox(this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, false, true);

            //Draw the Dresser Texture if the Config is true
            if (Config.DrawDresserInMenu)
                b.Draw(Dresser.Texture, new Vector2(this.xPositionOnScreen + this.width / 2 - 96, this.yPositionOnScreen + this.height / 2 - 80), Dresser.TextureSourceRect, Color.White, 0f, Vector2.Zero, 12f, SpriteEffects.None, 0.86f);
            else
            {
                b.Draw(Game1.daybg, new Vector2(this.xPositionOnScreen + this.width / 2 - 64, this.yPositionOnScreen + this.height / 2 - 64), Color.White);
                if (shouldDrawDresserButtons)
                    b.Draw(Dresser.Texture, new Vector2(NewRightButtonsList[4].bounds.X + 64, NewRightButtonsList[4].bounds.Y), Dresser.TextureSourceRect, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.86f);
            }

            //Draw the Farmer!!!
            Game1.player.FarmerRenderer.draw(b, Game1.player.FarmerSprite.CurrentAnimationFrame, Game1.player.FarmerSprite.CurrentFrame, Game1.player.FarmerSprite.SourceRect, new Vector2((this.xPositionOnScreen + this.width / 2 - 32), (this.yPositionOnScreen + this.height / 2 - 32)), Vector2.Zero, 0.8f, Color.White, 0f, 1f, Game1.player);

            //Check if the menu should draw the base buttons if the base count is greater than 1
            if (Game1.player.isMale && PackHelper.MaleBaseTextureList.Count > 0)
                shouldDrawBaseButtons = true;
            else if (PackHelper.FemaleBaseTextureList.Count > 0)
                shouldDrawBaseButtons = true;

            //Check if the menu should draw the dresser buttons
            if (Dresser.GetNumberOfDressers() > 1)
                shouldDrawDresserButtons = true;

            if (PackHelper.GetNumberOfFacesAndNoses(Game1.player.isMale, BaseIndex, true) > 0)
            {
                shouldDrawNosesAndFaceButtons = true;
                UpdateFaceAndNoseButtonsPositions(shouldDrawNosesAndFaceButtons);
            }
            else
            {
                shouldDrawNosesAndFaceButtons = false;
                UpdateFaceAndNoseButtonsPositions(shouldDrawNosesAndFaceButtons);
            }

            //Draw each of the left selection buttons
            foreach (ClickableTextureComponent component in this.leftSelectionButtons)
            {
                if(!component.name.Contains("Pet"))
                    component.draw(b);
            }
            
            //Draw each of the right selection buttons
            foreach (ClickableTextureComponent component in this.rightSelectionButtons)
            {
                if(!component.name.Contains("Pet"))
                    component.draw(b);
            }

            //Draw each of the labels
            foreach (ClickableComponent component in this.labels)
            {
                //Check if Pet is null, this will crash the game if this isn't here
                if (Pet is null)
                {
                    if (!component.name.Contains("Fav") && !component.name.Contains("Name") && !component.name.Contains("Farm") && !component.name.Contains("animal"))
                        Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                }
                else
                {
                    if (!component.name.Contains("Fav") && !component.name.Contains("Name") && !component.name.Contains("Farm") && !component.name.Contains("animal") && !component.name.Contains(Pet.Name))
                        Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                }

                //Only draw the needed labels
                if (component.name.Contains("Hair") && !component.name.Contains("Color"))
                    Utility.drawTextWithShadow(b, Game1.player.hair.Value.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);
                else if (component.name.Contains("Acc"))
                    Utility.drawTextWithShadow(b, Game1.player.accessory.Value == -1 ? "NA" : Game1.player.accessory.Value.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);
                else if (component.name.Contains("Skin"))
                    Utility.drawTextWithShadow(b, Game1.player.skin.Value.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);

            }

            //Draw the eye color picker and hair color picker
            this.eyeColorPicker.draw(b);
            this.hairColorPicker.draw(b);

            //Check if the player is wearing a hat
            if (Game1.player.hat.Value != null)
            {
                HatCoversHairButton.draw(b);
                Utility.drawTextWithShadow(b, HatFixLabel.name, Game1.smallFont, new Vector2(HatFixLabel.bounds.X, HatFixLabel.bounds.Y), Game1.textColor);

                if (IsHatFixSelected)
                    HatCoversHairButton.sourceRect.X = 236;
            }

            //Draw each of the new left buttons
            foreach (ClickableTextureComponent component in NewLeftButtonsList)
            {
                if (component.name.Contains("Base") && shouldDrawBaseButtons)
                    component.draw(b);
                else if (component.name.Contains("Dresser") && shouldDrawDresserButtons)
                    component.draw(b);
                else if ((component.name.Contains("Nose") || component.name.Contains("Face") && shouldDrawNosesAndFaceButtons))
                    component.draw(b);
                else if (!component.name.Contains("Dresser") && !component.name.Contains("Base") && !component.name.Contains("Face") && !component.name.Contains("Nose"))
                    component.draw(b);
            }

            //Draw each of the new right buttons
            foreach (ClickableTextureComponent component in NewRightButtonsList)
            {
                if (component.name.Contains("Base") && shouldDrawBaseButtons)
                    component.draw(b);
                else if (component.name.Contains("Dresser") && shouldDrawDresserButtons)
                    component.draw(b);
                else if ((component.name.Contains("Nose") || component.name.Contains("Face") && shouldDrawNosesAndFaceButtons))
                    component.draw(b);
                else if (!component.name.Contains("Dresser") && !component.name.Contains("Base") && !component.name.Contains("Face") && !component.name.Contains("Nose"))
                    component.draw(b);
            }

            //Draw each of the new labels, I hate this tbh, this might needs to be done a different way
            foreach (ClickableComponent component in NewLabels)
            {
                if (component.name.Equals("Base") && shouldDrawBaseButtons)
                {
                    Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                    Utility.drawTextWithShadow(b, BaseIndex.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);
                }
                else if (component.name.Equals("Face") && shouldDrawNosesAndFaceButtons)
                {
                    Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                    Utility.drawTextWithShadow(b, FaceIndex.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);
                }
                else if (component.name.Equals("Nose") && shouldDrawNosesAndFaceButtons)
                {
                    Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                    Utility.drawTextWithShadow(b, NoseIndex.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);
                }
                else if (component.name.Equals("Shoe"))
                {
                    Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                    Utility.drawTextWithShadow(b, ShoeIndex.ToString(), Game1.smallFont, new Vector2(component.bounds.X + 16, component.bounds.Y + 32), Game1.textColor);
                }
                else if (component.name.Equals("Dresser") && shouldDrawDresserButtons)
                {
                    Utility.drawTextWithShadow(b, component.name, Game1.smallFont, new Vector2(component.bounds.X, component.bounds.Y), Game1.textColor);
                }
            }

            //Draw the gender buttons
            foreach (ClickableTextureComponent component in this.genderButtons)
            {
                component.draw(b);
                if (component.name.Equals("Male") && Game1.player.isMale || (component.name.Equals("Female") && !Game1.player.isMale))
                    b.Draw(Game1.mouseCursors, component.bounds, new Rectangle?(Game1.getSourceRectForStandardTileSheet(Game1.mouseCursors, 34, -1, -1)), Color.White);
            }

            //Draw the ok button
            this.okButton.draw(b);

            //Lastly, draw the mouse if they're not using the hardware cursor
            if (Game1.activeClickableMenu == this && !Game1.options.hardwareCursor)
                base.drawMouse(b);
        }
    }
}
