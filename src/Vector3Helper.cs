using UnityEngine;
 
using System;

public static class Vector3Helper
{
	public static string Serialize(Vector3 v)
	{
		return String.Format("{0}#{1}#{2}", v.x, v.y, v.z);
	}
	
	public static string Serialize(Vector3[] vs)
	{
		var length = vs.Length;
		var serialization = new string[length];
		
		for (int i = 0; i < length; i++)
		{
			serialization[i] = Serialize(vs[i]);
		}
		
		return String.Join("|", serialization);
	}
	
	public static Vector3 Deserialize(string v)
	{
		string[] parts = v.Split('#');
		
		return new Vector3(Convert.ToSingle(parts[0]), Convert.ToSingle(parts[1]), Convert.ToSingle(parts[2]));
	}
	
	public static Vector3[] DeserializeMany(string vs)
	{
		var serialization = vs.Split('|');
		var length = serialization.Length;
		var deserialization = new Vector3[length];
		
		for (int i = 0; i < length; i++)
		{
			deserialization[i] = Deserialize(serialization[i]);
		}
		
		return deserialization;
	}
}
