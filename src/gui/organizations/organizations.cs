// ============================================================
// Project          -      CityMod
// Description      -      Organizations GUI Code
// ============================================================
// Sections
//   1: Client Commands
//   2: Gui Methods
// ============================================================

exec("Add-Ons/Client_CityMod/res/gui/CMOrganizations.gui");

// ============================================================
// Section 1 - Client Commands
// ============================================================

function clientcmdCM_Organizations_clearOrganizations() {
	CMOrganizationsGui_organizationsList.deleteAll();
	CMOrganizationsGui_organizationsList.resize(1, 1, 331, 305);
}

function clientcmdCM_Organizations_addOrganization(%id, %type, %open, %owner, %name, %members, %jobs, %isMember) {
	if(!strLen(%id) || !strLen(%type) || !strLen(%open) || !strLen(%owner) || !strLen(%name) || !strLen(%members) || !strLen(%jobs) || !strLen(%isMember)) {
		return;
	}

	%listGUI = CMOrganizationsGui_organizationsList;

	%gui = new GuiBitmapBorderCtrl("_organizaton" @ %listGUI.getCount()) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "314 64";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "314 64";
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
			position = "6 6";
			extent = "24 24";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 34";
			extent = "24 24";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapCtrl("_type") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "10 10";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/" @ strLwr(%type);
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiBitmapCtrl("_entrance") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "10 38";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/" @ (%open ? "open" : "application");
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
			position = "32 2";
			extent = "6 64";
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

		new GuiTextCtrl("_name") {
			profile = "CMTextLargeBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "40 2";
			extent = "224 20";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %name;
			maxLength = "255";
		};

		new GuiTextCtrl() {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "40 18";
			extent = "37 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "Owner:";
			maxLength = "255";
		};

		new GuiTextCtrl() {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "40 31";
			extent = "48 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "Members:";
			maxLength = "255";
		};

		new GuiTextCtrl() {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "40 44";
			extent = "70 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "Job Openings:";
			maxLength = "255";
		};

		new GuiTextCtrl("_owner") {
			profile = "CMTextTinyProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "80 18";
			extent = "26 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %owner;
			maxLength = "255";
		};

		new GuiTextCtrl("_members") {
			profile = "CMTextTinyProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "91 31";
			extent = "8 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %members;
			maxLength = "255";
		};

		new GuiTextCtrl("_jobs") {
			profile = "CMTextTinyProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "113 44";
			extent = "8 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %jobs;
			maxLength = "255";
		};

		new GuiBitmapButtonCtrl() {
			profile = "CMButtonSmallProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "257 35";
			extent = "52 24";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = (%isMember ? "CMOrganizationsGui.manageOrganization(" @ %id @ ");" : "CMOrganizationsGui.joinOrganization(" @ %id @ ");");
			text = (%isMember ? "View" : (%open ? "Join" : "Apply"));
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

	CMOrganizationsGui_totalOrganizations.setText("<just:center>" @ (firstWord(stripMLControlChars(CMOrganizationsGui_totalOrganizations.getValue())) + 1) SPC "Organizations");
	CMOrganizationsGui_totalJobs.setText("<just:center>" @ (firstWord(stripMLControlChars(CMOrganizationsGui_totalJobs.getValue())) + %jobs) SPC "Job Opportunities");
}

function clientcmdCM_Organizations_openApplicationForm(%name, %applicantName, %isCriminal, %applicantSkills) {
	if(!strLen(%name) || !strLen(%applicantName) || !strLen(%isCriminal) || !strLen(%applicantSkills)) {
		return;
	}

	CMOrganizationsGui_organizationApplication.child("name").setText("<just:center><font:Verdana Bold:16><color:777777>" @ %name);
	CMOrganizationsGui_organizationApplication.child("applicantName").setText("<just:right><font:Verdana:14><color:444444>" @ %applicantName);
	CMOrganizationsGui_organizationApplication.child("criminal").setValue(%isCriminal);

	%applicantSkills = Stringify::parse(%applicantSkills);

	if(!isObject(%applicantSkills)) {
		return;
	}

	CMOrganizationsGui_organizationApplication.skills = %applicantSkills;
	CMOrganizationsGui_organizationApplication.setVisible(1);
}

function clientcmdCM_Organizations_addApplicationJob(%id, %name) {
	if(!strLen(%id) || !strLen(%name)) {
		return;
	}

	%listGUI = CMOrganizationsGui_applicationJobList;

	%gui = new GuiBitmapBorderCtrl("_job" @ %listGUI.getCount()) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "267 25";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "267 25";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";
		};

		new GuiMLTextCtrl("_name") {
			profile = "GuiMLTextProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 5";
			extent = "220 14";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<font:Verdana Bold:14><color:444444>" @ %name;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiBitmapButtonCtrl("_infoButton") {
			profile = "CMButtonProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "228 5";
			extent = "15 15";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMOrganizationsGui.viewApplicationJobDescription(" @ %id @ ");";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			bitmap = "Add-Ons/Client_CityMod/res/ui/infoButton/infoButton";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "91 173 255 255";
		};

		new GuiRadioCtrl("_selectorButton") {
			profile = "CMRadioButtonProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "248 6";
			extent = "13 13";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMOrganizationsGui.selectApplicationJob(" @ %id @ ", " @ "\"" @ %name @ "\");";
			text = " ";
			groupNum = "2";
			buttonType = "RadioButton";
		};
	};

	%listGUI.addListGuiObject(%gui);
}

function clientcmdCM_Organizations_addApplicationSkill(%name, %description, %reqLevel) {
	if(!strLen(%name) || !strLen(%description) || !strLen(%reqLevel)) {
		return;
	}

	%applicantLevel = CMOrganizationsGui_organizationApplication.skills.get(%name).get("level");
	%listGUI = CMOrganizationsGui_applicationJobSkillsList;

	%gui = new GuiBitmapBorderCtrl("_skill" @ %listGUI.getCount()) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "92 46";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "92 46";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "248 248 248 255";
		};

		new GuiSwatchCtrl("_indicator") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "43 30";
			extent = "48 15";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = (%applicantLevel >= %reqLevel ? "76 162 76" : "178 0 0") SPC "255";
		};

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "43 30";
			extent = "49 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "46 46";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiBitmapCtrl("_icon") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 6";
			extent = "34 34";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/skills/" @ (isFile("Add-Ons/Client_CityMod/res/gui/skills/" @ strLwr(%name)) ? %name : "unknown");
			wrap = "0";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};

		new GuiTextCtrl("_requiredLevel") {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "51 2";
			extent = "30 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "Lvl" SPC %reqLevel;
			maxLength = "255";
		};

		new GuiMLTextCtrl("_applicantLevel") {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "51 16";
			extent = "30 12";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<color:777777>Lvl" SPC %appLevel;
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};
		
		new GuiBitmapButtonCtrl("_button") {
			profile = "BlankButtonProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 6";
			extent = "34 34";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMOrganizationsGui.viewApplicationSkillInfo(" @ %name @ ", " @ %description @ ");";
			text = " ";
			groupNum = "-1";
			buttonType = "PushButton";
			lockAspectRatio = "0";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			mKeepCached = "0";
			mColor = "255 255 255 255";
		};
	};

	%listGUI.addListGuiObject(%gui);

	%prereqsGui = CMOrganizationsGui_organizationApplication.child("prereqs");
	%prereqsText = getWord(stripMLControlChars(%prereqsGui.getText()), 2);
	%meetsReqs = (%applicantLevel >= %reqLevel ? (%prereqsText $= "No" ? "No" : "Yes") : "No");
	%prereqsGui.setText("<just:center><font:Verdana Bold:14><color:444444>Meets Prerequisites? <font:Verdana:14>" @ %meetsReqs);
}

// ============================================================
// Section 2 - Gui Methods
// ============================================================

function CMOrganizationsGui::onWake(%this) {
	CMOrganizationsGui_organizationApplication.selectedJob = "";
	CMOrganizationsGui_organizationApplication.organizationID = "";
	CMOrganizationsGui_organizationApplication.confirmedSelection = "";
	CMOrganizationsGui_organizationApplication.setVisible(0);
	CMOrganizationsGui_organizationCreation.setVisible(0);
	CMOrganizationsGui_totalOrganizations.setText("<just:center>0 Organizations");
	CMOrganizationsGui_totalJobs.setText("<just:center>0 Job Opportunities");
	clientcmdCM_Organizations_clearOrganizations();

	commandToServer('CM_Organizations_requestOrganizations');
}

function CMOrganizationsGui::openOrganizationCreationForm(%this) {
	CMOrganizationsGui_organizationCreationName.setValue("");
	CMOrganizationsGui_organizationCreationType1.setValue(0);
	CMOrganizationsGui_organizationCreationType2.setValue(0);
	CMOrganizationsGui_organizationCreation.setVisible(1);
}

function CMOrganizationsGui::createOrganizationFrontend(%this) {
	if(CMOrganizationsGui_organizationCreationName.getValue() $= "") {
		pushCMDialog("OK", "You have to input a name for the organization!");
		return;
	}

	if(!CMOrganizationsGui_organizationCreationType1.getValue() && !CMOrganizationsGui_organizationCreationType2.getValue()) {
		pushCMDialog("OK", "You have to select the organization's type!");
		return;
	}

	pushCMDialog(
		"YESNO",
		"Are you sure you want to create an organization with the following input?\n\nName:" SPC CMOrganizationsGui_organizationCreationName.getValue() @ "\nType:" SPC (CMOrganizationsGui_organizationCreationType1.getValue() ? "Group" : "Company"),
		"CMOrganizationsGui.createOrganization();"
	);
}

function CMOrganizationsGui::createOrganization(%this) {
	%name = CMOrganizationsGui_organizationCreationName.getValue();
	%type = (CMOrganizationsGui_organizationCreationType1.getValue() ? "group" : "company");
	commandToServer('CM_Organizations_createOrganization', %name, %type);
	CMOrganizationsGui_organizationCreation.setVisible(0);
}

function CMOrganizationsGui::filterOrganizations(%this) {
	%text = CMOrganizationsGui_searchFilter.getValue();
	%organizations = 0;
	%jobs = 0;

	for(%i = 0; %i < CMOrganizationsGui_organizationsList.getCount(); %i++) {
		%object = CMOrganizationsGui_organizationsList.getObject(%i);

		if((%text !$= "") && (%text !$= "Search Filter") && !searchString(%object.child("name").getValue(), %text)) {
			%object.setVisible(0);
		} else {
			if(!%object.isVisible()) {
				%object.setVisible(1);
			}

			%organizations++;
			%jobs += %object.child("jobs").getValue();
		}
	}

	CMOrganizationsGui_totalOrganizations.setText("<just:center>" @ %organizations SPC "Organizations");
	CMOrganizationsGui_totalJobs.setText("<just:center>" @ %jobs SPC "Job Opportunities");
	CMOrganizationsGui_organizationsList.resizeListGui();
}

function CMOrganizationsGui::manageOrganization(%this, %id) {
	commandToServer('CM_Organizations_manageOrganization', %id);
}

function CMOrganizationsGui::joinOrganization(%this, %id) {
	commandToServer('CM_Organizations_joinOrganization', %id);
}

function CMOrganizationsGui::viewApplicationSkillInfo(%this, %name, %description) {
	pushCMDialog(
		"OK",
		%name @ "\n\n" @ %description
	);
}

function CMOrganizationsGui::selectApplicationJob(%this, %id, %name) {
	CMOrganizationsGui_organizationApplication.selectedJob = %id;
	CMOrganizationsGui_organizationApplication.child("selectedJob").setText("<font:Verdana Bold:16><color:444444>Selected Job:<font:Verdana:13>" SPC %name);
}

function CMOrganizationsGui::submitApplication(%this) {
	if(CMOrganizationsGui_organizationApplication.selectedJob $= "") {
		pushCMDialog("OK", "You have to select a job for which the application is submitted!");
		return;
	}

	if((getWord(stripMLControlChars(CMOrganizationsGui_organizationApplication.child("prereqs")), 2) !$= "Yes") && !CMOrganizationsGui_organizationApplication.confirmedSelection) {
		pushCMDialog("OK", "You do not meet the skill prerequisites for this job, and, as a result, may have a greater chance of not being accepted.  You may wish to review your job choice before applying.  However, if you still wish to submit the application, close this dialog and press the 'Submit Application' button again.");
		CMOrganizationsGui_organizationApplication.confirmedSelection = true;
		return;
	}

	commandToServer('CM_Organizations_sendApplication', CMOrganizationsGui_organizationApplication.organizationID, CMOrganizationsGui_organizationApplication.selectedJob);
	CMOrganizationsGui_organizationApplication.setVisible(0);
}
