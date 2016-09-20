$GuiHierarchy::PrefixFilter = "_";

function isGuiHierarchyName(%name) {
	return (getSubStr(%name, 0, strLen($GuiHierarchy::PrefixFilter)) $= $GuiHierarchy::PrefixFilter);
}

package GuiHierarchy {
	function GuiControl::filteredGuiHierarchyName(%this, %name) {
		if(%name $= "") {
			%name = %this.getName();
		}

		if(getSubStr(%name, 0, (%start = strLen($GuiHierarchy::PrefixFilter))) $= $GuiHierarchy::PrefixFilter) {
			return getSubStr(%name, %start, strLen(%name) - %start - (strStr(%name, "__") != -1 ? (strLen(%this.getID()) + 2) : 0));
		} else {
			return %name;
		}
	}

	function GuiControl::child(%this, %name) {
		return %this.childgui_[%name];
	}

	function GuiControl::onAdd(%this) {
		if(isObject(%this.getGroup()) && ((%name = %this.getName()) !$= "") && isGuiHierarchyName(%name)) {
			//%this.setName("_" @ %this.filteredGuiHierarchyName());
			%this.schedule(0, "setName", "_" @ %this.filteredGuiHierarchyName());
		}
	}

	function GuiControl::onRemove(%this) {
		if(isObject(%parent = %this.getGroup()) && ((%name = %this.getName()) !$= "") && isGuiHierarchyName(%name)) {
			%parent.childgui_[%this.filteredGuiHierarchyName(%name)] = "";
		}
	}

	function GuiControl::setName(%this, %newname) {
		if(isObject(%parent = %this.getGroup())) {
			if(((%oldname = %this.getName()) !$= "") && isGuiHierarchyName(%oldname)) {
				%parent.childgui_[%this.filteredGuiHierarchyName()] = "";
			}

			if(isGuiHierarchyName(%newname)) {
				%parent.childgui_[%newname = %this.filteredGuiHierarchyName(%newname)] = %this.getID();
				%newname = "_" @ %newname @ "__" @ %this.getID();
			}
		}

		parent::setName(%this, %newname);
	}
};

if(isPackage(GuiHierarchy))
	deactivatepackage(GuiHierarchy);
activatepackage(GuiHierarchy);