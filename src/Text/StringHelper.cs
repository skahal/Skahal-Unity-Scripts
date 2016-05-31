using UnityEngine;

public static class StringHelper
{
    #region FormatSingularOrPlural
    /// <summary>
    /// Executa um StringHelper.Format levando em conta o valor de counter,
    /// se counter for maior que 1 então utilizará a string informada em formatPlural, 
    /// caso contrário utilizará a string informada em formatSingular.
    /// </summary>
    /// <param name="counter">O contador para definir se será utilizado a string para singular ou plural.</param>
    /// <param name="formatSingular">A string para singular.</param>
    /// <param name="formatPlural">A string para plural.</param>
    /// <param name="args">Os argumentos para as strings.</param>
    /// <returns>Retorna a string formatada (formatSingular ou formatPlural) com os valores.</returns>
    public static string FormatSingularOrPlural(int counter, string formatSingular, string formatPlural, params object[] args)
    {
        return System.String.Format(Mathf.Abs(counter) > 1 ? formatPlural : formatSingular, args);
    }
    #endregion
}

