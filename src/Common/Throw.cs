using System;

namespace Skahal.Common
{
	public static class Throw
	{
		public static void AnyNull(object target)
		{
			if (target == null) {
				throw new ArgumentNullException ("target");
			}

			var properties = target.GetType ().GetProperties ();

			foreach (var p in properties) {
				if (p.GetValue (target, null) == null) {
					throw new ArgumentNullException (p.Name);
				}
			}
		}
	}
}