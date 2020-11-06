/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LabelTeamRequest_AwakeSystem : AwakeSystem<FUI_LabelTeamRequest, GObject>
    {
        public override void Awake(FUI_LabelTeamRequest self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LabelTeamRequest : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "LabelTeamRequest";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public GTextField m_Title;
    public GTextField m_Name;
    public GTextField m_Job;
    public GTextField m_Level;
    public GButton m_btnYes;
    public GButton m_btnNo;
    public GProgressBar m_progress;
    public Transition m_trans;
    public const string URL = "ui://cb3q22aola9rb";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LabelTeamRequest CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LabelTeamRequest, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LabelTeamRequest> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LabelTeamRequest> tcs = new ETTaskCompletionSource<FUI_LabelTeamRequest>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LabelTeamRequest, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LabelTeamRequest Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LabelTeamRequest, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LabelTeamRequest GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LabelTeamRequest>();

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
        
        self = (GLabel)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_Title = (GTextField)com.GetChild("Title");
    		m_Name = (GTextField)com.GetChild("Name");
    		m_Job = (GTextField)com.GetChild("Job");
    		m_Level = (GTextField)com.GetChild("Level");
    		m_btnYes = (GButton)com.GetChild("btnYes");
    		m_btnNo = (GButton)com.GetChild("btnNo");
    		m_progress = (GProgressBar)com.GetChild("progress");
    		m_trans = com.GetTransition("trans");
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
            
		m_Title = null;
		m_Name = null;
		m_Job = null;
		m_Level = null;
		m_btnYes = null;
		m_btnNo = null;
		m_progress = null;
		m_trans = null;
	}
}
}