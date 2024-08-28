using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;
using System;
using System.Threading.Tasks;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using AvaloniaBase=Avalonia;

namespace CosmosExplorer.Avalonia.Behaviours
{
    public class AutoCompleteZeroMinimumPrefixLengthDropdownBehaviour : Behavior<AutoCompleteBox>
    {
        static AutoCompleteZeroMinimumPrefixLengthDropdownBehaviour()
        {
        }

        protected override void OnAttached()
        {
            if (AssociatedObject is not null)
            {
                AssociatedObject.KeyUp += OnKeyUp;
                AssociatedObject.DropDownOpening += DropDownOpening;
                AssociatedObject.GotFocus += OnGotFocus;

                Task.Delay(500).ContinueWith(_ =>
                    AvaloniaBase.Threading.Dispatcher.UIThread.Invoke(() => { CreateDropdownButton(); }));
            }

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject is not null)
            {
                AssociatedObject.KeyUp -= OnKeyUp;
                AssociatedObject.DropDownOpening -= DropDownOpening;
                AssociatedObject.GotFocus -= OnGotFocus;
            }

            base.OnDetaching();
        }

        //have to use KeyUp as AutoCompleteBox eats some of the KeyDown events
        private void OnKeyUp(object? sender, AvaloniaBase.Input.KeyEventArgs e)
        {
            if ((e.Key == AvaloniaBase.Input.Key.Down || e.Key == AvaloniaBase.Input.Key.F4))
            {
                if (string.IsNullOrEmpty(AssociatedObject?.Text))
                {
                    ShowDropdown();
                }
            }
        }

        private void DropDownOpening(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            var prop = AssociatedObject.GetType().GetProperty("TextBox",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var tb = (TextBox?)prop?.GetValue(AssociatedObject);
            if (tb.IsReadOnly)
            {
                e.Cancel = true;
                return;
            }
        }

        private void ShowDropdown()
        {
            if (AssociatedObject is not null && !AssociatedObject.IsDropDownOpen)
            {
                typeof(AutoCompleteBox)
                    .GetMethod("PopulateDropDown",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.Invoke(AssociatedObject, new object[] { AssociatedObject, EventArgs.Empty });
                typeof(AutoCompleteBox)
                    .GetMethod("OpeningDropDown",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                    ?.Invoke(AssociatedObject, new object[] { false });

                if (!AssociatedObject.IsDropDownOpen)
                {
                    //We *must* set the field and not the property as we need to avoid the changed event being raised (which prevents the dropdown opening).
                    var ipc = typeof(AutoCompleteBox).GetField("_ignorePropertyChange",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    if ((bool)ipc?.GetValue(AssociatedObject) == false)
                        ipc?.SetValue(AssociatedObject, true);

                    AssociatedObject.SetCurrentValue<bool>(AutoCompleteBox.IsDropDownOpenProperty, true);
                }
            }
        }

        private void CreateDropdownButton()
        {
            if (AssociatedObject != null)
            {
                var prop = AssociatedObject.GetType().GetProperty("TextBox",
                    System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                var tb = (TextBox?)prop?.GetValue(AssociatedObject);

                var btn = new Border() {
                    MinHeight = 32,
                    MinWidth = 32,
                    MaxHeight = AssociatedObject.FontSize*2,
                    MaxWidth = AssociatedObject.FontSize*2,
                    Cursor = Cursor.Parse("Hand"),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Background = Brushes.Transparent,
                    Padding = new AvaloniaBase.Thickness(4),
                    Child = new Path() {
                        Name="arrow",
                        Width=8,
                        Height=8,
                        Stretch=Stretch.Uniform,
                        HorizontalAlignment=HorizontalAlignment.Right,
                        VerticalAlignment=VerticalAlignment.Center,
                        Data=Geometry.Parse("M7,10L12,15L17,10H7Z"),
                        Fill=tb.Foreground
                    }
                };
                btn.PointerPressed += (s, a) => {
                    AssociatedObject.Text = null;
                    ShowDropdown();
                }; 
                tb.InnerRightContent = btn;
            }
        }

        private void OnGotFocus(object? sender, RoutedEventArgs e)
        {
            if (AssociatedObject != null)
            {
                CreateDropdownButton();
            }
        }
    }

}