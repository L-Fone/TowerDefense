/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_RequestFriendUI_AwakeSystem : AwakeSystem<FUI_RequestFriendUI, GObject>
    {
        public override void Awake(FUI_RequestFriendUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_RequestFriendUI : FUI
    {	
        public const string UIPackageName = "Friend";
        public const string UIResName = "RequestFriendUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public FUI_FrameRequestFriendUI m_frame;
    public GList m_itemList;
    public const string URL = "ui://0nnc7y49j03xj";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_RequestFriendUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_RequestFriendUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_RequestFriendUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_RequestFriendUI> tcs = new ETTaskCompletionSource<FUI_RequestFriendUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_RequestFriendUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_RequestFriendUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_RequestFriendUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_RequestFriendUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_RequestFriendUI>();

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
            
    		m_frame = FUI_FrameRequestFriendUI.Create(domain, com.GetChild("frame"));
    		m_itemList = (GList)com.GetChild("itemList");
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
	}
}
}