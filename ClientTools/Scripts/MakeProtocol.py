#!/usr/bin/python -*- coding: UTF-8 -*-
 
import re
import os, sys

outputDir = sys.argv[2]	
inputDir = sys.argv[1]

clientOutDir = os.path.join(outputDir, 'LuaProto.lua')
clientInDir = os.path.join(inputDir, 'LuaCProto.proto')

#--------------------------------------------
# 替换字符串方法
#--------------------------------------------
#define a function  
def ReplaceStr(s):
    s = s.strip()              #先把两头多余的空格去掉
    s = s.replace('=', ' ')    #先把所有的'='替换成'空格'
    s = s.replace('{', ' ')    #先把所有的'{'替换成'空格'
    s = s.replace('\t', ' ')   #先把所有的'\t'替换成'空格'
    s = re.sub(r'\s+', '-', s) #把任意多个'空格'替换成'-'
    return s
	
#--------------------------------------------
# 检测类型赋值
#--------------------------------------------
#define a function  
def GetTypeValue(s):
	typeValue = ""
	if s == "int32":
		typeValue = "0"
	elif s == "int64":
		typeValue = "0"
	elif s == "bool":
		typeValue = "false"
	elif s == "string":
		typeValue = "\"\""
	elif s == "double":
		typeValue = "0.0"
	elif s == "float":
		typeValue = "0.0"
	else:
		typeValue = "{}" + " --@ " + s
	return typeValue

#--------------------------------------------
# 检测类型替换后的字符串
#--------------------------------------------
#define a function  
def CheckType(s,clsStr):
	rtStr = ""
	newCls = ""
	arrSplit = s.split('-')
	arrLength = len(arrSplit)

	##注释行忽略
	if arrLength > 0 and arrSplit[0].find("//") != -1:
            return rtStr, newCls
	
	if arrSplit[0] == "message":
		newCls = arrSplit[1]
		rtStr += "\n---@class " + newCls + '\n'
		rtStr += "local " + newCls + " = {}"
		rtStr += '\n'
	elif (arrLength > 2):
            if arrSplit[0] == "repeated":
                rtStr +=  clsStr + "." + arrSplit[2] + " = " + "{}" + " --@ " + arrSplit[1]
                rtStr += '\n'
            elif arrSplit[0] == "required" or arrSplit[0] == "optional":
                rtStr +=  clsStr + "." + arrSplit[2] + " = " + GetTypeValue(arrSplit[1])
                rtStr += '\n'
            else:
                rtStr += clsStr + "." + arrSplit[1] + " = " + GetTypeValue(arrSplit[0])
                rtStr += '\n'
	#elif arrSplit[0] == '}':
		#rtStr += '}'
	return rtStr, newCls

#---------------------------------
#根据table名生成模板代码
#---------------------------------    
def GenerateModuleCode(clsStr, moduleName):
    retStr = ""
    retStr += "\n"
    retStr += "function " + moduleName + "." + clsStr + "()"
    retStr += "\n"
    retStr +=  "    " + "return new(" + clsStr + ")"
    retStr += "\n"
    retStr += "end"
    retStr += "\n\n"
    return retStr

#---------------------------------
#写文件
#---------------------------------
def ExcuteWriteFile(filePath, content):
    fileWrite = open(filePath, 'w',encoding='UTF-8')
    fileWrite.write(content)
    fileWrite.close( )
    

#--------------------------------------------
# 读取文件
#--------------------------------------------

#----模板代码
ModuleName = "Protocol" #协议模块名称
InfoStr = "---@type Protocol\n\n"
TemplateStr = "local " + ModuleName + " = {}" + "\n"
EndTemplateStr = "return " + ModuleName + "\n"

writeStrList = []

allStr = ""    #拼接的字符串,用来写文件
lastClass = "" #记录上一个ClassName
moduleDefineStr = "" #模块new代码

#-------开始生成代码
allStr += InfoStr
allStr += TemplateStr

#files = os.listdir(".")
#files = [f for f in files if f.endswith(".proto")]

fileindex = 0
moduleCount = 0
multiFiles = False

#for file in files:
fileRead = open(clientInDir,'r',encoding='UTF-8')             # 返回一个文件对象
line = fileRead.readline()             # 调用文件的 readline()方法
        
while line:
    rptStr = ""
    moduleDefineStr = ""
    #print '----line = ' + line
    rptStr = ReplaceStr(line)          #自己写的替换字符串
    rptStr,tmpCls = CheckType(rptStr,lastClass)
    
    if tmpCls != "":
        if lastClass != "":
            moduleDefineStr = GenerateModuleCode(lastClass, ModuleName)
            allStr += moduleDefineStr
            moduleCount = moduleCount + 1
            
        lastClass = tmpCls

    if moduleCount >= 100:
        writeStrList.append(allStr)
        fileindex = fileindex + 1
        allStr = ""
        moduleCount = 0
        multiFiles = True
                        
    allStr += rptStr
    
    line = fileRead.readline()

        #---文件最后要补一次定义
if lastClass != "":
    moduleDefineStr = GenerateModuleCode(lastClass, ModuleName)
    allStr += moduleDefineStr
                
fileRead.close()

#if multiFiles == False:
#    writeStrList.append(allStr)
writeStrList.append(allStr)

writeStrList[0] += EndTemplateStr
    
#allStr += EndTemplateStr
#--------生成代码结束

#--------------------------------------------
# 写文件
#--------------------------------------------
for index in range(len(writeStrList)):
    if index == 0:
        filepath = os.path.join(outputDir, 'LuaProto.lua')
        print ('====Generated proto file for lua.txt ' + 'LuaProto.lua')
        ExcuteWriteFile(filepath, writeStrList[index])
    else:
        finalfilename = 'LuaProto' + str(index) + '.lua'
        exfilepath = os.path.join(outputDir, finalfilename)
        print ('====Generated proto file for lua.txt ' + finalfilename)
        ExcuteWriteFile(exfilepath, writeStrList[index])
