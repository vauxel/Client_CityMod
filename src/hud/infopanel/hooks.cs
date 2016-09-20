// ============================================================
// Project          -      CityMod
// Description      -      Info Panel Hooks Code
// ============================================================
// Sections
//   1: Keybinds
//   2: Function Hooks
// ============================================================

addKeyBind("CityMod", "Toggle Info Panel", "CMClient_HUD_InfoPanel_Toggle");

// ============================================================
// Section 1 - Keybinds
// ============================================================

function CMClient_HUD_InfoPanel_Toggle(%i) {
	if(!%i) {
		return;
	}

	if(!$CMClient::ConnectedToCMServer) {
		pushCMDialog("OK", "You can't open the CityMod Info Panel when you're not in a server running the CityMod Gamemode!");
		return;
	}

	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("infopanel") || (CMClient_HUD.gui["infopanel"].isTransitioning == true)) {
		return;
	}

	if(CMClient_HUD.componentExists("chat") && (CMClient_HUD.gui["chat"].isTransitioning == true)) {
		return;
	}

	if(!CMClient_HUD.isComponentOnScreen("infopanel")) { // Show Info Panel
		setScrollMode(0); // Hide PaintGUI

		commandToServer('CM_Infopanel_requestSkillsets');
		CMClient_HUD.setComponentShown("infopanel", true, "left", "easeInOutSine");

		if(CMClient_HUD.componentExists("chat")) {
			CMClient_HUD.chat.anchorToInfopanel();
		}
	} else { // Hide Info Panel
		CMClient_HUD.setComponentShown("infopanel", false, "left", "easeInOutSine");

		if(CMClient_HUD.componentExists("chat")) {
			CMClient_HUD.chat.anchorToInfopanel();
		}
	}
}

// ============================================================
// Section 2 - Function Hooks
// ============================================================

package CityModClient_InfoPanelHooks {
	function CMClient_HUD::onInitialize(%this) {
		parent::onInitialize(%this);

		%this.addComponent("infopanel", new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "110 900";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "20 20 20 120";

			new GuiSwatchCtrl("_avatar") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 5";
				extent = "100 90";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 120";
					defCameraRotation_X = "0";
					defCameraRotation_Y = "0.5";
					defCameraRotation_Z = "3.5";
					defLightDirection_X = "0";
					defLightDirection_Y = "0.2";
					defLightDirection_Z = "0.2";
					defOrbitDistance = "6";
					defFov = "16";

				new GuiObjectView("_preview") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "0 0";
					extent = "100 200";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					cameraZRot = "0";
					forceFOV = "16";
					lightDirection = "0 0.2 0.2";
					lightColor = "0.600000 0.580000 0.500000 1.000000";
					ambientColor = "0.300000 0.300000 0.300000 1.000000";
				};
			};

			new GuiSwatchCtrl("_statsHeader") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 97";
				extent = "100 19";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 120";

				new GuiMLTextCtrl("_name") {
					profile = "GuiMLTextProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "4 0";
					extent = "92 18";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					lineSpacing = "2";
					allowColorChars = "0";
					maxChars = "-1";
					text = "<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>Blockhead";
					maxBitmapHeight = "-1";
					selectable = "1";
					autoResize = "1";
				};
			};

			new GuiSwatchCtrl("_stats") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 118";
				extent = "100 265";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "0 0 0 0";
			};

			new GuiSwatchCtrl("_movableArea") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 438";
				extent = "110 462";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "0 0 0 0";

				new GuiSwatchCtrl("_tasksHeader") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 0";
					extent = "100 19";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "20 20 20 120";

					new GuiMLTextCtrl("_name") {
						profile = "GuiMLTextProfile";
						horizSizing = "right";
						vertSizing = "bottom";
						position = "0 0";
						extent = "100 18";
						minExtent = "8 2";
						enabled = "1";
						visible = "1";
						clipToParent = "1";
						lineSpacing = "2";
						allowColorChars = "0";
						maxChars = "-1";
						text = "<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>Tasks";
						maxBitmapHeight = "-1";
						selectable = "1";
						autoResize = "1";
					};
				};

				new GuiSwatchCtrl("_tasks") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 21";
					extent = "100 175";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "20 20 20 120";

					new GuiScrollCtrl("_scroll") {
						profile = "ImpactScrollProfile";
						horizSizing = "relative";
						vertSizing = "height";
						position = "0 0";
						extent = "100 175";
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
							extent = "87 175";
							minExtent = "8 2";
							enabled = "1";
							visible = "1";
							clipToParent = "1";
							color = "0 0 0 0";
						};

						new GuiMLTextCtrl("_emptyText") {
							profile = "GuiMLTextProfile";
							horizSizing = "right";
							vertSizing = "bottom";
							position = "0 60";
							extent = "90 54";
							minExtent = "8 2";
							enabled = "1";
							visible = "1";
							clipToParent = "1";
							lineSpacing = "2";
							allowColorChars = "0";
							maxChars = "-1";
							text = "<just:center><shadow:2:2><shadowcolor:00000066><color:E2E2E2><font:Impact:18>You don\'t\nhave any\ntasks!";
							maxBitmapHeight = "-1";
							selectable = "1";
							autoResize = "1";
						};
					};
				};

				new GuiSwatchCtrl("_skillsetsHeader") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 201";
					extent = "100 19";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "20 20 20 120";

					new GuiMLTextCtrl("_name") {
						profile = "GuiMLTextProfile";
						horizSizing = "right";
						vertSizing = "bottom";
						position = "0 0";
						extent = "100 18";
						minExtent = "8 2";
						enabled = "1";
						visible = "1";
						clipToParent = "1";
						lineSpacing = "2";
						allowColorChars = "0";
						maxChars = "-1";
						text = "<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:18>Skillsets";
						maxBitmapHeight = "-1";
						selectable = "1";
						autoResize = "1";
					};
				};

				new GuiSwatchCtrl("_skillsets") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 222";
					extent = "100 175";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "20 20 20 120";

					new GuiScrollCtrl("_scroll") {
						profile = "ImpactScrollProfile";
						horizSizing = "relative";
						vertSizing = "height";
						position = "0 0";
						extent = "100 175";
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
							extent = "87 175";
							minExtent = "8 2";
							enabled = "1";
							visible = "1";
							clipToParent = "1";
							color = "0 0 0 0";
						};

						new GuiMLTextCtrl("_emptyText") {
							profile = "GuiMLTextProfile";
							horizSizing = "right";
							vertSizing = "bottom";
							position = "0 60";
							extent = "90 54";
							minExtent = "8 2";
							enabled = "1";
							visible = "1";
							clipToParent = "1";
							lineSpacing = "2";
							allowColorChars = "0";
							maxChars = "-1";
							text = "<just:center><shadow:2:2><shadowcolor:00000066><color:E2E2E2><font:Impact:18>You don\'t\nhave any\nskills!";
							maxBitmapHeight = "-1";
							selectable = "1";
							autoResize = "1";
						};
					};
				};

				new GuiSwatchCtrl("_openSkills") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 402";
					extent = "100 20";
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
						extent = "100 20";
						minExtent = "8 2";
						enabled = "1";
						visible = "1";
						clipToParent = "1";
						command = "openCMGui(\"Skills\");";
						text = "View All Skills";
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

				new GuiSwatchCtrl("_openInventory") {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 427";
					extent = "100 30";
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
						extent = "100 30";
						minExtent = "8 2";
						enabled = "1";
						visible = "1";
						clipToParent = "1";
						command = "CMClient_HUD_Inventory_Toggle(1);";
						text = "Inventory";
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
		});
	}

	function CMClient_HUD::onInitialized(%this) {
		parent::onInitialized(%this);

		%this.gui["infopanel"].child("avatar").child("preview").loadPlayer("CM_playerObject");
		%this.setComponentShown("infopanel", false, "left", "easeInOutSine");

		if(%this.componentExists("chat")) {
			%this.chat.anchorToInfopanel();
		}

		commandToServer('CM_Infopanel_requestStats');
	}

	function CMClient_HUD::onResized(%this) {
		parent::onResized(%this);

		%screenX = getWord(getRes(), 0);
		%screenY = getWord(getRes(), 1);
		%infopanelWidth = %this.gui["infopanel"].getExtentW();
		%movableAreaWidth = %this.gui["infopanel"].child("movableArea").getExtentW();
		%movableAreaHeight = %this.gui["infopanel"].child("movableArea").getExtentH();

		%this.gui["infopanel"].resize(-%infopanelWidth, 0, %infopanelWidth, %screenY);
		%this.gui["infopanel"].child("movableArea").resize(0, %screenY < 768 ? %screenY : (%screenY - %movableAreaHeight), %movableAreaWidth, %movableAreaHeight);

		%this.infopanel.schedule(0, "updateAvatarPreview");
	}
};