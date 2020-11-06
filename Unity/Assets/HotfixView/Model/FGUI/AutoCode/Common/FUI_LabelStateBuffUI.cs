/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LabelStateBuffUI_AwakeSystem : AwakeSystem<FUI_LabelStateBuffUI, GObject>
    {
        public override void Awake(FUI_LabelStateBuffUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LabelStateBuffUI : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "LabelStateBuffUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public Controller m_button;
    public GList m_buffList;
    public const string URL = "ui://kqsmrpxlh2b5kp";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LabelStateBuffUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LabelStateBuffUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LabelStateBuffUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LabelStateBuffUI> tcs = new ETTaskCompletionSource<FUI_LabelStateBuffUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LabelStateBuffUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LabelStateBuffUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LabelStateBuffUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LabelStateBuffUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LabelStateBuffUI>();

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
        
        self = (GLabel)go;
        
        self.Add(this);
        
        var com = go.asCom;
            
        if(com != null)
        {	
            
    		m_button = com.GetController("button");
    		m_buffList = (GList)com.GetChild("buffList");
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
            
		m_button = null;
		m_buffList = null;
	}
}
}