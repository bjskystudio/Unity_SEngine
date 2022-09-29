using SEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class GenConfig
{
    //lua��Ҫʹ�õ�C#������ã�����C#��׼�⣬����Unity API����������ȡ�
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
		// unity
		typeof(System.Object),
        typeof(UnityEngine.Object),
        typeof(UnityEngine.CapsuleCollider),
        typeof(Ray2D),
        typeof(GameObject),
        typeof(Component),
        typeof(Behaviour),
        typeof(Transform),
        typeof(Resources),
        typeof(TextAsset),
        typeof(Keyframe),
        typeof(AnimationCurve),
        typeof(AnimationClip),
        typeof(MonoBehaviour),
        typeof(ParticleSystem),
        typeof(SkinnedMeshRenderer),
        typeof(Renderer),
        typeof(SpriteRenderer),
        typeof(WWW),
        typeof(List<int>),
        typeof(Action<string>),
        typeof(UnityEngine.Debug),
        typeof(Delegate),
        //typeof(Dictionary<string, GameObject>),
        typeof(UnityEngine.Events.UnityEvent),
        typeof(UnityEngine.RenderTexture),
        typeof(UnityEngine.RenderTextureFormat),
        typeof(UnityEngine.ScriptableObject),
        //typeof(StoryTimeLine.StoryTimeLineControl),

        // unity���lua���ⲿ�ֵ����ܶ๦����lua������ʵ�֣�û��ʵ�ֵĹ��ܲŻ��ܵ�cs��
        typeof(Bounds),
        typeof(Color),
        typeof(LayerMask),
        typeof(Mathf),
        typeof(Plane),
        typeof(Quaternion),
        typeof(Camera),
        typeof(Ray),
        typeof(RaycastHit),

        typeof(Time),
        typeof(Touch),
        typeof(TouchPhase),
        typeof(Vector2),
        typeof(Vector3),
        typeof(Vector4),
        typeof(Matrix4x4),
        typeof(Application),
        typeof(UnityEngine.LogType),
        
        // ��Ⱦ
        typeof(RenderMode),
        typeof(AdditionalCanvasShaderChannels),
        typeof(RenderSettings),
        typeof(UnityEngine.MaterialPropertyBlock),
        typeof(UnityEngine.Shader),
        
        // UGUI  
        typeof(UnityEngine.Canvas),
        typeof(UnityEngine.CanvasGroup),
        typeof(UnityEngine.Rect),
        typeof(UnityEngine.RectTransform),
        typeof(UnityEngine.RectOffset),
        typeof(UnityEngine.Sprite),
        typeof(UnityEngine.BoxCollider2D),
        typeof(UnityEngine.Rigidbody2D),
        typeof(UnityEngine.RigidbodyType2D),
        typeof(UnityEngine.Sprites.DataUtility),

        typeof(UnityEngine.Input),
        typeof(UnityEngine.Random),
        typeof(UnityEngine.Font),
        typeof(UnityEngine.UI.CanvasScaler),
        typeof(UnityEngine.UI.CanvasScaler.ScaleMode),
        typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode),
        typeof(UnityEngine.UI.GraphicRaycaster),
        typeof(UnityEngine.UI.Text),
        typeof(UnityEngine.UI.InputField),
        typeof(UnityEngine.UI.Button),
        typeof(UnityEngine.UI.Image),
        typeof(UnityEngine.UI.ScrollRect),
        typeof(UnityEngine.UI.Scrollbar),
        typeof(UnityEngine.UI.Toggle),
        typeof(UnityEngine.UI.ToggleGroup),
        typeof(UnityEngine.UI.Button.ButtonClickedEvent),
        typeof(UnityEngine.UI.ScrollRect.ScrollRectEvent),
        typeof(UnityEngine.UI.GridLayoutGroup),
        typeof(UnityEngine.UI.VerticalLayoutGroup),
        typeof(UnityEngine.UI.HorizontalLayoutGroup),
        typeof(UnityEngine.UI.ContentSizeFitter),
        typeof(UnityEngine.UI.Slider),
        typeof(UnityEngine.TextAnchor),
        typeof(UnityEngine.UI.RawImage),
        typeof(UnityEngine.RectTransformUtility),
        typeof(UnityEngine.UI.Outline),
        typeof(UnityEngine.AI.NavMesh),
        typeof(UnityEngine.AI.NavMeshPath),
        typeof(UnityEngine.AI.NavMeshAgent),
        typeof(UnityEngine.AI.NavMeshData),
        typeof(UnityEngine.AI.NavMeshObstacle),
        typeof(UnityEngine.AI.NavMeshObstacleShape),
        typeof(UnityEngine.AI.OffMeshLink),
        typeof(UnityEngine.AI.OffMeshLinkData),
        typeof(UnityEngine.AI.OffMeshLinkType),
        typeof(UnityEngine.Video.VideoPlayer),

        //Extends
        
        //����

        //�¼�����
        typeof(UnityEngine.EventSystems.EventTrigger.Entry),
        typeof(UnityEngine.EventSystems.EventTrigger.TriggerEvent),
        typeof(UnityEngine.EventSystems.EventTriggerType),
        typeof(UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.BaseEventData>),
        typeof(System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry>),

        // ��������Դ����
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.ResourceRequest),
        typeof(UnityEngine.SceneManagement.SceneManager),
        typeof(UnityEngine.SceneManagement.Scene),

        // ����
        typeof(PlayerPrefs),
        typeof(System.GC),

   

        //XLua
        //typeof(System.Type),
        typeof(UnityEngine.Random),
        typeof(UnityEngine.UI.MaskableGraphic),
        typeof(System.Reflection.Missing),
        typeof(UnityEngine.Random.State),
        typeof(UnityEngine.UI.MaskableGraphic.CullStateChangedEvent),
        typeof(UnityEngine.EventSystems.UIBehaviour),

        typeof(UnityEngine.Texture2D),
        typeof(UnityEngine.Texture2D.EXRFlags),
        typeof(UnityEngine.Camera.MonoOrStereoscopicEye),
        typeof(UnityEngine.Camera.StereoscopicEye),
        typeof(UnityEngine.WrapMode),
        typeof(UnityEngine.Physics),

        typeof(UnityEngine.RectTransform.Axis),
        typeof(UnityEngine.RectTransform.Edge),
        typeof(System.DateTime),
        typeof(System.DayOfWeek),
        typeof(System.Diagnostics.Stopwatch),

        typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects),
        typeof(UnityEngine.UI.InputField.OnChangeEvent),
        typeof(UnityEngine.UI.InputField.SubmitEvent),
        typeof(UnityEngine.UI.InputField.LineType),
        typeof(UnityEngine.UI.InputField.CharacterValidation),
        typeof(UnityEngine.UI.InputField.InputType),
        typeof(UnityEngine.UI.InputField.ContentType),
        typeof(UnityEngine.UI.Selectable),
        typeof(UnityEngine.UI.Selectable.Transition),

        typeof(UnityEngine.AudioListener),
        typeof(UnityEngine.AudioSource),
        typeof(UnityEngine.AudioRolloffMode),
        typeof(UnityEngine.AudioRolloffMode),

        typeof(UnityEngine.EventSystems.BaseRaycaster),
        typeof(UnityEngine.UI.Slider),
        typeof(UnityEngine.UI.Slider.SliderEvent),
        typeof(UnityEngine.UI.Slider.Direction),
        typeof(UnityEngine.UI.Image.Origin360),
        typeof(UnityEngine.UI.Image.Origin180),
        typeof(UnityEngine.UI.Image.Origin90),
        typeof(UnityEngine.UI.Image.OriginVertical),
        typeof(UnityEngine.UI.Image.OriginHorizontal),
        typeof(UnityEngine.UI.Image.FillMethod),
        typeof(UnityEngine.UI.Image.Type),
        typeof(UnityEngine.Events.UnityEvent<System.String>),
        typeof(UnityEngine.Events.UnityEvent<UnityEngine.GameObject,UnityEngine.EventSystems.PointerEventData>),
        typeof(UnityEngine.Events.UnityEventBase),
        typeof(UnityEngine.AudioClip),
        typeof(UnityEngine.BoxCollider),
        typeof(UnityEngine.SphereCollider),
        typeof(UnityEngine.CapsuleCollider),
        typeof(UnityEngine.Collider),
        typeof(UnityEngine.Animation),
        typeof(System.Reflection.BindingFlags),
        typeof(System.Array),
        typeof(UnityEngine.Texture),
        typeof(UnityEngine.Material),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.EventSystems.PointerEventData.FramePressState),
        typeof(UnityEngine.EventSystems.PointerEventData.InputButton),
        typeof(UnityEngine.EventSystems.AbstractEventData),
        typeof(System.ValueType),
        typeof(UnityEngine.UI.GridLayoutGroup.Constraint),
        typeof(UnityEngine.UI.GridLayoutGroup.Axis),
        typeof(UnityEngine.UI.GridLayoutGroup.Corner),
        typeof(UnityEngine.UI.LayoutGroup),
        typeof(UnityEngine.UI.Toggle.ToggleEvent),
        typeof(UnityEngine.UI.Toggle.ToggleTransition),
        typeof(UnityEngine.Events.UnityEvent<System.Boolean>),
        typeof(UnityEngine.Events.UnityEvent<System.Single>),
        typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility),
        typeof(UnityEngine.UI.ScrollRect.MovementType),
        typeof(UnityEngine.ParticleSystemRenderer),
        typeof(UnityEngine.MeshRenderer),
        typeof(UnityEngine.UI.Scrollbar.ScrollEvent),
        typeof(UnityEngine.UI.Scrollbar.Direction),
        typeof(System.Collections.Generic.List<UnityEngine.GameObject>),
        typeof(UnityEngine.UI.Shadow),
        typeof(UnityEngine.Transform),
        typeof(System.Reflection.MemberInfo),
        typeof(System.TimeSpan),
        typeof(TMPro.TMP_Dropdown),
        typeof(UnityEngine.UI.Dropdown),
        typeof(TMPro.TextAlignmentOptions),
        typeof(UnityEngine.Playables.PlayableAsset),
        typeof(UnityEngine.Playables.PlayableDirector),
        typeof(Animator),
        typeof(AudioRolloffMode),

        #region �Զ�����
        typeof(Launcher),
        typeof(XLuaManager),
        typeof(UIModel),
        //typeof(CSTimerManager),
	    #endregion
    };

    //C#��̬����Lua�����ã������¼���ԭ�ͣ�����������delegate��interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>()
    {
    };

        //������
        [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()
    {
    };
    }