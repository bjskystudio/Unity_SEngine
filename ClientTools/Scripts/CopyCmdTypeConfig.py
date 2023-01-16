#!/usr/bin/python -*- coding: UTF-8 -*-
 
import re
import os, sys

srcCmdPath = sys.argv[1]
copyToPath = sys.argv[2]

srcCmdFile = os.path.join(srcCmdPath, 'CmdConfig.txt')
copyCmdFile = os.path.join(copyToPath, 'CmdConfig.txt')

#删除之前的文件
if os.path.exists(copyCmdFile):
    os.remove(copyCmdFile)

towriteContent = ''
fileRead = open(srcCmdFile)
line = fileRead.readline()
while line:
    line = line.strip()
    if line == None or len(line) == 0:
        line = fileRead.readline()
        continue
    if line[0] == '#':
        line = fileRead.readline()
        continue
    towriteContent = towriteContent + line
    line = fileRead.readline()
    if not line == None and len(line) > 0:
        towriteContent = towriteContent + ';'
fileRead.close()

fileWrite = open(copyCmdFile, 'w')
fileWrite.write(towriteContent)
fileWrite.close()
print "Copy CmdConfig.txt OK"







