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
    public class SEngineUIUISpineWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(SEngine.UI.UISpine);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 4, 4);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnUserDefinedEvent", _m_OnUserDefinedEvent);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSpineAnimationStart", _m_OnSpineAnimationStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSpineAnimationInterrupt", _m_OnSpineAnimationInterrupt);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSpineAnimationEnd", _m_OnSpineAnimationEnd);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSpineAnimationDispose", _m_OnSpineAnimationDispose);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OnSpineAnimationComplete", _m_OnSpineAnimationComplete);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PlayAnim", _m_PlayAnim);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StopAnim", _m_StopAnim);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetBonePropValue", _m_SetBonePropValue);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetSkin", _m_SetSkin);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "SkelAnim", _g_get_SkelAnim);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AnimState", _g_get_AnimState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "Skel", _g_get_Skel);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "completeCallback", _g_get_completeCallback);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "SkelAnim", _s_set_SkelAnim);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "AnimState", _s_set_AnimState);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "Skel", _s_set_Skel);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "completeCallback", _s_set_completeCallback);
            
			
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
					
					var gen_ret = new SEngine.UI.UISpine();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to SEngine.UI.UISpine constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnUserDefinedEvent(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spine.TrackEntry _trackEntry = (Spine.TrackEntry)translator.GetObject(L, 2, typeof(Spine.TrackEntry));
                    Spine.Event _e = (Spine.Event)translator.GetObject(L, 3, typeof(Spine.Event));
                    
                    gen_to_be_invoked.OnUserDefinedEvent( _trackEntry, _e );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSpineAnimationStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spine.TrackEntry _trackEntry = (Spine.TrackEntry)translator.GetObject(L, 2, typeof(Spine.TrackEntry));
                    
                    gen_to_be_invoked.OnSpineAnimationStart( _trackEntry );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSpineAnimationInterrupt(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spine.TrackEntry _trackEntry = (Spine.TrackEntry)translator.GetObject(L, 2, typeof(Spine.TrackEntry));
                    
                    gen_to_be_invoked.OnSpineAnimationInterrupt( _trackEntry );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSpineAnimationEnd(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spine.TrackEntry _trackEntry = (Spine.TrackEntry)translator.GetObject(L, 2, typeof(Spine.TrackEntry));
                    
                    gen_to_be_invoked.OnSpineAnimationEnd( _trackEntry );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSpineAnimationDispose(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spine.TrackEntry _trackEntry = (Spine.TrackEntry)translator.GetObject(L, 2, typeof(Spine.TrackEntry));
                    
                    gen_to_be_invoked.OnSpineAnimationDispose( _trackEntry );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OnSpineAnimationComplete(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Spine.TrackEntry _trackEntry = (Spine.TrackEntry)translator.GetObject(L, 2, typeof(Spine.TrackEntry));
                    
                    gen_to_be_invoked.OnSpineAnimationComplete( _trackEntry );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PlayAnim(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _name = LuaAPI.lua_tostring(L, 2);
                    bool _loop = LuaAPI.lua_toboolean(L, 3);
                    System.Action _complete = translator.GetDelegate<System.Action>(L, 4);
                    
                    gen_to_be_invoked.PlayAnim( _name, _loop, _complete );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StopAnim(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _setSetupPose = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.StopAnim( _setSetupPose );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetBonePropValue(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _boneName = LuaAPI.lua_tostring(L, 2);
                    string _propName = LuaAPI.lua_tostring(L, 3);
                    float _propValue = (float)LuaAPI.lua_tonumber(L, 4);
                    
                    gen_to_be_invoked.SetBonePropValue( _boneName, _propName, _propValue );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSkin(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _skinName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.SetSkin( _skinName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SkelAnim(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.SkelAnim);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AnimState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AnimState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Skel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.Skel);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_completeCallback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.completeCallback);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SkelAnim(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.SkelAnim = (Spine.Unity.SkeletonGraphic)translator.GetObject(L, 2, typeof(Spine.Unity.SkeletonGraphic));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_AnimState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.AnimState = (Spine.AnimationState)translator.GetObject(L, 2, typeof(Spine.AnimationState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Skel(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.Skel = (Spine.Skeleton)translator.GetObject(L, 2, typeof(Spine.Skeleton));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_completeCallback(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                SEngine.UI.UISpine gen_to_be_invoked = (SEngine.UI.UISpine)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.completeCallback = translator.GetDelegate<System.Action>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
