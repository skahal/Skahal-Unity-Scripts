#region Usings
using UnityEngine;
#endregion

public static class BoolHelper
{
	public static string Serialize(bool b)
	{
		return b ? "1" : "0";
	}

	public static bool Deserialize(string b)
	{
		return b == "1";
	}
}

