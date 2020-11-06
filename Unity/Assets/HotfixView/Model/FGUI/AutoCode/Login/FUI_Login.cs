/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Login_AwakeSystem : AwakeSystem<FUI_Login, GObject>
    {
        public override void Awake(FUI_Login self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Login : FUI
    {	
        public const string UIPackageName = "Login";
        public const string UIResName = "Login";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GButton m_btnLogin;
    public GButton m_btnToRegister;
    public GTextInput m_iptAcc;
    public GTextInput m_iptPsd;
    public const string URL = "ui://k3d3mc7geh2af";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Login CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Login, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Login> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Login> tcs = new ETTaskCompletionSource<FUI_Login>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Login, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Login Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Login, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Login GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Login>();

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
            
    		m_btnLogin = (GButton)com.GetChild("btnLogin");
    		m_btnToRegister = (GButton)com.GetChild("btnToRegister");
    		m_iptAcc = (GTextInput)com.GetChild("iptAcc");
    		m_iptPsd = (GTextInput)com.GetChild("iptPsd");
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
            
		m_btnLogin = null;
		m_btnToRegister = null;
		m_iptAcc = null;
		m_iptPsd = null;
	}
}
}