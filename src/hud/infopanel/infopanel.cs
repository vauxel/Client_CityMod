// ============================================================
// Project          -      CityMod
// Description      -      Info Panel Code
// ============================================================

function clientcmdCM_Infopanel_refreshAvatar() {
	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("infopanel")) {
		return;
	}

	CMClient_HUD.infopanel.updateAvatarPreview();
}

function clientcmdCM_Infopanel_updateName(%name) {
	if(!strLen(%name)) {
		return;
	}

	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("infopanel")) {
		return;
	}

	%namegui = CMClient_HUD.gui["infopanel"].child("statsHeader").child("name");
	%namelen = strLen(%name);

	%fontsize = 18;
	%namegui.setText("<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:" @ %fontsize @ ">" @ %name);
	%namegui.forceReflow();

	while((%namegui.getExtentH() > 18) && (%fontsize > 12)) {
		%fontsize--;
		%namegui.setText("<just:center><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:" @ %fontsize @ ">" @ %name);
		%namegui.forceReflow();
	}
}

function clientcmdCM_Infopanel_updateStats(%stats) {
	if(!strLen(%stats)) {
		return;
	}

	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("infopanel")) {
		return;
	}

	if(!isObject(%stats = Stringify::parse(%stats))) {
		return;
	}

	for(%i = 0; %i < %stats.length; %i++) {
		%name = getField(%stats.value[%i], 0);
		%value = getFields(%stats.value[%i], 1);

		%statsGUI = CMClient_HUD.gui["infopanel"].child("stats");

		if(isObject(%statsGUI.child(strLwr(%name) @ "Name"))) {
			%statsGUI.child(strLwr(%name) @ "Name").setText("<shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>" @ %name @ ":");
			%statsGUI.child(strLwr(%name) @ "Value").setText("<just:right><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>" @ %value);
		} else {
			%lastobj = %statsGUI.getObject(%statsGUI.getCount() - 1);
			%ypos = isObject(%lastobj) ? %lastobj.getPositionY() + %lastobj.getExtentH() + 1 : 0;

			%statsGUI.add(new GuiMLTextCtrl("_" @ strLwr(%name) @ "Name") {
				profile = "GuiMLTextProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0" SPC %ypos;
				extent = "100 16";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>" @ %name @ ":";
				maxBitmapHeight = "0";
				selectable = "1";
				autoResize = "1";
			});

			%statsGUI.add(new GuiMLTextCtrl("_" @ strLwr(%name) @ "Value") {
				profile = "GuiMLTextProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0" SPC %ypos;
				extent = "100 16";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				lineSpacing = "2";
				allowColorChars = "0";
				maxChars = "-1";
				text = "<just:right><shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>" @ %value;
				maxBitmapHeight = "0";
				selectable = "1";
				autoResize = "1";
			});
		}
	}
}

function clientcmdCM_Infopanel_updateSkillsets(%skillsets) {
	if(!strLen(%skillsets)) {
		return;
	}

	if(!isObject(CMClient_HUD) || !CMClient_HUD.componentExists("infopanel")) {
		return;
	}

	%skillsetScroll = CMClient_HUD.gui["infopanel"].child("movableArea").child("skillsets").child("scroll");
	%listGUI = %skillsetScroll.child("list");

	%listGUI.deleteAll();

	if(%skillsets $= "none") {
		%skillsetScroll.child("emptyText").setVisible(1);
	} else {
		if(!isObject(%skillsets = Stringify::parse(%skillsets))) {
			return;
		}

		for(%i = 0; %i < %skillsets.length; %i++) {
			%skillset = %skillsets.value[%i];

			%gui = new GuiSwatchCtrl("_" @ %skillsets.value[%i]) {
				profile = "GuiDefaultProfile";
				horizSizing = "right";
				vertSizing = "bottom";
				position = "0 0";
				extent = "87 30";
				minExtent = "8 2";
				enabled = "1";
				visible = "1";
				clipToParent = "1";
				color = "10 10 10 120";

				new GuiMLTextCtrl("_name") {
					profile = "GuiMLTextProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "3 0";
					extent = "81 16";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					lineSpacing = "2";
					allowColorChars = "0";
					maxChars = "-1";
					text = "<shadow:2:2><shadowcolor:00000066><color:EEEEEE><font:Impact:16>" @ getField(%skillset, 0);
					maxBitmapHeight = "-1";
					selectable = "1";
					autoResize = "1";
				};

				new GuiMLTextCtrl("_level") {
					profile = "GuiMLTextProfile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "3 13";
					extent = "81 15";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
					lineSpacing = "2";
					allowColorChars = "0";
					maxChars = "-1";
					text = "<shadow:2:2><shadowcolor:00000066><color:E8E8E8><font:Impact:15>Lvl" SPC pad(getField(%skillset, 1), 2);
					maxBitmapHeight = "-1";
					selectable = "1";
					autoResize = "1";
				};

				new GuiProgressCtrl("_xp") {
					profile = "CMProgress2Profile";
					horizSizing = "right";
					vertSizing = "bottom";
					position = "33 17";
					extent = "52 10";
					minExtent = "8 2";
					enabled = "1";
					visible = "1";
					clipToParent = "1";
				};
			};

			%listGUI.addListGuiObject(%gui);
			%gui.child("xp").setValue(getField(%skillset, 2) / 100);
		}

		%skillsetScroll.child("emptyText").setVisible(0);
	}
}

function CityModClientHUDInfopanel::updateAvatarPreview(%this) {
	%avatar = %this.gui.child("avatar");

	if(%avatar.child("preview").dynamicObject $= "") {
		return;
	}

	%avatar.child("preview").forceFOV = %avatar.def["Fov"];
	%avatar.child("preview").setOrbitDist(%avatar.def["OrbitDistance"]);

	%avatar.child("preview").setCameraRot(
		%avatar.def["CameraRotation", "X"],
		%avatar.def["CameraRotation", "Y"],
		%avatar.def["CameraRotation", "Z"]
	);

	%avatar.child("preview").lightDirection =
		%avatar.def["LightDirection", "X"] SPC
		%avatar.def["LightDirection", "Y"] SPC
		%avatar.def["LightDirection", "Z"];

	%avatar.child("preview").setMouse(false, true);
	%avatar.child("preview").syncWithPlayerAvatar();
}

function GuiObjectView::loadPlayer(%this, %dynamicObject) {
	%this.setEmpty();

	%this.dynamicObject = (%dynamicObject $= "" ? "playerObject" : %dynamicObject);
	%this.setObject(%this.dynamicObject, "base/data/shapes/player/m.dts", "", 0);

	%this.setSequence(%this.dynamicObject, 0, "headup", 0);
}

function GuiObjectView::hideAllNodes(%this) {
	%nodeCount = 0;

	%node[%nodeCount++] = "plume";
	%node[%nodeCount++] = "triPlume";
	%node[%nodeCount++] = "septPlume";
	%node[%nodeCount++] = "Visor";

	%node[%nodeCount++] = "helmet";
	%node[%nodeCount++] = "pointyHelmet";
	%node[%nodeCount++] = "flareHelmet";
	%node[%nodeCount++] = "scoutHat";
	%node[%nodeCount++] = "bicorn";
	%node[%nodeCount++] = "copHat";
	%node[%nodeCount++] = "knitHat";

	%node[%nodeCount++] = "chest";
	%node[%nodeCount++] = "femchest";

	%node[%nodeCount++] = "pants";
	%node[%nodeCount++] = "skirtHip";

	%node[%nodeCount++] = "armor";
	%node[%nodeCount++] = "bucket";
	%node[%nodeCount++] = "cape";
	%node[%nodeCount++] = "pack";
	%node[%nodeCount++] = "quiver";
	%node[%nodeCount++] = "tank";

	%node[%nodeCount++] = "epaulets";
	%node[%nodeCount++] = "epauletsRankA";
	%node[%nodeCount++] = "epauletsRankB";
	%node[%nodeCount++] = "epauletsRankC";
	%node[%nodeCount++] = "epauletsRankD";
	%node[%nodeCount++] = "ShoulderPads";

	%node[%nodeCount++] = "LArm";
	%node[%nodeCount++] = "LArmSlim";

	%node[%nodeCount++] = "LHand";
	%node[%nodeCount++] = "LHook";

	%node[%nodeCount++] = "RArm";
	%node[%nodeCount++] = "RArmSlim";

	%node[%nodeCount++] = "RHand";
	%node[%nodeCount++] = "RHook";

	%node[%nodeCount++] = "pants";
	%node[%nodeCount++] = "skirtHip";

	%node[%nodeCount++] = "LShoe";
	%node[%nodeCount++] = "LPeg";

	%node[%nodeCount++] = "RShoe";
	%node[%nodeCount++] = "RPeg";

	%node[%nodeCount++] = "SkirtTrimLeft";
	%node[%nodeCount++] = "SkirtTrimRight";
	%node[%nodeCount++] = "LSki";
	%node[%nodeCount++] = "RSki";

	for(%i = 1; %i <= %nodeCount; %i++) {
		%this.hideNode(%this.dynamicObject, %node[%i]);
	}
}

function GuiObjectView::syncWithPlayerAvatar(%this) {
	%this.hideAllNodes();

	%this.unHideNode(%this.dynamicObject, "headSkin");
	%this.setNodeColor(%this.dynamicObject, "headSkin", $Pref::Avatar::HeadColor);

	// Chest
	if($Pref::Avatar::Chest == 0) {
		%this.unHideNode(%this.dynamicObject, "chest");
		%this.setNodeColor(%this.dynamicObject, "chest", $Pref::Avatar::TorsoColor);
	}	else {
		%this.unHideNode(%this.dynamicObject, "femchest");
		%this.setNodeColor(%this.dynamicObject, "femchest", $Pref::Avatar::TorsoColor);
	}

	// Hip / Legs
	if(!$Pref::Avatar::Hip) {
		%this.unHideNode(%this.dynamicObject, "pants");
		%this.setNodeColor(%this.dynamicObject, "pants", $Pref::Avatar::HipColor);

		if(!$Pref::Avatar::LLeg) {
			%this.unHideNode(%this.dynamicObject, "LShoe");
			%this.setNodeColor(%this.dynamicObject, "LShoe", $Pref::Avatar::LLegColor);
		} else {
			%this.unHideNode(%this.dynamicObject, "LPeg");
			%this.setNodeColor(%this.dynamicObject, "LPeg", $Pref::Avatar::LLegColor);
		}

		if(!$Pref::Avatar::RLeg) {
			%this.unHideNode(%this.dynamicObject, "RShoe");
			%this.setNodeColor(%this.dynamicObject, "RShoe", $Pref::Avatar::RLegColor);
		} else {
			%this.unHideNode(%this.dynamicObject, "RPeg");
			%this.setNodeColor(%this.dynamicObject, "RPeg", $Pref::Avatar::RLegColor);
		}
	} else {
		%this.unHideNode(%this.dynamicObject, "skirtHip");
		%this.setNodeColor(%this.dynamicObject, "skirtHip", $Pref::Avatar::HipColor);

		%this.unHideNode(%this.dynamicObject, "SkirtTrimRight");
		%this.setNodeColor(%this.dynamicObject, "SkirtTrimRight", $Pref::Avatar::LLegColor);

		%this.unHideNode(%this.dynamicObject, "SkirtTrimLeft");
		%this.setNodeColor(%this.dynamicObject, "SkirtTrimLeft", $Pref::Avatar::RLegColor);
	}

	// Arms
	if($Pref::Avatar::LArm == 0) {
		%this.unHideNode(%this.dynamicObject, "LArm");
		%this.setNodeColor(%this.dynamicObject, "LArm", $Pref::Avatar::LArmColor);
	} else {
		%this.unHideNode(%this.dynamicObject, "LArmSlim");
		%this.setNodeColor(%this.dynamicObject, "LArmSlim", $Pref::Avatar::RArmColor);
	}

	if($Pref::Avatar::RArm == 0) {
		%this.unHideNode(%this.dynamicObject, "RArm");
		%this.setNodeColor(%this.dynamicObject, "RArm", $Pref::Avatar::RArmColor);
	} else {
		%this.unHideNode(%this.dynamicObject, "RArmSlim");
		%this.setNodeColor(%this.dynamicObject, "RArmSlim", $Pref::Avatar::RArmColor);
	}

	// Hands
	if(!$Pref::Avatar::LHand) {
		%this.unHideNode(%this.dynamicObject, "LHand");
		%this.setNodeColor(%this.dynamicObject, "LHand", $Pref::Avatar::LHandColor);
	} else {
		%this.unHideNode(%this.dynamicObject, "LHook");
		%this.setNodeColor(%this.dynamicObject, "LHooK", $Pref::Avatar::LHandColor);
	}

	if(!$Pref::Avatar::RHand) {
		%this.unHideNode(%this.dynamicObject, "RHand");
		%this.setNodeColor(%this.dynamicObject, "RHand", $Pref::Avatar::RHandColor);
	} else {
		%this.unHideNode(%this.dynamicObject, "RHook");
		%this.setNodeColor(%this.dynamicObject, "RHook", $Pref::Avatar::RHandColor);
	}

	// Hat
	if($Pref::Avatar::Hat == 1) {
		%this.unHideNode(%this.dynamicObject, "helmet");
		%this.setNodeColor(%this.dynamicObject, "helmet", $Pref::Avatar::HatColor);
	} else if($Pref::Avatar::Hat == 2) {
		%this.unHideNode(%this.dynamicObject, "pointyHelmet");
		%this.setNodeColor(%this.dynamicObject, "pointyHelmet", $Pref::Avatar::HatColor);
	} else if($Pref::Avatar::Hat == 3) {
		%this.unHideNode(%this.dynamicObject, "flareHelmet");
		%this.setNodeColor(%this.dynamicObject, "flareHelmet", $Pref::Avatar::HatColor);
	} else if($Pref::Avatar::Hat == 4) {
		%this.unHideNode(%this.dynamicObject, "scoutHat");
		%this.setNodeColor(%this.dynamicObject, "scouthat", $Pref::Avatar::HatColor);
	} else if($Pref::Avatar::Hat == 5) {
		%this.unHideNode(%this.dynamicObject, "bicorn");
		%this.setNodeColor(%this.dynamicObject, "bicorn", $Pref::Avatar::HatColor);
	} else if($Pref::Avatar::Hat == 6) {
		%this.unHideNode(%this.dynamicObject, "cophat");
		%this.setNodeColor(%this.dynamicObject, "cophat", $Pref::Avatar::HatColor);
	} else if($Pref::Avatar::Hat == 7) {
		%this.unHideNode(%this.dynamicObject, "knithat");
		%this.setNodeColor(%this.dynamicObject, "knithat", $Pref::Avatar::HatColor);
	}

	// Accent
	if($Pref::Avatar::Accent == 1) {
		%this.unHideNode(%this.dynamicObject, "plume");
		%this.setNodeColor(%this.dynamicObject, "plume", $Pref::Avatar::AccentColor);
	} else if($Pref::Avatar::Accent == 2) {
		%this.unHideNode(%this.dynamicObject, "septplume");
		%this.setNodeColor(%this.dynamicObject, "septplume", $Pref::Avatar::AccentColor);
	} else if($Pref::Avatar::Accent == 3) {
		%this.unHideNode(%this.dynamicObject, "triplume");
		%this.setNodeColor(%this.dynamicObject, "triplume", $Pref::Avatar::AccentColor);
	}

	// Pack
	if($Pref::Avatar::Pack == 0) {
	} else if($Pref::Avatar::Pack == 1) {
		%this.unHideNode(%this.dynamicObject, "armor");
		%this.setNodeColor(%this.dynamicObject, "armor", $Pref::Avatar::PackColor);
	} else if($Pref::Avatar::Pack == 2) {
		%this.unHideNode(%this.dynamicObject, "bucket");
		%this.setNodeColor(%this.dynamicObject, "bucket", $Pref::Avatar::PackColor);
	} else if($Pref::Avatar::Pack == 3) {
		%this.unHideNode(%this.dynamicObject, "cape");
		%this.setNodeColor(%this.dynamicObject, "cape", $Pref::Avatar::PackColor);
	} else if($Pref::Avatar::Pack == 4) {
		%this.unHideNode(%this.dynamicObject, "pack");
		%this.setNodeColor(%this.dynamicObject, "pack", $Pref::Avatar::PackColor);
	} else if($Pref::Avatar::Pack == 5) {
		%this.unHideNode(%this.dynamicObject, "quiver");
		%this.setNodeColor(%this.dynamicObject, "quiver", $Pref::Avatar::PackColor);
	} else if($Pref::Avatar::Pack == 6) {
		%this.unHideNode(%this.dynamicObject, "tank");
		%this.setNodeColor(%this.dynamicObject, "tank", $Pref::Avatar::PackColor);
	}

	// Second Pack
	if($Pref::Avatar::SecondPack == 1) {
		%this.unHideNode(%this.dynamicObject, "epaulets");
		%this.setNodeColor(%this.dynamicObject, "epaulets", $Pref::Avatar::SecondPackColor);
	} else if($Pref::Avatar::SecondPack == 2) {
		%this.unHideNode(%this.dynamicObject, "epauletsRankA");
		%this.setNodeColor(%this.dynamicObject, "epauletsRankA", $Pref::Avatar::SecondPackColor);
	} else if($Pref::Avatar::SecondPack == 3) {
		%this.unHideNode(%this.dynamicObject, "epauletsRankB");
		%this.setNodeColor(%this.dynamicObject, "epauletsRankB", $Pref::Avatar::SecondPackColor);
	} else if($Pref::Avatar::SecondPack == 4) {
		%this.unHideNode(%this.dynamicObject, "epauletsRankC");
		%this.setNodeColor(%this.dynamicObject, "epauletsRankC", $Pref::Avatar::SecondPackColor);
	} else if($Pref::Avatar::SecondPack == 5) {
		%this.unHideNode(%this.dynamicObject, "epauletsRankD");
		%this.setNodeColor(%this.dynamicObject, "epauletsRankD", $Pref::Avatar::SecondPackColor);
	} else if($Pref::Avatar::SecondPack == 6) {
		%this.unHideNode(%this.dynamicObject, "ShoulderPads");
		%this.setNodeColor(%this.dynamicObject, "ShoulderPads", $Pref::Avatar::SecondPackColor);
	}

	%this.setIflFrame(%this.dynamicObject, "face", getIflFrame("face", $Pref::Avatar::FaceName));
	%this.setIflFrame(%this.dynamicObject, "decal", getIflFrame("decal", $Pref::Avatar::DecalName));
}