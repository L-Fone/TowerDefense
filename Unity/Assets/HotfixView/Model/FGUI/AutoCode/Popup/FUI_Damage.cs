/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Damage_AwakeSystem : AwakeSystem<FUI_Damage, GObject>
    {
        public override void Awake(FUI_Damage self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Damage : FUI
    {	
        public const string UIPackageName = "Popup";
        public const string UIResName = "Damage";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public Controller m_crit;
    public GTextField m_txt;
    public GTextField m_txtCrit;
    public Transition m_Effect;
    public Transition m_CritEffect;
    public const string URL = "ui://ldwujxh9lsk1c";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Damage CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Damage, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Damage> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Damage> tcs = new ETTaskCompletionSource<FUI_Damage>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Damage, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Damage Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Damage, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Damage GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Damage>();

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
            
    		m_crit = com.GetController("crit");
    		m_txt = (GTextField)com.GetChild("txt");
    		m_txtCrit = (GTextField)com.GetChild("txtCrit");
    		m_Effect = com.GetTransition("Effect");
    		m_CritEffect = com.GetTransition("CritEffect");
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
            
		m_crit = null;
		m_txt = null;
		m_txtCrit = null;
		m_Effect = null;
		m_CritEffect = null;
	}
}
}