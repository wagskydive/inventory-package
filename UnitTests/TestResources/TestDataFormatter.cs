using System.Linq;
public static class TestDataFormatter
{
    public static string ToSentence( this string Input )
    {
        return new string(Input.SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray());
    }
}