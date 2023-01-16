import os, sys
import types
import os.path
import shutil
import re
	
outputDir = sys.argv[2]	
inputDir = sys.argv[1]	


cleintPath  = outputDir+"/../../"
cleintPath2 = inputDir

fileList = [
    cleintPath2 + "CProto.proto",
    #cleintPath2 + "const.txt",
]

def EnumTxtToCs(txtFile, outputPath):
    baseName = os.path.basename(txtFile)
    shotname = ""

    if baseName == "CProto.proto":
        shotname = "CProto"

    wirteString = ""
    lines = open(txtFile).readlines()
    for line in lines:
        line = Trim(line)
        #print(line)

        beginIdx = line.find("message")
        if beginIdx == -1:
            continue

        print("1111")
        beginIdx = line.find("GS")
        if beginIdx == -1:
            continue

        print("2222 beginIdx" + str(beginIdx))
        enumName = ""

        nextIdx = line.find("{", beginIdx + 1)
        if nextIdx != -1:
            enumName = line[beginIdx:nextIdx]
        else:
            enumName = line[beginIdx:]

        print("3333")
        enumName.strip()
        wirteString += "  \"Protocol." + enumName + "\"," + "\n"

    csFileName = ""

    if outputPath is not None:
        csFileName = outputPath + "/" + shotname + ".txt"

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

