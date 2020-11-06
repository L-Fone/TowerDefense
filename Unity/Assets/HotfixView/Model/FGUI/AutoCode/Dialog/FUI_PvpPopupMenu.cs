/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_PvpPopupMenu_AwakeSystem : AwakeSystem<FUI_PvpPopupMenu, GObject>
    {
        public override void Awake(FUI_PvpPopupMenu self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_PvpPopupMenu : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "PvpPopupMenu";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GList m_list;
    public const string URL = "ui://cb3q22aot4cw6";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_PvpPopupMenu CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_PvpPopupMenu, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_PvpPopupMenu> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_PvpPopupMenu> tcs = new ETTaskCompletionSource<FUI_PvpPopupMenu>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_PvpPopupMenu, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_PvpPopupMenu Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_PvpPopupMenu, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_PvpPopupMenu GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_PvpPopupMenu>();

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
            
    		m_list = (GList)com.GetChild("list");
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
            
		m_list = null;
	}
}
}