function CMClient_HUD::onAdd(%this) {
	if(!isObject(CMHud)) {
		warn("CMClient_HUD::onAdd() ==> \"CMHud\" does not exist");
		return;
	}

	%this.gui = CMHud;
	%this.components = Array();

	%this.onInitialize();

	canvas.popDialog(NewChatHud);
	canvas.pushDialog(%this.gui);

	%this.resizeToScreen();
	%this.schedule(0, "onInitialized");
}

function CMClient_HUD::onRemove(%this) {
	%this.onDelete();

	for(%i = 0; %i < %this.components.length; %i++) {
		%this.getAttribute(%this.components.value[%i]).delete();
	}

	%this.components.delete();

	%this.gui.deleteAll();
	canvas.popDialog(%this.gui);
	canvas.pushDialog(NewChatHud);
}

function CMClient_HUD::onInitialize(%this) { }
function CMClient_HUD::onInitialized(%this) { }
function CMClient_HUD::onDelete(%this) { }

function CMClient_HUD::componentExists(%this, %name) {
	if(!strLen(%name)) {
		warn("CMClient_HUD::componentExists() ==> No \"%name\" given");
		return;
	}

	if(%name $= "gui") {
		warn("CMClient_HUD::componentExists() ==> \"%name\" given can't be \"gui\"");
		return;
	}

	if(%name $= "components") {
		warn("CMClient_HUD::componentExists() ==> \"%name\" given can't be \"components\"");
		return;
	}

	return %this.components.contains(%name) && isObject(%this.getAttribute(%name));
}

function CMClient_HUD::addComponent(%this, %name, %gui) {
	if(!strLen(%name)) {
		warn("CMClient_HUD::addComponent() ==> No \"%name\" given");
		return;
	}

	%name = strLwr(%name);
	%properName = properText(%name);

	if(%name $= "gui") {
		warn("CMClient_HUD::addComponent() ==> \"%name\" given can't be \"gui\"");
		return;
	}

	if(%name $= "components") {
		warn("CMClient_HUD::addComponent() ==> \"%name\" given can't be \"components\"");
		return;
	}

	if(%this.componentExists(%name)) {
		warn("CMClient_HUD::addComponent() ==> A component by this name, \"" @ %properName @ "\", already exists");
		return;
	}

	%this.componentBeingAdded = %name;
	%this.components.push(%name);

	%component = new ScriptObject() {
		class = "CityModClientHUD" @ %properName;
	};

	%this.setAttribute(%name, %component);

	if(isObject(%this.gui[%name]) && %this.gui.isMember(%this.gui[%name])) {
		// Gui was added when the component ScriptObject was created
	} else {
		if(!strLen(%gui) || !isObject(%gui)) {
			%gui = new GuiSwatchCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "center";
				vertSizing = "bottom";
				position = "0 0";
				extent = getRes();
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "0 0 0 0";
			};
		}

		%this.addGuiToComponent(%name, %gui);
	}

	if(%component.getAttribute("gui") $= "") {
		%component.gui = %this.gui[%name];
	}

	%this.componentBeingAdded = "";
	echo("Added" SPC %properName SPC "component to CityMod HUD");
}

function CMClient_HUD::addGuiToComponent(%this, %name, %gui) {
	if(!%this.componentExists(%name) && (%this.componentBeingAdded !$= %name)) {
		warn("CMClient_HUD::addGuiToComponent() ==> Nonexistent component by the name, \"" @ %name @ "\", given");
		return;
	}

	if(!isObject(%gui)) {
		warn("CMClient_HUD::addGuiToComponent() ==> Nonexistent \"%gui\" given");
		return;
	}

	if(isObject(%this.gui[%name])) {
		warn("CMClient_HUD::addGuiToComponent() ==> A gui for this component already exists");
		return;
	}

	%this.gui.add(%gui);
	%this.gui[%name] = %gui;
}

function CMClient_HUD::isShown(%this) {
	return %this.gui.isAwake();
}

function CMClient_HUD::isComponentShown(%this, %name) {
	if(!%this.componentExists(%name)) {
		warn("CMClient_HUD::addGuiToComponent() ==> Nonexistent component by the name, \"" @ %name @ "\", given");
		return;
	}

	if(!%this.isShown()) {
		%this.wasComponentShown[%name] = false;
		return false;
	}

	if(!%this.gui[%name].isAwake()) {
		%this.wasComponentShown[%name] = false;
		return false;
	}

	%this.wasComponentShown[%name] = true;
	return true;
}

function CMClient_HUD::isComponentOnScreen(%this, %name) {
	if(!%this.componentExists(%name)) {
		warn("CMClient_HUD::addGuiToComponent() ==> Nonexistent component by the name, \"" @ %name @ "\", given");
		return;
	}

	//if(!%this.isComponentShown()) {
	//	%this.wasComponentOnScreen[%name] = false;
	//	return false;
	//}

	// Left bounds-check
	if((%this.gui[%name].getPositionX() + %this.gui[%name].getExtentW()) <= 0) {
		%this.wasComponentOnScreen[%name] = false;
		return false;
	}

	// Right bounds-check
	if(%this.gui[%name].getPositionX() >= getWord(getRes(), 0)) {
		%this.wasComponentOnScreen[%name] = false;
		return false;
	}

	// Top bounds-check
	if((%this.gui[%name].getPositionY() + %this.gui[%name].getExtentH()) <= 0) {
		%this.wasComponentOnScreen[%name] = false;
		return false;
	}

	// Bottom bounds-check
	if(%this.gui[%name].getPositionY() >= getWord(getRes(), 1)) {
		%this.wasComponentOnScreen[%name] = false;
		return false;
	}

	%this.wasComponentOnScreen[%name] = true;
	return true;
}

function CMClient_HUD::resizeToScreen(%this) {
	%this.gui.resize(0, 0, getWord(getRes(), 0), getWord(getRes(), 1));
	%this.schedule(0, "onResized");
}

function CMClient_HUD::onResized(%this) { }

function CMClient_HUD::setComponentShown(%this, %name, %shown, %anchor, %method, %speed, %blend) {
	if(!%this.componentExists(%name)) {
		warn("CMClient_HUD::addGuiToComponent() ==> Nonexistent component by the name, \"" @ %name @ "\", given");
		return;
	}

	if((%anchor !$= "left") && (%anchor !$= "right") && (%anchor !$= "top") && (%anchor !$= "bottom")) {
		warn("CMClient_HUD::addGuiToComponent() ==> Invalid anchor, \"" @ %name @ "\", given (left | right | top | bottom)");
		return;
	}

	%screenX = getWord(getRes(), 0);
	%screenY = getWord(getRes(), 1);
	%extentW = %this.gui[%name].getExtentW();
	%extentH = %this.gui[%name].getExtentH();
	%startX = %this.gui[%name].getPositionX();
	%startY = %this.gui[%name].getPositionY();

	if(%speed == 0) {
		%speed = $CMClient::Prefs::AnimationSpeed;
	}

	if(!%shown) { // Hide GUI
		switch$(%anchor) {
			case "left": %this.gui[%name].transitionMove(%method, 0 - %extentW, %startY, %speed, (%blend ? %startX : 0), %startY);
			case "right": %this.gui[%name].transitionMove(%method, %screenX, %startY, %speed, (%blend ? %startX : %screenX - %extentW), %startY);
			case "top": %this.gui[%name].transitionMove(%method, %startX, 0 - %extentH, %speed, %startX, (%blend ? %startY : 0));
			case "bottom": %this.gui[%name].transitionMove(%method, %startX, %screenY, %speed, %startX, (%blend ? %startY : %screenY - %extentH));
		}
	} else { // Show GUI
		switch$(%anchor) {
			case "left": %this.gui[%name].transitionMove(%method, 0, %startY, %speed, (%blend ? %startX : 0 - %extentW), %startY);
			case "right": %this.gui[%name].transitionMove(%method, %screenX - %extentW, %startY, %speed, (%blend ? %startX : %screenX), %startY);
			case "top": %this.gui[%name].transitionMove(%method, %startX, 0, %speed, %startX, (%blend ? %startY : 0 - %extentH));
			case "bottom": %this.gui[%name].transitionMove(%method, %startX, %screenY - %extentH, %speed, %startX, (%blend ? %startY : %screenY));
		}
	}
}

function CMClient_HUD::toggleComponentShown(%this, %name, %anchor, %method, %blend) {
	if(!%this.componentExists(%name)) {
		warn("CMClient_HUD::addGuiToComponent() ==> Nonexistent component by the name, \"" @ %name @ "\", given");
		return;
	}

	if(%this.isComponentOnScreen(%name)) { // Hide GUI
		%this.setComponentShown(%name, false, %anchor, %method, %blend);
	} else { // Show GUI
		%this.setComponentShown(%name, true, %anchor, %method, %blend);
	}
}

package CityModClient_HUDHooks {
	function GameConnection::initialControlSet(%this) {
		parent::initialControlSet(%this);
		CMClient_Cleanup.add(new ScriptObject(CMClient_HUD));
	}

	function PlayGui::onWake(%this) {
		parent::onWake(%this);

		if(isObject(CMClient_HUD) && !CMClient_HUD.isShown()) {
			canvas.popDialog(NewChatHud);
			canvas.pushDialog(CMClient_HUD.gui);
		}
	}

	function PlayGui::onSleep(%this) {
		parent::onSleep(%this);

		if(isObject(CMClient_HUD) && CMClient_HUD.isShown()) {
			canvas.popDialog(CMClient_HUD.gui);
		}
	}

	function resetCanvas() {
		parent::resetCanvas();

		if(isObject(CMClient_HUD) && CMClient_HUD.isShown()) {
			CMClient_HUD.resizeToScreen();
		}
	}
};