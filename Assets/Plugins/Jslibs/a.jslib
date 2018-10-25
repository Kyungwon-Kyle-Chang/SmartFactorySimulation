mergeInto(LibraryManager.library, {

Hello: function(){
	window.alert("Hello, world!");
},

Initialize: function(){
	Initialize();
},

MachineStatusClickEvent: function(machineNum, lineNum, status){
	OnMachineStatusClick(machineNum, lineNum, status);
},


});