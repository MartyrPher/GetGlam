# GetGlam
Get Glam is a Stardew Valley mod that allows different player customizations to be added to the game, based off of GetDressed/Kisekae.

## Contents
* [Install](#install)
* [Features](#features)
	* [Overview](#overview)
	* [Accessories](#accessories)
	* [Bases](#bases)
	* [Dresser](#dresser)
	* [Faces and Noses](#faceandnose)
	* [Hairstyles](#hairstyles)
	* [Shoes](#shoes)

## Install
1. [Install the latest version of SMAPI](https://smapi.io/).
2. Install the mod from [Nexus]("Nexus").
3. Unzip any Get Glam content packs into the `Mods` folder to intstall them.
4. Run the game using SMAPI.

## Features
### Overview
Get Glam is an rewritten GetDressed/Kisekae for Stardew Valley 1.4.
The base mod includes the dresser and all the face and nose options from Get Dressed.
This mod differs from previous iterations since it adds content pack support.

There are six folders that you are likely to see when downloading a Get Glam content pack:
* Accessories
* Base
* Dresser
* FaceAndNose
* Hairstyles
* Shoes

Each of these subfolders will contain `.png` images and one `.json` in the `FaceAndNose` folder.

### Accessories
The player can wear accessories including facial hair and earnings, all contained within one image.

To add accessories to a content pack you need to:
* Create a folder in the pack named `Accessories`.
* Add in the custom image named `accessories.png` to the newly created `Accessories` folder. Size: 128xNumberOfAccessories

Currently, Get Glam only supports one `accessories.png`.

### Bases
The player can swap between farmer bases that content packs provide.

To add a base to a content pack you need to:
* Create a folder in the pack named `Base`.
* Add in the custom image `farmer_base.png` for the male farmers base to the `Base` folder.
* Add in the custom image `farmer_base_bald.png` for the male farmers bald base to the `Base` folder.
* Add in the custom image `farmer_girl_base.png` for the female farmers base to the `Base` folder.
* Add in the custom image `farmer_girl_base_bald.png` for the female farmers bald base to the `Base` folder.

The bald option NEEDS to be added to the `Base` folder in order for the content pack to work correctly with the mod.
Currently, Get Glam only supports one type of base in a content pack.

### Dresser
The dresser comes as the default from Get Dressed/Kisekae and can be changed with content packs.

To add dressers to a content pack you need to:
* Create a folder in the pack named `Dresser`.
* Add in the custom dresser image `dresser.png` to the newly created `Dresser` folder. Dresser Size: 16x32, `dresser.png` Size: 16xNumberOfDressers

Currently, Get Glam only supports one `dresser.png`.

### Faces and Noses
Get Glam adds the option for the farmer to be able to swap between faces and noses for a particular base.

To add faces and noses for your base, you need to:
* Create a folder in the pack named `FaceAndNose`.
* Add in the custom face and nose images `"gender"_face"FaceNumber"_nose"NoseNumber".png` to the `FaceAndNose` folder. Size: 96x672
* The naming convention for the Face and Nose number need to be sequential. Ex: First face and nose needs to be named `male_face0_nose0.png`, second `male_face0_nose1.png`...and so on.
* Add in a `count.json` to the `FaceAndNose` folder.

field			      | purpose
----------------------|--------
`NumberOfMaleFaces`   | The number of male faces added by the content pack.
`NumerOfMaleNoses`    | The number of male noses added by the content pack.
`NumerOfFemaleFaces`  | The number of female faces added by the content pack.
`NumerOfFemaleNoses`  | The number of female noses added by the content pack.

### Hairstyles
The player can change their hairstyle, all contained within one image.

To add hair to a content pack you need to:
* Create a folder in the pack named `Hairstyles`.
* Add in the custom image named `hairstyles.png` to the newly created `Hairstyles` folder. Size:128xNumberOfHairstyles

Currently, Get Glam only supports one `hairstyles.png`.

### Shoes
The player can change their shoes for any base provided with content packs.

To add shoes to a content pack you need to:
* Create a folder in the pack named `Shoes`.
* Add in the custom images named `"gender"_shoes{ShoeNumber}.png` to the newly created `Shoes` folder. Size: 92x672
* Note: The naming convention for shoes need to be sequential. EX: First show needs to be named `male_shoes0.png`, second `male_shoes1.png`...and so on.