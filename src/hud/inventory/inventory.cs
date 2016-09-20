// ============================================================
// Project          -      CityMod
// Attribution      -      Siba, for the base code
// Description      -      Inventory Code
// ============================================================
// Sections
//   1: Client Commands
//   2: Inventory Items
//   3: Inventories
//   4: Inventory
// ============================================================

// ============================================================
// Section 1 - Client Commands
// ============================================================

function clientcmdCM_Inventory_openInventory(%id, %name, %sizeX, %sizeY) {
	if((%id $= "") || !isNumber(%id)) {
		warn("clientcmdCM_Inventory_openInventory ==> Invalid \"Id\" given");
		return;
	}

	if(%name $= "") {
		warn("clientcmdCM_Inventory_openInventory ==> No \"Name\" given");
		return;
	}

	if(%sizeX $= "") {
		warn("clientcmdCM_Inventory_openInventory ==> No \"SizeX\" given -- defaulting to a \"SizeX\" of '2'");
		%sizeX = 2;
	}

	if(%sizeY $= "") {
		warn("clientcmdCM_Inventory_openInventory ==> No \"SizeY\" given -- defaulting to a \"SizeY\" of '3'");
		%sizeY = 3;
	}

	CMClient_HUD.inventories.createInventory(%id, properText(%name), %sizeX, %sizeY);
}

function clientcmdCM_Inventory_setSlot(%id, %x, %y, %type, %name, %count) {
	if(%id $= "") {
		warn("clientcmdCM_Inventory_setSlot ==> Invalid \"%id\" given");
		return;
	}

	if((%x $= "") || (%y $= "")) {
		warn("clientcmdCM_Inventory_setSlot ==> Invalid \"%x\" and/or \"%y\" given");
		return;
	}

	if(%name $= "") {
		warn("clientcmdCM_Inventory_setSlot ==> Invalid \"%name\" given");
		return;
	}

	if((%count !$= "") && !isInteger(%count)) {
		warn("clientcmdCM_Inventory_setSlot ==> Invalid \"%count\" given -- if not empty, \"%count\" must be an integer");
		return;
	}

	if(!isObject(CMClient_InventoryItems)) {
		new ScriptGroup(CMClient_InventoryItems);
	}

	// Potential to pass custom data fields to the item as a MapObject in the second parameter.
	%item = CMClient_InventoryItems.createItemData(%type, %name, "");

	if(isObject(%inventory = CMClient_HUD.inventories.inventory[CMClient_HUD.inventories.findInventoryByID(%id)])) {
		%inventory.setSlot(%x, %y, %item, %count);
	}
}

function clientcmdCM_Inventory_clearSlot(%id, %x, %y) {
	if(%id $= "") {
		warn("clientcmdCM_Inventory_clearSlot ==> Invalid \"%id\" given");
		return;
	}

	if((%x $= "") || (%y $= "")) {
		warn("clientcmdCM_Inventory_clearSlot ==> Invalid \"%x\" and/or \"%y\" given");
		return;
	}

	if(isObject(%inventory = CMClient_HUD.inventories.inventory[CMClient_HUD.inventories.findInventoryByID(%id)])) {
		%inventory.setSlot(%x, %y, "");
	}
}

// ============================================================
// Section 2 - Inventory Items
// ============================================================

function CMClient_InventoryItems::findItemDataByName(%this, %name) {
	for(%i = 0; %i < %this.getCount(); %i++) {
		if(%this.getObject(%i).name $= %name) {
			return %this.getObject(%i);
		}
	}
}

function CMClient_InventoryItems::createItemData(%this, %type, %name, %customdata) {
	if(isObject(%item = %this.findItemDataByName(%name))) {
		return %item;
	}

	%image = "Add-Ons/Client_CityMod/res/gui/items/" @ (%type $= "BRICK" ? "bricks/" : "") @ strReplace(strLwr(%name), " ", "_");

	%item = new ScriptObject() {
		type = %type;
		name = %name;
		image = (isFile(%image @ ".png") ? %image : "Add-Ons/Client_CityMod/res/gui/items/unknown.png");
	} @ "\x01";

	if((%customdata !$= "") && isMapObject(%customdata)) {
		for(%i = 0; %i < %customdata.keys.length; %i++) {
			%item.setAttribute();
		}

		%customdata.delete();
	}

	%this.add(%item);
	return %item;
}

// ============================================================
// Section 3 - Inventories
// ============================================================

function CityModClientHUDInventories::onAdd(%this) {
	%this.inventorycount = 0;
}

function CityModClientHUDInventories::onRemove(%this) {
	%this.switchCursor("Default");

	for(%i = 0; %i < %this.inventorycount; %i++) {
		if(isObject(%object = %this.inventory[%i])) {
			%object.delete();
		}
	}
}

function CityModClientHUDInventories::createInventory(%this, %id, %name, %sizeX, %sizeY) {
	%inventory = new ScriptObject() {
		class = "CityModClientHUDInventory";
		inventoryID = %id;
		name = %name;
		sizeX = %sizeX;
		sizeY = %sizeY;
	};

	if(!isObject(%inventory)) {
		return;
	}

	%this.inventory[%this.inventorycount] = %inventory;
	%this.inventorycount++;

	%this.gui.add(%inventory.gui);
	%this.organizeGUI();

	commandToServer('CM_Inventory_requestData', %inventory.inventoryID);
}

function CityModClientHUDInventories::deleteInventory(%this, %id) {
	if(!isObject(%this.inventory[%index = %this.findInventoryByID(%id)])) {
		warn("CityModClientHUDInventories[" @ %this.getID() @ "]::deleteInventory() ==> An Inventory by this ID does not exist");
		return;
	}

	%this.inventory[%index].delete();
	%this.inventory[%index] = "";

	if(%index != (%this.inventorycount - 1)) {
		for(%i = (%index + 1); %i < %this.inventorycount; %i++) {
			%this.inventory[%i - 1] = %this.inventory[%i];
			%this.inventory[%i] = "";
		}
	}

	%this.inventorycount--;
}

function CityModClientHUDInventories::deleteOtherInventories(%this) {
	for(%i = 0; %i < %this.inventorycount; %i++) {
		if(%this.inventory[%i].inventoryID $= "CLIENT") {
			%inventory = %this.inventory[%i];
			continue;
		}

		%this.inventory[%i].delete();
		%this.inventory[%i] = "";
	}

	%this.inventorycount = 1;
	%this.inventory[0] = %inventory;
}

function CityModClientHUDInventories::findInventoryByID(%this, %id) {
	for(%i = 0; %i < %this.inventorycount; %i++) {
		if(!isObject(%this.inventory[%i])) {
			continue;
		}

		if(%this.inventory[%i].inventoryID $= %id) {
			return %i;
		}
	}
}

function CityModClientHUDInventories::organizeGUI(%this) {
	%topMargin = 10;
	%leftMargin = 10;
	%rightMargin = 10;
	%bottomMargin = 10;
	%guiSize = CMClient_HUD.gui["inventories"].getExtent();
	%count = CMClient_HUD.gui["inventories"].getCount();

	%sumWidth = 0;

	for(%i = 0; %i < %count; %i++) {
		%obj = CMClient_HUD.gui["inventories"].getObject(%i);
		%sumWidth += %obj.getExtentW() + %leftMargin + %rightMargin;
	}

	if(%sumWidth <= getWord(%guiSize, 0)) {
		for(%i = 0; %i < %count; %i++) {
			%ext = CMClient_HUD.gui["inventories"].getObject(%i).getExtent();
			%area[%i] = getWord(%ext, 0) * getWord(%ext, 1);
		}

		for(%i = 0; %i < %count; %i++) {
			%max = -999999;
			%right = !%right;

			for(%j = 0; %j < %count; %j++) {
				if(%sorted[%j]) {
					continue;
				}

				if(%area[%j] > %max) {
					%max = %area[%j];
					%sortI = %j;
				}
			}

			%obj = CMClient_HUD.gui["inventories"].getObject(%sortI);
			%sorted[%sortI] = true;

			if(%right) {
				%ext = %obj.getExtent();
				%obj.position = %highX + %leftMargin SPC -mFloor(getWord(%ext, 1) / 2);
				%obj.extent = getWord(%ext, 0) SPC getWord(%ext, 1);


				%posX = %obj.getPositionX();
				%highX = %posX + getWord(%ext, 0) + %rightMargin;
			} else {
				%ext = %obj.getExtent();
				%obj.position = %lowX - (%rightMargin + getWord(%ext, 0)) SPC -mFloor(getWord(%ext, 1) / 2);
				%obj.extent = getWord(%ext, 0) SPC getWord(%ext, 1);
				%posX = %obj.getPositionY();
				%lowX = %posX - %leftMargin;
			}
		}
	}

	%this.centerObjects();
}

function CityModClientHUDInventories::centerObjects(%this) {
	%lowX = 999999;
	%lowY = 999999;
	%highX = -999999;
	%highY = -999999;
	%guiSize = CMClient_HUD.gui["inventories"].getExtent();
	%count = CMClient_HUD.gui["inventories"].getCount();

	for(%i = 0; %i < %count; %i++) {
		%obj = CMClient_HUD.gui["inventories"].getObject(%i);
		%pos = %obj.getPosition();
		%ext = %obj.getExtent();

		%posX = getWord(%pos, 0);
		%posY = getWord(%pos, 1);

		%lowX = %posX < %lowX ? %posX : %lowX;
		%lowY = %posY < %lowY ? %posY : %lowY;

		%extX = getWord(%ext, 0);
		%extY = getWord(%ext, 1);

		%highX = (%posX + %extX) > %highX ? (%posX + %extX) : %highX;
		%highY = (%posY + %extY) > %highY ? (%posY + %extY) : %highY;
	}

	%centX = (%lowX + %highX) / 2;
	%centY = (%lowY + %highY) / 2;

	%diffX = mFloatLength(getWord(%guiSize, 0) / 2 - %centX, 0);
	%diffY = mFloatLength(getWord(%guiSize, 1) / 2 - %centY, 0);

	for(%i = 0; %i < %count; %i++) {
		%obj = CMClient_HUD.gui["inventories"].getObject(%i);
		%pos = %obj.getPosition();
		%ext = %obj.getExtent();

		%obj.position = getWord(%pos, 0) + %diffX SPC getWord(%pos, 1) + %diffY;
		%obj.extent = getWord(%ext, 0) SPC getWord(%ext, 1);
	}
}

function CityModClientHUDInventories::switchCursor(%this, %img) {
	DefaultCursor.delete();

	%cursor = new GuiCursor(DefaultCursor) {
		bitmapName = %img;
		hotSpot = "27 27";
	};

	if(%img $= "Default") {
		DefaultCursor.bitmapName = "base/client/ui/CUR_3darrow";
		DefaultCursor.hotSpot = "1 1";
	}

	canvas.setCursor(DefaultCursor);
}

function CityModClientHUDInventories::dropItem(%this) {
	if(!isObject(%this.pickedUpItem)) {
		return;
	}

	commandToServer('CM_Inventory_dropItem', %this.pickedUpItemInventory, getWord(%this.pickedUpItemSlot, 0), getWord(%this.pickedUpItemSlot, 1));

	%this.pickedUpItem = "";
	%this.pickedUpItemCount = 0;
	%this.pickedUpItemSlot = "";
	%this.pickedUpItemInventory = "";
	%this.switchCursor("Default");
}

function CityModClientHUDInventories::setSplitSlot(%this) {
	if(!isObject(%itemsplitter = %this.gui.child("itemsplitter")) || isObject(%itemsplitter.item)) {
		return;
	}

	if(!isObject(%this.pickedUpItem) || (%this.pickedUpItemCount <= 1)) {
		return;
	}

	%itemsplitter.count["total"] = %this.pickedUpItemCount;
	%itemsplitter.count["new"] = mFloatLength(%this.pickedUpItemCount * %itemsplitter.child("controls").child("slider").getValue(), 0);
	%itemsplitter.item = %this.pickedUpItem;
	%itemsplitter.item["count"] = %this.pickedUpItemCount;
	%itemsplitter.item["slot"] = %this.pickedUpItemSlot;
	%itemsplitter.item["inventory"] = %this.pickedUpItemInventory;

	%icon = %itemsplitter.child("slot").getObject(0).getObject(0);
	%text = %icon.getObject(1);
	%countbg = %icon.getObject(2);
	%counttext = %countbg.getObject(0);

	%icon.setBitmap(%this.pickedUpItem.image);
	%text.setText(%this.pickedUpItem.name);

	%countbg.setVisible(1);
	%counttext.setText(%this.pickedUpItemCount);

	%itemsplitter.child("controls").child("count").child("counttext").setText("<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>" @ mFloatLength(%itemsplitter.count["total"] * %itemsplitter.child("controls").child("slider").getValue(), 0));

	%inventory = %this.inventory[%this.findInventoryByID(%this.pickedUpItemInventory)];
	%inventory.setSlot(getWord(%this.pickedUpItemSlot, 0), getWord(%this.pickedUpItemSlot, 1), %this.pickedUpItem, %this.pickedUpItemCount);

	%this.pickedUpItem = "";
	%this.pickedUpItemCount = 0;
	%this.pickedUpItemSlot = "";
	%this.pickedUpItemInventory = "";
	%this.switchCursor("Default");
}

function CityModClientHUDInventories::splitItem(%this) {
	if(!isObject(%itemsplitter = %this.gui.child("itemsplitter")) || !isObject(%itemsplitter.item)) {
		return;
	}

	commandToServer('CM_Inventory_splitItem', %itemsplitter.item["inventory"], getWord(%itemsplitter.item["slot"], 0), getWord(%itemsplitter.item["slot"], 1), %itemsplitter.count["new"]);

	%icon = %itemsplitter.child("slot").getObject(0).getObject(0);
	%text = %icon.getObject(1);
	%countbg = %icon.getObject(2);
	%counttext = %countbg.getObject(0);

	%icon.setBitmap("");
	%text.setText("");

	%counttext.setText("");
	%countbg.setVisible(0);

	%itemsplitter.count["total"] = "";
	%itemsplitter.count["new"] = "";
	%itemsplitter.item = "";
	%itemsplitter.item["count"] = "";
	%itemsplitter.item["slot"] = "";
	%itemsplitter.item["inventory"] = "";

	%this.deleteItemSplitter();
}

function CityModClientHUDInventories::addItemSplitter(%this) {
	if(isObject(%this.gui.child("itemsplitter"))) {
		return;
	}

	%clientInventoryPos = %this.inventory[%this.findInventoryByID("CLIENT")].gui.getPosition();

	%itemsplitter = new GuiSwatchCtrl("_itemsplitter") {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = getWord(%clientInventoryPos, 0) SPC (getWord(%clientInventoryPos, 1) - 61 - 2);
		extent = "386 61";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "0 0 0 0";

		new GuiSwatchCtrl("_controls") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 31";
			extent = "323 30";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "20 20 20 120";

			new GuiSwatchCtrl("_count") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 5";
				extent = "24 20";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 120";

				new GuiMLTextCtrl("_counttext") {
					profile = "GuiMLTextProfile";
					horizSizing = "center";
					vertSizing = "bottom";
					position = "0 1";
					extent = "24 18";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					lineSpacing = "2";
					allowColorChars = "0";
					maxChars = "-1";
					text = "<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>99";
					maxBitmapHeight = "-1";
					selectable = "0";
					autoResize = "1";
				};
			};

			new GuiProgressCtrl("_slider") {
				profile = "GuiProgressProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "34 5";
				extent = "214 20";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";

				new GuiMouseEventCtrl("CityModClientHUDInvItemSplitter") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "214 20";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					lockMouse = "0";
				};
			};

			new GuiSwatchCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "253 5";
				extent = "65 20";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 120";

				new GuiBitmapButtonCtrl() {
					profile = "BlankButtonProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "65 20";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					command = "CMClient_HUD.inventories.splitItem();";
					text = "Split Item";
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
		};

		new GuiSwatchCtrl("_slot") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "325 0";
			extent = "61 61";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "20 20 20 120";

			new GuiSwatchCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "3 3";
				extent = "55 55";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 120";

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
						command = "CMClient_HUD.inventories.setSplitSlot();";
						text = "";
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

					new GuiSwatchCtrl() {
						profile = "GuiDefaultProfile";
						horizSizing = "right";
						vertSizing = "bottom";
						position = "0 0";
						extent = "18 17";
						minExtent = "8 2";
						enabled = "1";
						visible = "0";
						clipToParent = "1";
						color = "20 20 20 120";

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
							text = "00";
							maxLength = "255";
						};
					};
				};
			};
		};
	};

	%this.gui.add(%itemsplitter);
	%itemsplitter.child("controls").child("slider").setValue(0.5);
}

function CityModClientHUDInventories::deleteItemSplitter(%this) {
	if(!isObject(%itemsplitter = %this.gui.child("itemsplitter"))) {
		return;
	}

	%itemsplitter.delete();
}

function CityModClientHUDInvItemSplitter::onMouseDown(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	if(!isObject(%this.parent().parent().parent().item)) {
		return;
	}

	%currentParent = %this;

	while(isObject(%currentParent = %currentParent.parent())) {
		%offset += getWord(%currentParent.getPosition(), 0);

		if(%currentParent.getClassName() $= "GuiControl") {
			break;
		}
	}

	%slider = %this.parent();
	%slidervalue = (getWord(%mousePoint, 0) - %offset) / %slider.getExtentW();
	%slider.setValue(%slidervalue);

	%itemsplitter = %slider.parent().parent();
	%newcount = mClamp(mFloatLength(%itemsplitter.count["total"] * %slider.getValue(), 0), 1, %itemsplitter.count["total"] - 1);
	%itemsplitter.count["new"] = %newcount;
	%itemsplitter.child("controls").child("count").child("counttext").setText("<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>" @ %newcount);
}

// ============================================================
// Section 4 - Inventory
// ============================================================

function CityModClientHUDInventory::onAdd(%this) {
	if(!strLen(%this.inventoryID)) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::onAdd() ==> No \"InventoryID\" given");
		%this.delete();
		return;
	} else if(isObject(CMClient_HUD.inventories.inventory[CMClient_HUD.inventories.findInventoryByID(%this.inventoryID)])) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::onAdd() ==> An Inventory with the \"InventoryID\" given already exists");
		%this.delete();
		return;
	}

	if(!strLen(%this.name)) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::onAdd() ==> No \"Name\" given");
		%this.delete();
		return;
	}

	if(!strLen(%this.sizeX)) {
		%this.sizeX = 2;
	} else if(%this.sizeX <= 0) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::onAdd() ==> Invalid \"SizeX\" given -- Cannot be less than zero");
		%this.delete();
		return;
	}

	if(!strLen(%this.sizeY)) {
		%this.sizeY = 3;
	} else if(%this.sizeY <= 0) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::onAdd() ==> Invalid \"SizeX\" given -- Cannot be less than zero");
		%this.delete();
		return;
	}

	// Populate Horizontal "Line" From Length
	%lineCount = mFloor(((63 * %this.sizeX) + 8) / 8) - 1;
	for(%i = 0; %i < %lineCount; %i++) {
		%line = %line @ "_";
	}

	%this.gui = new GuiSwatchCtrl("_inventory" @ %this.inventoryID) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = (63 * %this.sizeX) + 8 SPC (63 * %this.sizeY) + 32;
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "20 20 20 120";

		// Inventory Name Text
		new GuiMLTextCtrl() {
			profile = "GuiMLTextProfile";
			horizSizing = "center";
			vertSizing = "bottom";
			position = "0 2";
			extent = (63 * %this.sizeX) + 8 SPC 18;
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>" @ %this.name;
			maxBitmapHeight = "-1";
			selectable = "0";
			autoResize = "1";
		};

		// Horizontal Line Text
		new GuiMLTextCtrl() {
			profile = "GuiMLTextProfile";
			horizSizing = "center";
			vertSizing = "bottom";
			position = "0 8";
			extent = (63 * %this.sizeX) + 8 SPC 18;
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center><shadow:2:2><shadowcolor:00000066><color:DDDDDD><font:Impact:18>" @ %line;
			maxBitmapHeight = "-1";
			selectable = "0";
			autoResize = "1";
		};
	};

	// Populate Slots
	for(%yy = 0; %yy < %this.sizeY; %yy++) {
		for(%xx = 0; %xx < %this.sizeX; %xx++) {
			%this.addSlot(%xx, %yy);
		}
	}

	// Add the 'Close Button' if the Inventory being created is not the player's
	// - All Inventories except the player's are allowed to be closed, whereas the 'Player Inventory' is toggle-only
	if(%this.inventoryID !$= "CLIENT") {
		%closeButton = new GuiBitmapButtonCtrl() {
			profile = "BlankButtonProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = (getWord(%this.gui.extent, 0) - 20) SPC "2";
			extent = "18 18";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMClient_HUD.inventories.deleteInventory(" @ %this.inventoryID @ ");";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "Add-Ons/Client_CityMod/res/ui/closeButton/closeButton";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "255 0 0 255";
		};

		%this.gui.add(%closeButton);
	}

	// The Player Inventory gets a 'Hotbar Row'
	if(%this.inventoryID $= "CLIENT") {
		%this.gui.resize(getWord(%this.gui.position, 0), getWord(%this.gui.position, 1), getWord(%this.gui.extent, 0), getWord(%this.gui.extent, 1) + 7);

		for(%i = 0; %i < %this.sizeX; %i++) {
			if(!isObject(%slot = %this.slot[%i @ "_" @ %this.sizeY - 1])) {
				warn("CityModClientHUDInventory[" @ %this.getID() @ "]::onAdd() ==> A linked Hotbar Slot could not be found for Slot '" @ %i @ "_" @ (%this.sizeY - 1) @ "'");
				return;
			}

			%slot.position = getWord(%slot.position, 0) SPC getWord(%slot.position, 1) + 7;
			%slot.isHotbar = 1;
		}

		%dividerLine = new GuiMLTextCtrl() {
			profile = "GuiMLTextProfile";
			horizSizing = "center";
			vertSizing = "bottom";
			position = 0 SPC -48 + (%this.sizeY * 63);
			extent = (63 * %this.sizeX) + 8 SPC 18;
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:center><shadow:2:2><shadowcolor:00000066><color:DDDDDD><font:Impact:18>" @ %line;
			maxBitmapHeight = "-1";
			selectable = "0";
			autoResize = "1";
		};

		%this.gui.add(%dividerLine);
	}
}

function CityModClientHUDInventory::onRemove(%this) {
	if(isObject(%this.gui)) {
		%this.gui.delete();
	}

	if(isObject(CMClient_HUD.inventories)) {
		CMClient_HUD.inventories.organizeGUI();
	}
}

function CityModClientHUDInventory::addSlot(%this, %x, %y) {
	%slotBackground = new GuiSwatchCtrl() {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = 8 + (%x * 63) SPC 32 + (%y * 63);
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
					command = %this @ ".clickButton(" @ %x @ ", " @ %y @ ");";
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
					extent = "18 17";
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
						text = "00";
						maxLength = "255";
					};
				};
			};
		};
	};

	%this.gui.add(%slotBackground);
	%this.slot[%x @ "_" @ %y] = %slotBackground;
}

function CityModClientHUDInventory::clickButton(%this, %x, %y) {
	if(!isObject(%slot = %this.slot[%x @ "_" @ %y])) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::clickButton() ==> A Slot at the specified coordinates does not exist");
		return;
	}

	if(isObject(CMClient_HUD.inventories.pickedUpItem)) { // Placing down picked up Item
		if(isObject(%slot.item)) { // Placing down Item in occupied slot (Swapping)
			if(CMClient_HUD.inventories.pickedUpItemInventory $= %this.inventoryID) { // Same inventory, no transfer required
				commandToServer('CM_Inventory_swapItem',
					%this.inventoryID,
					%x,
					%y,
					getWord(CMClient_HUD.inventories.pickedUpItemSlot, 0),
					getWord(CMClient_HUD.inventories.pickedUpItemSlot, 1)
				);
			} else { // Different inventory, transfer required
				commandToServer('CM_Inventory_transferSwapItem',
					CMClient_HUD.inventories.pickedUpItemInventory,
					getWord(CMClient_HUD.inventories.pickedUpItemSlot, 0),
					getWord(CMClient_HUD.inventories.pickedUpItemSlot, 1),
					%this.inventoryID,
					%x,
					%y
				);
			}

			%this.swapItemWithSlot(%x, %y);
		} else { // Placing down Item in unoccupied slot (Moving)
			CMClient_HUD.inventories.deleteItemSplitter();

			if(CMClient_HUD.inventories.pickedUpItemInventory $= %this.inventoryID) { // Same inventory, no transfer required
				if(CMClient_HUD.inventories.pickedUpItemSlot !$= (%x SPC %y)) {
					commandToServer('CM_Inventory_moveItem',
						%this.inventoryID,
						getWord(CMClient_HUD.inventories.pickedUpItemSlot, 0),
						getWord(CMClient_HUD.inventories.pickedUpItemSlot, 1),
						%x,
						%y
					);
				}
			} else { // Different inventory, transfer required
				commandToServer('CM_Inventory_transferItem',
					CMClient_HUD.inventories.pickedUpItemInventory,
					getWord(CMClient_HUD.inventories.pickedUpItemSlot, 0),
					getWord(CMClient_HUD.inventories.pickedUpItemSlot, 1),
					%this.inventoryID,
					%x,
					%y
				);
			}

			%this.dropItemIntoSlot(%x, %y);
		}
	} else { // Picking up Item
		if(isObject(%slot.item)) {
			if((%slot.count !$= "") && (%slot.count > 1)) {
				CMClient_HUD.inventories.addItemSplitter();
			}

			%this.pickupItemFromSlot(%x, %y);
		}
	}
}

function CityModClientHUDInventory::swapItemWithSlot(%this, %x, %y) {
	if(!isObject(%slot = %this.slot[%x @ "_" @ %y])) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::swapItemWithSlot() ==> A Slot at the specified coordinates does not exist");
		return;
	}

	if(%slot.item $= "") {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::swapItemWithSlot() ==> The specified Slot is empty");
		return;
	}

	if(CMClient_HUD.inventories.pickedUpItem $= "") {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::swapItemWithSlot() ==> An Item is not currently picked up");
		return;
	}

	%oldItem = %slot.item;
	%oldCount = %slot.count;

	%this.setSlot(%x, %y, CMClient_HUD.inventories.pickedUpItem, CMClient_HUD.inventories.pickedUpItemCount);

	CMClient_HUD.inventories.pickedUpItem = %oldItem;
	CMClient_HUD.inventories.pickedUpItemCount = %oldCount;
	CMClient_HUD.inventories.pickedUpItemSlot = CMClient_HUD.inventories.pickedUpItemSlot;
	CMClient_HUD.inventories.pickedUpItemInventory = CMClient_HUD.inventories.pickedUpItemInventory;
	CMClient_HUD.inventories.switchCursor(CMClient_HUD.inventories.pickedUpItem.image);
}

function CityModClientHUDInventory::pickupItemFromSlot(%this, %x, %y) {
	if(!isObject(%slot = %this.slot[%x @ "_" @ %y])) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::pickupItemFromSlot() ==> A Slot at the specified coordinates does not exist");
		return;
	}

	if(%slot.item $= "") {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::pickupItemFromSlot() ==> The specified Slot is empty");
		return;
	}

	CMClient_HUD.inventories.pickedUpItem = %slot.item;
	CMClient_HUD.inventories.pickedUpItemCount = %slot.count;
	CMClient_HUD.inventories.pickedUpItemSlot = %x SPC %y;
	CMClient_HUD.inventories.pickedUpItemInventory = %this.inventoryID;
	CMClient_HUD.inventories.switchCursor(CMClient_HUD.inventories.pickedUpItem.image);

	%this.setSlot(%x, %y, "");
}

function CityModClientHUDInventory::dropItemIntoSlot(%this, %x, %y) {
	if(!isObject(%slot = %this.slot[%x @ "_" @ %y])) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::dropItemIntoSlot() ==> A Slot at the specified coordinates does not exist");
		return;
	}

	if(%slot.item !$= "") {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::dropItemIntoSlot() ==> The specified Slot is not empty");
		return;
	}

	if(CMClient_HUD.inventories.pickedUpItem $= "") {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::dropItemIntoSlot() ==> An Item is not currently picked up");
		return;
	}

	%this.setSlot(%x, %y, CMClient_HUD.inventories.pickedUpItem, CMClient_HUD.inventories.pickedUpItemCount);

	CMClient_HUD.inventories.pickedUpItem = "";
	CMClient_HUD.inventories.pickedUpItemCount = 0;
	CMClient_HUD.inventories.pickedUpItemSlot = "";
	CMClient_HUD.inventories.pickedUpItemInventory = "";
	CMClient_HUD.inventories.switchCursor("Default");
}

function CityModClientHUDInventory::setSlot(%this, %x, %y, %item, %count) {
	if(!isObject(%slot = %this.slot[%x @ "_" @ %y])) {
		warn("CityModClientHUDInventory[" @ %this.getID() @ "]::setSlot() ==> A Slot at the specified coordinates does not exist");
		return;
	}

	%border = %slot.getObject(0);
	%icon = %border.getObject(0);
	%layer = %icon.getObject(0);
	%text = %icon.getObject(1);
	%countbg = %icon.getObject(2);
	%counttext = %countbg.getObject(0);

	%slot.item = %item;
	%slot.count = %count;

	if((%item !$= "") && isObject(%item)) {
		%itemName = %item.name;
		%itemImage = %item.image;
	} else {
		%itemImage = "";
		%itemName = "";
	}

	%icon.setBitmap(%itemImage);
	%text.setText(%itemName);

	if(%count > 1) {
		%countbg.setVisible(1);
		%counttext.setText(%count);
	} else {
		%counttext.setText("");
		%countbg.setVisible(0);
	}

	if(%slot.isHotbar) {
		if(CMClient_HUD.componentExists("hotbar")) {
			if(!isObject(%slot = CMClient_HUD.hotbar.slot[%x])) {
				warn("CityModClientHUDInventory[" @ %this.getID() @ "]::setSlot() ==> A linked Hotbar Slot could not be found for this Slot");
				return;
			}

			%border = %slot.getObject(0);
			%icon = %border.getObject(0);
			%layer = %icon.getObject(0);
			%text = %icon.getObject(1);
			%countbg = %icon.getObject(2);
			%counttext = %countbg.getObject(0);

			%icon.setBitmap(%itemImage);
			%text.setText(%itemName);

			if(%count > 1) {
				%countbg.setVisible(1);
				%counttext.setText(%count);
			} else {
				%counttext.setText("");
				%countbg.setVisible(0);
			}
		}
	}
}

function CityModClientHUDInventory::clearSlots(%this) {
	for(%y = 0; %y < %this.sizeY; %y++) {
		for(%x = 0; %x < %this.sizeX; %x++) {
			%this.setSlot(%x, %y, "");
		}
	}
}