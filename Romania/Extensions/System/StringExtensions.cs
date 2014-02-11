namespace Romania.Extensions.System
{
    public static class StringExtensions
    {
        public static string TrimEnd(this string target, string trimString)
        {
            string result = target;
            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }

            return result;
        } 
    }
}