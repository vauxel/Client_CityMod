// ============================================================
// Project          -      CityMod
// Description      -      Custom Chat Hooks Code
// ============================================================
// Sections
//   1: Keybinds
//   2: Function Hooks
// ============================================================

addKeyBind("CityMod", "Toggle Chat", "CMClient_HUD_Chat_Toggle");

// ============================================================
// Section 1 - Keybinds
// ============================================================

function CMClient_HUD_Chat_Toggle(%i) {
	if(!%i) {
		return;
	}

	if(!$CMClient::ConnectedToCMServer) {
		pushCMDialog("OK", "You can't open the CityMod Custom Chat when you're not in a server running the CityMod Gamemode!");
		return;
	}

	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("chat") || (CMClient_HUD.gui["chat"].isTransitioning == true)) {
		return;
	}

	if(CMClient_HUD.componentExists("infopanel") && (CMClient_HUD.gui["infopanel"].isTransitioning == true)) {
		return;
	}

	if(!CMClient_HUD.isComponentOnScreen("chat")) { // Show Chat
		CMClient_HUD.setComponentShown("chat", true, "top", "easeInOutSine");
	} else { // Hide Chat
		CMClient_HUD.setComponentShown("chat", false, "top", "easeInOutSine");
	}
}

// ============================================================
// Section 2 - Function Hooks
// ============================================================

package CityModClient_ChatHooks {
	function CMClient_HUD::onInitialize(%this) {
		parent::onInitialize(%this);

		%this.addComponent("chat", new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "400 200";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "20 20 20 60";

			new GuiScrollCtrl("_scroll") {
				profile = "ImpactScrollProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "400 180";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				willFirstRespond = "0";
				hScrollBar = "alwaysOff";
				vScrollBar = "alwaysOn";
				constantThumbHeight = "0";
				childMargin = "0 0";
				rowHeight = "40";
				columnWidth = "30";

				new GuiSwatchCtrl("_list") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "385 1";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "0 0 0 0";
				};
			};

			new GuiTextEditCtrl("_input") {
				profile = "CMChatTextEditProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 180";
				extent = "400 20";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				altCommand = "CMClient_HUD.chat.sendMessage();";
				text = "";
				maxLength = "120";
				escapeCommand = "CMClient_HUD.chat.focusChat(false);";
				historySize = "0";
				password = "0";
				tabComplete = "0";
				sinkAllKeyEvents = "0";
			};
		});
	}

	function CMClient_HUD::onInitialized(%this) {
		parent::onInitialized(%this);

		%this.chat.focusChat(false);
		%this.chat.scrubNewChatSO();
	}

	function CMClient_HUD::onResized(%this) {
		parent::onResized(%this);
		%this.gui["chat"].setPosition(0, 0);
	}

	function NewChatSO::addLine(%this, %line) {
		parent::addLine(%this, %line);

		if(isObject(CMClient_HUD) && CMClient_HUD.componentExists("chat")) {
			CMClient_HUD.chat.addLine(%line);
		}
	}

	function GlobalChat(%i) {
		if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("chat")) {
			return parent::GlobalChat(%i);
		}

		CMClient_HUD.chat.focusChat(true);
	}

	function TeamChat(%i) {
		if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("chat")) {
			return parent::TeamChat(%i);
		}

		CMClient_HUD.chat.focusChat(true);
	}

	function ToggleCursor(%i) {
		parent::ToggleCursor(%i);

		if(%i) {
			// Hotfix
			if(MouseToolTip.isVisible()) {
				MouseToolTip.setVisible(0);
			}
		}
	}
};