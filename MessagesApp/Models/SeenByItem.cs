using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace MessagesApp.Models
{
    public class SeenByItem : BindableBase
    {
        public SeenByItem()
        {
            CreationDate = DateTime.Now;
        }
        
        private User user;
        public User User
        {
            get => user;
            set
            {
                SetProperty(ref user, value);
                SeenAt = $"Seen by {User.FirstName} at {DateTimeToString(CreationDate)}";
            }
        }

        private DateTime creationDate;
        public DateTime CreationDate
        {
            get => creationDate;
            set
            {
                SetProperty(ref creationDate, value);
                SeenAt = $"Seen by {User} at {DateTimeToString(CreationDate)}";
            }
        }

        public string SeenAt { get; set; }

        private string DateTimeToString(DateTime date)
        {
            //var usCulture = new System.Globalization.CultureInfo("pl-PL");
            //var usCulture = System.Globalization.CultureInfo.CreateSpecificCulture("de-DE");

            var cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            int dayDiff = (int)((DateTime.Now - date).TotalDays);

            string pattern;
            string timePattern = cultureInfo.DateTimeFormat.ShortTimePattern.ToString();
            string datePattern = cultureInfo.DateTimeFormat.LongDatePattern.ToString().Replace("dddd, ", null).Replace("MMMM", "MMM");

            if (dayDiff == 0) pattern = $"{timePattern}";
            else if (dayDiff < 7 && dayDiff > 0) pattern = $"ddd, {timePattern}";
            else pattern = $"{datePattern}, {timePattern}";

            return date.ToString(pattern, cultureInfo);
            //SetProperty(ref createdDate, Date.ToString(pattern, cultureInfo));

            //return Date.ToString($"{datePattern}, {timePattern}", usCulture);
            //return Date.ToString($"ddd, {timePattern}", usCulture);
        }
    }
}
