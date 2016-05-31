using UnityEngine;
 

public interface IGameObjectDataRepository
{
	string ReadString(string key);
	void WriteString(string key, string value);
}

