// ============================================================
// Project          -      CityMod
// Description      -      Skills GUI Code
// ============================================================
// Sections
//   1: Client Commands
//   2: Gui Methods
// ============================================================

exec("Add-Ons/Client_CityMod/res/gui/CMSkills.gui");

// ============================================================
// Section 1 - Client Commands
// ============================================================
function clientcmdCM_Skills_clearSkillsets() {
	CMSkillsGui_skillsetList.deleteAll();
	CMSkillsGui_skillsetList.resize(1, 1, 456, 351);
}

function clientcmdCM_Skills_addSkillset(%id, %name, %points, %level, %xppercent) {
	if(!strLen(%id) || !strLen(%name) || !strLen(%points) || !strLen(%level) || !strLen(%xppercent)) {
		return;
	}

  %listGUI = CMSkillsGui_skillsetList;

	%gui = new GuiSwatchCtrl("_" @ %id) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "150 332";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "0 112 112 2";
		skillsetID = %id;
		skillsetName = %name;
		skillsetPoints = %points;
		skillsetXP = %xppercent;

		new GuiBitmapBorderCtrl("_info") {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "150 37";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";

			new GuiSwatchCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "150 37";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "248 248 248 255";
			};

			new GuiBitmapBorderCtrl() {
				profile = "CMBorderThreeProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 19";
				extent = "150 18";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
			};

			new GuiMLTextCtrl("_name") {
				profile = "CMTextLargeBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 2";
				extent = "140 16";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = %name;
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiMLTextCtrl("_points") {
				profile = "CMTextTinyBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 4";
				extent = "140 12";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:right>" @ %points SPC "Pts";
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiProgressCtrl("_xp") {
				profile = "CMProgressProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 22";
				extent = "108 10";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
			};

			new GuiMLTextCtrl("_level") {
				profile = "CMTextTinyBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 21";
				extent = "140 12";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:right>Lvl" SPC pad(%level, 2);
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};
		};

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderOneProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 39";
			extent = "150 293";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";

			new GuiSwatchCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "150 313";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "255 255 255 255";
			};
		};

		new GuiScrollCtrl("_scroll") {
			profile = "CMScrollProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "3 42";
			extent = "144 287";
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
				position = "1 1";
				extent = "144 285";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "0 0 0 0";
				listSpacing = "3";
			};
		};
	};

	%listGUI.addListGuiObject(%gui, true);
	//%gui.child("info").child("xp").setValue(%xppercent / 100);

	commandToServer('CM_Skills_requestSkills', %id);
}

function clientcmdCM_Skills_addSkill(%skillset, %id, %name, %cost, %description, %reqs) {
	if(!strLen(%skillset) || !strLen(%id) || !strLen(%name) || !strLen(%cost) || !strLen(%description) || !strLen(%reqs)) {
		return;
	}

	if(!isObject(%listGUI = CMSkillsGui_skillsetList.child(%skillset).child("scroll").child("list"))) {
		return;
	}

	%unlocked = inWords(CMSkillsGui.playerSkills, (%skillset @ ":" @ %id));
	%unlockable = true;

	if(%reqs !$= "none") {
		for(%i = 0; %i < getWordCount(%reqs); %i++) {
			if(!inWords(CMSkillsGui.playerSkills, getWord(%reqs, %i))) {
				%unlockable = false;
				break;
			}
		}
	}

	%gui = new GuiBitmapBorderCtrl("_" @ %id) {
		profile = %unlocked ? "CMBorderTwoProfile" : (%unlockable ? "CMBorderOneProfile" : "CMBorderThreeProfile");
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "127 33";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		skillSet = %skillset;
		skillID = %id;
		skillName = %name;
		skillCost = %cost;
		skillDesc = %description;
		skillReqs = %reqs;
		skillUnlocked = %unlocked;
		skillUnlockable = %unlockable;

		new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "127 33";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = (%unlocked || %unlockable) ? "255 255 255 255" : "248 248 248 255";
		};

		new GuiMLTextCtrl("_name") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 3";
			extent = "92 26";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %name;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_cost") {
			profile = "CMTextLargeBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "105 1";
			extent = "16 29";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<font:Verdana Bold:30>" @ %cost;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "97 0";
			extent = "6 33";
			minExtent = "6 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/ui/dividerVertical";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiMouseEventCtrl("CMSkillsMouseEventCtrl") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "127 33";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lockMouse = "0";
		};
	};

	%listGUI.addListGuiObject(%gui);
}

function clientcmdCM_Skills_addPanelSkill(%skillsetName, %id, %name, %cost, %reqs, %unlocked, %unlockable) {
	if(!strLen(%skillsetName) || !strLen(%id) || !strLen(%name) || !strLen(%cost) || !strLen(%reqs) || !strLen(%unlocked) || !strLen(%unlockable)) {
		return;
	}

	%listGUI = CMSkillsGui_skillsPanel.child("reqsScroll").child("list");

	%gui = new GuiBitmapBorderCtrl("_req" @ %listGUI.getCount()) {
		profile = %unlocked ? "CMBorderTwoProfile" : (%unlockable ? "CMBorderOneProfile" : "CMBorderThreeProfile");
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "175 33";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "175 33";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = (%unlocked || %unlockable) ? "255 255 255 255" : "248 248 248 255";
		};

		new GuiMLTextCtrl("_name") {
			profile = "CMTextMediumBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 3";
			extent = "165 14";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %name;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_skillset") {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 16";
			extent = "165 12";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %skillsetName;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "145 0";
			extent = "6 33";
			minExtent = "6 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/ui/dividerVertical";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiMLTextCtrl("_cost") {
			profile = "CMTextLargeBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "153 1";
			extent = "16 29";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<font:Verdana Bold:30>" @ %cost;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};
	};

	%listGUI.addListGuiObject(%gui);

	if(CMSkillsGui_skillsPanel.skillUnlockable == true) {
		if(!%unlocked) {
			CMSkillsGui_skillsPanel.skillUnlockable = false;
			CMSkillsGui_skillsPanel.child("unlockable1").setText("<just:center>Skill Not Unlockable");
			CMSkillsGui_skillsPanel.child("unlockable2").setBitmap("Add-Ons/Client_CityMod/res/gui/icons/xmark");
			CMSkillsGui_skillsPanel.child("unlockable3").setBitmap("Add-Ons/Client_CityMod/res/gui/icons/xmark");
		}
	}
}

function clientcmdCM_Skills_addPlayerSkill(%skill) {
	CMSkillsGui.playerSkills = ltrim(CMSkillsGui.playerSkills SPC %skill);
}

function clientcmdCM_Skills_showSkillUnlocked(%skillName) {
	CMSkillsGui_unlockedWindow.setVisible(1);
	CMSkillsGui_unlockedWindow.child("name").setText("<font:Verdana Bold:20><just:center>" @ %skillName);
	CMSkillsGui.bringToFront(CMSkillsGui_unlockedWindow);
}

function clientcmdCM_Skills_refreshSkills() {
	commandToServer('CM_Skills_requestPlayerSkills');
	clientcmdCM_Skills_clearSkillsets();
	commandToServer('CM_Skills_requestSkillsets');
}

// ============================================================
// Section 2 - Gui Methods
// ============================================================
function CMSkillsGui::onWake(%this) {
	CMSkillsGui.playerSkills = "";
	CMSkillsGui_skillsPanel.setVisible(0);
	CMSkillsGui_unlockedWindow.setVisible(0);
	clientcmdCM_Skills_clearSkillsets();

	commandToServer('CM_Skills_requestPlayerSkills');
	commandToServer('CM_Skills_requestSkillsets');
}

function CMSkillsGui::unlockSkillFrontend(%this, %skill) {
	if(%skill.skillUnlocked) {
		pushCMDialog("OK", "You have already unlocked this skill!");
		return;
	}

	if(CMSkillsGui_skillsPanel.skillUnlockable == false) {
		pushCMDialog("OK", "You don't meet the requirements for this skill!");
		return;
	}

	pushCMDialog(
		"YESNO",
		"Are you sure you want to unlock the skill, " @ %skill.skillName @ ", for" SPC %skill.skillCost SPC "Point" @ (%skill.skillCost == 1 ? "" : "s") @ "?",
		"CMSkillsGui.unlockSkill(" @ %skill.skillSet @ ", " @ %skill.skillID @ ");"
	);
}

function CMSkillsGui::unlockSkill(%this, %skillset, %skill) {
	commandToServer('CM_Skills_unlockSkill', %skillset, %skill);
}

function CMSkillsGui::toggleSkillPanelLock(%this) {
	CMSkillsGui_skillsPanel.panelLocked = !CMSkillsGui_skillsPanel.panelLocked;
	CMSkillsGui_lockIndicator.setVisible(CMSkillsGui_skillsPanel.panelLocked);
}

function CMSkillsGui::showSkillInPanel(%this, %skill) {
	if(CMSkillsGui_skillsPanel.panelLocked) {
		return;
	}

	if(CMSkillsGui_skillsPanel.hiding == true) {
		CMSkillsGui_skillsPanel.hiding = false;
	}

	CMSkillsGui_skillsPanel.setVisible(1);

	CMSkillsGui_skillsPanel.child("skillset").setText("<font:Verdana Bold:20>" @ CMSkillsGui_skillsetList.child(%skill.skillSet).skillsetName);
	CMSkillsGui_skillsPanel.child("name").setText(%skill.skillName);
	CMSkillsGui_skillsPanel.child("cost").setText("<just:right>Cost:<font:Verdana Bold:20>" @ %skill.skillCost);
	CMSkillsGui_skillsPanel.child("description").setText("Description: <font:Verdana:12>" @ %skill.skillDesc);
	CMSkillsGui_skillsPanel.child("description").forceReflow();

	CMSkillsGui_skillsPanel.child("descBox").resize(
		CMSkillsGui_skillsPanel.child("descBox").getPositionX(),
		CMSkillsGui_skillsPanel.child("descBox").getPositionY(),
		CMSkillsGui_skillsPanel.child("descBox").getExtentW(),
		CMSkillsGui_skillsPanel.child("description").getExtentH() + 5
	);

	%reqsBoxPosY = CMSkillsGui_skillsPanel.child("descBox").getPositionY() + CMSkillsGui_skillsPanel.child("descBox").getExtentH() + 3;
	CMSkillsGui_skillsPanel.child("reqsBox").resize(
		CMSkillsGui_skillsPanel.child("reqsBox").getPositionX(),
		%reqsBoxPosY,
		CMSkillsGui_skillsPanel.child("reqsBox").getExtentW(),
		CMSkillsGui_skillsPanel.getExtentH() - %reqsBoxPosY - 27
	);

	CMSkillsGui_skillsPanel.child("reqsScroll").resize(
		CMSkillsGui_skillsPanel.child("reqsBox").getPositionX() + 3,
		CMSkillsGui_skillsPanel.child("reqsBox").getPositionY() + 3,
		CMSkillsGui_skillsPanel.child("reqsBox").getExtentW() - 6,
		CMSkillsGui_skillsPanel.child("reqsBox").getExtentH() - 6
	);

	CMSkillsGui_skillsPanel.child("reqsBox").child("noneText").setPosition(
		CMSkillsGui_skillsPanel.child("reqsBox").child("noneText").getPositionX(),
		(CMSkillsGui_skillsPanel.child("reqsBox").getExtentH() / 2) - (CMSkillsGui_skillsPanel.child("reqsBox").child("noneText").getExtentH() / 2)
	);

	CMSkillsGui_skillsPanel.skillUnlockable = true;
	CMSkillsGui_skillsPanel.child("unlockable1").setText("<just:center>Skill Unlockable");
	CMSkillsGui_skillsPanel.child("unlockable2").setBitmap("Add-Ons/Client_CityMod/res/gui/icons/checkmark");
	CMSkillsGui_skillsPanel.child("unlockable3").setBitmap("Add-Ons/Client_CityMod/res/gui/icons/checkmark");

	CMSkillsGui_skillsPanel.child("reqsScroll").child("list").deleteAll();

	if(%skill.skillReqs $= "none") {
		CMSkillsGui_skillsPanel.child("reqsBox").child("noneText").setVisible(1);
	} else {
		CMSkillsGui_skillsPanel.child("reqsBox").child("noneText").setVisible(0);

		for(%i = 0; %i < getWordCount(%skill.skillReqs); %i++) {
			%req_skillid = strReplace(getWord(%skill.skillReqs, %i), ":", " ");
			%req_skill = getWord(%req_skillid, 1);
			%req_skillset = CMSkillsGui_skillsetList.child(getWord(%req_skillid, 0));

			for(%j = 0; %j < %req_skillset.child("scroll").child("list").getCount(); %j++) {
				%req_subskill = %req_skillset.child("scroll").child("list").getObject(%j);
				if(%req_subskill.skillID $= %req_skill) {
					clientcmdCM_Skills_addPanelSkill(%req_skillset.child("info").child("name").getText(), %req_subskill.skillID, %req_subskill.skillName, %req_subskill.skillCost, %req_subskill.skillReqs, %req_subskill.skillUnlocked, %req_subskill.skillUnlockable);
				}
			}
		}
	}
}

function CMSkillsGui::hideSkillPanel(%this) {
	if(CMSkillsGui_skillsPanel.panelLocked) {
		return;
	}

	if(CMSkillsGui_skillsPanel.hiding == false) {
		return;
	}

	CMSkillsGui_skillsPanel.hiding = false;
	CMSkillsGui_skillsPanel.setVisible(0);
}

function CMSkillsMouseEventCtrl::onMouseDown(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	if((%modifierKey == 16) || (%modifierKey == 32)) {
		CMSkillsGui.toggleSkillPanelLock();
	} else {
		if(isObject(%skill = %this.parent())) {
			CMSkillsGui.unlockSkillFrontend(%skill);
		}
	}
}

function CMSkillsMouseEventCtrl::onMouseEnter(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	CMSkillsGui.showSkillInPanel(%this.parent());
}

function CMSkillsMouseEventCtrl::onMouseLeave(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	CMSkillsGui_skillsPanel.hiding = true;
	CMSkillsGui.schedule(150, "hideSkillPanel");
}