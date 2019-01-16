using System;
using System.Collections.Generic;
using VkursiAPI.Enums;
using VkursiAPI.Extensions;
using VkursiAPI.Models;
using VkursiAPI.Helpers;
using Newtonsoft.Json.Linq;

namespace VkursiAPI.Services
{
    /// <summary>
    /// Main logic class that contains core methods to work with Vkursis's API
    /// </summary>
    public class APIService
    {
        private static APIService instance;

        private APIService()
        { }

        public static APIService getInstance()
        {
            if (instance == null)
                instance = new APIService();
            return instance;
        }

        private string _bearer { get; set; }
        private readonly string _baseUrl = "https://vkursi.pro/api/1.0/";

        /// <summary>
        /// This method requires user name and password under which is registered on Vkursi to get certified token from server
        /// </summary>
        /// <param name="userName">User name which registered in Vkursi (user@user.com)</param>
        /// <param name="password">Password under which this user is registered (p@s$w0rd)</param>
        /// <example>
        /// Sample code to get token from server to provided userName and password
        /// <code>
        /// APIService api = APIService.getInstance();
        /// if(!api.Authentificate("user@user.com", "passwrod"))
        ///     return;
        /// //some else activity if user authentificate successfully
        /// </code>
        /// </example>
        /// <returns>
        /// Return true if user authentificate successfully, otherwise - false
        /// </returns>
        public bool Authentificate(string userName, string password)
        {
            var response = Helper.POSTRequestForAuth($"\"email\":\"{userName}\", \"password\":\"{password}\"", _baseUrl + "token").Result;

            if (!String.IsNullOrWhiteSpace(response))
            {
                try
                {
                    _bearer = ((JObject)Helper.ParseJson(response)).ToObject<Dictionary<string, string>>()["token"];
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// A method that saves data after a provided request to API
        /// </summary>
        /// <param name="data">Data which sended to API server</param>
        /// <param name="type">Api type which would parsed to get correct url</param>
        /// <example>
        /// The sample code to get data from Vkursi's API
        /// <code>
        /// APIService api = APIService.getInstance();
        /// api.GetData(new string[] { "00131305", "40073472" }, ApiType.GetOrganizationInfo);
        /// </code>
        /// </example>
        /// <returns>
        /// Return true if response with status OK and data saved, otherwise - false
        /// </returns>
        public object GetData(object data, APIType type)
        {
            var response = (type != APIType.GetChanges ? 
                Helper.RequestForData(Helper.MakeJson(data), _baseUrl + type.DisplayName(), _bearer).Result :
                Helper.RequestForData(null, _baseUrl + type.DisplayName() + "/" + ((string[])data)[0], _bearer).Result);

            if (String.IsNullOrWhiteSpace(response))
            {
                return null;
            }

            object result;

            if (type != APIType.GetChanges)
                result = ((JArray)Helper.ParseJson(response)).ToObject<CompanyModel[]>();
            else
                result = ((JArray)Helper.ParseJson(response)).ToObject<CompanyChanges[]>();

            return result;
        }
        /// <summary>
        /// This method saved data to choosen file in a specified location
        /// </summary>
        /// <param name="data">Data to save</param>
        /// <param name="extension">Extension of the file which would be saved</param>
        /// <param name="path">Base path to location</param>
        /// <example>
        /// Sample code to save data about organization(s) to result.xml file
        /// <code>
        /// APIService api = APIService.getInstance();
        /// var result = api.GetData(new string[] { "00131305", "40073472" }, ApiType.GetOrganizationInfo);
        /// api.SaveData(result, FileExtension.Xml, @"C:\Users\Desktop\ResultData\"); \\ Note that it is not possible to specify a filename (by default its name - result.*)
        /// </code>
        /// </example>
        public void SaveData(object data, FileExtension extension, string path)
        {
            if (extension == FileExtension.Xml)
                Helper.MakeXmlFile(data, path);
            if (extension == FileExtension.Json)
                Helper.MakeJsonFile(data, path);
            if (extension == FileExtension.Txt)
                Helper.MakeTxtFile(data, path);
        }
    }
}
