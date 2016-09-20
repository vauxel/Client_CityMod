// ============================================================
// Project          -      CityMod
// Description      -      Bank GUI Code
// ============================================================
// Sections
//   1: Client Commands
//   2: Gui Methods
// ============================================================

exec("Add-Ons/Client_CityMod/res/gui/CMBank.gui");

// ============================================================
// Section 1 - Client Commands
// ============================================================
function clientcmdCM_Bank_clearLedger() {
	CMBankGui_ledgerList.deleteAll();
	CMBankGui_ledgerList.setExtentH(1);
}

function clientcmdCM_Bank_setCredentials(%account, %pin) {
	CMBankGui_loginAccountNumber.setValue(%account);
	CMBankGui_loginPIN.setValue(%pin);
}

function clientcmdCM_Bank_loginSuccessful(%account, %pin, %blid, %name) {
	CMBankGui_loginWindow.setVisible(0);
	CMBankGui_window.setVisible(1);

	CMBankGui_window.account = %account;
	CMBankGui_window.pin = %pin;

	CMBankGui_credentialsContainer.child("name").setText("<just:right>" @ %name);
	CMBankGui_credentialsContainer.child("blid").setText("<just:right> BLID" SPC %blid);
	CMBankGui_credentialsContainer.child("account").setText("<just:right>#" @ %account);

	commandToServer('CM_Bank_requestBalance', %account, %pin);
	commandToServer('CM_Bank_requestLedger', %account, %pin);
}

function clientcmdCM_Bank_loginFailed() {
	pushCMDialog("OK", "Failed to Login\nIncorrect account number or PIN");
}

function clientcmdCM_Bank_setBalance(%amount) {
	CMBankGui_balanceAmount.setText("<just:center>" @ %amount);
}

function clientcmdCM_Bank_addLedgerRecord(%date, %title, %amount) {
	if(!strLen(%date) || !strLen(%title) || !strLen(%amount)) {
		return;
	}

	%listGUI = CMBankGui_ledgerList;
	%date = strReplace(%date, "/", " ");

	if(!isObject(%headingGUI = %listGUI.child("heading" @ stripChars(%date, " ")))) {
		switch(getWord(%date, 0)) {
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

		%headingGUI = new GuiSwatchCtrl("_heading" @ stripChars(%date, " ")) {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "1 0";
			extent = "304 16";
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
				extent = "304 14";
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
				extent = "304 13";
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
				extent = "304 6";
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
		%total = %headingGUI.child("amount").getText();
		%total = stripChars(%total, stripChars(%total, "1234")) + %amount;
		%headingGUI.child("amount").setText("<just:right>" @ (%total >= 0 ? "+$" : "-$") @ mAbs(%total));
	}

	%gui = new GuiBitmapBorderCtrl("_heading" @ %date @ "_record" @ %listGUI.getCount()) {
		profile = "CMBorderThreeProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1 0";
		extent = "304 23";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";

		new GuiSwatchCtrl("_bg") {
			profile = "GuiDefaultProfile";
			horizSizing = "relative";
			vertSizing = "relative";
			position = "0 0";
			extent = "304 23";
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

		new GuiBitmapBorderCtrl() {
			profile = "CMBorderThreeProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "20 0";
			extent = "30 23";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
		};

		new GuiMLTextCtrl("_date") {
			profile = "CMTextTinyBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "24 5";
			extent = "22 12";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			lineSpacing = "2";
			allowColorChars = "0";
			maxChars = "-1";
			text = "<color:555555>" @ ordinalNumber(getWord(%date, 1));
			maxBitmapHeight = "-1";
			selectable = "1";
			autoResize = "1";
		};

		new GuiMLTextCtrl("_title") {
			profile = "CMTextSmallBoldProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "52 4";
			extent = "247 13";
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
			extent = "276 13";
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
}

// ============================================================
// Section 2 - Gui Methods
// ============================================================
function CMBankGui::onWake(%this) {
	CMBankGui_window.setVisible(0);
	//CMBankGui_createWindow.setVisible(0);
	CMBankGui_loginWindow.setVisible(1);

	commandToServer('CM_Bank_requestCredentials');
}

function CMBankGui::login(%this) {
	commandToServer('CM_Bank_requestLogin', CMBankGui_loginAccountNumber.getValue(), CMBankGui_loginPIN.getValue());
}

function CMBankGui::depositAmount(%this) {
	commandToServer('CM_Bank_depositAmount', CMBankGui_loginAccountNumber.getValue(), CMBankGui_loginPIN.getValue(), CMBankGui_fundTransferAmount.getValue());
}

function CMBankGui::withdrawAmount(%this) {
	commandToServer('CM_Bank_withdrawAmount', CMBankGui_loginAccountNumber.getValue(), CMBankGui_loginPIN.getValue(), CMBankGui_fundTransferAmount.getValue());
}