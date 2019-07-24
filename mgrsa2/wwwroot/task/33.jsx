//ExportAllStories.jsx
//An InDesign CS6 JavaScript
/*  
@@@BUILDINFO@@@ "ExportAllStories.jsx" 3.0.0 15 December 2009
*/
//Exports all stories in an InDesign document in a specified text format.
//
//For more on InDesign scripting, go to http://www.adobe.com/products/indesign/scripting/index.html
//or visit the InDesign Scripting User to User forum at http://www.adobeforums.com
//
main();
function main(){
	//Make certain that user interaction (display of dialogs, etc.) is turned on.
	app.scriptPreferences.userInteractionLevel = UserInteractionLevels.interactWithAll;
	if(app.documents.length != 0){
		if (app.activeDocument.stories.length != 0){
			//myDisplayDialog();
             myExportAllStoriesD();
		}
		else{
			alert("The document does not contain any text. Please open a document containing text and try again.");
		}
	}
	else{
		alert("No documents are open. Please open a document and try again.");
	}
}

function myDisplayDialog(){
	with(myDialog = app.dialogs.add({name:"ExportAllStories"})){
		//Add a dialog column.
		myDialogColumn = dialogColumns.add()	
		with(myDialogColumn){
			with(borderPanels.add()){
				staticTexts.add({staticLabel:"Export as:"});
				with(myExportFormatButtons = radiobuttonGroups.add()){
					radiobuttonControls.add({staticLabel:"Text Only", checkedState:true});
					radiobuttonControls.add({staticLabel:"RTF"});
					radiobuttonControls.add({staticLabel:"InDesign Tagged Text"});
				}
			}
		}
		myReturn = myDialog.show();
		if (myReturn == true){
			//Get the values from the dialog box.
			myExportFormat = myExportFormatButtons.selectedButton;
			myDialog.destroy;
			myFolder= Folder.selectDialog ("Choose a Folder");
			if((myFolder != null)&&(app.activeDocument.stories.length !=0)){
				myExportAllStories(myExportFormat, myFolder);
			}
		}
		else{
			myDialog.destroy();
		}
	}
}
//myExportStories function takes care of exporting the stories.
//myExportFormat is a number from 0-2, where 0 = text only, 1 = rtf, and 3 = tagged text.
//myFolder is a reference to the folder in which you want to save your files.
function myExportAllStories(myExportFormat, myFolder){
	for(myCounter = 0; myCounter < app.activeDocument.stories.length; myCounter++){
         fullName  = app.activeDocument.fullName + "";
		myStory = app.activeDocument.stories.item(myCounter);
		myID = myStory.id;
		switch(myExportFormat){
			case 0:
				myFormat = ExportFormat.textType;
				myExtension = ".txt"
				break;
			case 1:
				myFormat = ExportFormat.RTF;
				myExtension = ".rtf"
				break;
			case 2:
				myFormat = ExportFormat.taggedText;
				myExtension = ".txt"
				break;
		}
		myFileName = fullName + "_" + myID + myExtension;
		myFilePath = myFolder + "/" + myFileName;
		myFile = new File(myFilePath);       
		//myStory.exportFile(myFormat, myFile);
         //myStory.exportFile(myFormat, /c/temp/myFileName.replace('~/desk);
         alert(myFileName.toLowerCase().replace("~/desktop/",""));
	}
}

function myExportAllStoriesD(){
    try{
        for(myCounter = 0; myCounter < app.activeDocument.stories.length; myCounter++){
             fullName  = app.activeDocument.fullName + "";        
            myStory = app.activeDocument.stories.item(myCounter);         
            myID = myStory.id;
            myExtension = ".txt"
            var aFileName = fullName.split("/");
            var lFileName = aFileName.length-1;
            //for(i=0; i < aFileName.length; i++){
            //        alert(aFileName[i]);
             //}
            //alert(fullName);
            myFileName =  myID + "_" + aFileName[lFileName].replace(".indd","").replace("_","") + myExtension;             
            //myFileName = myFileName.toLowerCase().replace("~/desktop/","").replace(".indd","");
            myFilePath = "/c/temp/editorial/"  + myFileName;
            //alert(myFilePath);
            myFile = new File(myFilePath);       
            myStory.exportFile(ExportFormat.textType, myFile);            
                   
        }
        alert("Export done...!!!");
    } 
    catch (err){        
            alert("error:" +  err.message );
     }
}
