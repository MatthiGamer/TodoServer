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
    }
}
