﻿using UnityEngine;

/// <summary>
/// 回调Manager事件参数
/// </summary>
public class EventArgs
{
    public int m_id;
    public object m_data;

    public EventArgs()
    {

    }
    public EventArgs(object data)
    {
        m_data = data;
    }
}
/// <summary>
/// UI基类，所有具体UI类继承该类
/// </summary>
public abstract class UIBase : BaseUI 
{

    /// <summary>
    /// UI动画
    /// </summary>
    [SerializeField]
    protected Animator _animator;

    [SerializeField]
    protected UIType m_uiType;

	/// <summary>
	/// UI 显示状态
	/// </summary>
	/// <value><c>true</c> if this instance is appear; otherwise, <c>false</c>.</value>
	[HideInInspector]
	public bool IsAppear{ get; private set;}



    public delegate void UICompleteHandle(GameObject sender, EventArgs eventArgs);

    public override void OnAwake ()
	{
		base.OnAwake ();
	}
			

	public override void OnStart ()
	{
		base.OnStart ();
	}
			
	public override void OnUpdate ()
	{
		base.OnUpdate ();
	}
	/// <summary>
	/// 初始化 UI数据传入
	/// </summary>
	/// <param name="parameters">参数封箱，拆箱，自定义</param>
	public override void Init (object[] parameters)
	{
		base.Init (parameters);

	}
			
	/// <summary>
	/// 显示UI 基类实现
	/// </summary>
	public override void Appear()
	{
		base.Appear ();
		if (IsAppear)
			return;
			
		UIUtility uiManager = UIUtility.Instance;
		if (uiManager.UIStackCount != 0)
		{
			UIBase curUI = uiManager.PeekUI ();
			curUI.OnPause ();
		}

		uiManager.PushUI (this);
		this.OnAppear();
        UIManager.instance.m_UICotroller = true;
        transform.SetAsLastSibling();
	}

	/// <summary>
	/// 隐藏UI 基类实现
	/// </summary>
	public override void DisAppear()
	{
		base.DisAppear ();
		if (!IsAppear)
			return;
			
		UIUtility uiManager = UIUtility.Instance;
		if (uiManager.UIStackCount != 0)
		{
			uiManager.PopUI ();

			this.OnDisAppear ();
		}

		if (uiManager.UIStackCount != 0) 
		{
			UIBase lastUI = uiManager.PeekUI ();
			lastUI.OnResume ();
		}
	}

	/// <summary>
	/// 显示回调 基类实现
	/// </summary>
    public override void OnAppear()
    {
		base.OnAppear ();
		if (_animator != null)
			_animator.SetTrigger("OnAppear");

		IsAppear = true;
		this.gameObject.SetActive (true);


    }

	/// <summary>
	/// 隐藏回调 基类实现
	/// </summary>
    public override void OnDisAppear()
    {
		base.OnDisAppear ();
		if (_animator != null)
			_animator.SetTrigger("OnDisAppear");

		this.gameObject.SetActive (false);
		IsAppear = false;
    }

	/// <summary>
	/// 暂停->处于后台 基类实现
	/// </summary>
    public override void OnPause()
    {
		base.OnPause ();
		if (_animator != null)
            _animator.SetTrigger("OnPause");
    }

	/// <summary>
	/// 唤起－>处于前台 基类实现
	/// </summary>
    public override void OnResume()
    {
		base.OnPause ();
		if (_animator != null)
            _animator.SetTrigger("OnResume");
    }

    /// <summary>
    /// 返回UI类型
    /// </summary>
    /// <returns></returns>
    public UIType GetUIType()
    {
        return m_uiType;
    }

}

