using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CB.IntegrationService.Utils
{
    public static class JsonHelper
    {
        #region WriteObjectToFile

        /// <summary>
        /// Writes the object to file as JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="absoluteFileName">Name of the absolute file.</param>
        public static void WriteObjectToFile<T>(T obj, string absoluteFileName)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
            };
            WriteObjectToFile<T>(obj, absoluteFileName, serializer);
        }

        #endregion WriteObjectToFile

        #region WriteObjectToFile

        /// <summary>
        /// Writes the object to file as JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="absoluteFileName">Name of the absolute file.</param>
        public static void WriteObjectToFile<T>(T obj, string absoluteFileName, JsonSerializer jsonSerializer)
        {
            try
            {
                // Create a backup.
                // Copy the current file to .bak; first delete .bak
                if (File.Exists(absoluteFileName + ".bak"))
                {
                    File.Delete(absoluteFileName + ".bak");
                }

                if (File.Exists(absoluteFileName))
                {
                    File.Move(absoluteFileName, absoluteFileName + ".bak");
                }

                using (FileStream fs = File.Open(absoluteFileName, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
#if DEBUG
                    jw.Formatting = Formatting.Indented;
#else
                    jw.Formatting = Formatting.None;
#endif

                    jsonSerializer.Serialize(jw, obj);
                }
            }
            catch (Exception ex)
            {
                ex.LogException();
                throw;
            }
        }

        #endregion WriteObjectToFile

        #region ReadObjectFromJsonFile

        /// <summary>
        /// Reads the object from json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="absoluteFileName">Name of the absolute file.</param>
        /// <returns></returns>
        public static T ReadObjectFromJsonFile<T>(string absoluteFileName)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate

            };

            return ReadObjectFromJsonFile<T>(absoluteFileName, serializer);
        }

        #endregion ReadObjectFromJsonFile

        #region ReadObjectFromJsonFile

        /// <summary>
        /// Reads the object from json file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="absoluteFileName">Name of the absolute file.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public static T ReadObjectFromJsonFile<T>(string absoluteFileName, JsonSerializer serializer)
        {
            using (StreamReader sReader = File.OpenText(absoluteFileName))
            using (JsonReader jReader = new JsonTextReader(sReader))
            {
                return serializer.Deserialize<T>(jReader);
            }
        }

        #endregion ReadObjectFromJsonFile

        #region Serialize - 2 overloads

        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            JsonSerializer serializer = new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                TypeNameHandling = TypeNameHandling.Auto

            };

            return Serialize<T>(obj, serializer);
        }

        /// <summary>
        /// Serializes the specified object using the specified JsonSerializer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns></returns>
        public static string Serialize<T>(T obj, JsonSerializer serializer)
        {
            string jsonString = string.Empty;
            using (StringWriter sw = new StringWriter())
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
#if DEBUG
                jw.Formatting = Formatting.Indented;
#else
                jw.Formatting = Formatting.None;
#endif

                serializer.Serialize(jw, obj);
                jsonString = sw.ToString();
            }

            return jsonString;
        }

        #endregion Serialize - 2 overloads

        #region DeSerialize - 2 overloads

        /// <summary>
        /// Deserializes the json string.
        /// </summary>
        /// <typeparam name="T">The type of object to serialize</typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string jsonStr)
        {
            JsonSerializerSettings serializer = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                TypeNameHandling = TypeNameHandling.Auto
            };


            return DeSerialize<T>(jsonStr, serializer);
        }

        /// <summary>
        /// Deserializes the specified Json string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr">The json string.</param>
        /// <param name="serializerSettings">The serializer.</param>
        /// <returns></returns>
        public static T DeSerialize<T>(string jsonStr, JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr, serializerSettings);
        }
        /// <summary>
        /// Deserializes the specified Json string to a string.
        /// </summary>
        /// <param name="jsonStr">The json string.</param>
        /// <returns></returns>
        public static object DeSerializeObject(string jsonStrs)
        {
            JsonSerializerSettings serializer = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.DeserializeObject(jsonStrs, serializer);
        }

        #endregion DeSerialize - 2 overloads

        #region CreateClone

        /// <summary>
        /// Gets a clone of the specified object by serializing and deserializing.
        /// </summary>
        /// <typeparam name="T">The type of object to clone.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>A clone of the original object</returns>
        public static T GetAClone<T>(T obj)
        {
            string jsonStr = Serialize<T>(obj);
            T clone = DeSerialize<T>(jsonStr);

            return clone;
        }

        #endregion CreateClone
    }
}
