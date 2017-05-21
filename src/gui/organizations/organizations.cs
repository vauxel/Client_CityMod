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
			position = "202 35";
			extent = "52 24";
			minExtent = "8 2";
			enabled = "1";
			visible = %isMember ? "0" : "1";
			clipToParent = "1";
			command = "CMOrganizationsGui.joinOrganization(" @ %id @ ");";
			text = %open ? "Join" : "Apply";
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
			position = "257 35";
			extent = "52 24";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			command = "CMOrganizationsGui.manageOrganization(" @ %id @ ");";
			text = "View";
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

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "293 5";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = %isMember ? "1" : "0";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/star";
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

	%listGUI.addListGuiObject(%gui);

	CMOrganizationsGui_totalOrganizations.setText("<just:center>" @ (firstWord(stripMLControlChars(CMOrganizationsGui_totalOrganizations.getValue())) + 1) SPC "Organizations");
	CMOrganizationsGui_totalJobs.setText("<just:center>" @ (firstWord(stripMLControlChars(CMOrganizationsGui_totalJobs.getValue())) + %jobs) SPC "Job Opportunities");
}

function clientcmdCM_Organizations_viewAvailableJobs(%id, %name, %skills) {
	if(!strLen(%id) || !strLen(%name) || !strLen(%skills)) {
		return;
	}

	CMOrganizationsGui_jobs.organizationID = %id;
	CMOrganizationsGui_jobs.applicantSkills = Stringify::parse(%skills);
	CMOrganizationsGui_jobs.setText("Available Jobs for" SPC %name);
	CMOrganizationsGui_jobList.deleteAll();

	CMOrganizationsGui_jobs.setVisible(1);
}

function clientcmdCM_Organizations_addAvailableJob(%name, %description, %salary, %openings, %autoaccept) {
	if(!strLen(%name) || !strLen(%description) || !strLen(%salary) || !strLen(%openings) || !strLen(%autoaccept)) {
		return;
	}

	%listGUI = CMOrganizationsGui_jobsList;

	%gui = new GuiBitmapBorderCtrl("_job" @ %listGUI.getCount()) {
		profile = "CMBorderOneProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "155 52";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		jobName = %name;
		jobDescription = %description;
		jobSalary = %salary;
		jobOpenings = %openings;
		jobAutoAccept = %autoaccept;

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "155 52";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "255 255 255 255";
		};

		new GuiTextCtrl("_name") {
			profile = "CMTextLargeBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 1";
			extent = "137 20";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %name;
			maxLength = "255";
		};

		new GuiTextCtrl("_openings") {
			profile = "CMTextTinyProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "61 31";
			extent = "36 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %openings;
			maxLength = "255";
		};

		new GuiTextCtrl("_salary") {
			profile = "CMTextTinyProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "44 18";
			extent = "39 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "$" @ commaSeparateAmount(%salary);
			maxLength = "255";
		};

		new GuiTextCtrl() {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 18";
			extent = "35 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "Salary:";
			maxLength = "255";
		};

		new GuiTextCtrl() {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "6 31";
			extent = "52 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = "Difficulty:";
			maxLength = "255";
		};

		new GuiMouseEventCtrl("CMJobsMouseEventCtrl") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "155 52";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lockMouse = "0";
		};
	};

	%listGUI.addListGuiObject(%gui);
}

function clientcmdCM_Organizations_addAvailableJobSkill(%skillID, %skillset, %skill) {
	if(!strLen(%skillID) || !strLen(%skillset) || !strLen(%skill)) {
		return;
	}

	%unlocked = CMOrganizationsGui_jobs.applicantSkills.contains(%skillID);
	%listGUI = CMOrganizationsGui_jobSkillsList;

	%gui = new GuiBitmapBorderCtrl("_skill" @ %listGUI.getCount()) {
		profile = (%unlocked ? "CMBorderOneProfile" : "CMBorderThreeProfile");
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "267 37";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 0";
			extent = "267 37";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = (%unlocked ? "255 255 255 255" : "248 248 248 255");
		};

		new GuiTextCtrl("_skill") {
			profile = "CMTextLargeBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 1";
			extent = "257 20";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %skill;
			maxLength = "255";
		};

		new GuiTextCtrl("_skillset") {
			profile = "CMTextTinyProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "5 17";
			extent = "257 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %skillset;
			maxLength = "255";
		};

		new GuiBitmapCtrl("_icon") {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "236 6";
			extent = "24 24";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_CityMod/res/gui/icons/" @ (%unlocked ? "checkmark" : "xmark") @ ".png";
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

	%listGUI.addListGuiObject(%gui);
}

// ============================================================
// Section 2 - Gui Methods
// ============================================================

function CMOrganizationsGui::onWake(%this) {
	CMOrganizationsGui_jobs.organizationID = "";
	CMOrganizationsGui_jobs.setVisible(0);
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
	%text = CMOrganizationsGui_organizationSearchFilter.getValue();
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

function CMOrganizationsGui::filterJobs(%this) {
	%text = CMOrganizationsGui_jobsSearchFilter.getValue();

	for(%i = 0; %i < CMOrganizationsGui_jobsList.getCount(); %i++) {
		%object = CMOrganizationsGui_jobsList.getObject(%i);

		if((%text !$= "") && (%text !$= "Search Filter") && !searchString(%object.child("name").getValue(), %text)) {
			%object.setVisible(0);
		} else {
			if(!%object.isVisible()) {
				%object.setVisible(1);
			}
		}
	}

	CMOrganizationsGui_jobsList.resizeListGui();
}

function CMOrganizationsGui::manageOrganization(%this, %id) {
	commandToServer('CM_Organizations_manageOrganization', %id);
}

function CMOrganizationsGui::joinOrganization(%this, %id) {
	commandToServer('CM_Organizations_joinOrganization', %id);
}

function CMOrganizationsGui::showJobInfo(%this, %job) {
	CMOrganizationsGui_jobInfo.child("name").setText(%job.jobName);
	CMOrganizationsGui_jobInfo.child("description").setText(%job.jobDescription);
	CMOrganizationsGui_jobInfo.child("salary").setText("$" @ commaSeparateAmount(%job.jobSalary));
	CMOrganizationsGui_jobInfo.child("difficulty").setText(%job.jobDifficulty);
	CMOrganizationsGui_jobInfo.child("autoaccept").setText(%job.jobAutoAccept);
	CMOrganizationsGui_jobSkillsList.deleteAll();

	CMOrganizationsGui_jobInfo.child("applybutton").command = "CMOrganizationsGui.applyForJob(" @ %job.jobID @ ");";
	CMOrganizationsGui_jobInfo.setVisible(1);
}

function CMJobsMouseEventCtrl::onMouseDown(%this, %modifierKey, %mousePoint, %mouseClickCount) {
	if(isObject(%job = %this.parent())) {
		CMOrganizationsGui.showJobInfo(%job);
	}
}