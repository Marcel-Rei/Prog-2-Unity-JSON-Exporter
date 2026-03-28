using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prog2JsonExporter.Scripts.Data;

namespace Prog2JsonExporter.Scripts.Json
{ 
    public class Prog2ObjectJsonDataConverter : JsonConverter<Prog2ObjectData>
    {
        public override void WriteJson(JsonWriter writer, Prog2ObjectData value, JsonSerializer serializer)
        {
            JObject jObject = new JObject();

            FieldInfo[] fields = typeof(Prog2ObjectData).GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field.Name == nameof(Prog2ObjectData.customObjectData))
                {
                    continue;
                }

                object fieldValue = field.GetValue(value);
                if (fieldValue != null)
                {
                    jObject[field.Name] = JToken.FromObject(fieldValue, serializer);
                }
            }
            
            if (value.customObjectData != null)
            {
                for (int i = 0; i < value.customObjectData.Length; ++i)
                {
                    var custom = value.customObjectData[i];

                    if (custom == null)
                    {
                        continue;
                    }
                    
                    JObject customJObject = JObject.FromObject(custom, serializer);

                    foreach (JProperty property in customJObject.Properties())
                    {
                        jObject[property.Name] = property.Value;
                    }
                    
                }
            }
            
            jObject.WriteTo(writer);
        }

        public override Prog2ObjectData ReadJson(JsonReader reader, Type objectType, Prog2ObjectData existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException("Deserialization not supported for Prog2ObjectData");
        }
    }
}