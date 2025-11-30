using System;

public static class ScoreFormatter
{
    private static readonly string[] Suffixes =
    {
        "", "K", "M", "B", "T",
        "aa","ab","ac","ad","ae","af","ag","ah","ai","aj","ak","al","am","an","ao","ap","aq","ar","as","at","au","av","aw","ax","ay","az",
        "ba","bb","bc","bd","be","bf","bg","bh","bi","bj","bk","bl","bm","bn","bo","bp","bq","br","bs","bt","bu","bv","bw","bx","by","bz",
    };

    /// <summary>
    /// 1532 -> "1K"
    /// </summary>
    public static string FormatF0(double value)
    {
        return Format(value, 0);
    }

    /// <summary>
    /// 1532 -> "1.53K"
    /// </summary>
    public static string FormatF2(double value)
    {
        return Format(value, 2);
    }

    private static string Format(double number, int decimals)
    {
        if (double.IsNaN(number) || double.IsInfinity(number))
            return "0";

        bool isNegative = number < 0;
        double val = Math.Abs(number);

        int suffixIndex = 0;

        while (val >= 1000d && suffixIndex < Suffixes.Length - 1)
        {
            val /= 1000d;
            suffixIndex++;
        }

        string format = decimals == 0 ? "0" : $"F{decimals}";
        string str = val.ToString(format);

        if (decimals > 0 && str.Contains("."))
            str = str.TrimEnd('0').TrimEnd('.');

        return (isNegative ? "-" : "") + str + Suffixes[suffixIndex];
    }
}
