#region Usings
using System;
using System.Reflection;
using UnityEngine;
#endregion

/// <summary>
/// Helpers for components.
/// </summary>
public class SHComponentHelper
{
    /// <summary>
    /// Permite o envio de mensagem com mais de um argumento, sem que seja necessário que o método receptor tenha um array como parâmetro.
    /// </summary>
    /// <param name="component">O componente que receberá a mensagem.</param>
    /// <param name="scriptName">O nome do script.</param>
    /// <param name="methodName">O nome do método.</param>
    /// <param name="args">Os argumetos do método.</param>
    public static void SendMessage (Component component, string scriptName, string methodName, params object[] args)
	{
		Component script = component.GetComponent (scriptName);
		Type[] argsTypes = new Type[args.Length];

		for (int i = 0; i < args.Length; i++)
		{
			argsTypes [i] = args [i].GetType ();
		}

		MethodInfo method = script.GetType ().GetMethod (methodName, argsTypes);

		if (method == null)
		{
			string msg = String.Format ("O método '{0}' não foi localizado no script '{1}'. Verifique o nome do script, do método e os argumentos informados.", scriptName, methodName);
			throw new InvalidOperationException (msg);
		}

		method.Invoke (script, args);
	}
	
	/// <summary>
	/// Destroy the component if exists.
	/// </summary>
	/// <param name='component'>
	/// Component.
	/// </param>
	public static void DestroyIfExists (UnityEngine.Object component)
	{
		if (component != null)
		{
			Component.Destroy (component);
		}
	}
}

