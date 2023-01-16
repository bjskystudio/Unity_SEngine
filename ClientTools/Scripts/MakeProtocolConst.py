#!/usr/bin/python
# -*- coding: UTF-8 -*-
 
import re
import os, sys

outputDir = sys.argv[2]
inputDir = sys.argv[1]

clientOutDir = os.path.join(outputDir, 'LuaProtoConst.lua.txt')
clientInDir = os.path.join(inputDir, 'const.txt')

#-------开始生成代码
allStr = ""
allStr += '---ProtoConst, all Global Const \n\n'
fileRead = open(clientInDir)
line = fileRead.readline()

while line:
    line = line.strip()
    if line == None or len(line) == 0 or line[0] == '#':
        line = fileRead.readline()
        continue
    lineSplit = line.split('-')
    if len(lineSplit) > 2:
        addstr = 'ProtocolConst_' + lineSplit[1] + ' = ' + lineSplit[0] + '\n'
        allStr += addstr
    line = fileRead.readline()
    
fileRead.close()
allStr += '\n\n'
#--------生成代码结束

#--------------------------------------------
# 写文件
#--------------------------------------------
fileWrite = open(clientOutDir, 'w')
fileWrite.write(allStr)
fileWrite.close( )
print "Generated LuaProtoConst.lua.txt!"
