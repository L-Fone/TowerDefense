/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LabelGlobalText_AwakeSystem : AwakeSystem<FUI_LabelGlobalText, GObject>
    {
        public override void Awake(FUI_LabelGlobalText self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LabelGlobalText : FUI
    {	
        public const string UIPackageName = "Common";
        public const string UIResName = "LabelGlobalText";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public Transition m_Effect;
    public const string URL = "ui://kqsmrpxlud62ki";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LabelGlobalText CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LabelGlobalText, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LabelGlobalText> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LabelGlobalText> tcs = new ETTaskCompletionSource<FUI_LabelGlobalText>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LabelGlobalText, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LabelGlobalText Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LabelGlobalText, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LabelGlobalText GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LabelGlobalText>();

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
            
    		m_Effect = com.GetTransition("Effect");
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
            
		m_Effect = null;
	}
}
}