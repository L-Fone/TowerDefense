/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Skill_JunGuan_AwakeSystem : AwakeSystem<FUI_Skill_JunGuan, GObject>
    {
        public override void Awake(FUI_Skill_JunGuan self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Skill_JunGuan : FUI
    {	
        public const string UIPackageName = "Skill";
        public const string UIResName = "Skill_军官";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GLabel m_root;
    public GButton m_100001;
    public GButton m_110101;
    public GButton m_110201;
    public GButton m_110301;
    public GButton m_110302;
    public GButton m_110401;
    public GButton m_110402;
    public GButton m_110501;
    public GButton m_110502;
    public GButton m_110601;
    public GButton m_110602;
    public GButton m_110202;
    public GButton m_110303;
    public GButton m_110304;
    public GButton m_110403;
    public GButton m_110404;
    public GButton m_110503;
    public GButton m_110504;
    public GButton m_110603;
    public GButton m_110604;
    public GButton m_120101;
    public GButton m_120201;
    public GGroup m_all;
    public const string URL = "ui://7fc8sjen10i4t2l";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Skill_JunGuan CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Skill_JunGuan, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Skill_JunGuan> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Skill_JunGuan> tcs = new ETTaskCompletionSource<FUI_Skill_JunGuan>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Skill_JunGuan, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Skill_JunGuan Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Skill_JunGuan, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Skill_JunGuan GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Skill_JunGuan>();

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
            
    		m_root = (GLabel)com.GetChild("root");
    		m_100001 = (GButton)com.GetChild("100001");
    		m_110101 = (GButton)com.GetChild("110101");
    		m_110201 = (GButton)com.GetChild("110201");
    		m_110301 = (GButton)com.GetChild("110301");
    		m_110302 = (GButton)com.GetChild("110302");
    		m_110401 = (GButton)com.GetChild("110401");
    		m_110402 = (GButton)com.GetChild("110402");
    		m_110501 = (GButton)com.GetChild("110501");
    		m_110502 = (GButton)com.GetChild("110502");
    		m_110601 = (GButton)com.GetChild("110601");
    		m_110602 = (GButton)com.GetChild("110602");
    		m_110202 = (GButton)com.GetChild("110202");
    		m_110303 = (GButton)com.GetChild("110303");
    		m_110304 = (GButton)com.GetChild("110304");
    		m_110403 = (GButton)com.GetChild("110403");
    		m_110404 = (GButton)com.GetChild("110404");
    		m_110503 = (GButton)com.GetChild("110503");
    		m_110504 = (GButton)com.GetChild("110504");
    		m_110603 = (GButton)com.GetChild("110603");
    		m_110604 = (GButton)com.GetChild("110604");
    		m_120101 = (GButton)com.GetChild("120101");
    		m_120201 = (GButton)com.GetChild("120201");
    		m_all = (GGroup)com.GetChild("all");
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
            
		m_root = null;
		m_100001 = null;
		m_110101 = null;
		m_110201 = null;
		m_110301 = null;
		m_110302 = null;
		m_110401 = null;
		m_110402 = null;
		m_110501 = null;
		m_110502 = null;
		m_110601 = null;
		m_110602 = null;
		m_110202 = null;
		m_110303 = null;
		m_110304 = null;
		m_110403 = null;
		m_110404 = null;
		m_110503 = null;
		m_110504 = null;
		m_110603 = null;
		m_110604 = null;
		m_120101 = null;
		m_120201 = null;
		m_all = null;
	}
}
}