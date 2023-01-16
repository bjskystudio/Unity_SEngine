#!/usr/bin/python
# -*- coding: UTF-8 -*-
 
import re
import os, sys

outputDir = sys.argv[2]
inputDir = sys.argv[1]

clientOutDir = os.path.join(outputDir, 'ProtocolEnum.lua')
clientInDir1 = os.path.join(inputDir, 'LuaCProto.proto')
clientInDir2 = os.path.join(inputDir, 'LuaCmd.proto')

CMD_MAX_NUM = 20000

#--------------------------------------------
# 替换字符串方法
#--------------------------------------------
#define a function  
def ReplaceStr(s):
    s = s.strip()              #先把两头多余的空格去掉
    return s

#--------------------------------------------
# 解析package name
#--------------------------------------------
#define a function
def GetPackageName(s):
    retStr = ""

    s = s.strip()
    s = s.replace('=', ' ')    #先把所有的'='替换成'空格'
    s = s.replace('{', ' ')    #先把所有的'{'替换成'空格'
    s = s.replace('\t', ' ')   #先把所有的'\t'替换成'空格'
    s = re.sub(r'\s+', '-', s) #把任意多个'空格'替换成'-'

    arrSplit = s.split('-')

    if len(arrSplit) > 1 and arrSplit[0] == "package":
        retStr = arrSplit[1].strip(';')

    return retStr
	
#--------------------------------------------
# 生成模板代码
#--------------------------------------------
#define a function  
def GenerateModuleCode(s, moduleName, tabName):
	retStr = ""

	#注释行 不解析
	if len(s) > 0 and s[0] == "#":
            return retStr
	
	arrSplit = s.split('#')
	arrLength = len(arrSplit)

	if arrLength >= 3:
            #20000以后的cmd不导出
            if int(arrSplit[0]) > CMD_MAX_NUM:
                return retStr
            retStr += moduleName + "." + arrSplit[1] + " = " + arrSplit[0] + "\n"
            retStr += moduleName + "." + tabName + "[" + arrSplit[0] + "]" + " = " + "\"" + arrSplit[2] + "\"" + "\n\n"
    
	return retStr

#--------------------------------------------
# 读取文件
#--------------------------------------------

#----模板代码
allStr = ""
InfoStr = "---@class ProtocolEnum\n"
ModuleName = "ProtocolEnum" #协议枚举模块名称
TableName = "MessageName" #协议结构体名称table
PackageName = "" #proto名

TemplateStr = "local " + ModuleName + " = Class('"+ ModuleName + "')" + "\n"
EndTemplateStr = "return " + ModuleName + "\n"

#-------开始生成代码
#----先从proto里读package name

fileRead = open(clientInDir1,'r',encoding='UTF-8')
line = fileRead.readline()

while line:
    tmpPackName = GetPackageName(line)

    if tmpPackName != "":
        PackageName = tmpPackName
        break
    
    line = fileRead.readline()
    
fileRead.close()
    

PackageNameStr = ModuleName + ".packageName = " + "\"" + PackageName + "\""

#先把定义写在开头
allStr += InfoStr
allStr += TemplateStr
allStr += PackageNameStr
allStr += "\n"
allStr += ModuleName + "." + TableName + " = {}" + "\n\n"


#files = os.listdir(".")
#files = [f for f in files if f.endswith(".proto")]

#for file in files:
fileRead = open(clientInDir2,'r',encoding='UTF-8')             # 返回一个文件对象
line = fileRead.readline()             # 调用文件的 readline()方法
                        
while line:
    rptStr = ""
    moduleDefineStr = ""
    rptStr = ReplaceStr(line)          #自己写的替换字符串
    rptStr = GenerateModuleCode(rptStr,ModuleName,TableName)
    allStr += rptStr
    line = fileRead.readline()

fileRead.close()


allStr += EndTemplateStr
#--------生成代码结束

#--------------------------------------------
# 写文件
#--------------------------------------------
fileWrite = open(clientOutDir, 'w',encoding='UTF-8')
fileWrite.write(allStr)
fileWrite.close( )
print ("Generated LuaProtoEnum.lua!")
