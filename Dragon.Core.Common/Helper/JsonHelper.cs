namespace Dragon.Core.Common.Helper
{
    public class JsonHelper
    {
        /// <summary>
        /// 对象序列化
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="isUseTextJson">是否使用textjson</param>
        /// <returns>返回json字符串</returns>
        public static string ObjToJson(object obj, bool isUseTextJson = false)
        {
            if (isUseTextJson)
            {
                return System.Text.Json.JsonSerializer.Serialize(obj);
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
        }
        /// <summary>
        /// json反序列化obj
        /// </summary>
        /// <typeparam name="T">反序列类型</typeparam>
        /// <param name="strJson">json</param>
        /// <param name="isUseTextJson">是否使用textjson</param>
        /// <returns>返回对象</returns>
        public static T JsonToObj<T>(string strJson, bool isUseTextJson = false)
        {
            if (isUseTextJson)
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(strJson);
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(strJson);
            }
        }
        /// <summary>
        /// 转换对象为JSON格式数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>字符格式的JSON数据</returns>
        public static string GetJSON<T>(object obj)
        {
            string result = String.Empty;
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    serializer.WriteObject(ms, obj);
                    result = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 转换List<T>的数据为JSON格式
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="vals">列表值</param>
        /// <returns>JSON格式数据</returns>
        public string JSON<T>(List<T> vals)
        {
            System.Text.StringBuilder st = new System.Text.StringBuilder();
            try
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer s = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));

                foreach (T city in vals)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        s.WriteObject(ms, city);
                        st.Append(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return st.ToString();
        }
        /// <summary>
        /// JSON格式字符转换为T类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T ParseFormByJson<T>(string jsonStr)
        {
            using (System.IO.MemoryStream ms =
            new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonStr)))
            {
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer =
                new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
