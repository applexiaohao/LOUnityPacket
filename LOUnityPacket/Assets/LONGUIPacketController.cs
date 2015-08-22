using System;
using UnityEngine;

namespace AssemblyCSharp
{
	/// <summary>
	/// 背包控制器
	/// </summary>
	public class LONGUIPacketController
	{
		/// <summary>
		/// 构造器
		/// </summary>

		private UITexture  Background{ set; get;}
		public LONGUIPacketController (GameObject parent)
		{
			this.Background = NGUITools.AddChild<UITexture> (parent);
			this.Background.gameObject.name = "LOPacketView";
			this.IninParam ();
			this.InitContainer ();
		}


		private Vector4 frame;
		public Vector4 Frame{
			set
			{ 
				this.frame = value;
			}
			get{ 
				return this.frame;
			}
		}

		public Vector2 CellSize{ set; get;}

		private void IninParam()
		{
			this.CellSize = new Vector2 (50f, 50f);
			this.Frame = new Vector4 (-200, -200, 400, 400);
		}

		/// <summary>
		/// 初始化背包
		/// </summary>
		private UIScrollView scrollView;
		//背包格子
		private UIGrid		 PacketGrid;
		private void InitContainer()
		{			
			Debug.Log (this.Background.parent);
			this.Background.SetRect (this.frame.x, this.frame.y, this.frame.z, this.frame.w);
			this.scrollView = NGUITools.AddChild<UIScrollView> (this.Background.gameObject);
			this.scrollView.gameObject.name = "PacketContainer";

			//重置滚动视图的大小
			this.LayoutScrollView (this.scrollView.GetComponent<UIPanel> ());

			//添加Grid表格
			this.PacketGrid = NGUITools.AddChild<UIGrid>(this.scrollView.gameObject);

			//设置Grid的一些属性
			this.PacketGrid.arrangement = UIGrid.Arrangement.Vertical;
			//设置最多5行
			this.PacketGrid.maxPerLine = 5;
			//设置gird表中,每个单元格的宽度和高度
			this.PacketGrid.cellWidth = this.CellSize.x;
			this.PacketGrid.cellHeight = this.CellSize.y;

			for (int i = 0; i < 10; i++) {
				AddItem ();
			}

			this.PacketGrid.repositionNow = true;
		}

		private void LayoutScrollView(UIPanel panel)
		{
			panel.SetRect (0, 0, this.frame.z, this.frame.w);
			panel.clipOffset = new Vector2 (0, 0);
		}

		//背包中小格子的北京
		public Texture GridBackgroundImage{ set; get;}

		/// <summary>
		/// 添加一个元素
		/// </summary>
		public void AddItem()
		{
			//创建一个单元格背景
			UITexture cell = NGUITools.AddChild<UITexture> (this.PacketGrid.gameObject);
			cell.autoResizeBoxCollider = true;

			cell.mainTexture = (Texture)Resources.Load<Texture> ("icon");

			NGUITools.AddMissingComponent<BoxCollider> (cell.gameObject);
			NGUITools.AddMissingComponent<UIDragScrollView> (cell.gameObject);

			UIDragDropItem drag = NGUITools.AddMissingComponent<UIDragDropItem> (cell.gameObject);
			drag.restriction = UIDragDropItem.Restriction.PressAndHold;
			cell.ResizeCollider ();

			//在单元格背景上创建一个Label
			GetTestLabel(cell.gameObject);
		}

		public void GetTestLabel(GameObject cell)
		{
			UILabel label = NGUITools.AddChild<UILabel> (cell);

			UIFont font = NGUITools.AddMissingComponent<UIFont> (label.gameObject);
			font.dynamicFont = Resources.Load<Font> ("Arial");

			label.text = "999";

		}
	}
}

