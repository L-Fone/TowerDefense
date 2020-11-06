/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_MainUI_AwakeSystem : AwakeSystem<FUI_MainUI, GObject>
    {
        public override void Awake(FUI_MainUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_MainUI : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "MainUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_setting;
    public FUI_HeadInfoItemMain m_mineHeadInfo;
    public GTextField m_txtEnergy;
    public GTextField m_txtMapName;
    public GGroup m_MapNameGroup;
    public GButton m_btnRank;
    public GButton m_btnLittleGame;
    public GButton m_btnTeam;
    public GList m_HeadInfoList;
    public GList m_chatContentList;
    public GButton m_btnInteractive;
    public GGroup m_chatBoard;
    public GList m_chatList;
    public GButton m_btnChatReceive;
    public GButton m_btnChatSend;
    public GTextField m_txtChatType;
    public GTextInput m_inptChatContent;
    public GGroup m_chatGroup;
    public FUI_LabelGlobalText m_txtGlobal;
    public GButton m_btnAuto;
    public GButton m_btnBattleIdle;
    public GButton m_btnExit;
    public GButton m_btnDisOther;
    public GButton m_btnMail;
    public GButton m_btnStateBuff;
    public GGroup m_Setting;
    public GButton m_btnShowSetting;
    public GList m_FunctionList;
    public GGroup m_funcGroup;
    public GList m_mainUISlotList;
    public GGroup m_mainUISlotGroup;
    public const string URL = "ui://kqsmrpxleh2a7";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_MainUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_MainUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_MainUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_MainUI> tcs = new ETTaskCompletionSource<FUI_MainUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_MainUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_MainUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_MainUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_MainUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_MainUI>();

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
            
    		m_setting = com.GetController("setting");
    		m_mineHeadInfo = FUI_HeadInfoItemMain.Create(domain, com.GetChild("mineHeadInfo"));
    		m_txtEnergy = (GTextField)com.GetChild("txtEnergy");
    		m_txtMapName = (GTextField)com.GetChild("txtMapName");
    		m_MapNameGroup = (GGroup)com.GetChild("MapNameGroup");
    		m_btnRank = (GButton)com.GetChild("btnRank");
    		m_btnLittleGame = (GButton)com.GetChild("btnLittleGame");
    		m_btnTeam = (GButton)com.GetChild("btnTeam");
    		m_HeadInfoList = (GList)com.GetChild("HeadInfoList");
    		m_chatContentList = (GList)com.GetChild("chatContentList");
    		m_btnInteractive = (GButton)com.GetChild("btnInteractive");
    		m_chatBoard = (GGroup)com.GetChild("chatBoard");
    		m_chatList = (GList)com.GetChild("chatList");
    		m_btnChatReceive = (GButton)com.GetChild("btnChatReceive");
    		m_btnChatSend = (GButton)com.GetChild("btnChatSend");
    		m_txtChatType = (GTextField)com.GetChild("txtChatType");
    		m_inptChatContent = (GTextInput)com.GetChild("inptChatContent");
    		m_chatGroup = (GGroup)com.GetChild("chatGroup");
    		m_txtGlobal = FUI_LabelGlobalText.Create(domain, com.GetChild("txtGlobal"));
    		m_btnAuto = (GButton)com.GetChild("btnAuto");
    		m_btnBattleIdle = (GButton)com.GetChild("btnBattleIdle");
    		m_btnExit = (GButton)com.GetChild("btnExit");
    		m_btnDisOther = (GButton)com.GetChild("btnDisOther");
    		m_btnMail = (GButton)com.GetChild("btnMail");
    		m_btnStateBuff = (GButton)com.GetChild("btnStateBuff");
    		m_Setting = (GGroup)com.GetChild("Setting");
    		m_btnShowSetting = (GButton)com.GetChild("btnShowSetting");
    		m_FunctionList = (GList)com.GetChild("FunctionList");
    		m_funcGroup = (GGroup)com.GetChild("funcGroup");
    		m_mainUISlotList = (GList)com.GetChild("mainUISlotList");
    		m_mainUISlotGroup = (GGroup)com.GetChild("mainUISlotGroup");
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
            
		m_setting = null;
		m_mineHeadInfo.Dispose();
		m_mineHeadInfo = null;
		m_txtEnergy = null;
		m_txtMapName = null;
		m_MapNameGroup = null;
		m_btnRank = null;
		m_btnLittleGame = null;
		m_btnTeam = null;
		m_HeadInfoList = null;
		m_chatContentList = null;
		m_btnInteractive = null;
		m_chatBoard = null;
		m_chatList = null;
		m_btnChatReceive = null;
		m_btnChatSend = null;
		m_txtChatType = null;
		m_inptChatContent = null;
		m_chatGroup = null;
		m_txtGlobal.Dispose();
		m_txtGlobal = null;
		m_btnAuto = null;
		m_btnBattleIdle = null;
		m_btnExit = null;
		m_btnDisOther = null;
		m_btnMail = null;
		m_btnStateBuff = null;
		m_Setting = null;
		m_btnShowSetting = null;
		m_FunctionList = null;
		m_funcGroup = null;
		m_mainUISlotList = null;
		m_mainUISlotGroup = null;
	}
}
}