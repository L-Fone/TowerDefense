/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_HeadTitleInfo_AwakeSystem : AwakeSystem<FUI_HeadTitleInfo, GObject>
    {
        public override void Awake(FUI_HeadTitleInfo self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_HeadTitleInfo : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "HeadTitleInfo";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_job;
    public GProgressBar m_progress;
    public GTextField m_txtTitle;
    public GLoader m_icon;
    public GTextField m_txtName;
    public GImage m_imgLevelBG;
    public GTextField m_txtLevel;
    public GGroup m_group;
    public const string URL = "ui://kqsmrpxlqyfh6i";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_HeadTitleInfo CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_HeadTitleInfo, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_HeadTitleInfo> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_HeadTitleInfo> tcs = new ETTaskCompletionSource<FUI_HeadTitleInfo>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_HeadTitleInfo, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_HeadTitleInfo Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_HeadTitleInfo, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_HeadTitleInfo GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_HeadTitleInfo>();

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
            
    		m_job = com.GetController("job");
    		m_progress = (GProgressBar)com.GetChild("progress");
    		m_txtTitle = (GTextField)com.GetChild("txtTitle");
    		m_icon = (GLoader)com.GetChild("icon");
    		m_txtName = (GTextField)com.GetChild("txtName");
    		m_imgLevelBG = (GImage)com.GetChild("imgLevelBG");
    		m_txtLevel = (GTextField)com.GetChild("txtLevel");
    		m_group = (GGroup)com.GetChild("group");
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
            
		m_job = null;
		m_progress = null;
		m_txtTitle = null;
		m_icon = null;
		m_txtName = null;
		m_imgLevelBG = null;
		m_txtLevel = null;
		m_group = null;
	}
}
}