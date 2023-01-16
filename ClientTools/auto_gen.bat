call config.bat
@echo on
set PROTOCOL_DIR=%PROJ_DIR%\ClientTools\Protocal
set LUA_PROTO_DIR=%PROJ_DIR%\Assets\Res\PBData
set LUA_PROTO_CONFIG_DIR=%PROJ_DIR%\Assets\Res\LuaScript\LuaConfig\Proto
set SCRIPTS_PATH=%PROJ_DIR%\ClientTools\Scripts
set Proto=%PROJ_DIR%\ClientTools\ProtobufTool_Rect

cd %SCRIPTS_PATH%\
%PYTHON% %SCRIPTS_PATH%\GenLuaProto.py %PROTOCOL_DIR%

cd %PROTOCOL_DIR%
%Proto%\protoc.exe  --csharp_out=%PROJ_DIR%\Assets\Script\Game\Protocol\ CProto.proto
%Proto%\protoc.exe  --descriptor_set_out=%LUA_PROTO_DIR%\LuaCProto.pb.bytes LuaCProto.proto

cd %SCRIPTS_PATH%\
%PYTHON% %SCRIPTS_PATH%\MakeProtocol.py %PROTOCOL_DIR% %LUA_PROTO_CONFIG_DIR%
%PYTHON% %SCRIPTS_PATH%\MakeProtocolEnum.py %PROTOCOL_DIR% %LUA_PROTO_CONFIG_DIR%
pause