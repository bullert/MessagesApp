using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessagesApp.Models;
using Prism.Mvvm;
using Prism.Commands;
using System.Collections.ObjectModel;

namespace MessagesApp.ViewModels
{
    public static class AttachedProperty
    {
        public static double GetHeight(DependencyObject element)
        {
            return (double)element.GetValue(HeightOnChangeProperty);
        }

        public static void SetHeight(DependencyObject element, double value)
        {
            element.SetValue(HeightOnChangeProperty, value);
        }

        public static readonly DependencyProperty HeightOnChangeProperty = DependencyProperty.RegisterAttached(
            "Height", typeof(double), typeof(AttachedProperty),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnHeightPropertyChanged)
        );

        private static void OnHeightPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ListViewItem;

            scrollViewer.SizeChanged += (s, se) =>
            {
                SetHeight(scrollViewer, scrollViewer.ActualHeight);
            };
        }
    }
    
    public class MessageViewModel : BindableBase
    {
        public MessageViewModel()
        {
            //Author = new User();
            //Date = new DateTime(2019, 08, 04, 1, 0, 0);
            //Author.FirstName = "Sasha";
            //Author.IsOnline = true;
            //MessageTemplate = "LeftMessageTemplate";
            CreatedDate = DateTimeToString(Date);

            SeenBy = new ObservableCollection<SeenByItem>();

            //Cmd = new DelegateCommand(OnCmd, () => true);
        }

        public DelegateCommand Cmd { get; set; }

        private ObservableCollection<SeenByItem> seenBy;
        public ObservableCollection<SeenByItem> SeenBy
        {
            get => seenBy;
            private set => SetProperty(ref seenBy, value);
        }

        private User author;
        public User Author
        {
            get => author;
            set => SetProperty(ref author, value);
        }

        private string text;
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        private double text2;
        public double Text2
        {
            get => text2;
            set => SetProperty(ref text2, value);
        }

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                SetProperty(ref date, value);
                CreatedDate = DateTimeToString(Date);
                RaisePropertyChanged("CreatedDate");
            }
        }

        private string createdDate;
        public string CreatedDate
        {
            get => createdDate;
            set => SetProperty(ref createdDate, value);
        }

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
        
        private bool isLast;
        public bool IsLast
        {
            get => isLast;
            set => SetProperty(ref isLast, value);
        }

        private bool isFirst;
        public bool IsFirst
        {
            get => isFirst;
            set => SetProperty(ref isFirst, value);
        }

        private string messageTemplate;
        public string MessageTemplate
        {
            get => messageTemplate;
            set { SetProperty(ref messageTemplate, value); RaisePropertyChanged("Text2"); }
        }

        //private string style;
        //public string Style
        //{
        //    get => style;
        //    set => SetProperty(ref style, value);
        //}

        public void SetMessageTemplate(string template)
        {
            MessageTemplate = template;
        }

        //private void OnCmd()
        //{
        //    MessageBox.Show("dfafasdf");
        //}
    }
}
