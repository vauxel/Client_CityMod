// ============================================================
// Project          -      CityMod
// Description      -      Global Support Functions
// ============================================================

function SimObject::hasMethod(%this, %name) {
	return isFunction(%this.getName(), %name) || isFunction(%this.class, %name) || isFunction(%this.getClassName(), %name);
}

function SimObject::call(%this, %method, %p1, %p2, %p3, %p4, %p5, %p6, %p7, %p8, %p9, %p10, %p11, %p12, %p13, %p14, %p15, %p16) {
	%lastValue = 0;

	for(%i = 1; %i <= 16; %i++) {
		if(strLen(%p[%i])) {
			%lastValue = %i;
		}
	}

	for(%i = 1; %i <= %lastValue; %i++) {
		%args = %args @ "\"" @ expandEscape(%p[%i]) @ "\"";

		if(%i != %lastValue) {
			%args = %args @ ", ";
		} else {
			break;
		}
	}

	eval(%this @ "." @ %method @ "(" @ %args @ ");");
}

function SimObject::copy(%this) {
	%name = %this.getName(); %this.setName("TempCopyName");
	%copy = (new (%this.getClassName())(%name : TempCopyName) @ "\x01");
	%this.setName(%name);
	return %copy;
}

// Credits to Port for the get/setAttribute functions
function SimObject::getAttribute(%this, %attr) {
	if(%attr $= "") {
		return "";
	}

	switch(stripos("_abcdefghijklmnopqrstuvwxyz", getSubStr(%attr, 0, 1))) {
		case  0: return %this._[getSubStr(%attr, 1, strlen(%attr))];
		case  1: return %this.a[getSubStr(%attr, 1, strlen(%attr))];
		case  2: return %this.b[getSubStr(%attr, 1, strlen(%attr))];
		case  3: return %this.c[getSubStr(%attr, 1, strlen(%attr))];
		case  4: return %this.d[getSubStr(%attr, 1, strlen(%attr))];
		case  5: return %this.e[getSubStr(%attr, 1, strlen(%attr))];
		case  6: return %this.f[getSubStr(%attr, 1, strlen(%attr))];
		case  7: return %this.g[getSubStr(%attr, 1, strlen(%attr))];
		case  8: return %this.h[getSubStr(%attr, 1, strlen(%attr))];
		case  9: return %this.i[getSubStr(%attr, 1, strlen(%attr))];
		case 10: return %this.j[getSubStr(%attr, 1, strlen(%attr))];
		case 11: return %this.k[getSubStr(%attr, 1, strlen(%attr))];
		case 12: return %this.l[getSubStr(%attr, 1, strlen(%attr))];
		case 13: return %this.m[getSubStr(%attr, 1, strlen(%attr))];
		case 14: return %this.n[getSubStr(%attr, 1, strlen(%attr))];
		case 15: return %this.o[getSubStr(%attr, 1, strlen(%attr))];
		case 16: return %this.p[getSubStr(%attr, 1, strlen(%attr))];
		case 17: return %this.q[getSubStr(%attr, 1, strlen(%attr))];
		case 18: return %this.r[getSubStr(%attr, 1, strlen(%attr))];
		case 19: return %this.s[getSubStr(%attr, 1, strlen(%attr))];
		case 20: return %this.t[getSubStr(%attr, 1, strlen(%attr))];
		case 21: return %this.u[getSubStr(%attr, 1, strlen(%attr))];
		case 22: return %this.v[getSubStr(%attr, 1, strlen(%attr))];
		case 23: return %this.w[getSubStr(%attr, 1, strlen(%attr))];
		case 24: return %this.x[getSubStr(%attr, 1, strlen(%attr))];
		case 25: return %this.y[getSubStr(%attr, 1, strlen(%attr))];
		case 26: return %this.z[getSubStr(%attr, 1, strlen(%attr))];
	}

	return "";
}

function SimObject::setAttribute(%this, %attr, %value) {
	if(%attr $= "") {
		return;
	}

	switch(stripos("_abcdefghijklmnopqrstuvwxyz", getSubStr(%attr, 0, 1))) {
		case  0: %this._[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  1: %this.a[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  2: %this.b[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  3: %this.c[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  4: %this.d[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  5: %this.e[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  6: %this.f[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  7: %this.g[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  8: %this.h[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case  9: %this.i[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 10: %this.j[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 11: %this.k[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 12: %this.l[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 13: %this.m[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 14: %this.n[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 15: %this.o[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 16: %this.p[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 17: %this.q[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 18: %this.r[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 19: %this.s[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 20: %this.t[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 21: %this.u[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 22: %this.v[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 23: %this.w[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 24: %this.x[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 25: %this.y[getSubStr(%attr, 1, strlen(%attr))] = %value;
		case 26: %this.z[getSubStr(%attr, 1, strlen(%attr))] = %value;
	}
}

function SimObject::deleteAttribute(%this, %attr) {
	return %this.setAttribute(%attr, "");
}

// Inheritance navigation helpers
function GuiControl::child(%this, %name) {
	if(%this.getCount() <= 0) {
		return;
	}

	for(%i = 0; %i < %this.getCount(); %i++) {
		%child = %this.getObject(%i);
		%childName = %child.getName();

		if(getSubStr(%childName, 0, 1) !$= "_") {
			continue;
		}

		if(getSubStr(%childName, 1, strLen(%childName) - 1) $= %name) {
			return %child;
		}
	}
}

function GuiControl::parent(%this) {
	return %this.getGroup();
}

// Position and Extent helpers
function GuiControl::getPositionX(%this) {
	return getWord(%this.getPosition(), 0);
}

function GuiControl::getPositionY(%this) {
	return getWord(%this.getPosition(), 1);
}

function GuiControl::setPosition(%this, %x, %y) {
	%this.resize(%x, %y, %this.getExtentW(), %this.getExtentH());
}

function GuiControl::setPositionX(%this, %x) {
	%this.setPosition(%x, %this.getPositionY());
}

function GuiControl::setPositionY(%this, %y) {
	%this.setPosition(%this.getPositionX(), %y);
}

function GuiControl::getExtentW(%this) {
	return getWord(%this.getExtent(), 0);
}

function GuiControl::getExtentH(%this) {
	return getWord(%this.getExtent(), 1);
}

function GuiControl::setExtent(%this, %w, %h) {
	%this.resize(%this.getPositionX(), %this.getPositionY(), %w, %h);
}

function GuiControl::setExtentW(%this, %w) {
	%this.setExtent(%w, %this.getExtentH());
}

function GuiControl::setExtentH(%this, %h) {
	%this.setExtent(%this.getExtentW(), %h);
}

// GUI animation
function GuiControl::transitionMove(%this, %method, %targetX, %targetY, %speed, %startX, %startY, %callback, %elapsed) {
	if(isEventPending(%this.transitionMove)) {
		cancel(%this.transitionMove);
	}

	%this.isTransitioning = true;

	if((%targetX $= "") || (%targetY $= "")) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::transitionMove() ==> Invalid target position given");
	}

	if(%elapsed $= "") {
		if(%speed == 0) {
			%speed = 1;
		}

		%this.transitionMove(%method, %targetX, %targetY, %speed, (%startX $= "" ? %this.getPositionX() : %startX), (%startY $= "" ? %this.getPositionY() : %startY), %callback, 0);
	} else {
		if((%startX $= "") || (%startY $= "")) {
			warn(nonempty(%this.getName(), %this.getID()) @ "::transitionMove() ==> Invalid start position given");
		}

		%step = %elapsed / 100;

		switch$(%method) {
			case "easeInQuad": %step = (%step * %step);
			case "easeInCubic": %step = (%step * %step * %step);
			case "easeInQuart": %step = (%step * %step * %step * %step);
			case "easeInQuint": %step = (%step * %step * %step * %step * %step);
			case "easeInSine": %step = (-1 * (mCos(%step * ($pi / 2)) + 1));
			case "easeInExpo": %step = (mPow(2, (-10 * (%step - 1))));
			case "easeInCirc": %step = (-1 * (mSqrt(1 - %step * %step) - 1));
			case "easeOutQuad": %step = (-1 * %step * (%step - 2));
			case "easeOutCubic": %step--; %step = (%step * %step * %step + 1);
			case "easeOutQuart": %step--; %step = (-1 * (%step * %step * %step * %step - 1));
			case "easeOutQuint": %step--; %step = (%step * %step * %step * %step * %step + 1);
			case "easeOutSine": %step = (mSin(%step * ($pi / 2)));
			case "easeOutExpo": %step = (-mPow(2, -10 * %step) + 1);
			case "easeOutCirc": %step--; %step = (mSqrt(1 - %step * %step));
			case "easeInOutSine": %step = (-0.5 * (mCos($pi * %step) - 1));
			case "easeInOutExpo":
				if(%step <= 0) { %step = 0; }
				else {
				if(%step >= 1) { %step = 1; }
				else { %step *= 2;
				if(%step < 1) { %step = (0.5 * mPow(2, 10 * (%step - 1))); }
				else { %step = (0.5 * (-mPow(2, -10 * (%step - 1)) + 2)); } } }
			case "easeInOutCirc":
				%step *= 2;
				if(%step < 1) { %step = (-0.5 * (mSqrt(1 - %step * %step) - 1)); }
				else { %step -= 2; %step = (0.5 * (mSqrt(1 - %step * %step) + 1)); }
			case "easeInOutBack":
				%step *= 2;
				if(%step < 1) { %step = (0.5 * (%step * %step * (((1.70158 * 1.525) + 1) * %step - (1.70158 * 1.525)))); }
				else { %step -= 2; %step = (0.5 * (%step * %step * (((1.70158 * 1.525) + 1) * %step + (1.70158 * 1.525)) + 2)); }
			case "easeOutBounce":
				if(%step < (1 / 2.75)) { %step = (7.5625 * %step * %step); }
				else if(%step < (2 / 2.75)) { %step -= (1.5 / 2.75); %step = (7.5625 * %step * %step + 0.75); }
				else if(%step < (2.5 / 2.75)) { %step -= (2.25 / 2.75); %step = (7.5625 * %step * %step + 0.9375); }
				else { %step -= (2.625 / 2.75); %step = (7.5625 * %step * %step + 0.984375); }
		}

		%this.setPosition(mCeil(%startX + (%targetX - %startX) * %step), mCeil(%startY + (%targetY - %startY) * %step));

		if(%elapsed < 100) {
			%this.transitionMove = %this.schedule(10, "transitionMove", %method, %targetX, %targetY, %speed, %startX, %startY, %callback, %elapsed + %speed);
		} else {
			%this.isTransitioning = false;
			%this.transitionMove = "";

			if(%this.hasMethod(%callback)) {
				%this.call(%callback);
			}
		}
	}
}

// List GUI helpers
function GuiSwatchCtrl::resizeListGui(%this, %spacing, %horizontal) {
	if(!%this.isAwake()) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::resizeListGui() ==> List GUI isn't awake");
		return;
	}

	if(%this.getCount() <= 0) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::resizeListGui() ==> List GUI doesn't have any objects");
		return;
	}

	if(%spacing $= "") {
		if(%this.listSpacing !$= "") {
			%spacing = %this.listSpacing;
		} else {
			%spacing = 2;
		}
	}

	%extent = 0;

	for(%i = 0; %i < %this.getCount(); %i++) {
		%object = %this.getObject(%i);

		if(!%object.isVisible()) {
			continue;
		}

		if(%extent != 0) {
			%extent += %spacing;
		}

		if(%horizontal) {
			%object.setPosition(%extent, %object.getPositionY());
			%extent += %object.getExtentW();
		} else {
			%object.setPosition(%object.getPositionX(), %extent);
			%extent += %object.getExtentH();
		}
	}

	if(%horizontal) {
		%this.setExtent(%extent, %this.getExtentH());
	} else {
		%this.setExtent(%this.getExtentW(), %extent);
	}
}

function GuiSwatchCtrl::deleteListGuiObject(%this, %index, %horizontal) {
	if(!%this.isAwake()) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::deleteListGuiObject() ==> List GUI isn't awake");
		return;
	}

	if(%this.getCount() <= 0) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::deleteListGuiObject() ==> List GUI doesn't have any objects");
		return;
	}

	if(!isObject(%this.getObject(%index))) {
		return;
	}

	if(%this.getCount() != 1) {
		for(%i = (%index + 1); %i < %this.getCount(); %i++) {
			if(%horizontal) {
				%this.getObject(%i).setPositionX(%this.getObject(%i).getPositionX() - %this.getObject(%i).getExtentW());
			} else {
				%this.getObject(%i).setPositionY(%this.getObject(%i).getPositionY() - %this.getObject(%i).getExtentH());
			}
		}
	}

	%this.getObject(%index).delete();
	%this.resizeListGui("", %horizontal);
}

function GuiSwatchCtrl::addListGuiObject(%this, %object, %horizontal) {
	if(!%this.isAwake()) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::addListGuiObject() ==> List GUI isn't awake");
		return;
	}

	if(!isObject(%object)) {
		warn(nonempty(%this.getName(), %this.getID()) @ "::addListGuiObject() ==> Nonexistent GUI Object given");
		return;
	}

	if(%this.getCount() <= 0) {
		%pos = "0";
	} else {
		%pos = getWord(%this.getObject(%this.getCount() - 1).position, (%horizontal ? 0 : 1)) + getWord(%this.getObject(%this.getCount() - 1).extent, (%horizontal ? 0 : 1)) + %this.listSpacing;
	}

	%object.position = (%horizontal ? %pos SPC getWord(%object.position, 1) : getWord(%object.position, 0) SPC %pos);
	%this.add(%object);

	%this.resizeListGui("", %horizontal);
}

// Credit to IBan for the getIflFrame function
function getIflFrame(%node, %name) {
	%file = "base/data/shapes/player/" @ %node @ ".ifl";

	if(isFile(%file)) {
		%name = stripTrailingSpaces(fileBase(%name));

		if(%name !$= "") {
			%fo = new FileObject();
			%fo.openForRead(%file);

			%hasFound = false;
			%lineNum = 0;

			while(!%fo.isEOF()) {
				%line = %fo.readLine();
				%thisDecal = stripTrailingSpaces(fileBase(%line));

				if(%thisDecal $= %name) {
					%hasFound = true;
					break;
				} else {
					%lineNum++;
				}
			}

			%fo.close();
			%fo.delete();

			if(%hasFound) {
				return %lineNum;
			} else {
				return 0;
			}
		} else {
			return 0;
		}
	} else {
		echo("\c2getIflFrame(\"" @ %node @ "\", \"" @ %name @ "\") : You did not supply a proper IFL file name to read from.");
		return 0;
	}
}

// Credit to Greek2me for the addKeyBind function
function addKeyBind(%div, %name, %cmd, %device, %action, %overWrite) {
	if((%device !$= "") && (%action !$= "")) {
		if((moveMap.getCommand(%device, %action) $= "") || %overWrite) {
			moveMap.bind(%device, %action, %cmd);
		}
	}

	%divIndex = -1;
	for(%i = 0; %i < $RemapCount; %i++) {
		if($RemapDivision[%i] $= %div) {
			%divIndex = %i;
			break;
		}
	}

	if(%divIndex >= 0) {
		for(%i = $RemapCount - 1; %i > %divIndex; %i--) {
			$RemapDivision[%i + 1] = $RemapDivision[%i];
			$RemapName[%i + 1] = $RemapName[%i];
			$RemapCmd[%i + 1] = $RemapCmd[%i];
		}

		$RemapDivision[%divIndex + 1] = "";
		$RemapName[%divIndex + 1] = %name;
		$RemapCmd[%divIndex + 1] = %cmd;
		$RemapCount++;
	} else {
		$RemapDivision[$RemapCount] = %div;
		$RemapName[$RemapCount] = %name;
		$RemapCmd[$RemapCount] = %cmd;
		$RemapCount++;
	}
}