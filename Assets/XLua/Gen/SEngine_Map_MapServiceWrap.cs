#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class SEngineMapMapServiceWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(SEngine.Map.MapService);
			Utils.BeginObjectRegister(type, L, translator, 0, 51, 3, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetMapBounds", _m_GetMapBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPutAreaBounds", _m_GetPutAreaBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPhyTilemap", _m_GetPhyTilemap);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WallCellToUseCell", _m_WallCellToUseCell);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTileCellListFromBounds", _m_GetTileCellListFromBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UseCellToWallCell", _m_UseCellToWallCell);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetDoorPos", _m_GetDoorPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetDoorPosListCount", _m_GetDoorPosListCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetWaiterRanomBornPos", _m_GetWaiterRanomBornPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPhyActive", _m_SetPhyActive);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTile", _m_GetTile);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTileMap", _m_GetTileMap);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CellPosIsInPutArea", _m_CellPosIsInPutArea);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CellPosCanMove", _m_CellPosCanMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CellPosCanUse", _m_CellPosCanUse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WorldPosCanUse", _m_WorldPosCanUse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WorldPosCanMove", _m_WorldPosCanMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRanomdCanMovePos", _m_GetRanomdCanMovePos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCanMovePos", _m_GetCanMovePos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindNearstCanMoveWorldPos", _m_FindNearstCanMoveWorldPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "FindNearstCanMovePos", _m_FindNearstCanMovePos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTileByCellPos", _m_GetTileByCellPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTilePhyFlag", _m_GetTilePhyFlag);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCellCanMove", _m_SetCellCanMove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCellCanStand", _m_SetCellCanStand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCellCanQueue", _m_SetCellCanQueue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCellCanInteract", _m_SetCellCanInteract);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCellCanCoinPos", _m_SetCellCanCoinPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RestPhyPoints", _m_RestPhyPoints);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCellCanUse", _m_SetCellCanUse);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WorldPosToCellPos", _m_WorldPosToCellPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LocalPosToCellPos", _m_LocalPosToCellPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CellPosToWorldPos", _m_CellPosToWorldPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CellPosToLocalPos", _m_CellPosToLocalPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetCellPosBelongPutArea", _m_GetCellPosBelongPutArea);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTileByBounds", _m_SetTileByBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetWorldPosBelongPutArea", _m_GetWorldPosBelongPutArea);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetmapComponent", _m_SetmapComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetmapComponent", _m_GetmapComponent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitTile", _m_InitTile);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitAstar", _m_InitAstar);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitPath", _m_InitPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetPath", _m_GetPath);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanMovePoint", _m_SetCanMovePoint);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanStand", _m_SetCanStand);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanQueue", _m_SetCanQueue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanInteract", _m_SetCanInteract);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCanCoinPos", _m_SetCanCoinPos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPutAreaBounds", _m_SetPutAreaBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WallObjectFindMovePos", _m_WallObjectFindMovePos);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "WallWorldPosToFloorCellPos", _m_WallWorldPosToFloorCellPos);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "DoorPathPosList", _g_get_DoorPathPosList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mapId", _g_get_mapId);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Line", _g_get_Line);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "DoorPathPosList", _s_set_DoorPathPosList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mapId", _s_set_mapId);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Line", _s_set_Line);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 2, 2);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UnUseCellPos", _g_get_UnUseCellPos);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "UnUsePos", _g_get_UnUsePos);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UnUseCellPos", _s_set_UnUseCellPos);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "UnUsePos", _s_set_UnUsePos);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new SEngine.Map.MapService();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetMapBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 2)) 
                {
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 2, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetMapBounds( _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.GetMapBounds(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetMapBounds!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPutAreaBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 2)) 
                {
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 2, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetPutAreaBounds( _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.GetPutAreaBounds(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetPutAreaBounds!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPhyTilemap(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 2)) 
                {
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 2, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetPhyTilemap( _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1) 
                {
                    
                        var gen_ret = gen_to_be_invoked.GetPhyTilemap(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetPhyTilemap!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WallCellToUseCell(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.WallCellToUseCell( _pos, _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTileCellListFromBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.BoundsInt _bounds;translator.Get(L, 2, out _bounds);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetTileCellListFromBounds( _bounds, _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UseCellToWallCell(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.UseCellToWallCell( _pos, _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDoorPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetDoorPos( _index );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDoorPosListCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetDoorPosListCount(  );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetWaiterRanomBornPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetWaiterRanomBornPos(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPhyActive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    bool _active = LuaAPI.lua_toboolean(L, 2);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                    gen_to_be_invoked.SetPhyActive( _active, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _active = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetPhyActive( _active );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetPhyActive!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTile(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 2, out _tileMapType);
                    bool _physic = LuaAPI.lua_toboolean(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetTile( _tileMapType, _physic );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTileMap(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _mapid = LuaAPI.xlua_tointeger(L, 2);
                    
                        var gen_ret = gen_to_be_invoked.GetTileMap( _mapid );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 2)) 
                {
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 2, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetTileMap( _tileMapType );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetTileMap!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CellPosIsInPutArea(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.CellPosIsInPutArea( _pos, _tileMapType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.CellPosIsInPutArea( _pos );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.CellPosIsInPutArea!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CellPosCanMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.CellPosCanMove( _pos, _tileMapType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.CellPosCanMove( _pos );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.CellPosCanMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CellPosCanUse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.CellPosCanUse( _pos, _tileMapType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.CellPosCanUse( _pos );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.CellPosCanUse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WorldPosCanUse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosCanUse( _pos, _tileMapType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosCanUse( _pos );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.WorldPosCanUse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WorldPosCanMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosCanMove( _pos, _mapId, _tileMapType );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosCanMove( _pos, _mapId );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosCanMove( _pos );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.WorldPosCanMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRanomdCanMovePos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 5)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    bool _isOutDoor = LuaAPI.lua_toboolean(L, 4);
                    SEngine.Map.Enum.EnTileMapType _enTileMapType;translator.Get(L, 5, out _enTileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetRanomdCanMovePos( _pos, _offset, _isOutDoor, _enTileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    bool _isOutDoor = LuaAPI.lua_toboolean(L, 4);
                    
                        var gen_ret = gen_to_be_invoked.GetRanomdCanMovePos( _pos, _offset, _isOutDoor );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetRanomdCanMovePos( _pos, _offset );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetRanomdCanMovePos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCanMovePos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    SEngine.Map.Enum.EnTileMapType _enTileMapType;translator.Get(L, 4, out _enTileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetCanMovePos( _pos, _offset, _enTileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    int _offset = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetCanMovePos( _pos, _offset );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetCanMovePos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearstCanMoveWorldPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.FindNearstCanMoveWorldPos( _pos );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_FindNearstCanMovePos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _enTileMapType;translator.Get(L, 3, out _enTileMapType);
                    
                        var gen_ret = gen_to_be_invoked.FindNearstCanMovePos( _pos, _enTileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.FindNearstCanMovePos( _pos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.FindNearstCanMovePos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTileByCellPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetTileByCellPos( _pos, _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.GetTileByCellPos( _pos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetTileByCellPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTilePhyFlag(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.GetTilePhyFlag( _pos, _tileMapType );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.GetTilePhyFlag( _pos );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetTilePhyFlag!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCellCanMove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                    gen_to_be_invoked.SetCellCanMove( _pos, _canMove, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCellCanMove( _pos, _canMove );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetCellCanMove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCellCanStand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                    gen_to_be_invoked.SetCellCanStand( _pos, _canMove, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCellCanStand( _pos, _canMove );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetCellCanStand!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCellCanQueue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                    gen_to_be_invoked.SetCellCanQueue( _pos, _canMove, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCellCanQueue( _pos, _canMove );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetCellCanQueue!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCellCanInteract(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                    gen_to_be_invoked.SetCellCanInteract( _pos, _canMove, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCellCanInteract( _pos, _canMove );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetCellCanInteract!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCellCanCoinPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                    gen_to_be_invoked.SetCellCanCoinPos( _pos, _canMove, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    bool _canMove = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCellCanCoinPos( _pos, _canMove );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetCellCanCoinPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RestPhyPoints(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RestPhyPoints(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCellCanUse(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 5)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    bool _canUse = LuaAPI.lua_toboolean(L, 3);
                    int _mapId = LuaAPI.xlua_tointeger(L, 4);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 5, out _tileMapType);
                    
                    gen_to_be_invoked.SetCellCanUse( _pos, _canUse, _mapId, _tileMapType );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    bool _canUse = LuaAPI.lua_toboolean(L, 3);
                    int _mapId = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.SetCellCanUse( _pos, _canUse, _mapId );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3Int>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3Int _pos;translator.Get(L, 2, out _pos);
                    bool _canUse = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.SetCellCanUse( _pos, _canUse );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.SetCellCanUse!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WorldPosToCellPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosToCellPos( _pos, _mapId, _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosToCellPos( _pos, _mapId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.WorldPosToCellPos( _pos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.WorldPosToCellPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LocalPosToCellPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.LocalPosToCellPos( _pos, _mapId, _tileMapType );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.LocalPosToCellPos( _pos, _mapId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.LocalPosToCellPos( _pos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.LocalPosToCellPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CellPosToWorldPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.CellPosToWorldPos( _pos, _mapId, _tileMapType );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.CellPosToWorldPos( _pos, _mapId );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.CellPosToWorldPos( _pos );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.CellPosToWorldPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CellPosToLocalPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<SEngine.Map.Enum.EnTileMapType>(L, 4)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 4, out _tileMapType);
                    
                        var gen_ret = gen_to_be_invoked.CellPosToLocalPos( _pos, _mapId, _tileMapType );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.CellPosToLocalPos( _pos, _mapId );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.CellPosToLocalPos( _pos );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.CellPosToLocalPos!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetCellPosBelongPutArea(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _cellPos;translator.Get(L, 2, out _cellPos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetCellPosBelongPutArea( _cellPos, _mapId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _cellPos;translator.Get(L, 2, out _cellPos);
                    
                        var gen_ret = gen_to_be_invoked.GetCellPosBelongPutArea( _cellPos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetCellPosBelongPutArea!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTileByBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.BoundsInt _bounds;translator.Get(L, 2, out _bounds);
                    SEngine.Map.Enum.EnTileMapType _tileMapType;translator.Get(L, 3, out _tileMapType);
                    bool _physical = LuaAPI.lua_toboolean(L, 4);
                    UnityEngine.Color _color;translator.Get(L, 5, out _color);
                    
                    gen_to_be_invoked.SetTileByBounds( _bounds, _tileMapType, _physical, _color );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetWorldPosBelongPutArea(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.Vector3>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    int _mapId = LuaAPI.xlua_tointeger(L, 3);
                    
                        var gen_ret = gen_to_be_invoked.GetWorldPosBelongPutArea( _pos, _mapId );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.Vector3>(L, 2)) 
                {
                    UnityEngine.Vector3 _pos;translator.Get(L, 2, out _pos);
                    
                        var gen_ret = gen_to_be_invoked.GetWorldPosBelongPutArea( _pos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetWorldPosBelongPutArea!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetmapComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    SEngine.Map.MapComponent _mapComponent = (SEngine.Map.MapComponent)translator.GetObject(L, 2, typeof(SEngine.Map.MapComponent));
                    
                    gen_to_be_invoked.SetmapComponent( _mapComponent );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetmapComponent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        var gen_ret = gen_to_be_invoked.GetmapComponent(  );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitTile(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    SEngine.Map.Enum.EnTileMapType _enTileMapType;translator.Get(L, 2, out _enTileMapType);
                    
                    gen_to_be_invoked.InitTile( _enTileMapType );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitAstar(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.InitAstar( _mapId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    SEngine.Map.Astar _pathFinding = (SEngine.Map.Astar)translator.GetObject(L, 2, typeof(SEngine.Map.Astar));
                    UnityEngine.BoundsInt _mapbounds;translator.Get(L, 3, out _mapbounds);
                    
                    gen_to_be_invoked.InitPath( _pathFinding, _mapbounds );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPath(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 5&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& translator.Assignable<UnityEngine.Vector3>(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector3 _startPos;translator.Get(L, 3, out _startPos);
                    UnityEngine.Vector3 _endPos;translator.Get(L, 4, out _endPos);
                    bool _isIgnoreCorner = LuaAPI.lua_toboolean(L, 5);
                    
                        var gen_ret = gen_to_be_invoked.GetPath( _mapId, _startPos, _endPos, _isIgnoreCorner );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<UnityEngine.Vector3>(L, 3)&& translator.Assignable<UnityEngine.Vector3>(L, 4)) 
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector3 _startPos;translator.Get(L, 3, out _startPos);
                    UnityEngine.Vector3 _endPos;translator.Get(L, 4, out _endPos);
                    
                        var gen_ret = gen_to_be_invoked.GetPath( _mapId, _startPos, _endPos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.Map.MapService.GetPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanMovePoint(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapid = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector2 _pos;translator.Get(L, 3, out _pos);
                    UnityEngine.Vector2 _size;translator.Get(L, 4, out _size);
                    
                    gen_to_be_invoked.SetCanMovePoint( _mapid, _pos, _size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanStand(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapid = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector2 _pos;translator.Get(L, 3, out _pos);
                    UnityEngine.Vector2 _size;translator.Get(L, 4, out _size);
                    
                    gen_to_be_invoked.SetCanStand( _mapid, _pos, _size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanQueue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapid = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector2 _pos;translator.Get(L, 3, out _pos);
                    UnityEngine.Vector2 _size;translator.Get(L, 4, out _size);
                    
                    gen_to_be_invoked.SetCanQueue( _mapid, _pos, _size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanInteract(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapid = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector2 _pos;translator.Get(L, 3, out _pos);
                    UnityEngine.Vector2 _size;translator.Get(L, 4, out _size);
                    
                    gen_to_be_invoked.SetCanInteract( _mapid, _pos, _size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCanCoinPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapid = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector2 _pos;translator.Get(L, 3, out _pos);
                    UnityEngine.Vector2 _size;translator.Get(L, 4, out _size);
                    
                    gen_to_be_invoked.SetCanCoinPos( _mapid, _pos, _size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPutAreaBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _mapId = LuaAPI.xlua_tointeger(L, 2);
                    UnityEngine.Vector3 _pos;translator.Get(L, 3, out _pos);
                    UnityEngine.Vector3 _size;translator.Get(L, 4, out _size);
                    
                    gen_to_be_invoked.SetPutAreaBounds( _mapId, _pos, _size );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WallObjectFindMovePos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _wallWorldPos;translator.Get(L, 2, out _wallWorldPos);
                    
                        var gen_ret = gen_to_be_invoked.WallObjectFindMovePos( _wallWorldPos );
                        translator.PushUnityEngineVector3(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_WallWorldPosToFloorCellPos(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Vector3 _wallWorldPos;translator.Get(L, 2, out _wallWorldPos);
                    
                        var gen_ret = gen_to_be_invoked.WallWorldPosToFloorCellPos( _wallWorldPos );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DoorPathPosList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.DoorPathPosList);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnUseCellPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, SEngine.Map.MapService.UnUseCellPos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UnUsePos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushUnityEngineVector3(L, SEngine.Map.MapService.UnUsePos);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mapId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.mapId);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Line(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.Line);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_DoorPathPosList(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.DoorPathPosList = (System.Collections.Generic.List<UnityEngine.Vector3Int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3Int>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnUseCellPos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			UnityEngine.Vector3Int gen_value;translator.Get(L, 1, out gen_value);
				SEngine.Map.MapService.UnUseCellPos = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UnUsePos(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			UnityEngine.Vector3 gen_value;translator.Get(L, 1, out gen_value);
				SEngine.Map.MapService.UnUsePos = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mapId(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mapId = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Line(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.Map.MapService gen_to_be_invoked = (SEngine.Map.MapService)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Line = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
