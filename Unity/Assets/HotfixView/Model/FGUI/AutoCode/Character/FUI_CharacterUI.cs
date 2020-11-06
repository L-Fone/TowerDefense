/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_CharacterUI_AwakeSystem : AwakeSystem<FUI_CharacterUI, GObject>
    {
        public override void Awake(FUI_CharacterUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_CharacterUI : FUI
    {	
        public const string UIPackageName = "Character";
        public const string UIResName = "CharacterUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_addPoint;
    public FUI_FrameCharacter m_frame;
    public GLoader m_bg;
    public GList m_atkEquipList;
    public GList m_commonEquipList;
    public GList m_infoList;
    public GGroup m_infoGroup;
    public GList m_battleAttributeList;
    public GGroup m_battleGroup;
    public GLoader m_icon;
    public GList m_basicAttributeList;
    public GGroup m_basicGroup;
    public GList m_addPointAttributeList;
    public GButton m_btnAddPoint;
    public GButton m_btnResetPoint;
    public GTextField m_txtPoint;
    public GButton m_btnPlusTransLevel;
    public GButton m_btnMinusTransLevel;
    public GTextField m_txtTransLevel;
    public GGroup m_addpointGroup;
    public Transition m_Effect;
    public const string URL = "ui://768jx61w7bww6";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_CharacterUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_CharacterUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_CharacterUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_CharacterUI> tcs = new ETTaskCompletionSource<FUI_CharacterUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_CharacterUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_CharacterUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_CharacterUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_CharacterUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_CharacterUI>();

        if(fui == null)
        {
            fui = Create(domain,go);
        }

        fui.isFromFGUIPool = true;

        return fui;
    }
        
    public void Awake(GObject go)
    {
        if(go == null)
        {
            return;
        }
        
        GObject = go;	
        
        if (string.IsNullOrWhiteSpace(Name))
        {
            Name = Id.ToString();
        }
        
        self = (GComponent)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_addPoint = com.GetController("addPoint");
    		m_frame = FUI_FrameCharacter.Create(domain, com.GetChild("frame"));
    		m_bg = (GLoader)com.GetChild("bg");
    		m_atkEquipList = (GList)com.GetChild("atkEquipList");
    		m_commonEquipList = (GList)com.GetChild("commonEquipList");
    		m_infoList = (GList)com.GetChild("infoList");
    		m_infoGroup = (GGroup)com.GetChild("infoGroup");
    		m_battleAttributeList = (GList)com.GetChild("battleAttributeList");
    		m_battleGroup = (GGroup)com.GetChild("battleGroup");
    		m_icon = (GLoader)com.GetChild("icon");
    		m_basicAttributeList = (GList)com.GetChild("basicAttributeList");
    		m_basicGroup = (GGroup)com.GetChild("basicGroup");
    		m_addPointAttributeList = (GList)com.GetChild("addPointAttributeList");
    		m_btnAddPoint = (GButton)com.GetChild("btnAddPoint");
    		m_btnResetPoint = (GButton)com.GetChild("btnResetPoint");
    		m_txtPoint = (GTextField)com.GetChild("txtPoint");
    		m_btnPlusTransLevel = (GButton)com.GetChild("btnPlusTransLevel");
    		m_btnMinusTransLevel = (GButton)com.GetChild("btnMinusTransLevel");
    		m_txtTransLevel = (GTextField)com.GetChild("txtTransLevel");
    		m_addpointGroup = (GGroup)com.GetChild("addpointGroup");
    		m_Effect = com.GetTransition("Effect");
    	}
}
public override void Dispose()
    {
        if(IsDisposed)
        {
            return;
        }
        
        base.Dispose();
        
        self.Remove();
        self = null;
            
		m_addPoint = null;
		m_frame.Dispose();
		m_frame = null;
		m_bg = null;
		m_atkEquipList = null;
		m_commonEquipList = null;
		m_infoList = null;
		m_infoGroup = null;
		m_battleAttributeList = null;
		m_battleGroup = null;
		m_icon = null;
		m_basicAttributeList = null;
		m_basicGroup = null;
		m_addPointAttributeList = null;
		m_btnAddPoint = null;
		m_btnResetPoint = null;
		m_txtPoint = null;
		m_btnPlusTransLevel = null;
		m_btnMinusTransLevel = null;
		m_txtTransLevel = null;
		m_addpointGroup = null;
		m_Effect = null;
	}
}
}