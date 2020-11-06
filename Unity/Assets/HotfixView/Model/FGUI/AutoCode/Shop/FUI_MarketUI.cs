/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_MarketUI_AwakeSystem : AwakeSystem<FUI_MarketUI, GObject>
    {
        public override void Awake(FUI_MarketUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_MarketUI : FUI
    {	
        public const string UIPackageName = "Shop";
        public const string UIResName = "MarketUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_marketType;
    public FUI_FrameShopUI m_frame;
    public GList m_pageList;
    public GList m_slotList;
    public GButton m_btnChangeMarketType;
    public const string URL = "ui://9r7gspaycm4u9";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_MarketUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_MarketUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_MarketUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_MarketUI> tcs = new ETTaskCompletionSource<FUI_MarketUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_MarketUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_MarketUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_MarketUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_MarketUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_MarketUI>();

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
            
    		m_marketType = com.GetController("marketType");
    		m_frame = FUI_FrameShopUI.Create(domain, com.GetChild("frame"));
    		m_pageList = (GList)com.GetChild("pageList");
    		m_slotList = (GList)com.GetChild("slotList");
    		m_btnChangeMarketType = (GButton)com.GetChild("btnChangeMarketType");
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
            
		m_marketType = null;
		m_frame.Dispose();
		m_frame = null;
		m_pageList = null;
		m_slotList = null;
		m_btnChangeMarketType = null;
	}
}
}