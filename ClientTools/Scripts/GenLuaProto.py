#!/usr/bin/python -*- coding: UTF-8 -*-
 
import re
import os, sys

protoPath = sys.argv[1]
#CmdConfigPath = sys.argv[2]

srcCmdFile = os.path.join(protoPath, 'ClientCmd.txt')
srcProtoFile = os.path.join(protoPath, 'ClientProto.proto')
#luaFilterFile = os.path.join(CmdConfigPath, 'CmdConfig.txt')
genLuaCmdFile = os.path.join(protoPath, 'LuaCmd.proto')
genLuaProtoFile = os.path.join(protoPath, 'LuaCProto.proto')

filterList = []
filterCmdStructList = []
filterCmdStrList = []
totalStructList = []
finalFilterStructList = []

def CheckAlreadyInFinalList(teststructname):
    for structname in finalFilterStructList:
        if structname == teststructname:
            return True
    return False

def CheckIsBaseType(typestr):
    if typestr == 'int32' or typestr == 'int64' or typestr == 'string' or typestr == 'bool':
        return True
    else:
        return False

def GetFilterStrcutRecursion(srcstructname):
    if CheckAlreadyInFinalList(srcstructname) == False:
        finalFilterStructList.append(srcstructname)
    for testinfo in totalStructList:
        if testinfo[0] == srcstructname:
            for substruct in testinfo[1]:
                GetFilterStrcutRecursion(substruct)

#删除之前的文件
if os.path.exists(genLuaCmdFile):
    os.remove(genLuaCmdFile)
if os.path.exists(genLuaProtoFile):
    os.remove(genLuaProtoFile)

#读取筛选文件
#print "start read fliter file"
#fileRead = open(luaFilterFile)
#line = fileRead.readline()
#while line:
#    line = line.strip()
#    strlist = line.split('#')
#    if strlist[0] == None or len(strlist[0]) == 0:
#        line = fileRead.readline()
#        continue
#    else:
#        intcmd = int(strlist[0])
#        filterList.append(intcmd)
#    line = fileRead.readline()
#fileRead.close()
#print "end read fliter file"

#处理cmd文件
print ("start excute cmd file")
fileRead = open(srcCmdFile,'r',encoding='UTF-8')
line = fileRead.readline()
while line:
    line = line.strip()
    lineOK = False
    strlist = line.split('#')
    strlistlength = len(strlist)
    if strlistlength >= 3:
        if strlist[0] == None or len(strlist[0]) == 0:
            line = fileRead.readline()
            continue
        testint = int(strlist[0])
        filterCmdStructList.append(strlist[2])
        lineOK = True
#        for filtercmd in filterList:
#            if filtercmd == testint:
#                filterCmdStructList.append(strlist[2])
#                lineOK = True
#                break
    if lineOK == True:
        filterCmdStrList.append(line)
    line = fileRead.readline()
fileRead.close()
print ("end excute cmd file")

#处理Proto文件= open(srcProtoFile)
print ("start excute Proto file")
lastStuctName = ''
inSideStruct = False
lastStructIndex = 0
fileRead = open(srcProtoFile,'r',encoding='UTF-8')
line = fileRead.readline()
while line:
    line = line.strip()
    if line == None or len(line) == 0:
        line = fileRead.readline()
        continue
    strsplitlist = line.split()
    oldInside = inSideStruct
    if strsplitlist[0] == 'message':
        structname = strsplitlist[1].replace('{',  '')
        lastStuctName = structname
        inSideStruct = True
        if oldInside == False and inSideStruct == True:
            newstruct = []
            newstruct.append(structname)
            structcontentTypes = []
            newstruct.append(structcontentTypes)
            totalStructList.append(newstruct)
            lastStructIndex = lastStructIndex + 1
    else:
        if strsplitlist[0] == '{':
            line = fileRead.readline()
            continue
        elif strsplitlist[0] == '}':
            inSideStruct = False
            line = fileRead.readline()
            continue
        elif strsplitlist[0] == None or len(strsplitlist[0]) == 0:
            line = fileRead.readline()
            continue
        else:
            if inSideStruct == True:
                if CheckIsBaseType(strsplitlist[0]) == False:
                    if strsplitlist[0] == 'repeated':
                        if CheckIsBaseType(strsplitlist[1]) == False:
                            totalStructList[lastStructIndex - 1][1].append(strsplitlist[1])
                    else:
                        #print "-------total list length = " + bytes(len(totalStructList))
                        #print "-------lastStructIndex = " + bytes(lastStructIndex)
                        totalStructList[lastStructIndex - 1][1].append(strsplitlist[0])
    line = fileRead.readline()
fileRead.close()
print ("end excute Proto file")
                    
#找到所有需要筛选出来的struct
for needBaseStruct in filterCmdStructList:
    GetFilterStrcutRecursion(needBaseStruct)

#写生成的Cmd文件
print ("start write file")
writecmdStr = ''
for useCmd in filterCmdStrList:
    writecmdStr += useCmd
    writecmdStr += '\n'
fileWrite = open(genLuaCmdFile, 'w',encoding='UTF-8')
fileWrite.write(writecmdStr)
fileWrite.close()
print ("Generate LuaCmd.proto OK")

#重新读取，并写生成的Struct文件
writeProtoStr = ''
isNeedToWrite = True
fileRead = open(srcProtoFile,'r',encoding='UTF-8')
line = fileRead.readline()
while line:
    linetest = line.strip()
    if linetest == None or len(linetest) == 0:
        line = fileRead.readline()
        continue
    strsplitlist = linetest.split()
    if strsplitlist[0] == 'message':
        structname = strsplitlist[1].replace('{',  '')
        if CheckAlreadyInFinalList(structname) == True:
            isNeedToWrite = True
        else:
            isNeedToWrite = False
    if isNeedToWrite == True:
        writeProtoStr += line
        if line.find('}') > -1:
            writeProtoStr += '\n'
    line = fileRead.readline()
fileRead.close()

fileWrite = open(genLuaProtoFile, 'w',encoding='UTF-8')
fileWrite.write(writeProtoStr)
fileWrite.close()
print ("Generate LuaCProto.proto OK")







