//============================================================
// Producnt name:		AUTOCODING
// Version: 			1.0
// Coded by:	
// Auto generated at: 	2016/2/22 14:59:35
//============================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
	/*
    
    */
	[Serializable()]
	public partial class TCustomer
	{  
		private int? customerID;
		private string loginName;
		private string loginPwd;
		private string customerName;
		private string mobile;
		private string product;
		private string version;
		private decimal? price;
		private int? term;
		private DateTime? openTime;
		private DateTime? lastTime;
		private DateTime? remitTime;
		
		/// <summary>
        ///  客户编号
        /// </summary>
		public int? CustomerID
		{
			get { return customerID; }
			set { customerID = value; }
		}		
		/// <summary>
        ///  登陆名
        /// </summary>
		public string LoginName
		{
			get { return loginName; }
			set { loginName = value; }
		}		
		/// <summary>
        ///  登录密码
        /// </summary>
		public string LoginPwd
		{
			get { return loginPwd; }
			set { loginPwd = value; }
		}		
		/// <summary>
        ///  客户名称
        /// </summary>
		public string CustomerName
		{
			get { return customerName; }
			set { customerName = value; }
		}		
		/// <summary>
        ///  手机
        /// </summary>
		public string Mobile
		{
			get { return mobile; }
			set { mobile = value; }
		}		
		/// <summary>
        ///  产品
        /// </summary>
		public string Product
		{
			get { return product; }
			set { product = value; }
		}		
		/// <summary>
        ///  版本
        /// </summary>
		public string Version
		{
			get { return version; }
			set { version = value; }
		}		
		/// <summary>
        ///  价格
        /// </summary>
		public decimal? Price
		{
			get { return price; }
			set { price = value; }
		}		
		/// <summary>
        ///  服务期限
        /// </summary>
		public int? Term
		{
			get { return term; }
			set { term = value; }
		}		
		/// <summary>
        ///  开通时间
        /// </summary>
		public DateTime? OpenTime
		{
			get { return openTime; }
			set { openTime = value; }
		}		
		/// <summary>
        ///  最后服务月份
        /// </summary>
		public DateTime? LastTime
		{
			get { return lastTime; }
			set { lastTime = value; }
		}		
		/// <summary>
        ///  付款时间
        /// </summary>
		public DateTime? RemitTime
		{
			get { return remitTime; }
			set { remitTime = value; }
		}		
		/// <summary>
        ///  ??????--??
        /// </summary>
		public long? ABCD
		{
			get;
			set;
		}
		/// <summary>
        ///  ??????--????
        /// </summary>
		public int TEMP
		{
			get;
			set;
		}
	}
}