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

namespace _13W数据关联颁布机构
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="MIS")]
	public partial class DataClasses2DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertAct_Dept(Act_Dept instance);
    partial void UpdateAct_Dept(Act_Dept instance);
    partial void DeleteAct_Dept(Act_Dept instance);
    partial void InsertDept(Dept instance);
    partial void UpdateDept(Dept instance);
    partial void DeleteDept(Dept instance);
    #endregion
		
		public DataClasses2DataContext() : 
				base(global::_13W数据关联颁布机构.Properties.Settings.Default.MISConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses2DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses2DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses2DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses2DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Act_Dept> Act_Dept
		{
			get
			{
				return this.GetTable<Act_Dept>();
			}
		}
		
		public System.Data.Linq.Table<Dept> Dept
		{
			get
			{
				return this.GetTable<Dept>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.fn_SolrJoinActDeptID", IsComposable=true)]
		public string fn_SolrJoinActDeptID([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ActID", DbType="Int")] System.Nullable<int> actID, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="char", DbType="VarChar(1)")] string @char)
		{
			return ((string)(this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), actID, @char).ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Act_Dept")]
	public partial class Act_Dept : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _DeptID;
		
		private int _ActID;
		
		private System.Nullable<int> _Type;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnDeptIDChanging(int value);
    partial void OnDeptIDChanged();
    partial void OnActIDChanging(int value);
    partial void OnActIDChanged();
    partial void OnTypeChanging(System.Nullable<int> value);
    partial void OnTypeChanged();
    #endregion
		
		public Act_Dept()
		{
			OnCreated();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeptID", DbType="Int NOT NULL")]
		public int DeptID
		{
			get
			{
				return this._DeptID;
			}
			set
			{
				if ((this._DeptID != value))
				{
					this.OnDeptIDChanging(value);
					this.SendPropertyChanging();
					this._DeptID = value;
					this.SendPropertyChanged("DeptID");
					this.OnDeptIDChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="Int")]
		public System.Nullable<int> Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
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
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Dept")]
	public partial class Dept : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _DeptID;
		
		private System.Guid _rowId;
		
		private byte _IsOnline;
		
		private System.Nullable<int> _Dept_Code;
		
		private string _Dept_Type;
		
		private string _Dept_Name;
		
		private string _Abbreviation;
		
		private string _Dept_Trans;
		
		private string _Dept_Alias;
		
		private System.Nullable<int> _TDeptID;
		
		private System.Nullable<System.DateTime> _OpDate;
		
		private System.Nullable<System.DateTime> _syn_date;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDeptIDChanging(int value);
    partial void OnDeptIDChanged();
    partial void OnrowIdChanging(System.Guid value);
    partial void OnrowIdChanged();
    partial void OnIsOnlineChanging(byte value);
    partial void OnIsOnlineChanged();
    partial void OnDept_CodeChanging(System.Nullable<int> value);
    partial void OnDept_CodeChanged();
    partial void OnDept_TypeChanging(string value);
    partial void OnDept_TypeChanged();
    partial void OnDept_NameChanging(string value);
    partial void OnDept_NameChanged();
    partial void OnAbbreviationChanging(string value);
    partial void OnAbbreviationChanged();
    partial void OnDept_TransChanging(string value);
    partial void OnDept_TransChanged();
    partial void OnDept_AliasChanging(string value);
    partial void OnDept_AliasChanged();
    partial void OnTDeptIDChanging(System.Nullable<int> value);
    partial void OnTDeptIDChanged();
    partial void OnOpDateChanging(System.Nullable<System.DateTime> value);
    partial void OnOpDateChanged();
    partial void Onsyn_dateChanging(System.Nullable<System.DateTime> value);
    partial void Onsyn_dateChanged();
    #endregion
		
		public Dept()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DeptID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int DeptID
		{
			get
			{
				return this._DeptID;
			}
			set
			{
				if ((this._DeptID != value))
				{
					this.OnDeptIDChanging(value);
					this.SendPropertyChanging();
					this._DeptID = value;
					this.SendPropertyChanged("DeptID");
					this.OnDeptIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_rowId", DbType="UniqueIdentifier NOT NULL")]
		public System.Guid rowId
		{
			get
			{
				return this._rowId;
			}
			set
			{
				if ((this._rowId != value))
				{
					this.OnrowIdChanging(value);
					this.SendPropertyChanging();
					this._rowId = value;
					this.SendPropertyChanged("rowId");
					this.OnrowIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsOnline", DbType="TinyInt NOT NULL")]
		public byte IsOnline
		{
			get
			{
				return this._IsOnline;
			}
			set
			{
				if ((this._IsOnline != value))
				{
					this.OnIsOnlineChanging(value);
					this.SendPropertyChanging();
					this._IsOnline = value;
					this.SendPropertyChanged("IsOnline");
					this.OnIsOnlineChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dept_Code", DbType="Int")]
		public System.Nullable<int> Dept_Code
		{
			get
			{
				return this._Dept_Code;
			}
			set
			{
				if ((this._Dept_Code != value))
				{
					this.OnDept_CodeChanging(value);
					this.SendPropertyChanging();
					this._Dept_Code = value;
					this.SendPropertyChanged("Dept_Code");
					this.OnDept_CodeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dept_Type", DbType="VarChar(5)")]
		public string Dept_Type
		{
			get
			{
				return this._Dept_Type;
			}
			set
			{
				if ((this._Dept_Type != value))
				{
					this.OnDept_TypeChanging(value);
					this.SendPropertyChanging();
					this._Dept_Type = value;
					this.SendPropertyChanged("Dept_Type");
					this.OnDept_TypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dept_Name", DbType="NVarChar(50) NOT NULL", CanBeNull=false)]
		public string Dept_Name
		{
			get
			{
				return this._Dept_Name;
			}
			set
			{
				if ((this._Dept_Name != value))
				{
					this.OnDept_NameChanging(value);
					this.SendPropertyChanging();
					this._Dept_Name = value;
					this.SendPropertyChanged("Dept_Name");
					this.OnDept_NameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Abbreviation", DbType="VarChar(20)")]
		public string Abbreviation
		{
			get
			{
				return this._Abbreviation;
			}
			set
			{
				if ((this._Abbreviation != value))
				{
					this.OnAbbreviationChanging(value);
					this.SendPropertyChanging();
					this._Abbreviation = value;
					this.SendPropertyChanged("Abbreviation");
					this.OnAbbreviationChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dept_Trans", DbType="VarChar(200)")]
		public string Dept_Trans
		{
			get
			{
				return this._Dept_Trans;
			}
			set
			{
				if ((this._Dept_Trans != value))
				{
					this.OnDept_TransChanging(value);
					this.SendPropertyChanging();
					this._Dept_Trans = value;
					this.SendPropertyChanged("Dept_Trans");
					this.OnDept_TransChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Dept_Alias", DbType="NVarChar(100)")]
		public string Dept_Alias
		{
			get
			{
				return this._Dept_Alias;
			}
			set
			{
				if ((this._Dept_Alias != value))
				{
					this.OnDept_AliasChanging(value);
					this.SendPropertyChanging();
					this._Dept_Alias = value;
					this.SendPropertyChanged("Dept_Alias");
					this.OnDept_AliasChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TDeptID", DbType="Int")]
		public System.Nullable<int> TDeptID
		{
			get
			{
				return this._TDeptID;
			}
			set
			{
				if ((this._TDeptID != value))
				{
					this.OnTDeptIDChanging(value);
					this.SendPropertyChanging();
					this._TDeptID = value;
					this.SendPropertyChanged("TDeptID");
					this.OnTDeptIDChanged();
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
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_syn_date", DbType="DateTime")]
		public System.Nullable<System.DateTime> syn_date
		{
			get
			{
				return this._syn_date;
			}
			set
			{
				if ((this._syn_date != value))
				{
					this.Onsyn_dateChanging(value);
					this.SendPropertyChanging();
					this._syn_date = value;
					this.SendPropertyChanged("syn_date");
					this.Onsyn_dateChanged();
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
