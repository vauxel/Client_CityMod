// ============================================================
// Project          -      CityMod
// Attribution      -      Siba, for the base code
// Description      -      Inventory Hotbar Code
// ============================================================

function CityModClientHUDHotbar::onAdd(%this) {
	%this.numSlots = isObject(%inventory = CMClient_HUD.inventories.inventory[CMClient_HUD.inventories.findInventoryByID("CLIENT")]) ? %inventory.sizeX : 6;

	%screenX = getWord(getRes(), 0);
	%screenY = getWord(getRes(), 1);
	%width = (63 * %this.numSlots) + 8;

	CMClient_HUD.addGuiToComponent("hotbar", new GuiSwatchCtrl() {
		profile = "GuiDefaultProfile";
		horizSizing = "center";
		vertSizing = "bottom";
		position = mFloor((%screenX / 2) - (%width / 2)) SPC %screenY;
		extent = %width SPC 71;
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "20 20 20 120";
		active = 1;
	});

	for(%i = 0; %i < %this.numSlots; %i++) {
		%this.addSlot(%i);
	}

	%selectedSlot = new GuiBitmapCtrl() {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "71 71";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		bitmap = "Add-Ons/Client_CityMod/res/ui/selectedHotbarSlot";
		wrap = "0";
		lockAspectRatio = "0";
		alignLeft = "0";
		alignTop = "0";
		overflowImage = "0";
		keepCached = "0";
		mColor = "255 255 255 255";
		mMultiply = "0";
	};

	CMClient_HUD.gui["hotbar"].add(%selectedSlot);

	CMClient_HUD.gui["hotbar"].selectedSlot = %selectedSlot;
	%this.selectedSlotNumber = 0;
}

function CityModClientHUDHotbar::addSlot(%this, %x) {
	%slotBackground = new GuiSwatchCtrl() {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = 8 + (%x * 63) SPC 8;
		extent = "55 55";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "20 20 20 120";

		// Border Bitmap
		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "55 55";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";

			// Icon Bitmap
			new GuiBitmapCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "55 55";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				wrap = "0";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				keepCached = "0";
				mColor = "255 255 255 255";
				mMultiply = "0";

				// Background Swatch
				new GuiSwatchCtrl() {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 36";
					extent = "55 19";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "20 20 20 120";
				};

				// Name Text Button
				new GuiBitmapButtonCtrl() {
					profile = "BlankButtonProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "55 91";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					command = "";
					text = " ";
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

				// Count Background Swatch
				new GuiSwatchCtrl() {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "19 17";
					minExtent = "8 2";
					enabled = "1";
					visible = "0";
					clipToParent = "1";
					color = "20 20 20 120";

					// Count Text
					new GuiTextCtrl() {
						profile = "BlankButtonProfile";
						horizSizing = "right";
						vertSizing = "bottom";
						position = "2 -1";
						extent = "15 18";
						minExtent = "8 2";
						enabled = "1";
						visible = "1";
						clipToParent = "1";
						lineSpacing = "2";
						allowColorChars = "0";
						maxChars = "-1";
						text = "0";
						maxBitmapHeight = "-1";
						selectable = "1";
						autoResize = "1";
					};
				};
			};
		};
	};

	CMClient_HUD.gui["hotbar"].add(%slotBackground);
	%this.slot[%x] = %slotBackground;
}

function CityModClientHUDHotbar::isSlotOccupied(%this, %pos) {
	if((%pos $= "") || (%this.slot[%pos] $= "")) {
		return -1;
	}

	// Quick and fast fix, although it may not be the proper way to find if the slot is occupied
	return %this.slot[%pos].getObject(0).getObject(0).bitmap !$= "";
}

function CityModClientHUDHotbar::switchSlot(%this, %pos) {
	if(((%pos !$= "LEFT") && (%pos !$= "RIGHT")) && !isInteger(%pos)) {
		warn("CityModClientHUDHotbar[" @ %this.getID() @ "]::switchSlot() ==> Invalid \"%pos\" given");
		return;
	}

	if(!CMClient_HUD.isComponentOnScreen("hotbar")) {
		return;
	}

	%selectedSlot = CMClient_HUD.gui["hotbar"].selectedSlot;
	%selectedSlotNumber = %this.selectedSlotNumber;

	if(%pos $= "RIGHT") {
		if((%selectedSlotNumber + 1) > (%this.numSlots - 1)) { // Restrict overflow
			return;
		}

		%x = (%selectedSlotNumber + 1) * 63;
		%selectedSlot.position = %x SPC 0;
		%this.selectedSlotNumber++;
	} else if(%pos $= "LEFT") {
		if((%selectedSlotNumber - 1) < 0) { // Restrict overflow
			return;
		}

		%x = (%selectedSlotNumber - 1) * 63;
		%selectedSlot.position = %x SPC 0;
		%this.selectedSlotNumber--;
	} else {
		%pos = mClamp(%pos, 0, (%this.numSlots - 1));
		%x = %pos * 63;
		%selectedSlot.position = %x SPC 0;
		%this.selectedSlotNumber = %pos;
	}

	if(%this.isSlotOccupied(%this.selectedSlotNumber)) {
		commandToServer('CM_Inventory_useItem', %this.selectedSlotNumber);
	} else {
		commandToServer('CM_Inventory_unUseItem', %this.selectedSlotNumber);
	}
}

function CityModClientHUDHotbar::toggleSelectedSlot(%this) {
	if(!%this.isSlotOccupied(%this.selectedSlotNumber)) {
		return;
	}

	commandToServer('CM_Inventory_toggleUseItem', %this.selectedSlotNumber);
}

function CityModClientHUDHotbar::useSelectedSlot(%this) {
	if(!%this.isSlotOccupied(%this.selectedSlotNumber)) {
		return;
	}

	commandToServer('CM_Inventory_useItem', %this.selectedSlotNumber);
}

function CityModClientHUDHotbar::unUseSelectedSlot(%this) {
	if(!%this.isSlotOccupied(%this.selectedSlotNumber)) {
		return;
	}

	commandToServer('CM_Inventory_unUseItem', %this.selectedSlotNumber);
}

function CityModClientHUDHotbar::dropSelectedSlot(%this) {
	if(!%this.isSlotOccupied(%this.selectedSlotNumber)) {
		return;
	}

	commandtoserver('CM_Inventory_dropItem', "CLIENT", %this.selectedSlotNumber);
}