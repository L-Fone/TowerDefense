/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_StoreUI_AwakeSystem : AwakeSystem<FUI_StoreUI, GObject>
    {
        public override void Awake(FUI_StoreUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_StoreUI : FUI
    {	
        public const string UIPackageName = "Bag";
        public const string UIResName = "StoreUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameStore m_frame;
    public GList m_slotList;
    public GButton m_btnLast;
    public GButton m_btnNext;
    public GTextField m_txtPage;
    public GTextField m_txtGold;
    public GTextField m_txtSliver;
    public GTextField m_txtCoin;
    public GButton m_btnSaveCoin;
    public GButton m_btnGetCoin;
    public GButton m_btnSort;
    public const string URL = "ui://71ktouo7qc59iv";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_StoreUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_StoreUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_StoreUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_StoreUI> tcs = new ETTaskCompletionSource<FUI_StoreUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_StoreUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_StoreUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_StoreUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_StoreUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_StoreUI>();

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
            
    		m_frame = FUI_FrameStore.Create(domain, com.GetChild("frame"));
    		m_slotList = (GList)com.GetChild("slotList");
    		m_btnLast = (GButton)com.GetChild("btnLast");
    		m_btnNext = (GButton)com.GetChild("btnNext");
    		m_txtPage = (GTextField)com.GetChild("txtPage");
    		m_txtGold = (GTextField)com.GetChild("txtGold");
    		m_txtSliver = (GTextField)com.GetChild("txtSliver");
    		m_txtCoin = (GTextField)com.GetChild("txtCoin");
    		m_btnSaveCoin = (GButton)com.GetChild("btnSaveCoin");
    		m_btnGetCoin = (GButton)com.GetChild("btnGetCoin");
    		m_btnSort = (GButton)com.GetChild("btnSort");
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
		m_slotList = null;
		m_btnLast = null;
		m_btnNext = null;
		m_txtPage = null;
		m_txtGold = null;
		m_txtSliver = null;
		m_txtCoin = null;
		m_btnSaveCoin = null;
		m_btnGetCoin = null;
		m_btnSort = null;
	}
}
}