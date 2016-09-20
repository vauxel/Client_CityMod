// ============================================================
// Project          -      CityMod
// Description      -      Organization Manager GUI Code
// ============================================================
// Sections
//   1: Client Commands
//   2: Gui Methods
// ============================================================

exec("Add-Ons/Client_CityMod/res/gui/CMOrganizationManager.gui");

// ============================================================
// Section 1 - Client Commands
// ============================================================

function clientcmdCM_Organizations_manageOrganization(%id, %name) {
	CMOrganizationManagerGui.organizationID = %id;
	CMOrganizationManagerGui.organizationName = %name;
	CMOrganizationManagerGui_window.text = %name SPC "Manager";

	if(CMOrganizationsGui.isAwake()) {
		closeCMGui("Organizations");
	}

	openCMGui("OrganizationManager");
}

function clientcmdCM_Organizations_clearMembers() {
	CMOrganizationManagerGui_membersList.deleteAll();
	CMOrganizationManagerGui_membersList.setExtentH(1);
}

function clientcmdCM_Organizations_clearJobs() {
	CMOrganizationManagerGui_jobsList.deleteAll();
	CMOrganizationManagerGui_jobsList.setExtentH(1);
}

// Deprecated
function clientcmdCM_Organizations_clearInvites() {
	CMOrganizationManagerGui_userInvitationsList.deleteAll();
	CMOrganizationManagerGui_userInvitationsList.setExtentH(1);
}

function clientcmdCM_Organizations_clearApplications() {
	CMOrganizationManagerGui_applicationsList.deleteAll();
	CMOrganizationManagerGui_applicationsList.setExtentH(1);
}

function clientcmdCM_Organizations_clearFinanceLedger() {
	CMOrganizationManagerGui_financeLedgerList.deleteAll();
	CMOrganizationManagerGui_financeLedgerList.setExtentH(1);
}

function clientcmdCM_Organizations_setUserPrivelegeLevel(%level) {
	if(!strLen(%level)) {
		return;
	}

	CMOrganizationManagerGui.userPrivelegeLevel = %level;
}

function clientcmdCM_Organizations_setGeneralInfo(%name, %type, %owner, %open, %hidden, %description) {
	if(!strLen(%name) || !strLen(%type) || !strLen(%owner) || !strLen(%open) || !strLen(%hidden) || !strLen(%description)) {
		return;
	}

	CMOrganizationManagerGui_generalPanel.child("nameValue").setText(%name);

	(%typeGui = CMOrganizationManagerGui_generalPanel.child("type")).child("value").setText("<just:center>" @ %type);
	(%ownerGui = CMOrganizationManagerGui_generalPanel.child("owner")).child("value").setText("<just:right>" @ %owner);
	(%openGui = CMOrganizationManagerGui_generalPanel.child("open")).child("value").setText("<just:right>" @ (%open ? "Off" : "On"));
	(%hiddenGui = CMOrganizationManagerGui_generalPanel.child("hidden")).child("value").setText("<just:right>" @ (%hidden ? "Hidden" : "Shown"));
	(%descriptionGui = CMOrganizationManagerGui_generalPanel.child("description")).child("value").setText("<just:right>" @ "... [" @ strLen(%description) @ "]");
	%descriptionGui.fullDescription = %description;

	if(CMOrganizationManagerGui.userPrivelegeLevel < 2) {
		CMOrganizationManagerGui_generalPanel.child("nameEdit").setVisible(0);
		%typeGui.setExtentW(134);

		%ownerGui.child("edit").setVisible(0);
		%ownerGui.child("divider").setVisible(0);
		%ownerGui.child("value").setPositionX(25);

		%openGui.child("edit").setVisible(0);
		%openGui.child("divider").setVisible(0);
		%openGui.child("value").setPositionX(25);

		%hiddenGui.child("edit").setVisible(0);
		%hiddenGui.child("divider").setVisible(0);
		%hiddenGui.child("value").setPositionX(25);

		%descriptionGui.child("edit").setVisible(0);
		%descriptionGui.child("divider").setVisible(0);
		%descriptionGui.child("value").setPositionX(25);
	} else {
		CMOrganizationManagerGui_generalPanel.child("nameEdit").setVisible(1);
		%typeGui.setExtentW(290);

		%ownerGui.child("edit").setVisible(1);
		%ownerGui.child("divider").setVisible(1);
		%ownerGui.child("value").setPositionX(5);

		%openGui.child("edit").setVisible(1);
		%openGui.child("divider").setVisible(1);
		%openGui.child("value").setPositionX(5);

		%hiddenGui.child("edit").setVisible(1);
		%hiddenGui.child("divider").setVisible(1);
		%hiddenGui.child("value").setPositionX(5);

		%descriptionGui.child("edit").setVisible(1);
		%descriptionGui.child("divider").setVisible(1);
		%descriptionGui.child("value").setPositionX(5);
	}
}

function clientcmdCM_Organizations_addMember(%bl_id, %name, %jobID, %jobName, %owner) {
	if(!strLen(%bl_id) || !strLen(%name) || ((!strLen(%jobID) || !strLen(%jobName)) && !strLen(%owner))) {
		return;
	}

	%canEdit = CMOrganizationManagerGui.userPrivelegeLevel >= 2 ? true : false;
	%listGUI = CMOrganizationManagerGui_membersList;

	%gui = new GuiBitmapBorderCtrl("_member" @ %listGUI.getCount()) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 0";
		extent = "271 34";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		memberBL_ID = %bl_id;
		memberName = %name;
		memberJob = %jobID;
		memberIsOwner = %owner == true ? true : false;

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "271 34";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";
		};

		new GuiMLTextCtrl("_name") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 3";
			extent = "230 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %name SPC "<font:Verdana:10>(BL_ID" SPC %bl_id @ ")";
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_job") {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 17";
			extent = "230 12";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %owner == true ? "Organization Owner" : (%jobName SPC "<font:Verdana:10>(#" @ %jobID @ ")");
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapButtonCtrl() {
			profile = "CMButtonSmallColoredProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "249 9";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = %canEdit ? 1 : 0;
			clipToParent = "1";
			command = "CMOrganizationManagerGui.kickMemberFrontend(" @ %blid @ ", \"" @ %name @ "\");";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "Add-Ons/Client_CityMod/res/ui/closeButton/closeButton";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "255 75 75 255";
		};

		new GuiBitmapButtonCtrl() {
			profile = "CMButtonSmallProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "187 5";
			extent = "60 24";
			minExtent = "8 2";
			enabled = "1";
			visible = %canEdit ? 1 : 0;
			clipToParent = "1";
			command = "CMOrganizationManagerGui.changeMemberJobFrontend(" @ %blid @ ", \"" @ %name @ "\");";
			text = "Reassign";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "Add-Ons/Client_CityMod/res/ui/button_small/button";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "248 248 248 255";
		};

		new GuiBitmapButtonCtrl() {
			profile = "CMButtonSmallProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "126 5";
			extent = "60 24";
			minExtent = "8 2";
			enabled = "1";
			visible = %canEdit ? 1 : 0;
			clipToParent = "1";
			command = "CMOrganizationManagerGui.viewMember(" @ %blid @ ");";
			text = "Info";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "Add-Ons/Client_CityMod/res/ui/button_small/button";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "248 248 248 255";
		};
	};

	%listGUI.addListGuiObject(%gui);

	%plural = %listGUI.getCount() == 1 ? false : true;
	CMOrganizationManagerGui_memberInfoText.setText("<just:center>There" SPC (%plural ? "are" : "is") SPC "currently" SPC %listGUI.getCount() SPC "member" @ (%plural ? "s" : "") SPC "in the organization");
}

function clientcmdCM_Organizations_changeOrganizationMemberJob(%bl_id, %jobID, %jobName) {
	if(!strLen(%bl_id) || !strLen(%jobID) || !strLen(%jobName)) {
		return;
	}

	for(%i = 0; %i < CMOrganizationManagerGui_membersList.getCount(); %i++) {
		if(CMOrganizationManagerGui_membersList.getObject(%i).memberBL_ID == %bl_id) {
			if(CMOrganizationManagerGui_membersList.getObject(%i).memberIsOwner) {
				continue;
			}

			%member = CMOrganizationManagerGui_membersList.getObject(%i);
			break;
		}
	}

	if(!isObject(%member)) {
		return;
	}

	%member.memberJob = %jobID;
	%member.child("job").setText(%jobName SPC "<font:Verdana:10>(#" @ %jobID @ ")");
}

function clientcmdCM_Organizations_deleteOrganizationMember(%bl_id) {
	if(!strLen(%bl_id)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_membersList;
	for(%i = 0; %i < %listGUI.getCount(); %i++) {
		if(%listGUI.getObject(%i).memberBL_ID == %bl_id) {
			if(%listGUI.getObject(%i).memberIsOwner) {
				continue;
			}

			%listGUI.deleteListGuiObject(%i);
			return;
		}
	}
}

function clientcmdCM_Organizations_setJobConstraints(%desclength, %salaryamount) {
	if(!strLen(%desclength) || !strLen(%salaryamount)) {
		return;
	}

	CMOrganizationManagerGui_jobModificationDescription.maxChars = %desclength;
	CMOrganizationManagerGui_jobModificationDescriptionMax.setText("Max Length:" SPC %desclength);
	CMOrganizationManagerGui_jobModificationSalary.command = "CMOrganizationManagerGui_jobModificationSalary.setText(mClamp(stripChars(CMOrganizationManagerGui_jobModificationSalary.getValue(), stripChars(CMOrganizationManagerGui_jobModificationSalary.getValue(), \"0123456789\")), 0, " @ %salaryamount @ "));";
}

function clientcmdCM_Organizations_setJobModification(%name, %description, %salary, %openings, %autoaccept) {
	if(!strLen(%name) || !strLen(%description) || !strLen(%salary) || !strLen(%openings) || !strLen(%autoaccept)) {
		return;
	}

	CMOrganizationManagerGui_jobModificationName.setText(%name);
	CMOrganizationManagerGui_jobModificationDescription.setText(%description);
	CMOrganizationManagerGui_jobModificationSalary.setText(%salary);
	CMOrganizationManagerGui_jobModificationOpenings.setText(pad(%openings, 4));

	if(%autoaccept == true) {
		CMOrganizationManagerGui_jobModificationAAOn.setValue(1);
		CMOrganizationManagerGui_jobModificationAAOff.setValue(0);
	} else {
		CMOrganizationManagerGui_jobModificationAAOn.setValue(0);
		CMOrganizationManagerGui_jobModificationAAOff.setValue(1);
	}
}

function clientcmdCM_Organizations_closeJobModification() {
	CMOrganizationManagerGui_applicantSkillsWindow.setVisible(0);
}

function clientcmdCM_Organizations_addJob(%jobID, %name) {
	if(!strLen(%jobID) || !strLen(%name)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_jobsList;

	%gui = new GuiBitmapBorderCtrl("_job" @ %jobID) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 0";
		extent = "124 65";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		jobID = %jobID;
		jobName = %name;

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "124 79";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";
		};

		new GuiMLTextCtrl("_name") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 3";
			extent = "114 26";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %name @ "<font:Verdana:10> (#" @ %jobID @ ")";
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiSwatchCtrl("_moveable") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "3 17";
			extent = "118 43";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "0 0 0 0";

			new GuiBitmapButtonCtrl() {
				profile = "CMButtonSmallProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "118 18";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.editJob(" @ %jobID @ ");";
				text = "Edit";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_regular/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "248 248 248 255";
			};

			new GuiBitmapButtonCtrl() {
				profile = "CMButtonSmallProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 17";
				extent = "60 26";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.editJobSkills(" @ %jobID @ ");";
				text = "Skills";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_small/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "248 248 248 255";
			};

			new GuiBitmapButtonCtrl() {
				profile = "CMButtonSmallProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "58 17";
				extent = "60 26";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.editJobTasks(" @ %jobID @ ");";
				text = "Tasks";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_small/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "248 248 248 255";
			};
		};
	};

	%listGUI.addListGuiObject(%gui);

	%nameGUI = %gui.child("name");
	%moveableGUI = %gui.child("moveable");

	%nameGUI.forceReflow();
	%moveableGUI.setPositionY(%nameGUI.getPositionY() + %nameGUI.getExtentH() + 1);
	%gui.setExtentH(%moveableGUI.getPositionY() + %moveableGUI.getExtentH() + 5);

	%plural = %listGUI.getCount() == 1 ? false : true;
	CMOrganizationManagerGui_jobsInfoText.setText("<just:center>There" SPC (%plural ? "are" : "is") SPC "currently" SPC %listGUI.getCount() SPC "total job" @ (%plural ? "s" : ""));
}

function clientcmdCM_Organizations_deleteJob(%jobID) {
	if(!strLen(%jobID)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_jobsList;
	for(%i = 0; %i < %listGUI.getCount(); %i++) {
		if(%listGUI.getObject(%i).jobID == %jobID) {
			%index = %i;
			break;
		}
	}

	if(!strLen(%index)) {
		return;
	}

	%listGUI.deleteListGuiObject(%index);
}

function clientcmdCM_Organizations_addJobGroupMember(%jobID, %jobName, %memberName) {
	if(!strLen(%jobID) || !strLen(%jobName) || !strLen(%memberName)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_jobsGroupList;

	if(!isObject(%jobGUI = %listGUI.child("job" @ %jobID))) {
		%addJob = true;
		%jobGUI = new GuiSwatchCtrl("_job" @ %jobID) {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 0";
			extent = "121 65";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "0 76 112 2";
			jobID = %jobID;
			jobName = %jobName;

			new GuiBitmapBorderCtrl("_header") {
				profile = "CMBorderThreeProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "121 20";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";

				new GuiSwatchCtrl("_bg") {
					profile = "GuiDefaultProfile";
					horizSizing = "relative";
					vertSizing = "relative";
					position = "0 0";
					extent = "121 20";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					color = "248 248 248 255";
				};

				new GuiMLTextCtrl("_name") {
					profile = "CMTextSmallBoldProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "5 2";
					extent = "111 13";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					lineSpacing = "2";
					allowColorChars = "0";
					maxChars = "-1";
					text = %jobName @ "<font:Verdana:10> (#" @ %jobID @ ")";
					maxBitmapHeight = "-1";
					selectable = "1";
					autoResize = "1";
				};
			};

			new GuiBitmapBorderCtrl("_body") {
				profile = "CMBorderTwoProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 16";
				extent = "121 19";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
			};
		};
	}

	%jobGUICount = %jobGUI.getCount();
	%ypos = %jobGUICount < 1 ? 3 : %jobGUI.getObject(%jobGUICount - 1).getPositionY() + 12;

	%jobGUI.child("body").add(new GuiMLTextCtrl("_member" @ %jobGUICount) {
		profile = "CMTextTinyBoldProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "5" SPC %ypos;
		extent = "111 12";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		lineSpacing = "2";
		allowColorChars = "0";
		maxChars = "-1";
		text = %memberName;
		maxBitmapHeight = "-1";
		selectable = "1";
		autoResize = "1";
	});

	%jobGUI.child("body").setExtentH(%ypos + 12 + 4);
	%jobGUI.setExtentH(%jobGUI.child("body").getPositionY() + (%ypos + 12 + 4));

	if(%addJob) {
		%listGUI.addListGuiObject(%jobGUI);
	} else {
		%listGUI.resizeListGui();
	}
}

function clientcmdCM_Organizations_addJobAllSkill(%skillset, %skillsetName, %skill, %skillName) {
	if(!strLen(%skillset) || !strLen(%skillsetName) || !strLen(%skill) || !strLen(%skillName)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_jobSkillRequirementsList1;

	if(!isObject(%skillsetGUI = %listGUI.child(%skillset))) {
		%addSkillset = true;
		%skillsetGUI = new GuiBitmapBorderCtrl("_" @ %skillset) {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "127 96";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			skillsetID = %skillset;
			skillsetName = %skillsetName;

			new GuiSwatchCtrl("_bg") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "127 96";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "248 248 248 255";
			};

			new GuiMLTextCtrl("_name") {
				profile = "CMTextSmallBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 3";
				extent = "117 13";
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
				position = "0 16";
				extent = "127 6";
				minExtent = "6 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				bitmap = "~/Client_CityMod/res/ui/dividerHorizontal";
				wrap = "0";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				keepCached = "0";
				mColor = "255 255 255 255";
				mMultiply = "0";
			};

			new GuiSwatchCtrl("_skills") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 22";
				extent = "117 68";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "0 0 0 0";
			};
		};
	}

	%skillsetskillsGUI = %skillsetGUI.child("skills");
	%skillsetskillsGUICount = %skillsetskillsGUI.getCount();
	%ypos = %skillsetskillsGUICount < 1 ? 0 : %skillsetskillsGUI.getObject(%skillsetskillsGUICount - 1).getPositionY() + %skillsetskillsGUI.getObject(%skillsetskillsGUICount - 1).getExtentH() + 2;

	%skillsetskillsGUI.add(new GuiBitmapBorderCtrl("_" @ %skill) {
		profile = "CMBorderOneProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0" SPC %ypos;
		extent = "117 33";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		skillID = %skill;
		skillName = %skillName;

		new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "115 33";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "255 255 255 255";
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
			text = %skillName;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "89 0";
			extent = "6 33";
			minExtent = "6 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "~/Client_CityMod/res/ui/dividerVertical";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiBitmapButtonCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "96 8";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMOrganizationManagerGui.addJobSkill(\"" @ %skill @ "\");";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "~/Client_CityMod/res/ui/editButton/editButton";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "51 153 255 255";
		};
	});

	%skillsetskillsGUI.setExtentH(%ypos + 33);

	%skillsetGUI.setExtentH(%skillsetskillsGUI.getPositionY() + (%ypos + 33) + 6);
	%skillsetGUI.child("bg").setExtentH(%skillsetGUI.getExtentH());

	if(%addSkillset) {
		%listGUI.addListGuiObject(%skillsetGUI);
	} else {
		%listGUI.resizeListGui();
	}

	CMOrganizationManagerGui_jobSkillRequirementsTotal1.totalCount++;
	CMOrganizationManagerGui_jobSkillRequirementsTotal1.setText("<just:right><color:777777>" @ CMOrganizationManagerGui_jobSkillRequirementsTotal1.totalCount);
}

function clientcmdCM_Organizations_addJobSkill(%skillset, %skillsetName, %skill, %skillName) {
	if(!strLen(%skillset) || !strLen(%skillsetName) || !strLen(%skill) || !strLen(%skillName)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_jobSkillRequirementsList2;

	if(!isObject(%skillsetGUI = %listGUI.child(%skillset))) {
		%addSkillset = true;
		%skillsetGUI = new GuiBitmapBorderCtrl("_" @ %skillset) {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "127 96";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			skillsetID = %skillset;
			skillsetName = %skillsetName;

			new GuiSwatchCtrl("_bg") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "127 96";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "248 248 248 255";
			};

			new GuiMLTextCtrl("_name") {
				profile = "CMTextSmallBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 3";
				extent = "117 13";
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
				position = "0 16";
				extent = "127 6";
				minExtent = "6 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				bitmap = "~/Client_CityMod/res/ui/dividerHorizontal";
				wrap = "0";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				keepCached = "0";
				mColor = "255 255 255 255";
				mMultiply = "0";
			};

			new GuiSwatchCtrl("_skills") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 22";
				extent = "117 68";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "0 0 0 0";
			};
		};
	}

	%skillsetskillsGUI = %skillsetGUI.child("skills");
	%skillsetskillsGUICount = %skillsetskillsGUI.getCount();
	%ypos = %skillsetskillsGUICount < 1 ? 0 : %skillsetskillsGUI.getObject(%skillsetskillsGUICount - 1).getPositionY() + %skillsetskillsGUI.getObject(%skillsetskillsGUICount - 1).getExtentH() + 2;

	%skillsetskillsGUI.add(new GuiBitmapBorderCtrl("_" @ %skill) {
		profile = "CMBorderOneProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0" SPC %ypos;
		extent = "117 33";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		skillID = %skill;
		skillName = %skillName;

		new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "115 33";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "255 255 255 255";
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
			text = %skillName;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "89 0";
			extent = "6 33";
			minExtent = "6 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "~/Client_CityMod/res/ui/dividerVertical";
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiBitmapButtonCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "96 8";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMOrganizationManagerGui.removeJobSkill(\"" @ %skill @ "\");";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "~/Client_CityMod/res/ui/closeButton/closeButton";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "255 75 75 255";
		};
	});

	%skillsetskillsGUI.setExtentH(%ypos + 33);

	%skillsetGUI.setExtentH(%skillsetskillsGUI.getPositionY() + (%ypos + 33) + 6);
	%skillsetGUI.child("bg").setExtentH(%skillsetGUI.getExtentH());

	if(%addSkillset) {
		%listGUI.addListGuiObject(%skillsetGUI);
	} else {
		%listGUI.resizeListGui();
	}

	CMOrganizationManagerGui_jobSkillRequirementsTotal2.totalCount++;
	CMOrganizationManagerGui_jobSkillRequirementsTotal2.setText("<color:777777>" @ CMOrganizationManagerGui_jobSkillRequirementsTotal2.totalCount);
}

// Deprecated
function clientcmdCM_Organizations_addOrganizationInvitation(%bl_id) {
	if(!strLen(%bl_id)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_userInvitationsList;

	%GUIprefix = "CMOrganizationManagerGui_userInvitation" @ %listGUI.getCount();
	%gui = new GuiBitmapBorderCtrl(%GUIprefix @ "Frame") {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 0";
		extent = "271 21";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		// Inner-Background Swatch
		new GuiSwatchCtrl(%GUIprefix @ "BG") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "271 21";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";

			// Invitation Name Text
			new GuiMLTextCtrl(%GUIprefix @ "Name") {
				profile = "CMTextTinyBoldProfile";
				horizSizing = "right";
				vertSizing = "center";
				position = "6 4";
				extent = "246 12";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "BLID #" @ %bl_id;
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			// Invitation Revoke Button
			new GuiBitmapButtonCtrl(%GUIprefix @ "RevokeButton") {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "255 5";
				extent = "11 11";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.revokeInvitation(" @ %bl_id @ ");";
				text = " ";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/closeButton/closeButton";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "255 100 100 255";
			};
		};
	};

	%listGUI.addListGuiObject(%gui);
}

function clientcmdCM_Organizations_addApplication(%bl_id, %name, %jobID, %jobname) {
	if(!strLen(%bl_id) || !strLen(%name) || !strLen(%jobID) || !strLen(%jobname)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_applicationsList;

	%GUIprefix = "CMOrganizationManagerGui_application" @ %listGUI.getCount();
	%gui = new GuiBitmapBorderCtrl(%GUIprefix @ "Frame") {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 0";
		extent = "270 46";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl(%GUIprefix @ "BG") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "270 46";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";

			new GuiMLTextCtrl(%GUIprefix @ "Header") {
				profile = "CMTextSmallBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "5 3";
				extent = "261 13";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = %name @ "<font:Verdana:12> | Applying for <font:Verdana Bold:12>" @ %jobname @ "<font:Verdana:10> (#" @ %jobID @ ")";
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiBitmapButtonCtrl(%GUIprefix @ "AcceptButton") {
				profile = "CMButtonSmallColoredProfile";
				horizSizing = "right";
				vertSizing = "top";
				position = "174 19";
				extent = "46 22";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.acceptApplication(" @ %bl_id @ ");";
				text = "Accept";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_regular/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "0 171 102 255";
			};

			new GuiBitmapButtonCtrl(%GUIprefix @ "DeclineButton") {
				profile = "CMButtonSmallColoredProfile";
				horizSizing = "right";
				vertSizing = "top";
				position = "219 19";
				extent = "46 22";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.declineApplication(" @ %bl_id @ ");";
				text = "Decline";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_regular/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "255 75 75 255";
			};

			new GuiBitmapButtonCtrl(%GUIprefix @ "RequiredSkillsButton") {
				profile = "CMButtonSmallProfile";
				horizSizing = "right";
				vertSizing = "top";
				position = "5 19";
				extent = "85 22";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.viewApplicationJobSkills(" @ %jobID @ ");";
				text = "Required Skills";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_regular/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "255 255 255 255";
			};

			new GuiBitmapButtonCtrl(%GUIprefix @ "ApplicantSkillsButton") {
				profile = "CMButtonSmallProfile";
				horizSizing = "right";
				vertSizing = "top";
				position = "89 19";
				extent = "85 22";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				command = "CMOrganizationManagerGui.viewApplicantSkills(" @ %bl_id @ "," SPC %name @ ");";
				text = "Applicant Skills";
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_regular/button";
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "255 255 255 255";
			};
		};
	};

	%listGUI.addListGuiObject(%gui);
}

function clientcmdCM_Organizations_addApplicantSkill(%name, %description, %level, %requiredlevel) {
	if(!strLen(%name) || !strLen(%description) || !strLen(%level) || !strLen(%requiredlevel)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_applicationsList;

	%GUIprefix = "CMOrganizationManagerGui_applicantSkillsSkill" @ %listGUI.getCount();
	%gui = new GuiBitmapBorderCtrl(%GUIprefix @ "Frame") {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "2 0";
		extent = "282 50";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl(%GUIprefix @ "BG") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "282 50";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";

			new GuiBitmapBorderCtrl() {
				profile = "CMBorderThreeProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "47 32";
				extent = "235 50";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
			};
			new GuiMLTextCtrl(%GUIprefix @ "Description") {
				profile = "CMTextTinyProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "53 18";
				extent = "227 12";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = %description;
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};
			new GuiMLTextCtrl(%GUIprefix @ "Name") {
				profile = "CMTextMediumBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "53 4";
				extent = "227 14";
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
			new GuiBitmapBorderCtrl() {
				profile = "CMBorderThreeProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "50 50";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";

				new GuiBitmapCtrl(%GUIprefix @ "Icon") {
					profile = "GuiDefaultProfile";
					horizSizing = "center";
					vertSizing = "center";
					position = "7 7";
					extent = "35 35";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					bitmap = "Add-Ons/Client_CityMod/res/gui/skills/" @ strLwr(%name);
					wrap = "0";
					lockAspectRatio = "0";
					alignLeft = "0";
					alignTop = "0";
					overflowImage = "0";
					keepCached = "0";
					mColor = "255 255 255 255";
					mMultiply = "0";
				};
			};
			new GuiMLTextCtrl(%GUIprefix @ "LevelText") {
				profile = "CMTextTinyBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "54 33";
				extent = "227 12";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "Lvl" SPC %level SPC "<font:Verdana:12>" @ ((%level < %requiredlevel) ? "<color:FF0000>" : "") @ "(Req. Lvl" SPC %requiredlevel @ ")";
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};
		};
	};

	%listGUI.addListGuiObject(%gui);

	CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.totalSkills++;

	if(%level >= %requiredlevel) {
		CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.skillsMet++;
	}

	CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.setText("0/0");

	if(CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.skillsMet == CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.totalSkills) {
		CMOrganizationManagerGui_applicantSkillsMeetsRequirementsText.setText("Yes");
	} else {
		CMOrganizationManagerGui_applicantSkillsMeetsRequirementsText.setText("No");
	}
}

function clientcmdCM_Organizations_setBalance(%amount) {
	if(!strLen(%amount)) {
		return;
	}

	CMOrganizationManagerGui_financeBalance.setText("<just:center>$" @ %amount);
}

function clientcmdCM_Organizations_addLedgerRecord(%month, %title, %amount, %currentMonth) {
	if(!strLen(%month) || !strLen(%title) || !strLen(%amount) || !strLen(%currentMonth)) {
		return;
	}

	%listGUI = CMOrganizationManagerGui_financeLedgerList;

	if(!isObject(%headingGUI = %listGUI.child("heading" @ %month))) {
		switch(%month) {
			case 0: %monthName = "January";
			case 1: %monthName = "February";
			case 2: %monthName = "March";
			case 3: %monthName = "April";
			case 4: %monthName = "May";
			case 5: %monthName = "June";
			case 6: %monthName = "July";
			case 7: %monthName = "August";
			case 8: %monthName = "September";
			case 9: %monthName = "October";
			case 10: %monthName = "November";
			case 11: %monthName = "December";
			default: %monthName = "ERROR";
		}

		%headingGUI = new GuiSwatchCtrl("_heading" @ %month) {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 0";
			extent = "271 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "0 0 0 0";

			new GuiMLTextCtrl("_month") {
				profile = "CMTextMediumBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 -2";
				extent = "271 14";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = %monthName;
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiMLTextCtrl("_amount") {
				profile = "CMTextSmallBoldProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 -2";
				extent = "271 13";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:right>" @ (%amount >= 0 ? "+$" : "-$") @ mAbs(%amount);
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};

			new GuiBitmapCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 11";
				extent = "271 6";
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
		};

		%listGUI.addListGuiObject(%headingGUI);
	} else {
		%total = stripMLControlChars(%headingGUI.child("amount").getText()) + %amount;
		%headingGUI.child("amount").setText("<just:right>" @ (%total > 0 ? "+" : "") @ %total);
	}

	%gui = new GuiBitmapBorderCtrl("_heading" @ %month @ "_record" @ %listGUI.getCount()) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 0";
		extent = "271 23";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "271 23";
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
			position = "0 0";
			extent = "21 23";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapCtrl("_icon") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 5";
			extent = "12 12";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/" @ (%amount >= 0 ? "plus" : "minus");
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiMLTextCtrl("_title") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "23 4";
			extent = "244 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = %title;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_amount") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "23 4";
			extent = "244 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<just:right>" @ (%amount >= 0 ? "+$" : "-$") @ mAbs(%amount);
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};
	};

	%listGUI.addListGuiObject(%gui);

	if(%month == %currentMonth) {
		if(%amount > 0) {
			CMOrganizationManagerGui_financeCurrentContainer.profit += %amount;
			CMOrganizationManagerGui_financeCurrentContainer.child("profit").setText("$" @ CMOrganizationManagerGui_financeCurrentContainer.profit);
		} else if(%amount < 0) {
			CMOrganizationManagerGui_financeCurrentContainer.deficit += mAbs(%amount);
			CMOrganizationManagerGui_financeCurrentContainer.child("deficit").setText("$" @ CMOrganizationManagerGui_financeCurrentContainer.deficit);
		}

		CMOrganizationManagerGui_financeCurrentContainer.balance = CMOrganizationManagerGui_financeCurrentContainer.profit - CMOrganizationManagerGui_financeCurrentContainer.deficit;

		if(CMOrganizationManagerGui_financeCurrentContainer.balance < 0) {
			CMOrganizationManagerGui_financeCurrentContainer.child("balance").setText("-$" @ mAbs(CMOrganizationManagerGui_financeCurrentContainer.balance));
		} else {
			CMOrganizationManagerGui_financeCurrentContainer.child("balance").setText("$" @ CMOrganizationManagerGui_financeCurrentContainer.balance);
		}
	} else {
		if(CMOrganizationManagerGui_financePreviousContainer.month[%month] $= "") {
			CMOrganizationManagerGui_financePreviousContainer.month[%month] = (%amount > 0 ? %amount SPC %amount SPC 0 : (0 - %amount) SPC 0 SPC mAbs(%amount));
		} else {
			if(%amount > 0) {
				CMOrganizationManagerGui_financePreviousContainer.month[%month] = (getWord(CMOrganizationManagerGui_financePreviousContainer.month[%month], 0) + %amount) SPC (getWord(CMOrganizationManagerGui_financePreviousContainer.month[%month], 1) + %amount) SPC getWord(CMOrganizationManagerGui_financePreviousContainer.month[%month], 2);
			} else if(%amount < 0) {
				CMOrganizationManagerGui_financePreviousContainer.month[%month] = (getWord(CMOrganizationManagerGui_financePreviousContainer.month[%month], 0) + %amount) SPC getWord(CMOrganizationManagerGui_financePreviousContainer.month[%month], 1) SPC (getWord(CMOrganizationManagerGui_financePreviousContainer.month[%month], 2) + mAbs(%amount));
			}
		}
	}
}

// ============================================================
// Section 2 - Gui Methods
// ============================================================

function CMOrganizationManagerGui::onWake(%this) {
	CMOrganizationManagerGui_jobTasksEditWindow.setVisible(0);
	CMOrganizationManagerGui_jobSkillRequirementsWindow.setVisible(0);
	CMOrganizationManagerGui_applicantSkillsWindow.setVisible(0);

	CMOrganizationManagerGui.userPrivelegeLevel = 0;
	commandtoserver('CM_Organizations_requestUserPrivelegeLevel', %this.organizationID);

	%this.selectPanel("GENERAL");
}

function CMOrganizationManagerGui::leaveOrganizationFrontend(%this) {
	pushCMDialog(
		"YESNO",
		"Are you sure you want to leave the organization," SPC %this.organizationName @ "?",
		"CMOrganizationManagerGui.leaveOrganization();"
	);
}

function CMOrganizationManagerGui::editFieldFrontend(%this, %field) {
	switch$(%field) {
		case "name":
			pushCMDialog(
				"INPUT",
				"Please input a new name for the organization",
				"CMOrganizationManagerGui.editField(" @ %field @ ", $CMNotificationInput);"
			);
		case "owner":
			pushCMDialog(
				"INPUT",
				"Please input the BLID for the new owner of the organization",
				"CMOrganizationManagerGui.editField(" @ %field @ ", $CMNotificationInput);"
			);
		case "open":
			pushCMDialog(
				"CHOICE",
				"What setting would you like for the requirement of applications?",
				"On (Required)" TAB "Off (Not Required)",
				"CMOrganizationManagerGui.editField(" @ %field @ ", false);" TAB "CMOrganizationManagerGui.editField(" @ %field @ ", true);"
			);
		case "hidden":
			pushCMDialog(
				"CHOICE",
				"What setting would you like for the organization's visibility?",
				"Shown" TAB "Hidden",
				"CMOrganizationManagerGui.editField(" @ %field @ ", false);" TAB "CMOrganizationManagerGui.editField(" @ %field @ ", true);"
			);
		case "description":
			pushCMDialog(
				"YESNO",
				"The following is the organization's current description:\n\n\"" @ CMOrganizationManagerGui_generalPanel.child("description").fullDescription @ "\"\n\nWould you like to change it?",
				"pushCMDialog(\"INPUT\",\"Please input a new description for the organization\",\"CMOrganizationManagerGui.editField(\"" @ %field @ "\", $CMNotificationInput);\");"
			);
	}
}

function CMOrganizationManagerGui::kickMemberFrontend(%this, %bl_id, %name) {
	pushCMDialog(
		"YESNO",
		"Are you sure you want to kick" SPC %name SPC "(BL_ID" SPC %bl_id @ ")" SPC "from the organization?",
		"CMOrganizationManagerGui.kickMember(" @ %bl_id @ ");"
	);
}

function CMOrganizationManagerGui::changeMemberJobFrontend(%this, %bl_id, %name) {
	pushCMDialog(
		"INPUT",
		"Please input the ID of the Job that you want" SPC %name SPC "(BL_ID" SPC %bl_id @ ")" SPC "to be assigned to.",
		"CMOrganizationManagerGui.changeMemberJob(" @ %bl_id @ ", $CMNotificationInput);"
	);
}

function CMOrganizationManagerGui::createJobFrontend(%this) {
	pushCMDialog(
		"YESNO",
		"Are you sure you want to create a new Job?",
		"CMOrganizationManagerGui.createJob(" @ CMOrganizationManagerGui_jobCreationIDText.getValue() @ "," SPC CMOrganizationManagerGui_jobCreationNameText.getValue() @ ");"
	);
}

function CMOrganizationManagerGui::selectPanel(%this, %name) {
	CMOrganizationManagerGui_restrictedText.setVisible(0);

	if(%name $= "GENERAL") {
		CMOrganizationManagerGui_generalPanel.setVisible(1);
		CMOrganizationManagerGui_membersPanel.setVisible(0);
		CMOrganizationManagerGui_jobsPanel.setVisible(0);
		CMOrganizationManagerGui_applicationsPanel.setVisible(0);
		CMOrganizationManagerGui_financePanel.setVisible(0);

		commandtoserver('CM_Organizations_requestGeneralInfo', %this.organizationID);
	} else if(%name $= "MEMBERS") {
		CMOrganizationManagerGui_generalPanel.setVisible(0);
		CMOrganizationManagerGui_membersPanel.setVisible(1);
		CMOrganizationManagerGui_jobsPanel.setVisible(0);
		CMOrganizationManagerGui_applicationsPanel.setVisible(0);
		CMOrganizationManagerGui_financePanel.setVisible(0);

		CMOrganizationManagerGui_memberInfoText.setText("<just:center>There are currently 0 Members in the Organization");
		clientcmdCM_Organizations_clearMembers();

		commandtoserver('CM_Organizations_requestMembers', %this.organizationID);
	} else if(%name $= "JOBS") {
		CMOrganizationManagerGui_generalPanel.setVisible(0);
		CMOrganizationManagerGui_membersPanel.setVisible(0);
		CMOrganizationManagerGui_jobsPanel.setVisible(1);
		CMOrganizationManagerGui_applicationsPanel.setVisible(0);
		CMOrganizationManagerGui_financePanel.setVisible(0);

		CMOrganizationManagerGui_jobsInfoText.setText("<just:center>There are currently 0 total Jobs");
		clientcmdCM_Organizations_clearJobs();

		commandtoserver('CM_Organizations_requestJobs', %this.organizationID);
		commandtoserver('CM_Organizations_requestJobGroups', %this.organizationID);
	} else if(%name $= "APPLICATIONS") {
		CMOrganizationManagerGui_generalPanel.setVisible(0);
		CMOrganizationManagerGui_membersPanel.setVisible(0);
		CMOrganizationManagerGui_jobsPanel.setVisible(0);
		CMOrganizationManagerGui_applicationsPanel.setVisible(1);
		CMOrganizationManagerGui_financePanel.setVisible(0);

		if(CMOrganizationManagerGui.userPrivelegeLevel < 2) {
			CMOrganizationManagerGui_applicationsPanel.setVisible(0);
			CMOrganizationManagerGui_restrictedText.setVisible(1);
			return;
		}

		clientcmdCM_Organizations_clearApplications();

		commandtoserver('CM_Organizations_requestApplications', %this.organizationID);
	} else if(%name $= "FINANCE") {
		CMOrganizationManagerGui_generalPanel.setVisible(0);
		CMOrganizationManagerGui_membersPanel.setVisible(0);
		CMOrganizationManagerGui_jobsPanel.setVisible(0);
		CMOrganizationManagerGui_applicationsPanel.setVisible(0);
		CMOrganizationManagerGui_financePanel.setVisible(1);

		if(CMOrganizationManagerGui.userPrivelegeLevel < 2) {
			CMOrganizationManagerGui_financePanel.setVisible(0);
			CMOrganizationManagerGui_restrictedText.setVisible(1);
			return;
		}

		clientcmdCM_Organizations_clearFinanceLedger();

		commandtoserver('CM_Organizations_requestBalance', %this.organizationID);
		commandtoserver('CM_Organizations_requestLedger', %this.organizationID);
	}
}

function CMOrganizationManagerGui::leaveOrganization(%this) {
	commandtoserver('CM_Organizations_leaveOrganization', %this.organizationID);
}

function CMOrganizationManagerGui::showTypeInfo(%this, %type) {
	if(%type $= "group") {
		pushCMDialog(
			"OK",
			"A Group Organization is a normal, generic organization without the ability to sell wares or to execute other high-level bureaucratic functions."
		);
	} else if(%type $= "company") {
		pushCMDialog(
			"OK",
			"A Company Organization differs from a Group Organization in that it is able to sell wares, allow others to invest in it (stocks), and more extensively manage company-owned land.  However, changing the organization's type to become a company costs money."
		);
	}
}

function CMOrganizationManagerGui::editField(%this, %field, %value) {
	if(%value $= "") {
		pushCMDialog("OK", "You can't enter a blank value!");
		return;
	}

	commandtoserver('CM_Organizations_editField', %this.organizationID, %field, %value);
}

function CMOrganizationManagerGui::viewMember(%this, %bl_id) {
	// TO-DO
}

function CMOrganizationManagerGui::kickMember(%this, %bl_id) {
	commandtoserver('CM_Organizations_kickMember', %this.organizationID, %bl_id);
}

function CMOrganizationManagerGui::changeMemberJob(%this, %bl_id, %jobID) {
	if(!isNumber(%jobID)) {
		pushCMDialog(
			"OK",
			"You entered an invalid Job ID for BL_ID" SPC %bl_id SPC "to be assigned to!"
		);
		return;
	}

	commandtoserver('CM_Organizations_changeMemberJob', %this.organizationID, %bl_id, %jobID);
}

function CMOrganizationManagerGui::createJob(%this) {
	CMOrganizationManagerGui_jobModificationWindow.setVisible(1);
	CMOrganizationManagerGui_jobModificationWindow.newJob = true;
	CMOrganizationManagerGui_jobModificationWindow.jobID = "";
	CMOrganizationManagerGui_jobModificationWindow.setText("Create a New Job");
	CMOrganizationManagerGui_jobModificationConfirmation.setText("Confirm");

	CMOrganizationManagerGui_jobModificationName.setText("Default Organization");
	CMOrganizationManagerGui_jobModificationDescription.setText("This job's description has not been set");
	CMOrganizationManagerGui_jobModificationSalary.setText("0");
	CMOrganizationManagerGui_jobModificationOpenings.setText("0000");

	CMOrganizationManagerGui_jobModificationAAOn.setValue(1);
	CMOrganizationManagerGui_jobModificationAAOff.setValue(0);

	commandtoserver('CM_Organizations_requestJobConstraints');
}

function CMOrganizationManagerGui::editJob(%this, %jobID) {
	CMOrganizationManagerGui_jobModificationWindow.setVisible(1);
	CMOrganizationManagerGui_jobModificationWindow.newJob = false;
	CMOrganizationManagerGui_jobModificationWindow.jobID = %jobID;
	CMOrganizationManagerGui_jobModificationWindow.setText("Edit Job #" @ %jobID);
	CMOrganizationManagerGui_jobModificationConfirmation.setText("Confirm Changes");

	commandtoserver('CM_Organizations_requestJobConstraints');
	commandtoserver('CM_Organizations_requestJobModification', %this.organizationID, %jobID);
}

function CMOrganizationManagerGui::confirmJobModification(%this) {
	if(CMOrganizationManagerGui_jobModificationWindow.newJob == true) {
		%name = CMOrganizationManagerGui_jobModificationName.getValue();
		%description = CMOrganizationManagerGui_jobModificationDescription.getValue();
		%salary = CMOrganizationManagerGui_jobModificationSalary.getValue();
		%openings = CMOrganizationManagerGui_jobModificationOpenings.getValue();
		%autoaccept = CMOrganizationManagerGui_jobModificationAAOn.getValue() == 1 ? true : false;

		commandtoserver('CM_Organizations_createJob', %this.organizationID, %name, %description);
	} else {
		commandtoserver('CM_Organizations_updateJob', %this.organizationID, CMOrganizationManagerGui_jobModificationWindow.jobID);
	}
}

function CMOrganizationManagerGui::editJobSkills(%this, %jobID) {
	CMOrganizationManagerGui_jobSkillRequirementsWindow.setVisible(1);
	CMOrganizationManagerGui_jobSkillRequirementsWindow.jobID = %jobID;
	CMOrganizationManagerGui_jobSkillRequirementsWindow.setText("Edit Job #" @ %jobID @ "'s Prerequisite Skills");

	CMOrganizationManagerGui_jobSkillRequirementsTotal1.totalCount = 0;
	CMOrganizationManagerGui_jobSkillRequirementsTotal2.totalCount = 0;
	CMOrganizationManagerGui_jobSkillRequirementsTotal1.setText("<just:right><color:777777>0");
	CMOrganizationManagerGui_jobSkillRequirementsTotal2.setText("<color:777777>0");

	CMOrganizationManagerGui_jobSkillRequirementsList1.deleteAll();
	CMOrganizationManagerGui_jobSkillRequirementsList2.deleteAll();

	commandtoserver('CM_Organizations_requestJobSkills', %this.organizationID, %jobID);
	commandtoserver('CM_Organizations_requestJobAllSkills', %this.organizationID, %jobID);
}

function CMOrganizationManagerGui::jobHasSkill(%this, %skill) {
	for(%i = 0; %i < CMOrganizationManagerGui_jobSkillRequirementsList2.getCount(); %i++) {
		%skillsetList = CMOrganizationManagerGui_jobSkillRequirementsList2.getObject(%i).child("skills");
		for(%j = 0; %j < %skillsetList.getCount(); %j++) {
			if(%skillsetList.getObject(%j).skillID $= %skill) {
				return true;
			}
		}
	}

	return false;
}

function CMOrganizationManagerGui::filterSkillRequirements(%this) {
	%text = CMOrganizationManagerGui_jobSkillRequirementsFilter.getValue();

	for(%i = 0; %i < CMOrganizationManagerGui_jobSkillRequirementsList1.getCount(); %i++) {
		%skillset = CMOrganizationManagerGui_jobSkillRequirementsList1.getObject(%i);
		%skillsetSkills = %skillset.child("skills");

		for(%j = 0; %j < %skillsetSkills.getCount(); %j++) {
			%skill = %skillsetSkills.getObject(%j);

			if((%text !$= "") && (%text !$= "Search Filter") && !searchString(%skill.child("name").getValue(), %text)) {
				%skill.setVisible(0);
			} else {
				if(!%skill.isVisible()) {
					%skill.setVisible(1);
				}
			}
		}

		%skillsetSkills.resizeListGui();
		%skillset.setExtentH(%skillsetSkills.getPositionY() + %skillsetSkills.getExtentH() + 6);
		%skillset.child("bg").setExtentH(%skillset.getExtentH());
	}

	CMOrganizationManagerGui_jobSkillRequirementsList1.resizeListGui();
}

function CMOrganizationManagerGui::editJobTasks(%this, %jobID) {
	CMOrganizationManagerGui_jobTasksEditWindow.setVisible(1);
	CMOrganizationManagerGui_jobTasksEditWindow.jobID = %jobID;

	CMOrganizationManagerGui_jobTasksEditWindow.setText("Edit Job #" @ %jobID @ "'s Tasks");
	CMOrganizationManagerGui_jobTasksEditFilter.setText("Task Filter");

	commandtoserver('CM_Organizations_requestAllTasks');
	commandtoserver('CM_Organizations_requestJobTasks', %this.organizationID, CMOrganizationManagerGui_jobTasksEditWindow.jobID);
}

function CMOrganizationManagerGui::deleteJob(%this, %jobID) {
	commandtoserver('CM_Organizations_deleteJob', %this.organizationID, %jobID);
}

function CMOrganizationManagerGui::sendInvitation(%this, %bl_id) {
	%bl_id = (strLen(%bl_id) ? %bl_id : CMOrganizationManagerGui_inviteeBLIDText.getValue());
	commandtoserver('CM_Organizations_sendInvitation', %this.organizationID, %bl_id);
}

// Deprecated
function CMOrganizationManagerGui::revokeInvitation(%this, %bl_id) {
	commandtoserver('CM_Organizations_rekoveInvitation', %this.organizationID, %bl_id);
}

function CMOrganizationManagerGui::viewApplicationJobSkills(%this, %jobID) {
	// TO-DO
}

function CMOrganizationManagerGui::viewApplicantSkills(%this, %bl_id, %name) {
	CMOrganizationManagerGui_applicantSkillsWindow.setVisible(1);
	CMOrganizationManagerGui_applicantSkillsWindow.bl_id = %bl_id;
	CMOrganizationManagerGui_applicantSkillsWindow.setText("Applicant" SPC (%name $= "" ? ("BL_ID" SPC %bl_id) : %name) @ "'s Skills");

	CMOrganizationManagerGui_applicantSkillsMeetsRequirementsText.setText("N/A");
	CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.setText("0/0");
	CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.totalSkills = 0;
	CMOrganizationManagerGui_applicantSkillsRequirementsAmountText.skillsMet = 0;

	commandtoserver('CM_Organizations_requestApplicantSkills', %this.organizationID, %bl_id);
}

function CMOrganizationManagerGui::acceptApplication(%this, %bl_id) {
	commandtoserver('CM_Organizations_acceptApplication', %this.organizationID, %bl_id);
}

function CMOrganizationManagerGui::declineApplication(%this, %bl_id) {
	commandtoserver('CM_Organizations_declineApplication', %this.organizationID, %bl_id);
}

function CMOrganizationManagerGui::viewPreviousMonthFinance(%this, %direction) {
	if((%direction != -1) && (%direction != 1)) {
		return;
	}

	if((%direction == -1) && (CMOrganizationManagerGui_financePreviousContainer.currentMonth == 0)) {
		return;
	}

	if((%direction == 11) && (CMOrganizationManagerGui_financePreviousContainer.currentMonth == 11)) {
		return;
	}

	CMOrganizationManagerGui_financePreviousContainer.currentMonth += %direction;

	switch(CMOrganizationManagerGui_financePreviousContainer.currentMonth) {
		case 0: %monthName = "January";
		case 1: %monthName = "February";
		case 2: %monthName = "March";
		case 3: %monthName = "April";
		case 4: %monthName = "May";
		case 5: %monthName = "June";
		case 6: %monthName = "July";
		case 7: %monthName = "August";
		case 8: %monthName = "September";
		case 9: %monthName = "October";
		case 10: %monthName = "November";
		case 11: %monthName = "December";
		default: %monthName = "ERROR";
	}

	CMOrganizationManagerGui_financePreviousContainer.child("month").setText(%monthName);

	if(getWord(CMOrganizationManagerGui_financePreviousContainer.month[CMOrganizationManagerGui_financePreviousContainer.currentMonth], 0) < 0) {
		CMOrganizationManagerGui_financePreviousContainer.child("balance").setText("-$" @ suffixAmount(mAbs(getWord(CMOrganizationManagerGui_financePreviousContainer.month[CMOrganizationManagerGui_financePreviousContainer.currentMonth], 0), 1)));
	} else {
		CMOrganizationManagerGui_financePreviousContainer.child("balance").setText("$" @ suffixAmount(getWord(CMOrganizationManagerGui_financePreviousContainer.month[CMOrganizationManagerGui_financePreviousContainer.currentMonth], 0), 1));
	}

	CMOrganizationManagerGui_financePreviousContainer.child("profit").setText("$" @ suffixAmount(getWord(CMOrganizationManagerGui_financePreviousContainer.month[CMOrganizationManagerGui_financePreviousContainer.currentMonth], 0), 1));
	CMOrganizationManagerGui_financePreviousContainer.child("deficit").setText("$" @ suffixAmount(getWord(CMOrganizationManagerGui_financePreviousContainer.month[CMOrganizationManagerGui_financePreviousContainer.currentMonth], 0), 1));
}