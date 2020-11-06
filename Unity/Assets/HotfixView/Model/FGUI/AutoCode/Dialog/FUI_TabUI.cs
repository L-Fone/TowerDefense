/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_TabUI_AwakeSystem : AwakeSystem<FUI_TabUI, GObject>
    {
        public override void Awake(FUI_TabUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_TabUI : FUI
    {	
        public const string UIPackageName = "Dialog";
        public const string UIResName = "TabUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GRichTextField m_title;
    public GGroup m_group;
    public Transition m_Show;
    public Transition m_Hide;
    public const string URL = "ui://cb3q22aop2c6i";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_TabUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_TabUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_TabUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_TabUI> tcs = new ETTaskCompletionSource<FUI_TabUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_TabUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_TabUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_TabUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_TabUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_TabUI>();

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
            
    		m_title = (GRichTextField)com.GetChild("title");
    		m_group = (GGroup)com.GetChild("group");
    		m_Show = com.GetTransition("Show");
    		m_Hide = com.GetTransition("Hide");
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
            
		m_title = null;
		m_group = null;
		m_Show = null;
		m_Hide = null;
	}
}
}