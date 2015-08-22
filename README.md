![](http://d.pr/i/15Crh)

- `PacketCell` 
	- 对象作为单元格背景
- `PacketContainer`
	- 对象作为单元格容器
- `PacketLabel`
	- 对象作为单元格物体

### 物体能够被拖拽的几个条件

- 碰撞器	`BoxCollider`
- 拖拽功能	`UIDragDropItem`

### 创建第一个拖拽功能的空子类
```
using System;
using UnityEngine;

/// <summary>
/// 第一个自己创建的拖拽功能
/// </summary>
public class MyFirstDragDropItem:UIDragDropItem
{
	
}
```

### 容器可以监测正在被拖拽物体是否到自己对象位置的几个条件
- 碰撞器 `BoxCollider`
- 容器功能 `UIDragDropContainer`

### `GMUser.cs`
```
using System;

/// <summary>
/// 用户管理器
/// </summary>
public class GMUserManager
{
	//存储当前正在玩游戏的玩家信息
	private static GMUser user = null;
	//公开访问器
	public static GMUser User{ 
		get {
			if (GMUserManager.user == null) {
				GMUserManager.user = new GMUser ();
			}

			return GMUserManager.user;
		}
	}
}

public class GMUser
{
	//游戏用户的姓名
	public string Name{set;get;}
	public GMUser ()
	{
		//设置每个用户的默认姓名是Right
		this.Name = "Right";
	}
}

```

### `MyFirstDragDropItem.cs`
```
using System;
using UnityEngine;

/// <summary>
/// 第一个自己创建的拖拽功能
/// </summary>
public class MyFirstDragDropItem:UIDragDropItem
{

	private GameObject sourceParent;
	/// <summary>
	/// 重写父类的拖拽开始函数
	/// </summary>
	protected override void OnDragDropStart ()
	{
		//当拖拽开始时存储原始的父对象
		this.sourceParent = this.transform.parent.gameObject;
		base.OnDragDropStart ();
	}
	/// <summary>
	/// 重写父类的拖拽释放函数
	/// </summary>
	protected override void OnDragDropRelease (GameObject surface)
	{
		//如果不是拖拽到场景表面的话
		if (!surface.name.Equals ("UI Root")) {
			//寻找surface对象的父对象
			GameObject cell = surface.transform.parent.gameObject;

			//判断当前单元格的对象姓名
			if (cell.name.Equals ("PacketCell - Left")) {
				GMUserManager.User.Name = "Left";
			} 
			if (cell.name.Equals ("PacketCell - Right")) {
				GMUserManager.User.Name = "Right";
			}
		} else {
			//其他的错误位置时,重置父子关系
			this.transform.parent = this.sourceParent.transform;
		}

		//最终调用父类的功能
		base.OnDragDropRelease(surface);
		//调整位置
		this.transform.localPosition = new Vector3(0,0,0);
	}
}
```

### `Test.script`
```
using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	//指向游戏中间的那个label控件
	public UILabel	label;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.label.text = GMUserManager.User.Name;
	}
}

```






