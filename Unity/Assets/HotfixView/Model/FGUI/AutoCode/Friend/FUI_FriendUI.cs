/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_FriendUI_AwakeSystem : AwakeSystem<FUI_FriendUI, GObject>
    {
        public override void Awake(FUI_FriendUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_FriendUI : FUI
    {	
        public const string UIPackageName = "Friend";
        public const string UIResName = "FriendUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_page;
    public FUI_FrameFriendUI m_frame;
    public GList m_pageList;
    public GList m_friendList;
    public GButton m_btnFindFriend;
    public GButton m_btnRequest;
    public GList m_famailyList;
    public GTextField m_txtFamilyNotice;
    public GTextField m_txtFamilyInfo;
    public GButton m_btnRequestFamily;
    public GButton m_btnCreateFamily;
    public GButton m_btnDeleteFamily;
    public GButton m_btnHandleFamilyRequest;
    public GButton m_btnManagerFamily;
    public GButton m_btnEditNotice;
    public GTextField m_txtFamilyName;
    public GTextField m_txtFamilyLevel;
    public GTextField m_txtFamilyCount;
    public GTextField m_txtFamilyHornor;
    public const string URL = "ui://0nnc7y49sqcz7";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_FriendUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_FriendUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_FriendUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_FriendUI> tcs = new ETTaskCompletionSource<FUI_FriendUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_FriendUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_FriendUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_FriendUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_FriendUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_FriendUI>();

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
            
    		m_page = com.GetController("page");
    		m_frame = FUI_FrameFriendUI.Create(domain, com.GetChild("frame"));
    		m_pageList = (GList)com.GetChild("pageList");
    		m_friendList = (GList)com.GetChild("friendList");
    		m_btnFindFriend = (GButton)com.GetChild("btnFindFriend");
    		m_btnRequest = (GButton)com.GetChild("btnRequest");
    		m_famailyList = (GList)com.GetChild("famailyList");
    		m_txtFamilyNotice = (GTextField)com.GetChild("txtFamilyNotice");
    		m_txtFamilyInfo = (GTextField)com.GetChild("txtFamilyInfo");
    		m_btnRequestFamily = (GButton)com.GetChild("btnRequestFamily");
    		m_btnCreateFamily = (GButton)com.GetChild("btnCreateFamily");
    		m_btnDeleteFamily = (GButton)com.GetChild("btnDeleteFamily");
    		m_btnHandleFamilyRequest = (GButton)com.GetChild("btnHandleFamilyRequest");
    		m_btnManagerFamily = (GButton)com.GetChild("btnManagerFamily");
    		m_btnEditNotice = (GButton)com.GetChild("btnEditNotice");
    		m_txtFamilyName = (GTextField)com.GetChild("txtFamilyName");
    		m_txtFamilyLevel = (GTextField)com.GetChild("txtFamilyLevel");
    		m_txtFamilyCount = (GTextField)com.GetChild("txtFamilyCount");
    		m_txtFamilyHornor = (GTextField)com.GetChild("txtFamilyHornor");
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
            
		m_page = null;
		m_frame.Dispose();
		m_frame = null;
		m_pageList = null;
		m_friendList = null;
		m_btnFindFriend = null;
		m_btnRequest = null;
		m_famailyList = null;
		m_txtFamilyNotice = null;
		m_txtFamilyInfo = null;
		m_btnRequestFamily = null;
		m_btnCreateFamily = null;
		m_btnDeleteFamily = null;
		m_btnHandleFamilyRequest = null;
		m_btnManagerFamily = null;
		m_btnEditNotice = null;
		m_txtFamilyName = null;
		m_txtFamilyLevel = null;
		m_txtFamilyCount = null;
		m_txtFamilyHornor = null;
	}
}
}