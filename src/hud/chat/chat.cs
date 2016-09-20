// ============================================================
// Project          -      CityMod
// Description      -      Custom Chat Code
// ============================================================

function CityModClientHUDChat::anchorToInfopanel(%this) {
	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("infopanel")) {
		return;
	}

	CMClient_HUD.gui["chat"].setPositionX(CMClient_HUD.gui["infopanel"].getPositionX() + CMClient_HUD.gui["infopanel"].getExtentW());

	if(CMClient_HUD.gui["infopanel"].isTransitioning == true) {
		%this.schedule(10, "anchorToInfopanel");
	}
}

function CityModClientHUDChat::focusChat(%this, %focus) {
	%input = CMClient_HUD.gui["chat"].child("input");

	if(%focus) {
		if(!CMClient_HUD.isComponentOnScreen("chat")) {
			CMClient_HUD.setComponentShown("chat", true, "top", "easeInOutSine");
		}

		%input.setValue("");
		%input.makeFirstResponder(true);
	} else {
		%input.makeFirstResponder(false);
	}
}

function CityModClientHUDChat::sendMessage(%this) {
	%input = CMClient_HUD.gui["chat"].child("input");
	%message = %input.getValue();

	if(%message $= "") {
		return;
	}

	commandToServer('messagesent', stripTrailingSpaces(stripMLControlChars(%message)));
	%input.setValue("");
}

function CityModClientHUDChat::addChatMessage(%this, %name, %prefix, %suffix, %message) {
	%line = (%prefix !$= "" ? "\c7" @ %prefix : "") @ "\c3" @ %name @ (%suffix !$= "" ? "\c7" @ %suffix : "") @ "\c6:" SPC %message;
	%this.addLine(%line);
}

function CityModClientHUDChat::addLine(%this, %line) {
	%scroll = CMClient_HUD.gui["chat"].child("scroll");
	%list = %scroll.child("list");

	%listCount = %list.getCount();
	%ypos = %listCount > 0 ? %list.getObject(%listCount - 1).getPositionY() + %list.getObject(%listCount - 1).getExtentH() - 2 : 0;

	%list.add(%messagegui = new GuiMLTextCtrl("_message" @ %listCount) {
		profile = "BlockChatTextSize2Profile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "2" SPC %ypos;
		extent = %list.getExtentW() SPC "20";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		lineSpacing = "2";
		allowColorChars = "1";
		maxChars = "-1";
		text = %line;
		maxBitmapHeight = "-1";
		selectable = "1";
		autoResize = "1";
	});

	%messagegui.forceReflow();
	%list.setExtentH(%ypos + %messagegui.getExtentH());

	%scroll.scrollToBottom();
}

function CityModClientHUDChat::scrubNewChatSO(%this) {
	if(!isObject(NewChatSO)) {
		return;
	}

	for(%i = 0; %i < NewChatSO.head; %i++) {
		%this.addLine(NewChatSO.line[%i]);
	}
}