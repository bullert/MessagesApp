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

namespace MessagesApp.Controls
{
    /// <summary>
    /// Wykonaj kroki 1a lub 1b, a następnie 2, aby użyć tej kontrolki niestandardowej w pliku XAML.
    ///
    /// Krok 1a) Użycie tej kontrolki niestandardowej w pliku XAML, który istnieje w bieżącym projekcie.
    /// Dodaj ten atrybut XmlNamespace do głównego elementu pliku znaczników, gdzie jest 
    /// do użycia:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MessagesApp.Controls"
    ///
    ///
    /// Krok 1b) Użycie tej kontrolki niestandardowej w pliku XAML, który istnieje w innym projekcie.
    /// Dodaj ten atrybut XmlNamespace do głównego elementu pliku znaczników, gdzie jest 
    /// do użycia:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MessagesApp.Controls;assembly=MessagesApp.Controls"
    ///
    /// Należy również dodać odwołanie do projektu z projektu, w którym znajduje się plik XAML
    /// do tego projektu i skompiluj ponownie, aby uniknąć błędów kompilacji:
    ///
    ///     Kliknij prawym przyciskiem myszy docelowy projekt w Eksploratorze rozwiązań i
    ///     „Dodaj odwołanie”->„Projekty”->[Wyszukaj i wybierz ten projekt]
    ///
    ///
    /// Krok 2)
    /// Przejdź dalej i użyj swojego formantu w pliku XAML.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class CustomControl1 : ScrollViewer
    {
        static CustomControl1()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomControl1), new FrameworkPropertyMetadata(typeof(CustomControl1)));
            //EventManager.RegisterRoutedEvent("MouseWheel", RoutingStrategy.Bubble, typeof(MouseWheelEventHandler), typeof(CustomControl1));
        }

        public static readonly RoutedEvent OnMouseWheelEvent =
            EventManager.RegisterRoutedEvent("MouseWheel", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(CustomControl1));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.MouseWheel += OnMouseWheel;
        }

        public event RoutedEventHandler OnMouseWheelHandler
        {
            add { AddHandler(OnMouseWheelEvent, value); }
            remove { RemoveHandler(OnMouseWheelEvent, value); }
        }

        private void OnMouseWheel(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnMouseWheelEvent));
        }

        private void OnPreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            //scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            scv.ScrollToVerticalOffset(100);
            e.Handled = true;
        }
    }
}
