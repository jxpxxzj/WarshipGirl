using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace WarshipGirl.Utilities
{
    /// <summary>
    /// 反序列化器 Created by ScoreManager
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    class Deserializer<T>
    {
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <param name="text">Json文本</param>
        /// <returns>反序列化后的List形式列表</returns>
        public static List<T> JsonDeserializeListData(string text)
        {
            T[] preparedArray;
            List<T> ReturnList;

            JavaScriptSerializer jser = new JavaScriptSerializer();
            preparedArray = jser.Deserialize<T[]>(text);
            ReturnList = preparedArray.ToList<T>();
            return ReturnList;
        }
        public static T JsonDeserializeSingleData(string text)
        {
            T ReturnItem;

            JavaScriptSerializer jser = new JavaScriptSerializer();
            ReturnItem = jser.Deserialize<T>(text);
            return ReturnItem;
        }
    }
    /// <summary>
    /// 序列化器 Created by ScoreManager
    /// </summary>
    class Serialize
    {
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">传入的对象</param>
        /// <returns>对象的Json字符串</returns>
        public static string JsonSerialize(object obj)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            return jser.Serialize(obj);
        }
    }
}
