using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ConsoleJsonParser;

namespace ConsoleJsonParser
{
    public static class Program
    {
        public static int keyLevel=0;
        public static void Main(string[] args)
        {
            string myJson = "{\"key1\": 1,\"key2\": {\"key3\": 1,\"key4\": {\"key5\": 4}}}";
            //{"key1": 1,"key2": {"key3": 1,"key4": {"key5": 4}}

            ExpandoObject myExObj = myJson.SerializeJson();
            
            Console.ReadKey();
        }

        public static ExpandoObject SerializeJson(this string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            IDictionary<string, object> dictionary =
                serializer.Deserialize<IDictionary<string, object>>(json);
            return dictionary.Expando();
        }

        public static ExpandoObject Expando(this IDictionary<string, object> dictionary)
        {
            ExpandoObject expandoObject = new ExpandoObject();
            IDictionary<string, object> objects = expandoObject;

            foreach (var item in dictionary)
            {
                bool processed = false;

                if (item.Value is IDictionary<string, object>)
                {
                    //keyLevel--;
                    Console.Write(item.Key + ":");
                    Console.WriteLine(keyLevel);
                    objects.Add(item.Key, Expando((IDictionary<string, object>)item.Value));
                    processed = true;
                }
                else if (item.Value is ICollection)
                {
                    List<object> itemList = new List<object>();

                    foreach (var item2 in (ICollection)item.Value)

                        if (item2 is IDictionary<string, object>)
                            itemList.Add(Expando((IDictionary<string, object>)item2));
                        else
                            itemList.Add(Expando(new Dictionary<string,
                                object> { { "Unknown", item2 } }));

                    if (itemList.Count > 0)
                    {
                        objects.Add(item.Key, itemList);
                        processed = true;
                    }
                }

                if (!processed)
                {
                    keyLevel++;
                    Console.Write(item.Key+":");
                    Console.WriteLine(keyLevel);
                    objects.Add(item);
                }
            }

            return expandoObject;
        }
    }
}
