/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ForgeUI_AwakeSystem : AwakeSystem<FUI_ForgeUI, GObject>
    {
        public override void Awake(FUI_ForgeUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ForgeUI : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "ForgeUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameForge m_frame;
    public GList m_pageList;
    public GList m_itenList;
    public GButton m_btnLast;
    public GButton m_btnNext;
    public GTextField m_txtPage;
    public const string URL = "ui://71ktouo7mj9wjm";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ForgeUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ForgeUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ForgeUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ForgeUI> tcs = new ETTaskCompletionSource<FUI_ForgeUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ForgeUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ForgeUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ForgeUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ForgeUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ForgeUI>();

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
            
    		m_frame = FUI_FrameForge.Create(domain, com.GetChild("frame"));
    		m_pageList = (GList)com.GetChild("pageList");
    		m_itenList = (GList)com.GetChild("itenList");
    		m_btnLast = (GButton)com.GetChild("btnLast");
    		m_btnNext = (GButton)com.GetChild("btnNext");
    		m_txtPage = (GTextField)com.GetChild("txtPage");
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
            
		m_frame.Dispose();
		m_frame = null;
		m_pageList = null;
		m_itenList = null;
		m_btnLast = null;
		m_btnNext = null;
		m_txtPage = null;
	}
}
}