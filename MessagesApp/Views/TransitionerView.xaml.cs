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

namespace MessagesApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy TransitionerView.xaml
    /// </summary>
    public partial class TransitionerView : UserControl
    {
        //private double wheelscrolllines = System.Windows.SystemParameters.WheelScrollLines;
        private double bottomOffset = 0;

        public TransitionerView()
        {
            InitializeComponent();
        }

        //public static void SetBottomOffsetOnChangeProperty(DependencyObject element, double value)
        //{
        //    element.SetValue(BottomOffsetOnChangeProperty, value);
        //}
        //public static double GetBottomOffsetOnChangeProperty(DependencyObject element)
        //{
        //    return (double)element.GetValue(BottomOffsetOnChangeProperty);
        //}

        //public static readonly DependencyProperty BottomOffsetOnChangeProperty = DependencyProperty.RegisterAttached(
        //    "BottomOffsetOnChange", typeof(double), typeof(System.Windows.Controls.ScrollViewer)
        //);

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //var scrollViewer = sender as System.Windows.Controls.ScrollViewer;
            //var ofs = scrollViewer.ScrollableHeight - bottomOffset;

            ////if (scrollViewer.VerticalOffset == 0) scrollViewer.ScrollToVerticalOffset(ofs);

            //bottomOffset = scrollViewer.ScrollableHeight - scrollViewer.VerticalOffset;

            //var a = e.HorizontalChange;
            //if (a > 0) MessageBox.Show(a.ToString());
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            //scrollViewer.HorizontalOffset += 5; 
        }

        //private void OnScroll(object sender, ScrollChangedEventArgs e)
        //{
        //    MessageBox.Show("dfsafaf");
        //}

        //private void OnPreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        //{
        //    ScrollViewer scv = (ScrollViewer)sender;
        //    scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / wheelscrolllines);
        //    e.Handled = true;
        //}
    }
}
