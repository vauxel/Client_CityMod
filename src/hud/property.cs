// ============================================================
// Project          -      CityMod
// Description      -      Property Info HUD Code
// ============================================================
// Sections
//   1: Functions
//   2: Hooks
// ============================================================

// ============================================================
// Section 1 - Functions
// ============================================================

function clientcmdCM_Property_closePropertyInfo() {
	if(CMClient_HUD.property.inProperty) {
		CMClient_HUD.property.setInfoVisibility(false);
	}
}

function clientcmdCM_Property_updatePropertyInfo(%name, %owner) {
	if(!strLen(%name) || !strLen(%owner)) {
		return;
	}

	CMClient_HUD.property.updateInformation(%name, %owner);

	if(!CMClient_HUD.property.inProperty) {
		CMClient_HUD.property.setInfoVisibility(true);
	}
}

function CityModClientHUDProperty::updateInformation(%this, %name, %owner) {
	%this.propertyInfo["name"] = %name;
	%this.propertyInfo["owner"] = %owner;

	%this.gui.child("name").setValue("<just:right><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>" @ %name);
	%this.gui.child("owner").setValue("<just:right><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>" @ %owner);
}

function CityModClientHUDProperty::setInfoVisibility(%this, %value) {
	if((%value != true) && (%value != false)) {
		return;
	}

	%this.inProperty = %value;
	CMClient_HUD.setComponentShown("property", %value, "top", "easeOutExpo");
}

// ============================================================
// Section 2 - Hooks
// ============================================================

package CityModClient_NotificationHooks {
	function CMClient_HUD::onInitialize(%this) {
		parent::onInitialize(%this);

		%this.addComponent("property", new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "left";
			vertSizing = "bottom";
			position = (%this.gui.getExtentW() - 262) SPC "0";
			extent = "262 37";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "20 20 20 60";

			new GuiMLTextCtrl("_name") {
				profile = "GuiMLTextProfile";
				horizSizing = "left";
				vertSizing = "bottom";
				position = "48 1";
				extent = "210 18";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:right><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>Generic Property";
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiMLTextCtrl("_owner") {
				profile = "GuiMLTextProfile";
				horizSizing = "left";
				vertSizing = "bottom";
				position = "48 18";
				extent = "210 16";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:right><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>Blockhead";
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiSwatchCtrl("_propertyInfo") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "3 3";
				extent = "40 31";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 120";

				new GuiBitmapButtonCtrl("_button") {
					profile = "BlankButtonProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "40 31";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					command = "openCMGui(\"PropertyInfo\");";
					text = "Info";
					groupNum = "-1";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_CityMod/res/ui/blankButton/blank";
					lockAspectRatio = "0";
					alignLeft = "0";
					alignTop = "0";
					overflowImage = "0";
					mKeepCached = "0";
					mColor = "255 255 255 255";
				};
			};
		});

		%this.property.inProperty = false;
		%this.property.propertyInfo["name"] = "Generic Property";
		%this.property.propertyInfo["owner"] = "Blockhead";
	}

	function CMClient_HUD::onResized(%this) {
		parent::onResized(%this);

		%this.gui["property"].setPosition(%this.gui.getExtentW() - %this.gui["property"].getExtentW(), 0 - %this.gui["property"].getExtentH());

		if(%this.property.inProperty) {
			%this.setComponentShown("property", true, "top", "easeOutExpo");
		}
	}
};