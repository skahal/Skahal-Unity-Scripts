using System.Collections.Generic;
using System;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Skahal.Infrastructure.Framework.Commons
{
	/// <summary>
	/// Serviço para injeção de dependência.
	/// </summary>
	public static class DependencyService
	{
		#region Fields
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		private static Dictionary<Type, Func<object>> s_mapping = new Dictionary<Type, Func<object>>();
		#endregion

		#region Methods
		/// <summary>
		/// Registra a implementação para a tipo informado.
		/// </summary>
		/// <typeparam name="TType">O tipo.</typeparam>
		/// <param name="createImplementation">A função que retorna a implementação.</param>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static void Register<TType>(Func<object> createImplementation)
		{
			s_mapping[typeof(TType)] = createImplementation;
		}

		/// <summary>
		/// Registra a implementação para o tipo informado.
		/// </summary>
		/// <typeparam name="TType">O tipo</typeparam>
		/// <param name="implementationInstance"></param>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static void Register<TType>(object implementationInstance)
		{
			s_mapping[typeof(TType)] = () => { return implementationInstance; };
		}

		/// <summary>
		/// Cria a implentação para o tipo.
		/// </summary>
		/// <typeparam name="TType">O tipo.</typeparam>
		/// <returns>A inmplementação.</returns>
		[SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static TType Create<TType>()
		{
			var type = typeof(TType);

			if (s_mapping.ContainsKey(type))
			{
				return (TType)s_mapping[type]();
			}
			else
			{
				var msg = String.Format(CultureInfo.InvariantCulture, "Não existe implementação para o tipo '{0}' registrada em DomainFactory.", type);
				throw new InvalidOperationException(msg);
			}
		}
		#endregion
	}
}