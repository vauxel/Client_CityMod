// ============================================================
// Project          -      CityMod
// Description      -      Inventory Hooks Code
// ============================================================
// Sections
//   1: Keybinds
//   2: Function Hooks
// ============================================================

addKeyBind("CityMod", "Toggle Inventory", "CMClient_HUD_Inventory_Toggle");

// ============================================================
// Section 1 - Keybinds
// ============================================================

function CMClient_HUD_Inventory_Toggle(%i) {
	if(!%i) {
		return;
	}

	if(!$CMClient::ConnectedToCMServer) {
		pushCMDialog("OK", "You can't open the CityMod Inventory when you're not in a server running the CityMod Gamemode!");
		return;
	}

	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("hotbar") || (CMClient_HUD.gui["hotbar"].isTransitioning == true)) {
		return;
	}

	if(!CMClient_HUD.isComponentOnScreen("inventories")) { // Show Inventory
		CMClient_HUD.setComponentShown("inventories", true, "top", "easeOutCubic");

		CMClient_HUD.inventories.inventory[CMClient_HUD.inventories.findInventoryByID("CLIENT")].clearSlots();
		commandToServer('CM_Inventory_requestData', "CLIENT");

		// Hide Hotbar
		CMClient_HUD.hotbar.unUseSelectedSlot();
		CMClient_HUD.setComponentShown("hotbar", false, "bottom", "easeOutBounce");

		setScrollMode(0); // Hide PaintGUI
		cursorOn();
	} else { // Hide Inventory
		CMClient_HUD.setComponentShown("inventories", false, "top", "easeInCubic");

		CMClient_HUD.inventories.deleteOtherInventories();
		CMClient_HUD.inventories.pickedUpItem = "";
		CMClient_HUD.inventories.pickedUpItemSlot = "";
		CMClient_HUD.inventories.pickedUpItemInventory = "";
		CMClient_HUD.inventories.switchCursor("Default");

		// Show Hotbar
		CMClient_HUD.hotbar.useSelectedSlot();
		CMClient_HUD.setComponentShown("hotbar", true, "bottom", "easeOutBounce");

		cursorOff();
	}
}

// ============================================================
// Section 2 - Function Hooks
// ============================================================

package CityModClient_InventoryHooks {
	function CMClient_HUD::onInitialize(%this) {
		parent::onInitialize(%this);

		// Inventories
		if(!isObject(CMClient_InventoryItems)) {
			CMClient_Cleanup.add(new ScriptGroup(CMClient_InventoryItems));
		}

		%this.addComponent("inventories", new GuiBitmapButtonCtrl() {
			profile = "BlankButtonProfile";
			horizSizing = "width";
			vertSizing = "height";
			position = "0" SPC (0 - getWord(getRes(), 1));
			extent = getRes();
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMClient_HUD.inventories.dropItem();";
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
		});

		%this.inventories.createInventory("CLIENT", "Player Inventory", 6, 6);

		// Hotbar
		%this.addComponent("hotbar");
	}

	function CMClient_HUD::onInitialized(%this) {
		parent::onInitialized(%this);
		%this.gui["inventories"].resize(0, (0 - getWord(getRes(), 1)), getWord(getRes(), 0), getWord(getRes(), 1));
		%this.setComponentShown("hotbar", true, "bottom", "easeOutBounce");
	}

	function CMClient_HUD::onResized(%this) {
		parent::onResized(%this);

		%screenX = getWord(getRes(), 0);
		%screenY = getWord(getRes(), 1);

		%this.gui["inventories"].resize(0, -%screenY, %screenX, %screenY);
		%this.inventories.organizeGUI();

		%hotbarWidth = %this.gui["hotbar"].getExtentW();
		%hotbarHeight = %this.gui["hotbar"].getExtentH();
		%this.gui["hotbar"].resize(mFloor((%screenX / 2) - (%hotbarWidth / 2)), %screenY, %hotbarWidth, %hotbarHeight);

		%this.setComponentShown("hotbar", true, "bottom", "easeOutBounce");
	}

	function PlayGui::createInvHUD(%this) {
		parent::createInvHUD(%this);

		HUD_BrickNameBG.visible = 0;
		HUD_BrickBox.visible = 0;
	}

	function PlayGui::createToolHUD(%this) {
		parent::createToolHUD(%this);

		HUD_ToolNameBG.visible = 0;
		HUD_ToolBox.visible = 0;
	}

	function openBSD(%i) {
		if(!CMClient_HUD.isComponentOnScreen("hotbar") && !CMClient_HUD.isComponentOnScreen("inventories")) {
			CMClient_HUD.setComponentShown("hotbar", false, "bottom", "easeOutBounce");
			setScrollMode(0); // Hides PaintGUI
		} else {
			CMClient_HUD_Inventory_Toggle(%i);
		}
	}

	function useTools(%i) {
		if(%i) {
			if(CMClient_HUD.isComponentOnScreen("inventories")) {
				CMClient_HUD_Inventory_Toggle(%i);
			} else {
				CMClient_HUD.hotbar.toggleSelectedSlot();
			}
		}
	}

	function dropTool(%i) {
		if(%i) {
			CMClient_HUD.hotbar.dropSelectedSlot();
		}
	}

	function scrollInventory(%i) {
		if(!canvas.isCursorOn()) {
			if(%i > 0) {
				CMClient_HUD.hotbar.switchSlot("LEFT");
			} else {
				CMClient_HUD.hotbar.switchSlot("RIGHT");
			}
		}
	}

	function directSelectInv(%num) {
		if(CMClient_HUD.isComponentOnScreen("hotbar")) {
			if((%num >= 0) && (%num < CMClient_HUD.hotbar.numSlots)) {
				CMClient_HUD.hotbar.switchSlot(%num);
			}
		}
	}

	function useSprayCan(%i) {
		if(!CMClient_HUD.gui["inventories"].isAwake()) {
			if(CMClient_HUD.isComponentOnScreen("hotbar")) {
				CMClient_HUD.setComponentShown("hotbar", true, "bottom", "easeOutBounce");
			}
		}

		parent::useSprayCan(%i);
	}
};