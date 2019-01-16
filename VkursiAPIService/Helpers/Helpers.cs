using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace VkursiAPI.Helpers
{
    /// <summary>
    /// Class that contains helpful methods to easy work with APIService
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Method that invoke POST request to server for authentication
        /// </summary>
        /// <param name="postData">json string that contains an user email and password wich sended to server as "application/json" content"</param>
        /// <param name="url"></param>
        /// <see cref="VkursiAPI.Services.APIService.Authentificate(string, string)"/>
        /// <returns>
        /// string, that contains a response from server in json format
        /// </returns>
        public static async Task<string> POSTRequestForAuth(string postData, string url)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes("{" + postData + "}");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.ContentLength = dataBytes.Length;
            request.ContentType = "application/json";
            request.Method = "POST";

            using (Stream requestBody = await request.GetRequestStreamAsync())
            {
                requestBody.Write(dataBytes, 0, dataBytes.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Method that invoke request to server for getting data from specified url and provider token
        /// </summary>
        /// <param name="postData">json string that contains data to sended to server</param>
        /// <param name="url">Url to which the request will be sent</param>
        /// <param name="bearer">Certified token that got after uthentication</param>
        /// <see cref="VkursiAPI.Services.APIService.GetData(object, Enums.APIType)"/>
        /// <returns>
        /// String, that contains response data from server in json format
        /// </returns>
        public static async Task<string> RequestForData(string postData, string url, string bearer)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization", "bearer " + bearer);

            if (url.Contains("changes"))
            {
                httpWebRequest.Method = "GET";
            }
            else
            {
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = postData;

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                return await streamReader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Method that makes json string from object
        /// </summary>
        /// <param name="str">Provided object</param>
        /// <example>
        /// Sample code to get json string from object
        /// <code>
        /// var json = Helper.MakeJson(new { Name = "John", Surname = "Stones", Age = 23 })
        /// </code>
        /// </example>
        /// <returns>
        /// String, that contains json serialized object from provided object
        /// </returns>
        public static string MakeJson(object str)
        {
            return JsonConvert.SerializeObject(str);
        }

        private static void SaveToFile(string data, string name, Encoding encoding, bool append)
        {
            using (var sw = new StreamWriter(name, append, encoding))
            {
                sw.WriteLine(data);
            }
        }

        /// <summary>
        /// Method that make and save result.xml file with provided data
        /// </summary>
        /// <param name="data">object with data to save</param>
        /// <param name="location">base path to wotking directory</param>
        /// <example>
        /// Sample code to save data
        /// <code>
        /// APIService api = APIService.getInstance();
        /// var result = api.GetData(new string[] { "00131350" }, ApiType.GetOrganizationInfo);
        /// api.SaveData(result, FileExtension.Xml, @"C:\Users\Desktop\ResultData\");
        /// </code>
        /// </example>
        public static void MakeXmlFile(object data, string location)
        {
            Type t = data.GetType();
            XmlSerializer serializer = new XmlSerializer(t);
            using (var f = File.Create(location + "\\result.xml"))
            {
                serializer.Serialize(f, data);
            }
        }

        /// <summary>
        /// Method that make and save result.xml file with provided data
        /// </summary>
        /// <param name="data">object with data to save</param>
        /// <param name="location">base path to wotking directory</param>
        /// <example>
        /// Sample code to save data
        /// <code>
        /// APIService api = APIService.getInstance();
        /// var result = api.GetData(new string[] { "00131350" }, ApiType.GetOrganizationInfo);
        /// api.SaveData(result, FileExtension.Json, @"C:\Users\Desktop\ResultData\");
        /// </code>
        /// </example>
        public static void MakeJsonFile(object data, string location)
        {
            Type t = data.GetType();
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Formatting = Formatting.Indented;
            SaveToFile(JsonConvert.SerializeObject(data, t, settings).ToString(), location + "\\result.json", Encoding.Default, false);
        }

        /// <summary>
        /// Method that make and save result.xml file with provided data
        /// </summary>
        /// <param name="data">object with data to save</param>
        /// <param name="location">base path to wotking directory</param>
        /// <example>
        /// Sample code to save data
        /// <code>
        /// APIService api = APIService.getInstance();
        /// var result = api.GetData(new string[] { "00131350" }, ApiType.GetOrganizationInfo);
        /// api.SaveData(result, FileExtension.Txt, @"C:\Users\Desktop\ResultData\");
        /// </code>
        /// </example>
        public static void MakeTxtFile(object data, string location)
        {
            string text = "";
            foreach (var item in ((IEnumerable<object>)data).Select(MakeInstance))
            {
                var temp = $"{item["name"]}\t{item["shortName"]}\t{item["edrpou"]}\t{item["location"]}\t{item["state"]}\t{item["boss"]}\t{item["kved"]}\t{item["inn"]}\t{item["DateRegisterInn"]}\t{item["DateAddedToMonitoring"]}\n";
                temp = Regex.Replace(temp, "(?:\t(?:\t)+)", "\t");
                temp = Regex.Replace(temp, "(?:\t(?:\n))", "");
                text += temp + Environment.NewLine;
            }
            SaveToFile(text, location + "\\result.txt", Encoding.Default, false);
        }

        private static Dictionary<string, object> MakeInstance(object data)
        {
            return data.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => prop.GetValue(data, null));
        }


        /// <summary>
        /// Method that desirialize json string to object
        /// </summary>
        /// <param name="json">Provided json string</param>
        /// <example>
        /// Sample code to get object from json string
        /// <code>
        /// var obj = Helper.ParseJson("[{"name":"ПУБЛІЧНЕ АКЦІОНЕРНЕ ТОВАРИСТВО \"КИЇВЕНЕРГО\"","shortName":"ПАТ \"КИЇВЕНЕРГО\"","edrpou":"00131305","inn":"001313026657","dateRegisterInn":"1997-09-24T00:00:00","boss":"ФОМЕНКО ОЛЕКСАНДР ВАЛЕРІЙОВИЧ","location":"01001, м.Київ, Печерський район, ПЛОЩА ІВАНА ФРАНКА, будинок 5","kved":"35.11 Виробництво електроенергії","state":"зареєстровано"}]")
        /// </code>
        /// </example>
        /// <returns>
        /// Object, that deserialized from provided json string
        /// </returns>
        public static object ParseJson(string json)
        {
            return JsonConvert.DeserializeObject<object>(json);
        }
    }
}
