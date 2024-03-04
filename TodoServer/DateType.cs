namespace TodoServer
{
    [Serializable]
    public class DateType
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }

        public override string ToString()
        {
            string dayString = day < 10 ? $"0{day}" : day.ToString();
            string monthString = day < 10 ? $"0{month}" : month.ToString();

            return $"{monthString}/{dayString}/{this.year}";
        }

        public static DateType? GetDateTypeFromString(string? date)
        {
            if (date == null) return null;

            DateType dateType = new DateType();
            char[] chars = date.ToCharArray();
            try
            {
                dateType.month = Int32.Parse($"{chars[0]}{chars[1]}");
                dateType.day = Int32.Parse($"{chars[3]}{chars[4]}");
                dateType.year = Int32.Parse($"{chars[6]}{chars[7]}{chars[8]}{chars[9]}");
            }
            catch (Exception exception)
            {
                Logging.LogError($"Couldn't convert string to DateType.\nError: {exception}", "DateTypeConversionError");
            }
            return dateType;
        }
    }
}
