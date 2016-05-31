using UnityEngine;
 

public static class IntHelper
{
	public static string Serialize(int i)
	{
		return i.ToString();
	}

	public static int Deserialize(string i)
	{
		return System.Convert.ToInt32(i);
	}
}

