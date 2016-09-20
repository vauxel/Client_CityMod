// ============================================================
// Project          -      CityMod
// Description      -      Client Companion
// ============================================================
// Sections
//   1: GUI Profiles
//     1.1: Window Profiles
//     1.2: Text Profiles
//     1.3: Scroll Profiles
//     1.4: Check Box Profiles
//     1.5: Radio Button Profiles
//     1.6: Button Profiles
//     1.7: Border Profiles
//     1.8: Misc Profiles
//   2: Test Profiles
//   3: Core Code
//   4: Modules Initialization
// ============================================================

$CMClient::Version = 1;
$CMClient::ConnectedToCMServer = false;
$CMClient::Packages = "";

$CMClient::Prefs::AnimationSpeed = 1.25;

// ============================================================
// Section 1 - GUI Profiles
// ============================================================

// ------------------------------------------------------------
// Section 1.1 - Window Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMWindowProfile : BlockWindowProfile)
{
	fillColor = "249 249 249 255";
	fillColorHL = "249 249 249 255";
	fillColorNA = "249 249 249 255";

	fontType = "Calibri";
	fontSize = 18;
	fontColor = "249 249 249 255";
	textOffset = "5 4";
	justify = "left";

	bitmap = "./res/ui/citymodWindow_dark.png";
};

// ------------------------------------------------------------
// Section 1.2 - Text Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMTextEditProfile : GuiTextEditProfile)
{
	fontType = "Verdana";
	fontSize = 13;
	border = 0;
};

new GuiControlProfile(CMTextTinyProfile : GuiTextProfile)
{
	fontType = "Verdana";
	fontSize = 12;
	fontColor = "68 68 68 255";
};

new GuiControlProfile(CMTextTinyBoldProfile : CMTextTinyProfile) { fontType = "Verdana Bold"; };

new GuiControlProfile(CMTextSmallProfile : CMTextTinyProfile) { fontSize = 13; };
new GuiControlProfile(CMTextSmallBoldProfile : CMTextSmallProfile) { fontType = "Verdana Bold"; };
new GuiControlProfile(CMTextMediumProfile : CMTextTinyProfile) { fontSize = 14; };
new GuiControlProfile(CMTextMediumBoldProfile : CMTextMediumProfile) { fontType = "Verdana Bold"; };
new GuiControlProfile(CMTextLargeProfile : CMTextTinyProfile) { fontSize = 16; };
new GuiControlProfile(CMTextLargeBoldProfile : CMTextLargeProfile) { fontType = "Verdana Bold"; };
new GuiControlProfile(CMTextXLProfile : CMTextTinyProfile) { fontSize = 24; };
new GuiControlProfile(CMTextXLBoldProfile : CMTextXLProfile) { fontType = "Verdana Bold"; };

//new GuiControlProfile(CMTextTinyProfile : GuiTextProfile)
//{
//	fontType = "Calibri";
//	fontSize = 14;
//	fontColor = "68 68 68 255";
//};
//
//new GuiControlProfile(CMTextTinyBoldProfile : CMTextTinyProfile) { fontType = "Calibri Bold"; };
//
//new GuiControlProfile(CMTextSmallProfile : CMTextTinyProfile) { fontSize = 16; };
//new GuiControlProfile(CMTextSmallBoldProfile : CMTextSmallProfile) { fontType = "Calibri Bold"; };
//new GuiControlProfile(CMTextMediumProfile : CMTextTinyProfile) { fontSize = 18; };
//new GuiControlProfile(CMTextMediumBoldProfile : CMTextMediumProfile) { fontType = "Calibri Bold"; };
//new GuiControlProfile(CMTextLargeProfile : CMTextTinyProfile) { fontSize = 20; };
//new GuiControlProfile(CMTextLargeBoldProfile : CMTextLargeProfile) { fontType = "Calibri Bold"; };

new GuiControlProfile(CMChatTextProfile : GuiTextProfile)
{
	fontType = "Calibri";
	fontSize = 18;
	fontColor = "255 255 255 255";
};

new GuiControlProfile(CMChatTextEditProfile : GuiTextEditProfile)
{
	fontType = "Calibri";
	fontSize = 16;
	border = 0;

	fillColor = "20 20 20 60";
	fontColor = "255 255 255 255";
	fontOutlineColor = "0 0 0 255";
};

// ------------------------------------------------------------
// Section 1.3 - Scroll Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMScrollProfile : GuiScrollProfile)
{
	tab = "0";
	border = "0";
	canKeyFocus = "0";
	mouseOverSelected = "0";
	modal = "1";
	opaque = "0";
	fillColor = "255 255 255 0";
	fillColorHL = "200 200 200 255";
	fillColorNA = "200 200 200 255";
	fontType = "Verdana Bold";
	fontSize = "13";
	fontColors[0] = "0 0 0 255";
	fontColors[1] = "32 100 100 255";
	fontColors[2] = "0 0 0 255";
	fontColors[3] = "200 200 200 255";
	fontColors[4] = "0 0 204 255";
	fontColors[5] = "85 26 139 255";
	fontColors[6] = "0 0 0 0";
	fontColors[7] = "0 0 0 0";
	fontColors[8] = "0 0 0 0";
	fontColors[9] = "0 0 0 0";
	fontColor = "76 76 76 255";
	fontColorHL = "32 100 100 255";
	fontColorNA = "0 0 0 255";
	fontColorSEL = "200 200 200 255";
	fontColorLink = "0 0 204 255";
	fontColorLinkHL = "85 26 139 255";
	doFontOutline = "0";
	fontOutlineColor = "255 255 255 255";
	justify = "center";
	textOffset = "0 0";
	autoSizeWidth = "0";
	autoSizeHeight = "0";
	returnTab = "0";
	numbersOnly = "0";
	mouseOverSelected = "0";
	autoSizeWidth = "0";
	autoSizeHeight = "0";

	hasBitmapArray = "1";
	bitmap = "./res/ui/citymodScroll.png";
};

// ------------------------------------------------------------
// Section 1.4 - Check Box Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMCheckBoxProfile : GuiCheckBoxProfile)
{
	fontType = "Calibri";
	fontSize = 16;
	fontColor = "80 80 80 255";

	bitmap = "./res/ui/citymodCheckbox";
};

// ------------------------------------------------------------
// Section 1.5 - Radio Button Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMRadioButtonProfile : GuiRadioProfile)
{
	fontType = "Calibri";
	fontSize = 16;
	fontColor = "80 80 80 255";

	bitmap = "./res/ui/citymodRadio";
};

// ------------------------------------------------------------
// Section 1.6 - Button Profiles
// ------------------------------------------------------------
if(!isObject(BlankButtonProfile)) {
	new GuiControlProfile(BlankButtonProfile : BlockButtonProfile)
	{
		fontColor = "255 255 255 255";
	};
}

new GuiControlProfile(CMButtonProfile : GuiButtonProfile)
{
	fontType = "Verdana Bold";
	fontSize = "14";
	fontColor = "50 50 50 255";
	justify = "center";
};

new GuiControlProfile(CMButtonSmallProfile : CMButtonProfile) { fontSize = "12"; };
new GuiControlProfile(CMButtonColoredProfile : CMButtonProfile) { fontColor = "255 255 255 255"; };
new GuiControlProfile(CMButtonSmallColoredProfile : CMButtonColoredProfile) { fontSize = "12"; };

// ------------------------------------------------------------
// Section 1.7 - Border Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMBorderOneProfile : GuiBitmapBorderProfile)
{
	bitmap = "./res/ui/citymodBorder1";
};

new GuiControlProfile(CMBorderTwoProfile : GuiBitmapBorderProfile)
{
	bitmap = "./res/ui/citymodBorder2";
};

new GuiControlProfile(CMBorderThreeProfile : GuiBitmapBorderProfile)
{
	fillColor = "248 248 248 255";
	bitmap = "./res/ui/citymodBorder3";
};

// ------------------------------------------------------------
// Section 1.8 - Misc Profiles
// ------------------------------------------------------------
new GuiControlProfile(CMProgressProfile : GuiProgressProfile)
{
	borderColor = "248 248 248 255";
};

new GuiControlProfile(CMProgress2Profile : GuiProgressProfile)
{
	borderColor = "20 20 20 255";
	fillColor = "248 248 248 255";
};

new GuiControlProfile(CMProgressSliderProfile : GuiProgressProfile)
{
	borderColor = "0 0 0 0";
	fillColor = "20 20 20 120";
};

// ============================================================
// Section 2 - Test Profiles
// ============================================================

// ============================================================
// Section 3 - Core Code
// ============================================================

function clientcmdCM_errorMessage(%id, %message) {
	// TO-DO: Better error handling
	warn("[CMErrorMessage] - ID:" SPC %id SPC "-" SPC "Message:" SPC %message);
}

function clientcmdCM_authConfirmed() {
	echo(" - CityMod Client Authenticated");

	$CMClient::ConnectedToCMServer = true;
	loadCMClient();
}

function loadCMClient() {
	if(!$CMClient::ConnectedToCMServer) {
		warn("Can't load the CityMod Client whilst not connected to a CityMod Server");
		return;
	}

	// Load custom 'Loading Screen' background
	LOAD_MapPicture.setBitmap("Add-Ons/Client_CityMod/res/ui/loadingBG");

	// Activate necessary packages
	activatePackage(CityModClient_HUDHooks);
	activatePackage(CityModClient_ChatHooks);
	activatePackage(CityModClient_NotificationHooks);
	activatePackage(CityModClient_InfoPanelHooks);
	activatePackage(CityModClient_InventoryHooks);

	echo(" - CityMod Client Loaded");
}

function unloadCMClient() {
	if($CMClient::ConnectedToCMServer) {
		warn("Can't unload the CityMod Client while connected to a CityMod Server");
		return;
	}

	// Reset the custom 'Loading Screen' background
	LOAD_MapPicture.setBitmap("base/client/ui/loadingBG");

	// Cleanup objects
	CMClient_Cleanup.deleteAll();

	// Deactivate loaded CityMod packages
	deactivatePackage(CityModClient_InventoryHooks);
	deactivatePackage(CityModClient_InfoPanelHooks);
	deactivatePackage(CityModClient_NotificationHooks);
	deactivatePackage(CityModClient_ChatHooks);
	deactivatePackage(CityModClient_HUDHooks);

	echo(" - CityMod Client Unloaded");
}

function openCMGui(%name) {
	canvas.pushDialog("CM" @ %name @ "Gui");
}

function closeCMGui(%name) {
	canvas.popDialog("CM" @ %name @ "Gui");
	commandtoserver('CM_GUI_onClose');
}

function clientcmdCM_openGUI(%name) {
	openCMGui(%name);
}

function clientcmdCM_closeGUI(%name) {
	closeCMGui(%name);
}

package CityModClient_Core {
	function disconnectedCleanup() {
		if($CMClient::ConnectedToCMServer) {
			$CMClient::ConnectedToCMServer = false;
			unloadCMClient();
		}

		parent::disconnectedCleanup();
	}

	function GameConnection::setConnectArgs(%client, %lanname, %name, %e1, %e2, %e3, %rtb, %target) {
		%payload = "CITYMOD" SPC $CMClient::Version;
		parent::setConnectArgs(%client, %lanname, %name, %e1, %e2, %e3, %rtb, %payload);
	}
};

if(isPackage(CityModClient_Core))
	deactivatePackage(CityModClient_Core);
activatePackage(CityModClient_Core);

// ============================================================
// Section 4 - Modules Initialization
// ============================================================

if(!isObject(CMClient_Cleanup)) {
	new SimSet(CMClient_Cleanup);
}

exec("./lib/init.cs");
exec("./src/init.cs");