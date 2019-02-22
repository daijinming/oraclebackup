using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using ecojobscheduler.common;

namespace EcobpMQ.TaskSet
{   
    /// <summary>
    /// 配置文件信息初始化,为了解决团队开发中,每个人的config文件不一致,而需要修改app.config或者web.config
    /// </summary>
    class ConfigInit
    {   
        /// <summary>
        /// 配置文件地址
        /// </summary>
        private static readonly string ConfigPath = FileHelper.GetAbsolutePath("Config/AutoConfig.config");

        private static BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static | BindingFlags.SetProperty;

        private static XmlDocument doc = null;

        static ConfigInit()
        {   
            InitConfig();
        }
        public static string GetValue(string key)
        {
            var value =  GetConfigValue(new PathMapAttribute { Key = key });

            return value;
        }
        /// <summary>
        /// 初始化配置信息
        /// </summary>
        private static void InitConfig()
        {
            try
            {
                doc = new XmlDocument();
                if (!File.Exists(ConfigPath))
                {   
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "私有方法"
        /// <summary>
        /// 通过键读取配置文件信息
        /// </summary>
        /// <param name="PathMap">自定义属性信息</param>
        /// <returns>值</returns>
        private static string GetConfigValue(PathMapAttribute PathMap)
        {
            if (!File.Exists(ConfigPath))
            {
                return "";
            }
            string path = GetXmlPath(PathMap.Key, PathMap.XmlPath);
            XmlNode node = null;
            XmlAttribute attr = null;
            try
            {
                //读取服务器配置文件信息
                doc.Load(ConfigPath);
                node = doc.SelectSingleNode(path);
                if (node != null)
                {
                    attr = node.Attributes["value"];
                    if (attr == null)
                    {
                        throw new Exception("服务器配置文件设置异常,节点" + PathMap.Key + "没有相应的value属性,请检查！");
                    }
                    return attr.Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        /// <summary>
        /// 获取xmlpath全路径
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="XmlPath">xmlpath路径前缀</param>
        /// <returns>xmlpath全路径</returns>
        private static string GetXmlPath(string key, string XmlPath)
        {
            return string.Format("{0}[@key='{1}']", XmlPath, key);
        }
               

        /// <summary>
        /// 返回MemberInfo对象指定类型的Attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        private static T GetMyAttribute<T>(MemberInfo m, bool inherit) where T : Attribute
        {
            T[] array = m.GetCustomAttributes(typeof(T), inherit) as T[];

            if (array.Length == 1)
                return array[0];

            if (array.Length > 1)
                throw new InvalidProgramException(string.Format("方法 {0} 不能同时指定多次 [{1}]。", m.Name, typeof(T)));

            return default(T);
        }

        #endregion
    }


    /// <summary>
    /// 配置文件标注
    /// </summary>
    class PathMapAttribute : Attribute
    {
        /// <summary>
        /// 键
        /// </summary>
        public string Key;

        /// <summary>
        /// xmlPath路径前缀
        /// </summary>
        public string XmlPath = @"/configuration/add";

        /// <summary>
        /// 是否需要对该值进行DES解密
        /// </summary>
        public bool IsDecrypt = false;
    }
}
