// ============================================================
// Project          -      CityMod
// Description      -      Notification GUI Code
// ============================================================
// Sections
//   1: Client Commands
// ============================================================

exec("Add-Ons/Client_CityMod/res/gui/CMNotification.gui");

// ============================================================
// Section 1 - Client Commands
// ============================================================

function clientcmdCM_Notification_pushDialog(%type, %text, %callback, %altCallback) {
	pushCMDialog(%type, %text, %callback, %altCallback);
}

function pushCMDialog(%type, %text, %callback, %altCallback) {
	if(!strLen(%type) || !strLen(%text)) {
		return;
	}

	openCMGui("Notification");

	CMNotificationGui_messageText.setText("<just:center>" @ %text);
	CMNotificationGui_messageText.forceReflow();

	%typeHeight = CMNotificationGui_messageText.getPositionY() + CMNotificationGui_messageText.getExtentH() + 5;

	if(%type $= "OK") {
		CMNotificationGui_okPane.resize(25, %typeHeight, 250, 32);
		CMNotificationGui_notificationDialog.resize(0, 0, 300, (%typeHeight + CMNotificationGui_okPane.getExtentH() + 8));

		if(strLen(%callback)) {
			CMNotificationGui_okButton.command = "closeCMGui(\"Notification\");" SPC %callback;
		} else {
			CMNotificationGui_okButton.command = "closeCMGui(\"Notification\");";
		}

		CMNotificationGui_yesnoPane.setVisible(0);
		CMNotificationGui_inputPane.setVisible(0);
		CMNotificationGui_choicePane.setVisible(0);
		CMNotificationGui_okPane.setVisible(1);
	} else if(%type $= "YESNO") {
		CMNotificationGui_yesnoPane.resize(25, %typeHeight, 250, 32);
		CMNotificationGui_notificationDialog.resize(0, 0, 300, (%typeHeight + CMNotificationGui_yesnoPane.getExtentH() + 8));

		CMNotificationGui_noButton.command = "closeCMGui(\"Notification\");";

		if(strLen(%callback)) {
			CMNotificationGui_yesButton.command = "closeCMGui(\"Notification\");" SPC %callback;
		} else {
			CMNotificationGui_yesButton.command = "closeCMGui(\"Notification\");";
		}

		if(strLen(%altCallback)) {
			CMNotificationGui_noButton.command = "closeCMGui(\"Notification\");" SPC %altCallback;
		} else {
			CMNotificationGui_noButton.command = "closeCMGui(\"Notification\");";
		}

		CMNotificationGui_okPane.setVisible(0);
		CMNotificationGui_inputPane.setVisible(0);
		CMNotificationGui_choicePane.setVisible(0);
		CMNotificationGui_yesnoPane.setVisible(1);
	} else if(%type $= "INPUT") {
		CMNotificationGui_inputPane.resize(25, %typeHeight, 250, 32);
		CMNotificationGui_notificationDialog.resize(0, 0, 300, (%typeHeight + CMNotificationGui_inputPane.getExtentH() + 8));

		if(strLen(%callback)) {
			CMNotificationGui_enterButton.command = "closeCMGui(\"Notification\");" SPC %callback;
		} else {
			CMNotificationGui_enterButton.command = "closeCMGui(\"Notification\");";
		}

		CMNotificationGui_okPane.setVisible(0);
		CMNotificationGui_yesnoPane.setVisible(0);
		CMNotificationGui_choicePane.setVisible(0);
		CMNotificationGui_inputPane.setVisible(1);
	} else if(%type $= "CHOICE") {
		if(!strLen(%callback)) {
			return;
		}

		CMNotificationGui_choicePane.deleteAll();

		%extentY = 0;
		for(%i = 0; %i < getFieldCount(%callback); %i++) {
			if(%i != 0) {
				%extentY += 2;
			}

			%buttonGUI = new GuiBitmapButtonCtrl("CMNotificationGui_choice" @ %i @ "Button") {
				profile = "CMButtonProfile";
				horizSizing = "center";
				vertSizing = "bottom";
				position = "21" SPC %extentY;
				extent = "208 32";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				text = getField(%callback, %i);
				groupNum = "-1";
				buttonType = "PushButton";
				bitmap = "Add-Ons/Client_CityMod/res/ui/button_wide/button";
				command = "closeCMGui(\"Notification\");" SPC getField(%altCallback, %i);
				lockAspectRatio = "0";
				alignLeft = "0";
				alignTop = "0";
				overflowImage = "0";
				mKeepCached = "0";
				mColor = "255 255 255 255";
			};

			CMNotificationGui_choicePane.add(%buttonGUI);
			%extentY += %buttonGUI.getExtentH();
		}

		CMNotificationGui_choicePane.resize(25, %typeHeight, 250, %extentY);
		CMNotificationGui_notificationDialog.resize(0, 0, 300, (%typeHeight + CMNotificationGui_choicePane.getExtentH() + 8));

		CMNotificationGui_okPane.setVisible(0);
		CMNotificationGui_yesnoPane.setVisible(0);
		CMNotificationGui_inputPane.setVisible(0);
		CMNotificationGui_choicePane.setVisible(1);
	} else {
		CMNotificationGui_notificationDialog.resize(0, 0, 300, %typeHeight + 5);

		CMNotificationGui_okPane.setVisible(0);
		CMNotificationGui_yesnoPane.setVisible(0);
		CMNotificationGui_inputPane.setVisible(0);
		CMNotificationGui_choicePane.setVisible(0);
	}

	CMNotificationGui_notificationDialog.setPosition(
		(getWord(getRes(), 0) / 2) - (CMNotificationGui_notificationDialog.getExtentW() / 2),
		(getWord(getRes(), 1) / 2) - (CMNotificationGui_notificationDialog.getExtentH() / 2)
	);
}