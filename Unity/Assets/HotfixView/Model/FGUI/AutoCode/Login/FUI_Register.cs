/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Register_AwakeSystem : AwakeSystem<FUI_Register, GObject>
    {
        public override void Awake(FUI_Register self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Register : FUI
    {	
        public const string UIPackageName = "Login";
        public const string UIResName = "Register";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GTextInput m_iptAcc;
    public GTextInput m_iptPsd;
    public GTextInput m_iptQQ;
    public GButton m_btnRegisterAndLogin;
    public GButton m_btnClose;
    public const string URL = "ui://k3d3mc7gjshjk";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Register CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Register, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Register> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Register> tcs = new ETTaskCompletionSource<FUI_Register>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Register, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Register Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Register, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Register GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Register>();

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
            
    		m_iptAcc = (GTextInput)com.GetChild("iptAcc");
    		m_iptPsd = (GTextInput)com.GetChild("iptPsd");
    		m_iptQQ = (GTextInput)com.GetChild("iptQQ");
    		m_btnRegisterAndLogin = (GButton)com.GetChild("btnRegisterAndLogin");
    		m_btnClose = (GButton)com.GetChild("btnClose");
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
            
		m_iptAcc = null;
		m_iptPsd = null;
		m_iptQQ = null;
		m_btnRegisterAndLogin = null;
		m_btnClose = null;
	}
}
}