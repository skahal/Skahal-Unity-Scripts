using UnityEngine;
 

public interface IGameObjectDataHandler
{
	void SaveToRepository(IGameObjectDataRepository repository);
	void LoadFromRepository(IGameObjectDataRepository repository);
}

