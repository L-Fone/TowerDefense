/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using ET;
using FairyGUI;
using FairyGUI.Utils;

namespace ET
{
    [ObjectSystem]
    public class FUI_LabelRankItem_AwakeSystem : AwakeSystem<FUI_LabelRankItem, GObject>
    {
        public override void Awake(FUI_LabelRankItem self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_LabelRankItem : FUI
    {	
        public const string UIPackageName = "Rank";
        public const string UIResName = "LabelRankItem";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GLabel self;
            
    public Controller m_control;
    public GTextField m_txtName;
    public GTextField m_txtJob;
    public GRichTextField m_txtValue;
    public GTextField m_rank;
    public const string URL = "ui://g4444q3avj0yc";

    private static GObject CreateGObject()
    {
        return UIPackage.CreateObject(UIPackageName, UIResName);
    }
    
    private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
    {
        UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
    }
        
    public static FUI_LabelRankItem CreateInstance(Entity domain)
    {			
        return EntityFactory.Create<FUI_LabelRankItem, GObject>(domain, CreateGObject());
    }
        
    public static ETTask<FUI_LabelRankItem> CreateInstanceAsync(Entity domain)
    {
        ETTaskCompletionSource<FUI_LabelRankItem> tcs = new ETTaskCompletionSource<FUI_LabelRankItem>();

        CreateGObjectAsync((go) =>
        {
            tcs.SetResult(EntityFactory.Create<FUI_LabelRankItem, GObject>(domain, go));
        });

        return tcs.Task;
    }
        
    public static FUI_LabelRankItem Create(Entity domain,GObject go)
    {
        return EntityFactory.Create<FUI_LabelRankItem, GObject>(domain,go);
    }
        
    /// <summary>
    /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
    /// </summary>
    public static FUI_LabelRankItem GetFormPool(Entity domain,GObject go)
    {
        var fui = go.Get<FUI_LabelRankItem>();

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
            
    		m_control = com.GetController("control");
    		m_txtName = (GTextField)com.GetChild("txtName");
    		m_txtJob = (GTextField)com.GetChild("txtJob");
    		m_txtValue = (GRichTextField)com.GetChild("txtValue");
    		m_rank = (GTextField)com.GetChild("rank");
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
            
		m_control = null;
		m_txtName = null;
		m_txtJob = null;
		m_txtValue = null;
		m_rank = null;
	}
}
}