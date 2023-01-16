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
    public class SEngineUISButtonWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(SEngine.UI.SButton);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 4, 3);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetText", _m_SetText);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnPointerClick", _m_OnPointerClick);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGray", _m_SetGray);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetGrayWithInteractable", _m_SetGrayWithInteractable);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "NaClickIntervalmes", _g_get_NaClickIntervalmes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsGray", _g_get_IsGray);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IgnoreClickInterval", _g_get_IgnoreClickInterval);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "BtnText", _g_get_BtnText);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "NaClickIntervalmes", _s_set_NaClickIntervalmes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "IgnoreClickInterval", _s_set_IgnoreClickInterval);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "BtnText", _s_set_BtnText);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					var gen_ret = new SEngine.UI.SButton();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.UI.SButton constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetText(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _desc = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetText( _desc );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnPointerClick(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.EventSystems.PointerEventData _eventData = (UnityEngine.EventSystems.PointerEventData)translator.GetObject(L, 2, typeof(UnityEngine.EventSystems.PointerEventData));
                    
                    gen_to_be_invoked.OnPointerClick( _eventData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGray(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _bo = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetGray( _bo );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetGrayWithInteractable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _bo = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.SetGrayWithInteractable( _bo );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NaClickIntervalmes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushnumber(L, gen_to_be_invoked.NaClickIntervalmes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsGray(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsGray);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IgnoreClickInterval(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IgnoreClickInterval);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_BtnText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.BtnText);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NaClickIntervalmes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NaClickIntervalmes = (float)LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_IgnoreClickInterval(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.IgnoreClickInterval = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_BtnText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.SButton gen_to_be_invoked = (SEngine.UI.SButton)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.BtnText = (SEngine.UI.STMP)translator.GetObject(L, 2, typeof(SEngine.UI.STMP));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
