/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_HeadInfoItemMain_AwakeSystem : AwakeSystem<FUI_HeadInfoItemMain, GObject>
    {
        public override void Awake(FUI_HeadInfoItemMain self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_HeadInfoItemMain : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "HeadInfoItemMain";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GImage m_imgLeader;
    public GProgressBar m_pbarMp;
    public GProgressBar m_pBarHp;
    public GProgressBar m_pBarExp;
    public GTextField m_txtLevel;
    public GTextField m_txtName;
    public GButton m_btn;
    public const string URL = "ui://kqsmrpxluuap62";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_HeadInfoItemMain CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_HeadInfoItemMain, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_HeadInfoItemMain> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_HeadInfoItemMain> tcs = new ETTaskCompletionSource<FUI_HeadInfoItemMain>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_HeadInfoItemMain, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_HeadInfoItemMain Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_HeadInfoItemMain, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_HeadInfoItemMain GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_HeadInfoItemMain>();

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
            
    		m_imgLeader = (GImage)com.GetChild("imgLeader");
    		m_pbarMp = (GProgressBar)com.GetChild("pbarMp");
    		m_pBarHp = (GProgressBar)com.GetChild("pBarHp");
    		m_pBarExp = (GProgressBar)com.GetChild("pBarExp");
    		m_txtLevel = (GTextField)com.GetChild("txtLevel");
    		m_txtName = (GTextField)com.GetChild("txtName");
    		m_btn = (GButton)com.GetChild("btn");
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
            
		m_imgLeader = null;
		m_pbarMp = null;
		m_pBarHp = null;
		m_pBarExp = null;
		m_txtLevel = null;
		m_txtName = null;
		m_btn = null;
	}
}
}