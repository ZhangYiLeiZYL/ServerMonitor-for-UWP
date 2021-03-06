﻿using ServerMonitor.DAO;
using ServerMonitor.Models;
using ServerMonitor.SiteDb;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMonitor.DAOImpl
{
    /// <summary>
    /// Author:xb
    /// </summary>
    public class ContactSiteDAOImpl : IContactSiteDao
    {
        /// <summary>
        /// 延迟加载实例
        /// </summary>
        public static ContactSiteDAOImpl Instance
        {
            get {
                return Nested.instance;
            }
        }

        /// <summary>
        /// 禁止直接生成实例
        /// </summary>
        private ContactSiteDAOImpl() { }

        /// <summary>
        /// 根据单个站点ID删除此站点与指定ID联系人的记录
        /// </summary>
        /// <param name="SiteId">特定的站点ID</param>
        /// <param name="ConnectId">联系人ID</param>
        /// <returns>此操作影响的数据行数</returns>
        public int DeleteConnect(int SiteId, int ContactId)
        {
            int result = -1;
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), DataBaseControlImpl.DBPath))
            {
                result = conn.Execute("delete from ContactSite where SiteId = ? and ContactId = ?", SiteId, ContactId);
            }
            return result;
        }

        /// <summary>
        /// 删除指定ID的站点的全部绑定记录
        /// </summary>
        /// <param name="SiteId">指定站点的ID</param>
        /// <returns>此操作影响的数据行数</returns>
        public int DeletSiteAllConnect(int SiteId)
        {
            int result = -1;
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), DataBaseControlImpl.DBPath))
            {
                result = conn.Execute("delete from ContactSite where SiteId = ?", SiteId);
            }
            return result;
        }

        /// <summary>
        /// 通过指定的站点ID获取所有该站点的绑定记录
        /// </summary>
        /// <param name="SiteId">指定站点的ID</param>
        /// <returns>指定ID的站点的绑定记录的集合</returns>
        public List<ContactSiteModel> GetConnectsBySiteId(int SiteId)
        {
            List<ContactSiteModel> resultList;
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), DataBaseControlImpl.DBPath))
            {
                resultList = conn.Table<ContactSiteModel>().Where(s=>s.SiteId == 
                SiteId).ToList<ContactSiteModel>();
            }
            return resultList;
        }

        /// <summary>
        /// 插入一条绑定记录
        /// </summary>
        /// <param name="contact">待插入的绑定记录</param>
        /// <returns>此次操作影响的数据行数</returns>
        public int InsertConnect(ContactModel contact)
        {
            int result = -1;
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), DataBaseControlImpl.DBPath))
            {
                result = conn.Insert(contact);
            }
            return result;
        }

        /// <summary>
        /// 插入一个绑定记录的集合
        /// </summary>
        /// <param name="connects">绑定记录集合</param>
        /// <returns>此次操作影响的数据行数</returns>
        public int InsertListConnects(List<ContactSiteModel> connects)
        {
            int result = -1;
            using (SQLiteConnection conn = new SQLiteConnection(new SQLitePlatformWinRT(), DataBaseControlImpl.DBPath))
            {
                result = conn.InsertAll(connects);
            }
            return result;
        }

        /// <summary>
        /// 用于延迟加载的实例
        /// </summary>
        class Nested
        {
            static Nested()
            {

            }
            internal static readonly ContactSiteDAOImpl instance = new ContactSiteDAOImpl();
        }
    }
}
