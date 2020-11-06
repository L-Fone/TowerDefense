/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_MailUI_AwakeSystem : AwakeSystem<FUI_MailUI, GObject>
    {
        public override void Awake(FUI_MailUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_MailUI : FUI
    {	
        public const string UIPackageName = "Mail";
        public const string UIResName = "MailUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameMailUI m_frame;
    public GList m_itemList;
    public GButton m_btnLastPage;
    public GButton m_btnNextPage;
    public GTextField m_txtPage;
    public GButton m_btnDeleteAll;
    public GButton m_btnReceiveAll;
    public GButton m_btnDelete;
    public GButton m_btnReceive;
    public GTextField m_txtTitle;
    public GTextField m_txtSenderName;
    public GTextField m_txtContent;
    public GButton m_btnRewordSlotLeft;
    public GButton m_btnRewordSlotRight;
    public GTextField m_txtGold;
    public GTextField m_txtSliver;
    public GTextField m_txtCoin;
    public const string URL = "ui://ba6u515vralqa";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_MailUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_MailUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_MailUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_MailUI> tcs = new ETTaskCompletionSource<FUI_MailUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_MailUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_MailUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_MailUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_MailUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_MailUI>();

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
            
    		m_frame = FUI_FrameMailUI.Create(domain, com.GetChild("frame"));
    		m_itemList = (GList)com.GetChild("itemList");
    		m_btnLastPage = (GButton)com.GetChild("btnLastPage");
    		m_btnNextPage = (GButton)com.GetChild("btnNextPage");
    		m_txtPage = (GTextField)com.GetChild("txtPage");
    		m_btnDeleteAll = (GButton)com.GetChild("btnDeleteAll");
    		m_btnReceiveAll = (GButton)com.GetChild("btnReceiveAll");
    		m_btnDelete = (GButton)com.GetChild("btnDelete");
    		m_btnReceive = (GButton)com.GetChild("btnReceive");
    		m_txtTitle = (GTextField)com.GetChild("txtTitle");
    		m_txtSenderName = (GTextField)com.GetChild("txtSenderName");
    		m_txtContent = (GTextField)com.GetChild("txtContent");
    		m_btnRewordSlotLeft = (GButton)com.GetChild("btnRewordSlotLeft");
    		m_btnRewordSlotRight = (GButton)com.GetChild("btnRewordSlotRight");
    		m_txtGold = (GTextField)com.GetChild("txtGold");
    		m_txtSliver = (GTextField)com.GetChild("txtSliver");
    		m_txtCoin = (GTextField)com.GetChild("txtCoin");
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
		m_btnLastPage = null;
		m_btnNextPage = null;
		m_txtPage = null;
		m_btnDeleteAll = null;
		m_btnReceiveAll = null;
		m_btnDelete = null;
		m_btnReceive = null;
		m_txtTitle = null;
		m_txtSenderName = null;
		m_txtContent = null;
		m_btnRewordSlotLeft = null;
		m_btnRewordSlotRight = null;
		m_txtGold = null;
		m_txtSliver = null;
		m_txtCoin = null;
	}
}
}