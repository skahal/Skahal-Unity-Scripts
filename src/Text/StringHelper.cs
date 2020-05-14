using System.Globalization;
using System.Linq;
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

    /// <summary>
    /// A shortcut to string.Format(CultureInfo.InvariantCulture.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="args">The arguments.</param>
    /// <returns>The formatted string</returns>
    public static string With(this string value, params object[] args)
    {
        return string.Format(CultureInfo.InvariantCulture, value, args);
    }

    /// <summary>
    /// Converts string to acroynm.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="maxAcronymLength">The max acronym length.</param>
    /// <param name="minWordLength">The min word length to consider in acronim.</param>
    /// <param name="wordSeparators">The word separators. Default is space.</param>
    /// <returns>The acronum.</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public static string ToAcronym(this string value, int maxAcronymLength = 2, int minWordLength = 3, params string[] wordSeparators)
    {
        if (wordSeparators.Length == 0)
        {
            wordSeparators = new string[] { " " };
        }

        return string.Join(
            "",
            value
            .Split(wordSeparators, System.StringSplitOptions.RemoveEmptyEntries)
            .Where(w => w.Length >= minWordLength)
            .Select(w => w[0].ToString().ToUpperInvariant())
            .Take(maxAcronymLength)
            .ToArray());
    }
    #endregion
}

