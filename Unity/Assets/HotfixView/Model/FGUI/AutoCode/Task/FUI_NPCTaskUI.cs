/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_NPCTaskUI_AwakeSystem : AwakeSystem<FUI_NPCTaskUI, GObject>
    {
        public override void Awake(FUI_NPCTaskUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_NPCTaskUI : FUI
    {	
        public const string UIPackageName = "Task";
        public const string UIResName = "NPCTaskUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_contrl;
    public FUI_FrameNPCTaskUI m_frame;
    public GTextField m_txtDialog;
    public GList m_taskList;
    public GGroup m_first;
    public GTextField m_txtTaskName;
    public GTextField m_txtContent;
    public GTextField m_txtTarget;
    public GTextField m_txtTargetUnitOrPos;
    public GTextField m_txtRewardCount;
    public GList m_RewardList;
    public GButton m_btnAccept;
    public GButton m_btnBack;
    public GGroup m_second;
    public const string URL = "ui://thcq7hm4y69ub";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_NPCTaskUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_NPCTaskUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_NPCTaskUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_NPCTaskUI> tcs = new ETTaskCompletionSource<FUI_NPCTaskUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_NPCTaskUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_NPCTaskUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_NPCTaskUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_NPCTaskUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_NPCTaskUI>();

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
            
    		m_contrl = com.GetController("contrl");
    		m_frame = FUI_FrameNPCTaskUI.Create(domain, com.GetChild("frame"));
    		m_txtDialog = (GTextField)com.GetChild("txtDialog");
    		m_taskList = (GList)com.GetChild("taskList");
    		m_first = (GGroup)com.GetChild("first");
    		m_txtTaskName = (GTextField)com.GetChild("txtTaskName");
    		m_txtContent = (GTextField)com.GetChild("txtContent");
    		m_txtTarget = (GTextField)com.GetChild("txtTarget");
    		m_txtTargetUnitOrPos = (GTextField)com.GetChild("txtTargetUnitOrPos");
    		m_txtRewardCount = (GTextField)com.GetChild("txtRewardCount");
    		m_RewardList = (GList)com.GetChild("RewardList");
    		m_btnAccept = (GButton)com.GetChild("btnAccept");
    		m_btnBack = (GButton)com.GetChild("btnBack");
    		m_second = (GGroup)com.GetChild("second");
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
            
		m_contrl = null;
		m_frame.Dispose();
		m_frame = null;
		m_txtDialog = null;
		m_taskList = null;
		m_first = null;
		m_txtTaskName = null;
		m_txtContent = null;
		m_txtTarget = null;
		m_txtTargetUnitOrPos = null;
		m_txtRewardCount = null;
		m_RewardList = null;
		m_btnAccept = null;
		m_btnBack = null;
		m_second = null;
	}
}
}