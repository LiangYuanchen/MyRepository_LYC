﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace _13W数据添加Content
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SPDAct")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertAct(Act instance);
    partial void UpdateAct(Act instance);
    partial void DeleteAct(Act instance);
    partial void InsertAct_Items(Act_Items instance);
    partial void UpdateAct_Items(Act_Items instance);
    partial void DeleteAct_Items(Act_Items instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::_13W数据添加Content.Properties.Settings.Default.SPDActConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Act> Act
		{
			get
			{
				return this.GetTable<Act>();
			}
		}
		
		public System.Data.Linq.Table<Act_Items> Act_Items
		{
			get
			{
				return this.GetTable<Act_Items>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Act")]
	public partial class Act : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ActID;
		
		private string _ActName;
		
		private string _FileNumber;
		
		private System.Nullable<System.DateTime> _Pub_Date;
		
		private System.Nullable<System.DateTime> _Sta_Date;
		
		private string _Depts;
		
		private string _Content;
		
		private System.Nullable<int> _State;
		
		private System.Nullable<System.DateTime> _OpDate;
		
		private System.Nullable<int> _Effect;
		
		private int _ID;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnActIDChanging(int value);
    partial void OnActIDChanged();
    partial void OnActNameChanging(string value);
    partial void OnActNameChanged();
    partial void OnFileNumberChanging(string value);
    partial void OnFileNumberChanged();
    partial void OnPub_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnPub_DateChanged();
    partial void OnSta_DateChanging(System.Nullable<System.DateTime> value);
    partial void OnSta_DateChanged();
    partial void OnDeptsChanging(string value);
    partial void OnDeptsChanged();
    partial void OnContentChanging(string value);
    partial void OnContentChanged();
    partial void OnStateChanging(System.Nullable<int> value);
    partial void OnStateChanged();
    partial void OnOpDateChanging(System.Nullable<System.DateTime> value);
    partial void OnOpDateChanged();
    partial void OnEffectChanging(System.Nullable<int> value);
    partial void OnEffectChanged();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    #endregion
		
		public Act()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ActID
		{
			get
			{
				return this._ActID;
			}
			set
			{
				if ((this._ActID != value))
				{
					this.OnActIDChanging(value);
					this.SendPropertyChanging();
					this._ActID = value;
					this.SendPropertyChanged("ActID");
					this.OnActIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActName", DbType="NVarChar(500)")]
		public string ActName
		{
			get
			{
				return this._ActName;
			}
			set
			{
				if ((this._ActName != value))
				{
					this.OnActNameChanging(value);
					this.SendPropertyChanging();
					this._ActName = value;
					this.SendPropertyChanged("ActName");
					this.OnActNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FileNumber", DbType="NVarChar(200)")]
		public string FileNumber
		{
			get
			{
				return this._FileNumber;
			}
			set
			{
				if ((this._FileNumber != value))
				{
					this.OnFileNumberChanging(value);
					this.SendPropertyChanging();
					this._FileNumber = value;
					this.SendPropertyChanged("FileNumber");
					this.OnFileNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pub_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Pub_Date
		{
			get
			{
				return this._Pub_Date;
			}
			set
			{
				if ((this._Pub_Date != value))
				{
					this.OnPub_DateChanging(value);
					this.SendPropertyChanging();
					this._Pub_Date = value;
					this.SendPropertyChanged("Pub_Date");
					this.OnPub_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Sta_Date", DbType="DateTime")]
		public System.Nullable<System.DateTime> Sta_Date
		{
			get
			{
				return this._Sta_Date;
			}
			set
			{
				if ((this._Sta_Date != value))
				{
					this.OnSta_DateChanging(value);
					this.SendPropertyChanging();
					this._Sta_Date = value;
					this.SendPropertyChanged("Sta_Date");
					this.OnSta_DateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Depts", DbType="NVarChar(500)")]
		public string Depts
		{
			get
			{
				return this._Depts;
			}
			set
			{
				if ((this._Depts != value))
				{
					this.OnDeptsChanging(value);
					this.SendPropertyChanging();
					this._Depts = value;
					this.SendPropertyChanged("Depts");
					this.OnDeptsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Content", DbType="NVarChar(MAX)")]
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				if ((this._Content != value))
				{
					this.OnContentChanging(value);
					this.SendPropertyChanging();
					this._Content = value;
					this.SendPropertyChanged("Content");
					this.OnContentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_State", DbType="Int")]
		public System.Nullable<int> State
		{
			get
			{
				return this._State;
			}
			set
			{
				if ((this._State != value))
				{
					this.OnStateChanging(value);
					this.SendPropertyChanging();
					this._State = value;
					this.SendPropertyChanged("State");
					this.OnStateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_OpDate", DbType="DateTime")]
		public System.Nullable<System.DateTime> OpDate
		{
			get
			{
				return this._OpDate;
			}
			set
			{
				if ((this._OpDate != value))
				{
					this.OnOpDateChanging(value);
					this.SendPropertyChanging();
					this._OpDate = value;
					this.SendPropertyChanged("OpDate");
					this.OnOpDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Effect", DbType="Int")]
		public System.Nullable<int> Effect
		{
			get
			{
				return this._Effect;
			}
			set
			{
				if ((this._Effect != value))
				{
					this.OnEffectChanging(value);
					this.SendPropertyChanging();
					this._Effect = value;
					this.SendPropertyChanged("Effect");
					this.OnEffectChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Act_Items")]
	public partial class Act_Items : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ItemID;
		
		private int _ActID;
		
		private string _Item_Name;
		
		private System.Nullable<int> _Item_Type;
		
		private System.Nullable<int> _Item_ParentID;
		
		private System.Nullable<int> _Orders;
		
		private string _Content;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnItemIDChanging(int value);
    partial void OnItemIDChanged();
    partial void OnActIDChanging(int value);
    partial void OnActIDChanged();
    partial void OnItem_NameChanging(string value);
    partial void OnItem_NameChanged();
    partial void OnItem_TypeChanging(System.Nullable<int> value);
    partial void OnItem_TypeChanged();
    partial void OnItem_ParentIDChanging(System.Nullable<int> value);
    partial void OnItem_ParentIDChanged();
    partial void OnOrdersChanging(System.Nullable<int> value);
    partial void OnOrdersChanged();
    partial void OnContentChanging(string value);
    partial void OnContentChanged();
    #endregion
		
		public Act_Items()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ItemID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int ItemID
		{
			get
			{
				return this._ItemID;
			}
			set
			{
				if ((this._ItemID != value))
				{
					this.OnItemIDChanging(value);
					this.SendPropertyChanging();
					this._ItemID = value;
					this.SendPropertyChanged("ItemID");
					this.OnItemIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActID", DbType="Int NOT NULL")]
		public int ActID
		{
			get
			{
				return this._ActID;
			}
			set
			{
				if ((this._ActID != value))
				{
					this.OnActIDChanging(value);
					this.SendPropertyChanging();
					this._ActID = value;
					this.SendPropertyChanged("ActID");
					this.OnActIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Name", DbType="NVarChar(200)")]
		public string Item_Name
		{
			get
			{
				return this._Item_Name;
			}
			set
			{
				if ((this._Item_Name != value))
				{
					this.OnItem_NameChanging(value);
					this.SendPropertyChanging();
					this._Item_Name = value;
					this.SendPropertyChanged("Item_Name");
					this.OnItem_NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_Type", DbType="Int")]
		public System.Nullable<int> Item_Type
		{
			get
			{
				return this._Item_Type;
			}
			set
			{
				if ((this._Item_Type != value))
				{
					this.OnItem_TypeChanging(value);
					this.SendPropertyChanging();
					this._Item_Type = value;
					this.SendPropertyChanged("Item_Type");
					this.OnItem_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Item_ParentID", DbType="Int")]
		public System.Nullable<int> Item_ParentID
		{
			get
			{
				return this._Item_ParentID;
			}
			set
			{
				if ((this._Item_ParentID != value))
				{
					this.OnItem_ParentIDChanging(value);
					this.SendPropertyChanging();
					this._Item_ParentID = value;
					this.SendPropertyChanged("Item_ParentID");
					this.OnItem_ParentIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Orders", DbType="Int")]
		public System.Nullable<int> Orders
		{
			get
			{
				return this._Orders;
			}
			set
			{
				if ((this._Orders != value))
				{
					this.OnOrdersChanging(value);
					this.SendPropertyChanging();
					this._Orders = value;
					this.SendPropertyChanged("Orders");
					this.OnOrdersChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Content", DbType="NVarChar(MAX)")]
		public string Content
		{
			get
			{
				return this._Content;
			}
			set
			{
				if ((this._Content != value))
				{
					this.OnContentChanging(value);
					this.SendPropertyChanging();
					this._Content = value;
					this.SendPropertyChanged("Content");
					this.OnContentChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
