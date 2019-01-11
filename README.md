# 4NXCI-GUI
Graphical User Interface for 4NXCI conversion tool

Simple tool which allows you to:


  - Select a .xci
  - Select a save location for the nsp
  - Convert the xci->nsp using 4NXCI.EXE
 

Given the xci location and the save location, the GUI does the following when Convert is clicked:

    - create a temporary working folder in your save location
    - convert the xci to nsp and save the nsp in the temp folder
    - rename the newly created nsp to match the name of the xci
    - move the nsp to your save location
    - delete the temporary working folder
    - inform you that the conversion is complete

Install instructions:
  Place the 4NXCI-GUI.EXE in the same folder where your 4NXCI.EXE resides.
  Keys will need to be in the same folder as well.
