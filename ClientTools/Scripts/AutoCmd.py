import os, sys
import types
import os.path
import shutil
import re
	
outputDir = sys.argv[2]	
inputDir = sys.argv[1]	

cleintPath  = outputDir+"/Assets/Game/Project/Enums/"
cleintPath2 = inputDir
cleintPath3 = outputDir+"/Assets/Game/Project/Network/Message/Socket"

fileList = [
    cleintPath2 + "Cmd.proto",
]

def EnumTxtToCs(txtFile, outputPath):
    baseName = os.path.basename(txtFile)
    shotname = ""

    if baseName == "Cmd.proto":
        shotname = "ProtocolCMD"

    unPackString = "using EcsRx.Network;\nusing Protocol;\nusing protocol;\n\n"
    unPackString += "namespace TinyJoy.SSG.Game.Project.Network.Message.Socket\n{\n"
    
    #wirteString = "public enum "
    #wirteString += shotname + ": uint\n{\n"

    lines = open(txtFile).readlines()
    for line in lines:
        line = Trim(line)
        #print(line)

        beginIdx = line.find("#")
        if beginIdx == -1 or beginIdx == 0:
            continue

        strTable = line.split("#")
        if len(strTable) < 4:
            continue

        cmdName = strTable[1]
        cmdValue = strTable[0]
        unPackMsg = strTable[2]
        descStr = strTable[3]

        if int(cmdValue) > 20000:
            continue

        #print("    " + cmdName + " = " + cmdValue)
        #wirteString += "    " + cmdName + " = " + cmdValue + ",\n"

        if cmdName != "":
            unPackString += "   [Proto(value = " + cmdValue + ", description = \"" + descStr + "\")]\n"
            unPackString += "   public class " + cmdName + "_SocketMessage : SocketMessage<" + unPackMsg + ">\n   {\n"
            beginIdx = cmdName.find("_REQ")
            if beginIdx != -1:
                unPackString += "       public " + cmdName + "_SocketMessage(" + unPackMsg + " data) : base(data)\n     {}\n"
            else:
                unPackString += "       public " + cmdName + "_SocketMessage()\n     {}\n"

            unPackString += "   }\n"

    unPackString += "}\n"

    csFileName = ""
    csFileName1 = ""

    if outputPath is not None:
        csFileName = outputPath + "/" + shotname + ".cs"

    #wirteString += "}"

    #f = open(csFileName, "w+")
    #try:
        #f.write(wirteString)
        #f.close()
    #except:
        #f.close()


    f1 = open(cleintPath3 + "/SocketMessages.cs", "w+")
    try:
        f1.write(unPackString)
        f1.close()
    except:
        f1.close()

def Trim(x):
    p = re.compile('\s+')
    return re.sub(p, '', x)


for cppFile in fileList:
    print ("file name " + cppFile)
    EnumTxtToCs(cppFile, cleintPath)

