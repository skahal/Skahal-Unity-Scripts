using UnityEngine;
using System;

public class QuaternionHelper
{
	public static string Serialize(Quaternion[] qs)
	{
		var length = qs.Length;
		var serialization = new string[length];
		
		for (int i = 0; i < length; i++)
		{
			serialization[i] = Serialize(qs[i]);
		}
		
		return String.Join("|", serialization);
	}
	
	public static string Serialize(Quaternion q)
	{
		return String.Format("{0}#{1}#{2}#{3}", q.x, q.y, q.z, q.w);
	}

	public static Quaternion Deserialize(string q)
	{
		string[] parts = q.Split('#');
		
		return new Quaternion(Convert.ToSingle(parts[0]), Convert.ToSingle(parts[1]), Convert.ToSingle(parts[2]), Convert.ToSingle(parts[3]));
	}
	
	public static Quaternion[] DeserializeMany(string qs)
	{
		var serialization = qs.Split('|');
		var length = serialization.Length;
		var deserialization = new Quaternion[length];
		
		for (int i = 0; i < length; i++)
		{
			deserialization[i] = Deserialize(serialization[i]);
		}
		
		return deserialization;
	}
}

