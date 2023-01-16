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
    public class GameNetworkNetworkKitWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Game.Network.NetworkKit);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 9, 2, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Init", _m_Init_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "UnInit", _m_UnInit_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Connect", _m_Connect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SendServerHttpRequest", _m_SendServerHttpRequest_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "DisConnect", _m_DisConnect_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsConnected", _m_IsConnected_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SendLua", _m_SendLua_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TestMessage", _m_TestMessage_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "DefaultSocketChannel", _g_get_DefaultSocketChannel);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "IsIOSMJ", _g_get_IsIOSMJ);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "IsIOSMJ", _s_set_IsIOSMJ);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "Game.Network.NetworkKit does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.Network.NetworkKit.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UnInit_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.Network.NetworkKit.UnInit(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Connect_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _ip = LuaAPI.lua_tostring(L, 1);
                    int _port = LuaAPI.xlua_tointeger(L, 2);
                    System.Action<int, string> _eventCollect = translator.GetDelegate<System.Action<int, string>>(L, 3);
                    
                    Game.Network.NetworkKit.Connect( _ip, _port, _eventCollect );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendServerHttpRequest_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string _url = LuaAPI.lua_tostring(L, 1);
                    string _postData = LuaAPI.lua_tostring(L, 2);
                    System.Action<int, string> _callback = translator.GetDelegate<System.Action<int, string>>(L, 3);
                    
                    Game.Network.NetworkKit.SendServerHttpRequest( _url, _postData, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DisConnect_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.Network.NetworkKit.DisConnect(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsConnected_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        var gen_ret = Game.Network.NetworkKit.IsConnected(  );
                        LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SendLua_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    short _cmd = (short)LuaAPI.xlua_tointeger(L, 1);
                    byte[] _bytes = LuaAPI.lua_tobytes(L, 2);
                    
                    Game.Network.NetworkKit.SendLua( _cmd, _bytes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TestMessage_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                    Game.Network.NetworkKit.TestMessage(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DefaultSocketChannel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.PushAny(L, Game.Network.NetworkKit.DefaultSocketChannel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsIOSMJ(RealStatePtr L)
        {
		    try {
            
			    LuaAPI.lua_pushboolean(L, Game.Network.NetworkKit.IsIOSMJ);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IsIOSMJ(RealStatePtr L)
        {
		    try {
                
			    Game.Network.NetworkKit.IsIOSMJ = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
