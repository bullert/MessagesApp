using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;
using Prism.Commands;
using MessagesApp.Models;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MessagesApp.ViewModels
{
    class StyleConverter : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FrameworkElement targetElement = values[0] as FrameworkElement;
            string styleName = values[1] as string;

            if (styleName == null)
                return null;

            Style newStyle = (Style)targetElement.TryFindResource(styleName);

            if (newStyle == null)
                return null;

            return newStyle;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    //public class MyStateControl : ScrollViewer
    //{
    //    //public MyStateControl() : base() { }
    //    public Boolean State
    //    {
    //        get { return (Boolean)this.GetValue(StateProperty); }
    //        set { this.SetValue(StateProperty, value); }
    //    }

    //    public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
    //      "State", typeof(Boolean), typeof(MyStateControl), new PropertyMetadata(false));
    //}

    //public class aaa
    //{
    //    public string MyCustomProperty
    //    {
    //        get
    //        {
    //            return (string)GetValue(MyCustomPropertyProperty);
    //        }
    //        set
    //        {
    //            SetValue(MyCustomPropertyProperty, value);
    //        }
    //    }

    //    // Using a DependencyProperty as the backing store for MyCustomProperty.  This enables animation, styling, binding, etc...
    //    public static readonly DependencyProperty MyCustomPropertyProperty =
    //        DependencyProperty.Register("MyCustomProperty", typeof(double), typeof(System.Windows.Controls.ScrollViewer), new UIPropertyMetadata(MyPropertyChangedHandler));


    //    public static void MyPropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
    //    {
    //        // Get instance of current control from sender
    //        // and property value from e.NewValue

    //        // Set public property on TaregtCatalogControl, e.g.
    //        ((System.Windows.Controls.ScrollViewer)sender).VerticalOffset = e.NewValue;
    //    }

    //    // Example public property of control
    //    public double LabelText
    //    {
    //        get { return label1.Content; }
    //        set { label1.Content = value; }
    //    }
    //}

    public static class ScrollViewerBinding
    {
        public static readonly DependencyProperty ScrollableHeightProperty = DependencyProperty.RegisterAttached(
            "ScrollableHeight", typeof(bool), typeof(ScrollViewerBinding),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScrollableHeightPropertyChanged)
        );

        public static bool GetScrollableHeight(DependencyObject depObj)
        {
            return (bool)depObj.GetValue(ScrollableHeightProperty);
        }

        public static void SetScrollableHeight(DependencyObject depObj, bool value)
        {
            depObj.SetValue(ScrollableHeightProperty, value);
        }

        private static void OnScrollableHeightPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollViewer = d as ScrollViewer;
            if (scrollViewer == null)
                return;

            BindVerticalOffset(scrollViewer);
            //SetScrollableHeight(scrollViewer, scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight);
        }


        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
            "VerticalOffset", typeof(double), typeof(ScrollViewerBinding), 
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVerticalOffsetPropertyChanged)
        );
        
        private static readonly DependencyProperty VerticalScrollBindingProperty =
            DependencyProperty.RegisterAttached("VerticalScrollBinding", typeof(bool?), typeof(ScrollViewerBinding));

        public static double GetVerticalOffset(DependencyObject depObj)
        {
            return (double)depObj.GetValue(VerticalOffsetProperty);
        }

        public static void SetVerticalOffset(DependencyObject depObj, double value)
        {
            depObj.SetValue(VerticalOffsetProperty, value);
        }

        private static void OnVerticalOffsetPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ScrollViewer scrollViewer = d as ScrollViewer;
            if (scrollViewer == null)
                return;

            //BindVerticalOffset(scrollViewer);
            //scrollViewer.ScrollToVerticalOffset((double)e.NewValue);
            scrollViewer.ScrollToBottom();
            //SetScrollableHeight(scrollViewer, scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight);
        }

        public static void BindVerticalOffset(ScrollViewer scrollViewer)
        {
            if (scrollViewer.GetValue(VerticalScrollBindingProperty) != null)
                return;

            scrollViewer.SetValue(VerticalScrollBindingProperty, true);
            scrollViewer.ScrollChanged += (s, se) =>
            {
                if (se.VerticalChange == 0)
                    return;

                SetVerticalOffset(scrollViewer, se.VerticalOffset);
                //SetScrollableHeight(scrollViewer, (bool)(se.VerticalOffset == 0));
                SetScrollableHeight(scrollViewer, se.VerticalOffset == scrollViewer.ScrollableHeight);
            };
        }
    }

    public static class ScrollViewerAttachedProperties
    {
        public static void SetScrollToBottomOnChange(DependencyObject element, object value)
        {
            element.SetValue(ScrollToBottomOnChangeProperty, value);
        }

        public static object GetScrollToBottomOnChange(DependencyObject element)
        {
            return element.GetValue(ScrollToBottomOnChangeProperty);
        }

        public static double GetVerticalOffset(DependencyObject element)
        {
            return (double)element.GetValue(VerticalOffsetProperty);
        }

        public static void SetVerticalOffset(DependencyObject element, double value)
        {
            element.SetValue(VerticalOffsetProperty, value);
        }

        public static double GetScrollToVerticalOffset(DependencyObject element)
        {
            return (double)element.GetValue(ScrollToVerticalOffsetProperty);
        }

        public static void SetScrollToVerticalOffset(DependencyObject element, double value)
        {
            element.SetValue(ScrollToVerticalOffsetProperty, value);
        }

        public static readonly DependencyProperty ScrollToBottomOnChangeProperty = DependencyProperty.RegisterAttached(
            "ScrollToBottomOnChange", typeof(object), typeof(ScrollViewerAttachedProperties), 
            new PropertyMetadata(default(System.Windows.Controls.ScrollViewer), OnScrollToBottomOnChangeChanged)
        );

        //public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
        //    "VerticalOffset", typeof(double), typeof(ScrollViewerAttachedProperties), new FrameworkPropertyMetadata(default(double),
        //    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnVerticalOffsetPropertyChanged))
        //);
        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
            "VerticalOffset", typeof(double), typeof(ScrollViewerAttachedProperties), new FrameworkPropertyMetadata(default(double), OnVerticalOffsetPropertyChanged)
        );

        public static readonly DependencyProperty ScrollToVerticalOffsetProperty = DependencyProperty.RegisterAttached(
            "ScrollToVerticalOffset", typeof(double), typeof(ScrollViewerAttachedProperties), new FrameworkPropertyMetadata(default(double), OnScrollToVerticalOffsetPropertyChanged)
        );

        private static void OnScrollToBottomOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            scrollViewer?.ScrollToBottom();
            //scrollViewer?.ScrollToVerticalOffset((double)e.NewValue);
            //VerticalOffset = scrollViewer.VerticalOffset;
        }

        private static void OnVerticalOffsetPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            scrollViewer?.ScrollToBottom();
            //scrollViewer?.ScrollToVerticalOffset((double)e.NewValue);
            //VerticalOffset = scrollViewer.VerticalOffset;
        }

        private static void OnScrollToVerticalOffsetPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            scrollViewer?.ScrollToVerticalOffset((double)e.NewValue);
            //VerticalOffset = scrollViewer.VerticalOffset;
        }

        //public static double VerticalChange { get; set; }

        public static double VerticalOffset { get; set; }
        //public static double LastVerticalOffsett { get; set; }
    }

    public class ConversationViewModel : BindableBase
    {
        //private Conversation conversation;
        private User user;
        private User user2;
        private User user3;
        int index = 0;

        public ConversationViewModel()
        {
            SendMessageCmd = new DelegateCommand(OnSendMessage, CanSendMessage).ObservesProperty(() => MessageContent);
            PrependMessageCmd = new DelegateCommand(OnPrependMessage, () => true);
            SendMessageCmd2 = new DelegateCommand(OnSendMessage2);
            ScrollCmd = new DelegateCommand(OnScroll, CanScroll).ObservesProperty(() => Val2);

            user = new User
            {
                FirstName = "Bartek"
            };
            user2 = new User
            {
                FirstName = "Sasha",
                IsOnline = true
            };
            user3 = new User
            {
                FirstName = "Jane"
            };

            conversation = new Conversation();
            conversation.user = user;
            
            for (int i = 0; i < 100; i++)
            {
                conversation.AddMessage(new Message { Author = user3, Text = i.ToString() });
            }

            conversation.AddMessage(new Message { Author = user, Text = "a" });
            conversation.AddMessage(new Message { Author = user2, Text = "bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb" });
            conversation.AddMessage(new Message { Author = user, Text = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" });
            conversation.AddMessage(new Message { Author = user2, Text = "J 0000" });
            conversation.AddMessage(new Message { Author = user2, Text = "J 1111 afsdfsdfs" });
            conversation.AddMessage(new Message { Author = user2, Text = "22" });
            conversation.AddMessage(new Message { Author = user2, Text = "3" });
            conversation.AddMessage(new Message { Author = user, Text = "cccccccccccccc" });
            conversation.AddMessage(new Message { Author = user, Text = "d" });
            conversation.AddMessage(new Message { Author = user, Text = "ee" });
            //Conversation = conversation;            

            //RaisePropertyChanged("Reset");
        }

        public DelegateCommand SendMessageCmd { get; set; }
        public DelegateCommand SendMessageCmd2 { get; set; }
        public DelegateCommand PrependMessageCmd { get; set; }
        public DelegateCommand ScrollCmd { get; set; }

        private double val;
        public double Val
        {
            get => val;
            set
            {
                SetProperty(ref val, value);
                //RaisePropertyChanged("Val");
            }
        }

        private bool val2 = false;
        public bool Val2
        {
            get => val2;
            set
            {
                SetProperty(ref val2, value);
                //RaisePropertyChanged("Val");
            }
        }

        //public double Val
        //{
        //    get => ScrollViewerAttachedProperties.VerticalOffset;
        //    set {
        //        ScrollViewerAttachedProperties.VerticalOffset = value;
        //        RaisePropertyChanged("Val");
        //    }
        //}

        private bool _reset;
        public bool Reset
        {
            get => _reset;
            set => SetProperty(ref _reset, value);
        }

        private double _reset2 = 500;
        public double Reset2
        {
            get => _reset2;
            set => SetProperty(ref _reset2, value);
        }

        private string messageContent;
        public string MessageContent
        {
            get => messageContent;
            set => SetProperty(ref messageContent, value);
        }

        //private double verticalOffset;
        //public double VerticalOffset
        //{
        //    get => verticalOffset;
        //    set
        //    {
        //        SetProperty(ref verticalOffset, value);
        //        RaisePropertyChanged("VerticalOffset");
        //    }
        //}

        //public double VerticalOffset
        //{
        //    get { return ScrollViewerAttachedProperties.VerticalOffset; }
        //    set
        //    {
        //        ScrollViewerAttachedProperties.VerticalOffset = value;
        //        //RaisePropertyChanged("Reset");
        //    }
        //}

        private Conversation conversation;
        public Conversation Conversation
        {
            get => conversation;
            set => SetProperty(ref conversation, value);
        }

        private void OnScroll()
        {
            //Val = ScrollViewerAttachedProperties.VerticalOffset;
            //RaisePropertyChanged("Reset");
            //Val = 1000;
            Val2 = false;
            
            for (int i = 0; i < 16; i++)
            {
                conversation.PrependMessage(new Message { Author = user, Text = (index++).ToString() });
            }
        }

        private bool CanScroll()
        {
            return Val2;
        }

        private void OnSendMessage()
        {
            conversation.AddMessage(new Message { Author = user, Text = MessageContent });

            Val = 0;
            //RaisePropertyChanged("Reset");
            //RaisePropertyChanged("VerticalOffset");

            //Conversation = conversation;
            //Conversation.Messages = Conversation.messagesList;
            //MessageBox.Show(Conversation.Messages[Conversation.Messages.Count - 1].Style.ToString());
        }

        private bool CanSendMessage()
        {
            return true;
        }

        private void OnPrependMessage()
        {
            conversation.PrependMessage(new Message { Author = user, Text = "fsdfsdf" });
            //Val2 = false;
            //if (Val == 0)
            //{
            //    Val = 100;
            //    RaisePropertyChanged("Val");
            //}
        }

        private void OnSendMessage2()
        {
            //RaisePropertyChanged("Reset");
            //VerticalOffset = 500;
            //RaisePropertyChanged("Reset");
            
            //double last = ScrollViewerAttachedProperties.LastVerticalOffsett;
            //ScrollViewerAttachedProperties.VerticalOffsett = 500;
            //VerticalOffset = 500;

            //if (VerticalOffset == 0)
            //{
            //    conversation.PrependMessage(new Message { Author = user, Text = "fsdfsdf" });
            //}
            //MessageBox.Show("Sfsfsf");
        }
    }
}
