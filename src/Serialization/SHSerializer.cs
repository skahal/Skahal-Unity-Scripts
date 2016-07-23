#region Usings
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
using Skahal.Logging;
#endregion

namespace Skahal.Serialization
{
	/// <summary>
	/// A object serializer.
	/// </summary>
	public static class SHSerializer
	{
		#region Delegate
		delegate string SerializeHandler<TObject>(TObject obj);
		delegate TObject DeserializeHandler<TObject>(string serialized);
		#endregion
		
		#region System.Type
		public static string SerializeType(System.Type type)
		{
			return type.FullName;
		}
		
		public static System.Type DeserializeType(string typeSerialized)
		{
			return System.Type.GetType(typeSerialized);
		}
		
		public static string SerializeTypes(IList<System.Type> types)
		{
			return SerializeMany<System.Type>(types, SerializeType);
		}
		
		public static IList<System.Type> DeserializeTypes(string typesSerialized)
		{
			return DeserializeMany<System.Type>(typesSerialized, DeserializeType);
		}
		#endregion
		
		#region Vector2
		public static string SerializeVector2(Vector2 vector2)
		{
			return vector2.x + "#" + vector2.y;
		}
	
		public static Vector2 DeserializeVector2(string serialized)
		{
			var parts = serialized.Split('#');
			return new Vector2(System.Convert.ToSingle(parts[0]), System.Convert.ToSingle(parts[1]));
		}
	
		public static string SerializeVector2s(IList<Vector2> vector2s)
		{
			return SerializeMany<Vector2>(vector2s, SerializeVector2);
		}
	
		public static IList<Vector2> DeserializeVector2s(string serialized)
		{
			return DeserializeMany<Vector2>(serialized, DeserializeVector2);
		}
		#endregion

		#region Vector3
		public static string SerializeVector3(Vector3 vector3)
		{
			return "{0}#{1}#{2}".With (vector3.x, vector3.y, vector3.z);
		}

		public static Vector3 DeserializeVector3(string serialized)
		{
			var parts = serialized.Split('#');
			return new Vector3(System.Convert.ToSingle(parts[0]), System.Convert.ToSingle(parts[1]), System.Convert.ToSingle(parts[2]));
		}

		public static string SerializeVector3s(IList<Vector3> vector3s)
		{
			return SerializeMany<Vector3>(vector3s, SerializeVector3);
		}

		public static IList<Vector3> DeserializeVector3s(string serialized)
		{
			return DeserializeMany<Vector3>(serialized, DeserializeVector3);
		}
		#endregion
		
		#region To and from Objects
		public static string SerializeToString (object target)
		{
			var bytes = SerializeToBytes (target);
			var base64 = System.Convert.ToBase64String(bytes);
			return base64;
		}
		
		public static TTarget DeserializeFromString<TTarget> (string targetString)
		{
			byte[] targetBytes = System.Convert.FromBase64String(targetString);
			return DeserializeFromBytes<TTarget>(targetBytes);
		}
		#endregion
		
		#region To and from bytes
		public static byte[] SerializeToBytes<TTarget> (TTarget target)
		{
			var formatter = new BinaryFormatter ();
			
			using (var stream = new MemoryStream ()) {
				formatter.Serialize (stream, target);
				var bytes = stream.ToArray ();	
				return bytes;
			}
		}
		
		public static TTarget DeserializeFromBytes<TTarget> (byte[] targetBytes)
		{
			try 
			{
				var formatter = new BinaryFormatter ();
				
				using (var stream = new MemoryStream (targetBytes)) {
					return (TTarget) formatter.Deserialize(stream);
				}
			}
			catch(System.Exception ex)
			{
				SHLog.Error(ex.Message);
				return default(TTarget);
			}
				
		}
		#endregion
		
		#region Helpers
		static string SerializeMany<TObject>(IList<TObject> objects, SerializeHandler<TObject> serializeHandler)
		{
			var length = objects.Count;
			var serialization = new string[length];
			
			for (int i = 0; i < length; i++)
			{
				var obj = objects[i];
				serialization[i] = serializeHandler(obj);
			}
			
			return string.Join("|", serialization);
		}
		
		static IList<TObject> DeserializeMany<TObject>(string serialized, DeserializeHandler<TObject> deserializeHandler)
		{
			var serialization = serialized.Split('|');
			var length = serialization.Length;
			var objects = new List<TObject>(length);
			
			for (int i = 0; i < length; i++)
			{
				var ser = serialization[i];
				objects.Add(deserializeHandler(ser));
			}
			
			return objects;
		}
		#endregion
	}
}