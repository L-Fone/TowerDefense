/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ButtonFriendItem_AwakeSystem : AwakeSystem<FUI_ButtonFriendItem, GObject>
    {
        public override void Awake(FUI_ButtonFriendItem self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ButtonFriendItem : FUI
    {	
        public const string UIPackageName = "Friend";
        public const string UIResName = "ButtonFriendItem";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    public GImage m_bg;
    public GTextField m_txtName;
    public GTextField m_txtJob;
    public GTextField m_txtLevel;
    public GTextField m_txtState;
    public const string URL = "ui://0nnc7y49sqczb";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ButtonFriendItem CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ButtonFriendItem, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ButtonFriendItem> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ButtonFriendItem> tcs = new ETTaskCompletionSource<FUI_ButtonFriendItem>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ButtonFriendItem, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ButtonFriendItem Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ButtonFriendItem, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ButtonFriendItem GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ButtonFriendItem>();

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
        
        self = (GButton)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_bg = (GImage)com.GetChild("bg");
    		m_txtName = (GTextField)com.GetChild("txtName");
    		m_txtJob = (GTextField)com.GetChild("txtJob");
    		m_txtLevel = (GTextField)com.GetChild("txtLevel");
    		m_txtState = (GTextField)com.GetChild("txtState");
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
            
		m_bg = null;
		m_txtName = null;
		m_txtJob = null;
		m_txtLevel = null;
		m_txtState = null;
	}
}
}