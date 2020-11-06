/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ConsignmentUI_AwakeSystem : AwakeSystem<FUI_ConsignmentUI, GObject>
    {
        public override void Awake(FUI_ConsignmentUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ConsignmentUI : FUI
    {	
        public const string UIPackageName = "Consignment";
        public const string UIResName = "ConsignmentUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameConsignmentUI m_frame;
    public GList m_itemList;
    public FUI_ComboBoxConsign m_ComboItemType;
    public FUI_ComboBoxConsign m_ComboJobType;
    public GButton m_btnLastPage;
    public GButton m_btnNextPage;
    public GTextField m_txtPage;
    public GButton m_btnBuy;
    public GButton m_btnSell;
    public const string URL = "ui://ivv55tvqih6n6";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ConsignmentUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ConsignmentUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ConsignmentUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ConsignmentUI> tcs = new ETTaskCompletionSource<FUI_ConsignmentUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ConsignmentUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ConsignmentUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ConsignmentUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ConsignmentUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ConsignmentUI>();

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
            
    		m_frame = FUI_FrameConsignmentUI.Create(domain, com.GetChild("frame"));
    		m_itemList = (GList)com.GetChild("itemList");
    		m_ComboItemType = FUI_ComboBoxConsign.Create(domain, com.GetChild("ComboItemType"));
    		m_ComboJobType = FUI_ComboBoxConsign.Create(domain, com.GetChild("ComboJobType"));
    		m_btnLastPage = (GButton)com.GetChild("btnLastPage");
    		m_btnNextPage = (GButton)com.GetChild("btnNextPage");
    		m_txtPage = (GTextField)com.GetChild("txtPage");
    		m_btnBuy = (GButton)com.GetChild("btnBuy");
    		m_btnSell = (GButton)com.GetChild("btnSell");
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
		m_itemList = null;
		m_ComboItemType.Dispose();
		m_ComboItemType = null;
		m_ComboJobType.Dispose();
		m_ComboJobType = null;
		m_btnLastPage = null;
		m_btnNextPage = null;
		m_txtPage = null;
		m_btnBuy = null;
		m_btnSell = null;
	}
}
}