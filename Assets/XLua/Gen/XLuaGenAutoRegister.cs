#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
	{
        
        
        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(EventDef), EventDefWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(Launcher), LauncherWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(StartUpManager), StartUpManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(object), SystemObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Object), UnityEngineObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.CapsuleCollider), UnityEngineCapsuleColliderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Ray2D), UnityEngineRay2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.GameObject), UnityEngineGameObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Component), UnityEngineComponentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Behaviour), UnityEngineBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Transform), UnityEngineTransformWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Resources), UnityEngineResourcesWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.TextAsset), UnityEngineTextAssetWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Keyframe), UnityEngineKeyframeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AnimationCurve), UnityEngineAnimationCurveWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AnimationClip), UnityEngineAnimationClipWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.MonoBehaviour), UnityEngineMonoBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystem), UnityEngineParticleSystemWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SkinnedMeshRenderer), UnityEngineSkinnedMeshRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Renderer), UnityEngineRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SpriteRenderer), UnityEngineSpriteRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<int>), SystemCollectionsGenericList_1_SystemInt32_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Debug), UnityEngineDebugWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent), UnityEngineEventsUnityEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RenderTexture), UnityEngineRenderTextureWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RenderTextureFormat), UnityEngineRenderTextureFormatWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.ScriptableObject), UnityEngineScriptableObjectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Bounds), UnityEngineBoundsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Color), UnityEngineColorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.LayerMask), UnityEngineLayerMaskWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Mathf), UnityEngineMathfWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Plane), UnityEnginePlaneWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Quaternion), UnityEngineQuaternionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Camera), UnityEngineCameraWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Ray), UnityEngineRayWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RaycastHit), UnityEngineRaycastHitWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Time), UnityEngineTimeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Touch), UnityEngineTouchWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.TouchPhase), UnityEngineTouchPhaseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Vector2), UnityEngineVector2Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Vector3), UnityEngineVector3Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Vector4), UnityEngineVector4Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Matrix4x4), UnityEngineMatrix4x4Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Application), UnityEngineApplicationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.LogType), UnityEngineLogTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RenderMode), UnityEngineRenderModeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AdditionalCanvasShaderChannels), UnityEngineAdditionalCanvasShaderChannelsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RenderSettings), UnityEngineRenderSettingsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.MaterialPropertyBlock), UnityEngineMaterialPropertyBlockWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Shader), UnityEngineShaderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Canvas), UnityEngineCanvasWrap.__Register);
        
        }
        
        static void wrapInit1(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(UnityEngine.CanvasGroup), UnityEngineCanvasGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Rect), UnityEngineRectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RectTransform), UnityEngineRectTransformWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RectOffset), UnityEngineRectOffsetWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Sprite), UnityEngineSpriteWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.BoxCollider2D), UnityEngineBoxCollider2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Rigidbody2D), UnityEngineRigidbody2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RigidbodyType2D), UnityEngineRigidbodyType2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Sprites.DataUtility), UnityEngineSpritesDataUtilityWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Input), UnityEngineInputWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Random), UnityEngineRandomWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Font), UnityEngineFontWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.CanvasScaler), UnityEngineUICanvasScalerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.CanvasScaler.ScaleMode), UnityEngineUICanvasScalerScaleModeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode), UnityEngineUICanvasScalerScreenMatchModeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.GraphicRaycaster), UnityEngineUIGraphicRaycasterWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Text), UnityEngineUITextWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField), UnityEngineUIInputFieldWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Button), UnityEngineUIButtonWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image), UnityEngineUIImageWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ScrollRect), UnityEngineUIScrollRectWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Scrollbar), UnityEngineUIScrollbarWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Toggle), UnityEngineUIToggleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ToggleGroup), UnityEngineUIToggleGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Button.ButtonClickedEvent), UnityEngineUIButtonButtonClickedEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ScrollRect.ScrollRectEvent), UnityEngineUIScrollRectScrollRectEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.GridLayoutGroup), UnityEngineUIGridLayoutGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.VerticalLayoutGroup), UnityEngineUIVerticalLayoutGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.HorizontalLayoutGroup), UnityEngineUIHorizontalLayoutGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ContentSizeFitter), UnityEngineUIContentSizeFitterWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Slider), UnityEngineUISliderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.TextAnchor), UnityEngineTextAnchorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.RawImage), UnityEngineUIRawImageWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RectTransformUtility), UnityEngineRectTransformUtilityWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Outline), UnityEngineUIOutlineWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.NavMesh), UnityEngineAINavMeshWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.NavMeshPath), UnityEngineAINavMeshPathWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.NavMeshAgent), UnityEngineAINavMeshAgentWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.NavMeshData), UnityEngineAINavMeshDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.NavMeshObstacle), UnityEngineAINavMeshObstacleWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.NavMeshObstacleShape), UnityEngineAINavMeshObstacleShapeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.OffMeshLink), UnityEngineAIOffMeshLinkWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.OffMeshLinkData), UnityEngineAIOffMeshLinkDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AI.OffMeshLinkType), UnityEngineAIOffMeshLinkTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Video.VideoPlayer), UnityEngineVideoVideoPlayerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.EventTrigger.Entry), UnityEngineEventSystemsEventTriggerEntryWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.EventTrigger.TriggerEvent), UnityEngineEventSystemsEventTriggerTriggerEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.EventTriggerType), UnityEngineEventSystemsEventTriggerTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.BaseEventData>), UnityEngineEventsUnityEvent_1_UnityEngineEventSystemsBaseEventData_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry>), SystemCollectionsGenericList_1_UnityEngineEventSystemsEventTriggerEntry_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.ResourceRequest), UnityEngineResourceRequestWrap.__Register);
        
        }
        
        static void wrapInit2(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(UnityEngine.SceneManagement.SceneManager), UnityEngineSceneManagementSceneManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SceneManagement.Scene), UnityEngineSceneManagementSceneWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.PlayerPrefs), UnityEnginePlayerPrefsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.GC), SystemGCWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.MaskableGraphic), UnityEngineUIMaskableGraphicWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Reflection.Missing), SystemReflectionMissingWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Random.State), UnityEngineRandomStateWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.MaskableGraphic.CullStateChangedEvent), UnityEngineUIMaskableGraphicCullStateChangedEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.UIBehaviour), UnityEngineEventSystemsUIBehaviourWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Texture2D), UnityEngineTexture2DWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Texture2D.EXRFlags), UnityEngineTexture2DEXRFlagsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Camera.MonoOrStereoscopicEye), UnityEngineCameraMonoOrStereoscopicEyeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Camera.StereoscopicEye), UnityEngineCameraStereoscopicEyeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.WrapMode), UnityEngineWrapModeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Physics), UnityEnginePhysicsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RectTransform.Axis), UnityEngineRectTransformAxisWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.RectTransform.Edge), UnityEngineRectTransformEdgeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.DateTime), SystemDateTimeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.DayOfWeek), SystemDayOfWeekWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Diagnostics.Stopwatch), SystemDiagnosticsStopwatchWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects), UnityEngineUIGraphicRaycasterBlockingObjectsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField.OnChangeEvent), UnityEngineUIInputFieldOnChangeEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField.SubmitEvent), UnityEngineUIInputFieldSubmitEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField.LineType), UnityEngineUIInputFieldLineTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField.CharacterValidation), UnityEngineUIInputFieldCharacterValidationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField.InputType), UnityEngineUIInputFieldInputTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.InputField.ContentType), UnityEngineUIInputFieldContentTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Selectable), UnityEngineUISelectableWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Selectable.Transition), UnityEngineUISelectableTransitionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AudioListener), UnityEngineAudioListenerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AudioSource), UnityEngineAudioSourceWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AudioRolloffMode), UnityEngineAudioRolloffModeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.BaseRaycaster), UnityEngineEventSystemsBaseRaycasterWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Slider.SliderEvent), UnityEngineUISliderSliderEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Slider.Direction), UnityEngineUISliderDirectionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.Origin360), UnityEngineUIImageOrigin360Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.Origin180), UnityEngineUIImageOrigin180Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.Origin90), UnityEngineUIImageOrigin90Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.OriginVertical), UnityEngineUIImageOriginVerticalWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.OriginHorizontal), UnityEngineUIImageOriginHorizontalWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.FillMethod), UnityEngineUIImageFillMethodWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Image.Type), UnityEngineUIImageTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent<string>), UnityEngineEventsUnityEvent_1_SystemString_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent<UnityEngine.GameObject, UnityEngine.EventSystems.PointerEventData>), UnityEngineEventsUnityEvent_2_UnityEngineGameObjectUnityEngineEventSystemsPointerEventData_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEventBase), UnityEngineEventsUnityEventBaseWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.AudioClip), UnityEngineAudioClipWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.BoxCollider), UnityEngineBoxColliderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.SphereCollider), UnityEngineSphereColliderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Collider), UnityEngineColliderWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Animation), UnityEngineAnimationWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Reflection.BindingFlags), SystemReflectionBindingFlagsWrap.__Register);
        
        }
        
        static void wrapInit3(LuaEnv luaenv, ObjectTranslator translator)
        {
        
            translator.DelayWrapLoader(typeof(System.Array), SystemArrayWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Texture), UnityEngineTextureWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Material), UnityEngineMaterialWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Screen), UnityEngineScreenWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.PointerEventData.FramePressState), UnityEngineEventSystemsPointerEventDataFramePressStateWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.PointerEventData.InputButton), UnityEngineEventSystemsPointerEventDataInputButtonWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.EventSystems.AbstractEventData), UnityEngineEventSystemsAbstractEventDataWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.ValueType), SystemValueTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.GridLayoutGroup.Constraint), UnityEngineUIGridLayoutGroupConstraintWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.GridLayoutGroup.Axis), UnityEngineUIGridLayoutGroupAxisWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.GridLayoutGroup.Corner), UnityEngineUIGridLayoutGroupCornerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.LayoutGroup), UnityEngineUILayoutGroupWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Toggle.ToggleEvent), UnityEngineUIToggleToggleEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Toggle.ToggleTransition), UnityEngineUIToggleToggleTransitionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent<bool>), UnityEngineEventsUnityEvent_1_SystemBoolean_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Events.UnityEvent<float>), UnityEngineEventsUnityEvent_1_SystemSingle_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility), UnityEngineUIScrollRectScrollbarVisibilityWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.ScrollRect.MovementType), UnityEngineUIScrollRectMovementTypeWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.ParticleSystemRenderer), UnityEngineParticleSystemRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.MeshRenderer), UnityEngineMeshRendererWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Scrollbar.ScrollEvent), UnityEngineUIScrollbarScrollEventWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Scrollbar.Direction), UnityEngineUIScrollbarDirectionWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Collections.Generic.List<UnityEngine.GameObject>), SystemCollectionsGenericList_1_UnityEngineGameObject_Wrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Shadow), UnityEngineUIShadowWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.Reflection.MemberInfo), SystemReflectionMemberInfoWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(System.TimeSpan), SystemTimeSpanWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TMPro.TMP_Dropdown), TMProTMP_DropdownWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.UI.Dropdown), UnityEngineUIDropdownWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(TMPro.TextAlignmentOptions), TMProTextAlignmentOptionsWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Playables.PlayableAsset), UnityEnginePlayablesPlayableAssetWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Playables.PlayableDirector), UnityEnginePlayablesPlayableDirectorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UnityEngine.Animator), UnityEngineAnimatorWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(XLuaManager), XLuaManagerWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(UIModel), UIModelWrap.__Register);
        
        
            translator.DelayWrapLoader(typeof(SEngine.UnityObjectExtends), SEngineUnityObjectExtendsWrap.__Register);
        
        
        
        }
        
        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {
            
            wrapInit0(luaenv, translator);
            
            wrapInit1(luaenv, translator);
            
            wrapInit2(luaenv, translator);
            
            wrapInit3(luaenv, translator);
            
            
        }
        
	    static XLua_Gen_Initer_Register__()
        {
		    XLua.LuaEnv.AddIniter(Init);
		}
		
		
	}
	
}
namespace XLua
{
	public partial class ObjectTranslator
	{
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
		static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj {get{return s_gen_reg_dumb_obj;}}
	}
	
	internal partial class InternalGlobals
    {
	    
	    static InternalGlobals()
		{
		    extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
			{
			    
			};
			
			genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
		}
	}
}
