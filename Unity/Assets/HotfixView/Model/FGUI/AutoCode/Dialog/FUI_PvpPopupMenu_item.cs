/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_PvpPopupMenu_item_AwakeSystem : AwakeSystem<FUI_PvpPopupMenu_item, GObject>
    {
        public override void Awake(FUI_PvpPopupMenu_item self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_PvpPopupMenu_item : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "PvpPopupMenu_item";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    public Controller m_checked;
    public const string URL = "ui://cb3q22aot4cw5";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_PvpPopupMenu_item CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_PvpPopupMenu_item, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_PvpPopupMenu_item> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_PvpPopupMenu_item> tcs = new ETTaskCompletionSource<FUI_PvpPopupMenu_item>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_PvpPopupMenu_item, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_PvpPopupMenu_item Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_PvpPopupMenu_item, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_PvpPopupMenu_item GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_PvpPopupMenu_item>();

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
            
    		m_checked = com.GetController("checked");
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
            
		m_checked = null;
	}
}
}