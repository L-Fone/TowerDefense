/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_ButtonClose_Blue_AwakeSystem : AwakeSystem<FUI_ButtonClose_Blue, GObject>
    {
        public override void Awake(FUI_ButtonClose_Blue self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_ButtonClose_Blue : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "ButtonClose_Blue";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    public GImage m_closeButton;
    public const string URL = "ui://kqsmrpxlptly64";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_ButtonClose_Blue CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_ButtonClose_Blue, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_ButtonClose_Blue> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_ButtonClose_Blue> tcs = new ETTaskCompletionSource<FUI_ButtonClose_Blue>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_ButtonClose_Blue, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_ButtonClose_Blue Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_ButtonClose_Blue, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_ButtonClose_Blue GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_ButtonClose_Blue>();

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
            
    		m_closeButton = (GImage)com.GetChild("closeButton");
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
            
		m_closeButton = null;
	}
}
}