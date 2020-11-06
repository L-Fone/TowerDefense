/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_RewardUI_AwakeSystem : AwakeSystem<FUI_RewardUI, GObject>
    {
        public override void Awake(FUI_RewardUI self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_RewardUI : FUI
    {	
        public const string UIPackageName = "Popup";
        public const string UIResName = "RewardUI";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GLoader m_icon;
    public GTextField m_title;
    public Transition m_Effect;
    public const string URL = "ui://ldwujxh9hri1d";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_RewardUI CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_RewardUI, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_RewardUI> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_RewardUI> tcs = new ETTaskCompletionSource<FUI_RewardUI>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_RewardUI, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_RewardUI Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_RewardUI, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_RewardUI GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_RewardUI>();

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
            
    		m_icon = (GLoader)com.GetChild("icon");
    		m_title = (GTextField)com.GetChild("title");
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
            
		m_icon = null;
		m_title = null;
		m_Effect = null;
	}
}
}