import os, sys
import types
import os.path
import shutil
import re
	
outputDir = sys.argv[2]	
inputDir = sys.argv[1]	


cleintPath  = outputDir+"/Assets/KSFramework/Cos2/Code/Game/Enums"
cleintPath2 = inputDir

fileList = [
    cleintPath2 + "error.txt",
    cleintPath2 + "const.txt",
]

def EnumTxtToCs(txtFile, outputPath):
    baseName = os.path.basename(txtFile)
    shotname = ""

    if baseName == "error.txt":
        shotname = "ErrorCode"

    if baseName == "const.txt":
        shotname = "ProtocolConst"

    wirteString = "public class "
    wirteString += shotname + "\n{\n"

    lines = open(txtFile).readlines()
    for line in lines:
        line = Trim(line)
        #print(line)

        beginIdx = line.find("#")
        if beginIdx != -1:
            continue

        beginIdx = line.find("-")
        if beginIdx == -1:
            continue

        enumName = ""
        enumValue = 0
        enumDes = ""

        enumValue = line[:beginIdx]

        nextIdx = line.find("-", beginIdx + 1)
        if nextIdx != -1:
            enumName = line[beginIdx + 1:nextIdx]
        else:
            enumName = line[beginIdx + 1:]

        enumDes = line[nextIdx + 1:]

        #print("    " + enumName + " = " + enumValue)
        wirteString += "    public const int " + enumName + " = " + enumValue + "; //" + enumDes + "\n"

    csFileName = ""

    if outputPath is not None:
        csFileName = outputPath + "/" + shotname + ".cs"

    wirteString += "}"

    f = open(csFileName, "w+")
    try:
        f.write(wirteString)
        f.close()
    except:
        f.close()


def Trim(x):
    p = re.compile('\s+')
    return re.sub(p, '', x)

for cppFile in fileList:
    print ("file name " + cppFile)
    EnumTxtToCs(cppFile, cleintPath)

