// ============================================================
// Project          -      CityMod
// Description      -      Notification HUD Code
// ============================================================
// Sections
//   1: Functions
//   2: Hooks
// ============================================================

// ============================================================
// Section 1 - Functions
// ============================================================

function clientcmdCM_Notification_addNotification(%text, %icon) {
	CMClient_HUD.notification.addToQueue(%text, %icon);
}

function CityModClientHUDNotification::addToQueue(%this, %text, %icon) {
	%this.notificationQueue.add(new ScriptObject() {
		text = %text;
		icon = (%icon $= "" ? "info" : %icon);
	});

	%isMoving = false;

	for(%i = 0; %i < %this.gui.getCount(); %i++) {
		%notification = %this.gui.getObject(%i);

		if(%notification.state $= "IN") {
			%isMoving = true;
			break;
		}
	}

	if(%this.gui.getCount() <= 0 || !%isMoving) {
		%this.addFromQueue();
	}
}

function CityModClientHUDNotification::getGuiFromQueue(%this, %index) {
	if(!isObject(%notification = %this.notificationQueue.getObject(%index))) {
		return;
	}

	%gui = new GuiSwatchCtrl() {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0" SPC %this.gui.getExtentH();
		extent = "256 50";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = "0 0 0 0";

		new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "0 2";
			extent = "256 48";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "20 20 20 80";

			new GuiSwatchCtrl() {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "48 48";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "20 20 20 60";

				new GuiBitmapCtrl() {
					profile = "GuiDefaultProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "8 8";
					extent = "32 32";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					bitmap = "Add-Ons/Client_CityMod/res/gui/icons/notification/" @ %notification.icon;
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

			new GuiMLTextCtrl("_text") {
				profile = "GuiMLTextProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "52 4";
				extent = "200 16";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<color:FFFFFF><font:Impact:16>" @ %notification.text;
				maxBitmapHeight = "-1";
				selectable = "1";
				autoResize = "1";
			};
		};
	};

	return %gui;
}

function CityModClientHUDNotification::addFromQueue(%this) {
	if(%this.notificationQueue.getCount() <= 0) {
		return;
	}

	%notification = %this.notificationQueue.getObject(0);
	%gui = %this.getGuiFromQueue(0);

	%this.notificationQueue.remove(%notification);
	%notification.delete();

	alxPlay(CMClient_NotificationSound);

	%gui.state = "IN";
	%this.gui.add(%gui);
	%this.step();
}

function CityModClientHUDNotification::step(%this) {
	cancel(%this.notificationStep);

	%stepTime = 10;
	%pushUp = 0;

	for(%i = 0; %i < %this.gui.getCount(); %i++) {
		%notification = %this.gui.getObject(%i);

		switch$(%notification.state) {
			case "IN":
				if((%notification.getPositionY() + %notification.getExtentH()) > %this.gui.getExtentH()) {
					%notification.setPositionY(%notification.getPositionY() - 2);
					%pushUp = %notification.getPositionY();
				} else {
					%notification.holdTime = 3000 / %stepTime; // 3000ms
					%notification.state = "HOLD";

					%this.addFromQueue();
				}
			case "HOLD":
				if(%notification.holdTime > 0) {
					%notification.holdTime--;
				} else {
					%notification.state = "OUT";
				}
			case "OUT":
				if(%notification.getPositionX() < %this.gui.getExtentW()) {
					%notification.setPositionX(%notification.getPositionX() + ((%notification.getPositionX() / %stepTime) / 2) + 1);
				} else {
					%notification.state = "DONE";
				}
			case "DONE":
				%notification.delete();
		}
	}

	if(%pushUp != 0) {
		for(%j = 0; %j < %this.gui.getCount(); %j++) {
			%notification2 = %this.gui.getObject(%j);

			if(%notification2.getPositionY() < %pushUp) {
				%notification2.setPositionY(%notification2.getPositionY() - 2);
			}
		}
	}

	if(%this.gui.getCount() > 0) {
		%this.notificationStep = %this.schedule(%stepTime, "step");
	}
}

// ============================================================
// Section 2 - Hooks
// ============================================================

package CityModClient_NotificationHooks {
	function CMClient_HUD::onInitialize(%this) {
		parent::onInitialize(%this);

		%this.addComponent("notification", new GuiSwatchCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = (getWord(getRes(), 0) - 256) SPC "0";
			extent = "256" SPC getWord(getRes(), 1);
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			color = "0 0 0 0";
		});

		%this.notification.notificationQueue = new ScriptGroup();
	}

	function CMClient_HUD::onResized(%this) {
		parent::onResized(%this);
		%this.gui["notification"].setPosition(getWord(getRes(), 0) - %this.gui["notification"].getExtentW(), getWord(getRes(), 1) - %this.gui["notification"].getExtentH());
	}
};