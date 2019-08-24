using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessagesApp
{
    public static class Attr
    {
        public static void SetMouseWheelDownOnChange(DependencyObject element, object value)
        {
            element.SetValue(MouseWheelDownOnChangeProperty, value);
        }
        public static object GetMouseWheelDownOnChange(DependencyObject element)
        {
            return element.GetValue(MouseWheelDownOnChangeProperty);
        }
        public static readonly DependencyProperty MouseWheelDownOnChangeProperty = DependencyProperty.RegisterAttached(
            "MouseWheelDownOnChange", typeof(object), typeof(Attr),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMouseWheelDownOnChangeChanged)
        );
        private static void OnMouseWheelDownOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ScrollDown(dependencyObject as ScrollViewer, 48);
        }


        public static void SetMouseWheelUpOnChange(DependencyObject element, object value)
        {
            element.SetValue(MouseWheelUpOnChangeProperty, value);
        }
        public static object GetMouseWheelUpOnChange(DependencyObject element)
        {
            return element.GetValue(MouseWheelUpOnChangeProperty);
        }
        public static readonly DependencyProperty MouseWheelUpOnChangeProperty = DependencyProperty.RegisterAttached(
            "MouseWheelUpOnChange", typeof(object), typeof(Attr),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnMouseWheelUpOnChangeChanged)
        );
        private static void OnMouseWheelUpOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ScrollUp(dependencyObject as ScrollViewer, 48);
        }


        public static void SetArrowDownPressedOnChange(DependencyObject element, object value)
        {
            element.SetValue(ArrowDownPressedOnChangeProperty, value);
        }
        public static object GetArrowDownPressedOnChange(DependencyObject element)
        {
            return element.GetValue(ArrowDownPressedOnChangeProperty);
        }
        public static readonly DependencyProperty ArrowDownPressedOnChangeProperty = DependencyProperty.RegisterAttached(
            "ArrowDownPressedOnChange", typeof(object), typeof(Attr),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnArrowDownPressedOnChangeChanged)
        );
        private static void OnArrowDownPressedOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            ScrollDown(dependencyObject as ScrollViewer, 16);
        }


        public static void SetArrowUpPressedOnChange(DependencyObject element, object value)
        {
            element.SetValue(ArrowUpPressedOnChangeProperty, value);
        }
        public static object GetArrowUpPressedOnChange(DependencyObject element)
        {
            return element.GetValue(ArrowUpPressedOnChangeProperty);
        }
        public static readonly DependencyProperty ArrowUpPressedOnChangeProperty = DependencyProperty.RegisterAttached(
            "ArrowUpPressedOnChange", typeof(object), typeof(Attr),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnArrowUpPressedOnChangeChanged)
        );
        private static void OnArrowUpPressedOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            //System.Windows.MessageBox.Show("sdfasf");
            ScrollUp(dependencyObject as ScrollViewer, 16);
        }


        private static void ScrollUp(ScrollViewer scrollViewer, double offset)
        {
            double scrollTo = scrollViewer.VerticalOffset + offset;

            if (scrollTo > scrollViewer.ScrollableHeight) scrollTo = scrollViewer.ScrollableHeight;
            scrollViewer.ScrollToVerticalOffset(scrollTo);
        }

        private static void ScrollDown(ScrollViewer scrollViewer, double offset)
        {
            double scrollTo = scrollViewer.VerticalOffset - offset;

            if (scrollTo < 0) scrollTo = 0;
            scrollViewer.ScrollToVerticalOffset(scrollTo);
        }


        public static void SetScrollToOffsetOnChange(DependencyObject element, object value)
        {
            element.SetValue(ScrollToOffsetOnChangeProperty, value);
        }
        public static object GetScrollToOffsetOnChange(DependencyObject element)
        {
            return element.GetValue(ScrollToOffsetOnChangeProperty);
        }

        public static readonly DependencyProperty ScrollToOffsetOnChangeProperty = DependencyProperty.RegisterAttached(
            "ScrollToOffsetOnChange", typeof(object), typeof(Attr),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScrollToOffsetOnChangeChanged)
        );

        private static void OnScrollToOffsetOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;

            double a = GetVerticalOffset(scrollViewer);
            //double a = Offset3;
            //MessageBox.Show(a.ToString());
            //if (a.Equals(double.NaN)) return;
            //MessageBox.Show(a.ToString());
            //if (a < 0)
            //{
            //    a = 0;
            //    SetVerticalOffset(scrollViewer, a);
            //}
            //else if (a > scrollViewer.ExtentHeight - scrollViewer.ViewportHeight)
            //{
            //    a = scrollViewer.ExtentHeight;
            //    SetVerticalOffset(scrollViewer, a);
            //}
            scrollViewer.ScrollToVerticalOffset(a);
        }


        public static void SetScrollToBottomOnChange(DependencyObject element, object value)
        {
            element.SetValue(ScrollToBottomOnChangeProperty, value);
        }
        public static object GetScrollToBottomOnChange(DependencyObject element)
        {
            return element.GetValue(ScrollToBottomOnChangeProperty);
        }

        public static readonly DependencyProperty ScrollToBottomOnChangeProperty = DependencyProperty.RegisterAttached(
            "ScrollToBottomOnChange", typeof(object), typeof(Attr),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScrollToBottomOnChangeChanged)
        );

        private static void OnScrollToBottomOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            scrollViewer?.ScrollToTop();
        }


        public static void SetScrollToBottomValueOnChange(DependencyObject element, double value)
        {
            element.SetValue(ScrollToBottomValueOnChangeProperty, value);
        }
        public static double GetScrollToBottomValueOnChange(DependencyObject element)
        {
            return (double)element.GetValue(ScrollToBottomValueOnChangeProperty);
        }

        public static readonly DependencyProperty ScrollToBottomValueOnChangeProperty = DependencyProperty.RegisterAttached(
            "ScrollToBottomValueOnChange", typeof(double), typeof(Attr),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnScrollToBottomValueOnChangeChanged)
        );

        private static void OnScrollToBottomValueOnChangeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            //SetScrollToBottomValueOnChange(scrollViewer, scrollViewer.ScrollableHeight);
            //MessageBox.Show(GetScrollToBottomValueOnChange(scrollViewer).ToString() + "a");
            //scrollViewer?.ScrollToBottom();
        }



        public static double GetVerticalOffset(DependencyObject depObj)
        {
            return (double)depObj.GetValue(VerticalOffsetProperty);
        }

        public static void SetVerticalOffset(DependencyObject depObj, double value)
        {
            depObj.SetValue(VerticalOffsetProperty, value);
        }

        public static readonly DependencyProperty VerticalOffsetProperty = DependencyProperty.RegisterAttached(
            "VerticalOffset", typeof(double), typeof(Attr),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVerticalOffsetPropertyChanged)
        );

        private static void OnVerticalOffsetPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ScrollViewer;
            
            scrollViewer.ScrollChanged += (s, se) =>
            {
                if (se.VerticalChange == 0) return;
                //SetVerticalOffset(scrollViewer, se.VerticalOffset);
                SetVerticalOffset(scrollViewer, se.VerticalOffset + se.ViewportHeight);
                //SetScrollToBottomValueOnChange(scrollViewer, se.ExtentHeight - se.ViewportHeight);
            };
        }


        public static double GetVerticalOffset2(DependencyObject depObj)
        {
            return (double)depObj.GetValue(VerticalOffsetProperty2);
        }

        public static void SetVerticalOffset2(DependencyObject depObj, double value)
        {
            depObj.SetValue(VerticalOffsetProperty2, value);
        }

        public static readonly DependencyProperty VerticalOffsetProperty2 = DependencyProperty.RegisterAttached(
            "VerticalOffset2", typeof(double), typeof(Attr),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnVerticalOffsetPropertyChanged2)
        );

        private static void OnVerticalOffsetPropertyChanged2(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = dependencyObject as System.Windows.Controls.ListView;

            scrollViewer.SizeChanged += (s, se) =>
            {
                if (se.NewSize.Height - se.PreviousSize.Height == 0) return;

                SetVerticalOffset2(scrollViewer, se.NewSize.Height);
                //MessageBox.Show("fadfsf");
            };
        }
    }

    public partial class UserControl1 : UserControl
    {

        public UserControl1()
        {
            InitializeComponent();
        }

        private void OnScroll(object sender, ScrollChangedEventArgs e)
        {

        }
    }
}
