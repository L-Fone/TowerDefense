/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_Skill_YunDongYuan_AwakeSystem : AwakeSystem<FUI_Skill_YunDongYuan, GObject>
    {
        public override void Awake(FUI_Skill_YunDongYuan self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_Skill_YunDongYuan : FUI
    {	
        public const string UIPackageName = "Skill";
        public const string UIResName = "Skill_运动员";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    public GLabel m_root;
    public GButton m_200001;
    public GButton m_210101;
    public GButton m_210201;
    public GButton m_210301;
    public GButton m_210302;
    public GButton m_210401;
    public GButton m_210402;
    public GButton m_210501;
    public GButton m_210502;
    public GButton m_210601;
    public GButton m_210602;
    public GButton m_210202;
    public GButton m_210303;
    public GButton m_210304;
    public GButton m_210403;
    public GButton m_210404;
    public GButton m_210503;
    public GButton m_210504;
    public GButton m_210603;
    public GButton m_210604;
    public GButton m_220101;
    public GButton m_220201;
    public GGroup m_all;
    public const string URL = "ui://7fc8sjenbrj8t2q";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_Skill_YunDongYuan CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_Skill_YunDongYuan, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_Skill_YunDongYuan> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_Skill_YunDongYuan> tcs = new ETTaskCompletionSource<FUI_Skill_YunDongYuan>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_Skill_YunDongYuan, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_Skill_YunDongYuan Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_Skill_YunDongYuan, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_Skill_YunDongYuan GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_Skill_YunDongYuan>();

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
    		m_200001 = (GButton)com.GetChild("200001");
    		m_210101 = (GButton)com.GetChild("210101");
    		m_210201 = (GButton)com.GetChild("210201");
    		m_210301 = (GButton)com.GetChild("210301");
    		m_210302 = (GButton)com.GetChild("210302");
    		m_210401 = (GButton)com.GetChild("210401");
    		m_210402 = (GButton)com.GetChild("210402");
    		m_210501 = (GButton)com.GetChild("210501");
    		m_210502 = (GButton)com.GetChild("210502");
    		m_210601 = (GButton)com.GetChild("210601");
    		m_210602 = (GButton)com.GetChild("210602");
    		m_210202 = (GButton)com.GetChild("210202");
    		m_210303 = (GButton)com.GetChild("210303");
    		m_210304 = (GButton)com.GetChild("210304");
    		m_210403 = (GButton)com.GetChild("210403");
    		m_210404 = (GButton)com.GetChild("210404");
    		m_210503 = (GButton)com.GetChild("210503");
    		m_210504 = (GButton)com.GetChild("210504");
    		m_210603 = (GButton)com.GetChild("210603");
    		m_210604 = (GButton)com.GetChild("210604");
    		m_220101 = (GButton)com.GetChild("220101");
    		m_220201 = (GButton)com.GetChild("220201");
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
		m_200001 = null;
		m_210101 = null;
		m_210201 = null;
		m_210301 = null;
		m_210302 = null;
		m_210401 = null;
		m_210402 = null;
		m_210501 = null;
		m_210502 = null;
		m_210601 = null;
		m_210602 = null;
		m_210202 = null;
		m_210303 = null;
		m_210304 = null;
		m_210403 = null;
		m_210404 = null;
		m_210503 = null;
		m_210504 = null;
		m_210603 = null;
		m_210604 = null;
		m_220101 = null;
		m_220201 = null;
		m_all = null;
	}
}
}