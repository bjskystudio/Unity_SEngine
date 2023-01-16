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
    public class GameNetworkMessageQueueHandlerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.Network.MessageQueueHandler);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 0, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UnInit", _m_UnInit);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PushQueue", _m_PushQueue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PushError", _m_PushError);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DispatchNetMessage", _m_DispatchNetMessage);
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 1, 1);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "luaOnRecvMessage", _g_get_luaOnRecvMessage);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "luaOnRecvMessage", _s_set_luaOnRecvMessage);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new Game.Network.MessageQueueHandler();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Game.Network.MessageQueueHandler constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnInit(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Network.MessageQueueHandler gen_to_be_invoked = (Game.Network.MessageQueueHandler)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.UnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PushQueue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Network.MessageQueueHandler gen_to_be_invoked = (Game.Network.MessageQueueHandler)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _cmd = LuaAPI.xlua_tointeger(L, 2);
                    Game.Network.MessageData _msgData;translator.Get(L, 3, out _msgData);
                    
                    gen_to_be_invoked.PushQueue( _cmd, _msgData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PushError(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Network.MessageQueueHandler gen_to_be_invoked = (Game.Network.MessageQueueHandler)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    short _state = (short)LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.PushError( _msg, _state );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _msg = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.PushError( _msg );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Game.Network.MessageQueueHandler.PushError!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DispatchNetMessage(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Game.Network.MessageQueueHandler gen_to_be_invoked = (Game.Network.MessageQueueHandler)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _cmd = LuaAPI.xlua_tointeger(L, 2);
                    byte[] _cacheBuff = LuaAPI.lua_tobytes(L, 3);
                    int _len = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.DispatchNetMessage( _cmd, _cacheBuff, _len );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_luaOnRecvMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, Game.Network.MessageQueueHandler.luaOnRecvMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_luaOnRecvMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Game.Network.MessageQueueHandler.luaOnRecvMessage = translator.GetDelegate<Game.Network.MessageQueueHandler.LuaOnRecvMessage>(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
