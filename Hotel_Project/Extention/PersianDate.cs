using System.Globalization;

namespace Hotel_Project.Extention
{
    public static class PersianDate
    {
        public static string ToShamsiDate(this DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();

            return pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date).ToString("00");
        }

        public static DateTime ToMiladi(this DateTime date)
        {
            return new DateTime(date.Year , date.Month, date.Day, new PersianCalendar());
        }

    }
}
