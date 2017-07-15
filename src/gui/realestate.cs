// ============================================================
// Project          -      CityMod
// Description      -      Real Estate GUI Code
// ============================================================
// Sections
//   1: Client Commands
//   2: Gui Methods
// ============================================================

exec("Add-Ons/Client_CityMod/res/gui/CMRealEstate.gui");

// ============================================================
// Section 1 - Client Commands
// ============================================================
function clientcmdCM_RealEstate_clearProperties() {
	CMRealEstateGui_propertiesList.deleteAll();
	CMRealEstateGui_propertiesList.setExtentH(1);
}

function clientcmdCM_RealEstate_addProperty(%id, %type, %name, %size, %price, %description, %owner, %bricks) {
	if(!strLen(%id) || !strLen(%type) || !strLen(%name) || !strLen(%size) || !strLen(%price) || !strLen(%description) || !strLen(%owner) || !strLen(%bricks)) {
		return;
	}

	%listGUI = CMRealEstateGui_propertiesList;

	if(%listGUI.getCount() < 1) {
		%xpos = 0;
		%ypos = 0;
		%listGUI.setExtentH(134);
	} else if(%listGUI.getCount() % 3 == 0) {
		%xpos = 0;
		%ypos = %listGUI.getObject(%listGUI.getCount() - 1).getPositionY() + 134 + %listGUI.listSpacing;
		%listGUI.setExtentH(%listGUI.getExtentH() + %listGUI.listSpacing + 134);
	} else {
		%xpos = %listGUI.getObject(%listGUI.getCount() - 1).getPositionX() + 134 + %listGUI.listSpacing;
		%ypos = %listGUI.getObject(%listGUI.getCount() - 1).getPositionY();
	}

	%gui = new GuiBitmapBorderCtrl("_property" @ %id) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = %xpos SPC %ypos;
		extent = "134 134";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		propertyID = %id;
		propertyType = %type;
		propertyName = %name;
		propertySize = %size;
		propertyPrice = %price;
		propertyDescription = %description;
		propertyOwner = %owner;
		propertyBricks = %bricks;

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "134 134";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";
		};

		new GuiBitmapCtrl("_type") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "35 5";
			extent = "64 64";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/realestate/" @ strLwr(%type);
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 71";
			extent = "134 6";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/ui/dividerHorizontal";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiMLTextCtrl("_size") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "4 77";
			extent = "126 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %size;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_price") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "4 77";
			extent = "126 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:right>$" @ readableAmount(%price, 100000);
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 92";
			extent = "134 6";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/ui/dividerHorizontal";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiMLTextCtrl("_name") {
			profile = "CMTextSmallProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "4 100";
			extent = "126 26";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center>" @ %name;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMouseEventCtrl("CMPropertiesMouseEventCtrl") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "134 134";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lockMouse = "0";
		};
	};

	%listGUI.add(%gui);

	if(CMRealEstateGui_noPropertiesText.isVisible()) {
		CMRealEstateGui_noPropertiesText.setVisible(0);
	}
}

function clientcmdCM_RealEstate_addPlayerProperty(%id, %type, %name, %size, %forsale, %price, %description, %bricks) {
	if(!strLen(%id) || !strLen(%type) || !strLen(%name) || !strLen(%size) || !strLen(%forsale) || !strLen(%price) || !strLen(%description) || !strLen(%bricks)) {
		return;
	}

	%listGUI = CMOrganizationsGui_jobsList;

	%gui = new GuiBitmapBorderCtrl("_property" @ %id) {
		profile = "CMBorderOneProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "205 64";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "205 64";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "255 255 255 255";
		};

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderOneProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "63 42";
			extent = "141 24";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderOneProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "64 64";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapCtrl("_type") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "8 8";
			extent = "48 48";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/realestate/" @ strLwr(%type);
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiMLTextCtrl("_name") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "69 8";
			extent = "131 26";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center>" @ %name;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_status") {
			profile = "CMTextSmallProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "69 45";
			extent = "131 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center>" @ (%forsale ? ("For Sale (" @ readableAmount(%price, 100000) @ ")") : "Not For Sale");
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMouseEventCtrl("CMPlayerPropertiesMouseEventCtrl") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "205 64";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lockMouse = "0";
		};
	};

	%listGUI.addListGuiObject(%gui);
}

function clientcmdCM_RealEstate_closeTransfer() {
	CMRealEstateGui_transferProperty.setVisible(0);
}

function clientcmdCM_RealEstate_closePropertyInfo() {
	CMRealEstateGui_propertyInfo.setVisible(0);
}

function clientcmdCM_RealEstate_closePlayerProperties() {
	CMRealEstateGui_playerProperties.setVisible(0);
}

function clientcmdCM_RealEstate_openPlayerProperties() {
	CMRealEstateGui.viewPlayerProperties();
}

// ============================================================
// Section 2 - Gui Methods
// ============================================================
function CMRealEstateGui::onWake(%this) {
	CMRealEstateGui_propertyInfo.setVisible(0);
	CMRealEstateGui_playerProperties.setVisible(0);
	clientcmdCM_RealEstate_clearProperties();
	CMRealEstateGui_noPropertiesText.setVisible(1);

	commandToServer('CM_RealEstate_requestProperties');
}

function CMRealEstateGui::viewPropertyInfo(%this, %property) {
	CMRealEstateGui_propertyInfo.propertyID = %property.propertyID;

	CMRealEstateGui_propertyInfoType.setBitmap("Add-Ons/Client_CityMod/res/gui/icons/realestate/" @ strLwr(%property.propertyType));
	CMRealEstateGui_propertyInfoName.setValue("<just:center>" @ %property.propertyName);
	CMRealEstateGui_propertyInfoSize.setValue("<just:center>" @ %property.propertySize);
	CMRealEstateGui_propertyInfoPrice.setValue("<just:right>" @ readableAmount(%property.propertyPrice, 100000));
	CMRealEstateGui_propertyInfoOwner.setValue("<just:right>" @ %property.propertyOwner);
	CMRealEstateGui_propertyInfoBricks.setValue("<just:right>" @ commaSeparateAmount(%property.propertyBricks));
	CMRealEstateGui_propertyInfoDescription.setValue(%property.propertyDescription);

	CMRealEstateGui_propertyInfo.setVisible(1);
}

function CMRealEstateGui::transferPropertyFrontend(%this) {
	CMRealEstateGui_transferProperty.propertyID = CMRealEstateGui_propertyInfo.propertyID;

	CMRealEstateGui_transferPropertyRecipient.setValue("Enter a recipient..");
	CMRealEstateGui_transferPropertyType1.setValue(1);
	CMRealEstateGui_transferPropertyType2.setValue(0);

	CMRealEstateGui_transferProperty.setVisible(1);
}

function CMRealEstateGui::transferProperty(%this) {
	commandToServer('CM_RealEstate_transferProperty', CMRealEstateGui_transferProperty.propertyID, CMRealEstateGui_transferPropertyRecipient.getValue(), (CMRealEstateGui_transferPropertyType1.getValue() ? "player" : "organization"));
}

function CMRealEstateGui::purchaseProperty(%this) {
	commandToServer('CM_RealEstate_purchaseProperty', CMRealEstateGui_propertyInfo.propertyID);
}

function CMRealEstateGui::viewPlayerProperties(%this) {
	CMRealEstateGui_playerProperties.setVisible(1);
	clientcmdCM_RealEstate_clearPlayerProperties();

	commandToServer('CM_RealEstate_requestPlayerProperties');
}

function CMRealEstateGui::showPlayerPropertyInfo(%this, %property) {
	CMRealEstateGui_playerPropertyInfo.propertyID = %property.propertyID;

	CMRealEstateGui_playerPropertyInfo.child("type").setBitmap("Add-Ons/Client_CityMod/res/gui/icons/realestate/" @ strLwr(%property.propertyType));
	CMRealEstateGui_playerPropertyInfo.child("status").setValue("<just:center>Property owned by" NL %property.propertyOwner);
	CMRealEstateGui_playerPropertyInfoName.setValue(%property.propertyName);
	CMRealEstateGui_playerPropertyInfoDescription.setValue(%property.propertyDescription);
	CMRealEstateGui_playerPropertyInfoPrice.setValue(%property.propertyPrice);
	CMRealEstateGui_playerPropertyInfoSize.setValue(%property.propertySize);
	CMRealEstateGui_playerPropertyInfoBricks.setValue("<just:right>" @ commaSeparateAmount(%property.propertyBricks));

	if(%property.propertyListed) {
		CMRealEstateGui_playerPropertyInfoListedTrue.setValue(1);
		CMRealEstateGui_playerPropertyInfoListedFalse.setValue(0);
	} else {
		CMRealEstateGui_playerPropertyInfoListedTrue.setValue(0);
		CMRealEstateGui_playerPropertyInfoListedFalse.setValue(1);
	}

	CMRealEstateGui_playerPropertyInfo.setVisible(1);
}

function CMRealEstateGui::setPropertyListingStatus(%this, %status) {
	commandToServer('CM_RealEstate_setPropertyListingStatus', CMRealEstateGui_playerPropertyInfo.propertyID, %status);
}

function CMRealEstateGui::updatePlayerPropertyInfo(%this) {
	commandToServer('CM_RealEstate_updatePropertyInfo', CMRealEstateGui_playerPropertyInfo.propertyID, CMRealEstateGui_playerPropertyInfoName.getValue(), CMRealEstateGui_playerPropertyInfoDescription.getValue(), CMRealEstateGui_playerPropertyInfoPrice.getValue());
}

function CMRealEstateGui::filterProperties(%this) {
	%filter["name"] = CMRealEstateGui_propertyFilterName.getValue();
	%filter["price"] = CMRealEstateGui_propertyFilterPrice.getValue();
	%filter["size"] = CMRealEstateGui_propertyFilterSize.getValue();
	%filter["type1"] = CMRealEstateGui_propertyFilterType1.getValue();
	%filter["type2"] = CMRealEstateGui_propertyFilterType2.getValue();
	%filter["type3"] = CMRealEstateGui_propertyFilterType3.getValue();

	%found = false;
	%xpos = 0;
	%ypos = 0;
	%visiblei = 0;

	for(%i = 0; %i < CMRealEstateGui_propertiesList.getCount(); %i++) {
		%object = CMRealEstateGui_propertiesList.getObject(%i);
		%visible = true;

		if((%filter["name"] !$= "") && (%filter["name"] !$= "Property Name") && !searchString(%object.propertyName, %filter["name"])) {
			%visible = false;
		} else if((%filter["price"] !$= "") && (%object.propertyPrice > %filter["price"])) {
			%visible = false;
		} else if((%filter["size"] !$= "") && !searchString(%object.propertySize, %filter["size"])) {
			%visible = false;
		} else if((%filter["type1"] || %filter["type2"] || %filter["type3"]) && (!(%filter["type1"] && (%object.propertyType $= "residential")) && !(%filter["type2"] && (%object.propertyType $= "commercial")) && !(%filter["type3"] && (%object.propertyType $= "industrial")))) {
			%visible = false;
		}

		if(!%visible) {
			%object.setVisible(0);
		} else {
			if(!%object.isVisible()) {
				%object.setVisible(1);
			}

			%found = true;
			%visiblei++;
		}

		if(%visible) {
			if(%visiblei == 1) {
				%xpos = 0;
				%ypos = 0;
			} else if((%visiblei % 3) == 1) {
				%xpos = 0;
				%ypos += %object.getExtentH() + CMRealEstateGui_propertiesList.listSpacing;
			} else {
				%xpos += %object.getExtentW() + CMRealEstateGui_propertiesList.listSpacing;
			}

			%object.setPosition(%xpos, %ypos);
		}
	}

	CMRealEstateGui_propertiesList.setExtentH(%found ? %ypos + 134 : 1);
}

function CMRealEstateGui::filterPlayerProperties(%this) {
	%text = CMRealEstateGui_playerPropertiesSearchFilter.getValue();

	for(%i = 0; %i < CMRealEstateGui_playerPropertiesList.getCount(); %i++) {
		%object = CMRealEstateGui_playerPropertiesList.getObject(%i);

		if((%text !$= "") && (%text !$= "Search Filter") && !searchString(%object.propertyName, %text)) {
			%object.setVisible(0);
		} else {
			if(!%object.isVisible()) {
				%object.setVisible(1);
			}
		}
	}

	CMRealEstateGui_playerPropertiesList.resizeListGui();
}

function CMPropertiesMouseEventCtrl::onMouseDown(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	if(isObject(%property = %this.parent())) {
		CMRealEstateGui.viewPropertyInfo(%property);
	}
}

function CMPlayerPropertiesMouseEventCtrl::onMouseDown(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	if(isObject(%property = %this.parent())) {
		for(%i = 0; %i < CMRealEstateGui_playerPropertiesList.getCount(); %i++) {
			%otherProperty = CMRealEstateGui_playerPropertiesList.getObject(%i);

			if(%otherProperty.getID() == %property.getID()) {
				continue;
			}

			CMRealEstateGui_playerPropertiesList.remove(%otherProperty);
			%otherProperty.setProfile("CMBorderOneProfile");
			%otherProperty.child("bg").color = "255 255 255 255";
			CMRealEstateGui_playerPropertiesList.add(%otherProperty);
		}

		CMRealEstateGui_playerPropertiesList.remove(%property);
		%property.setProfile("CMBorderThreeProfile");
		%property.child("bg").color = "248 248 248 255";
		CMRealEstateGui_playerPropertiesList.add(%property);

		CMRealEstateGui.showJobInfo(%property);
	}
}