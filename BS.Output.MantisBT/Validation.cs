using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BS.Output.MantisBT
{
  class Validation
  {

    public static readonly DependencyProperty RequiredProperty = DependencyProperty.RegisterAttached("Required", typeof(Boolean), typeof(Validation), new FrameworkPropertyMetadata(OnRequiredChanged));

    public static void SetRequired(DependencyObject element, Boolean value)
    {
      element.SetValue(RequiredProperty, value);
    }

    public static Boolean GetRequired(DependencyObject element)
    {
      return (Boolean)element.GetValue(RequiredProperty);
    }

    public static void OnRequiredChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
    {
      if ((bool)args.NewValue == true)
      {

        if (obj is TextBox)
        {
          TextBox textbox = (TextBox)obj;
          textbox.Loaded += new RoutedEventHandler(Element_Validate);
          textbox.TextChanged += new TextChangedEventHandler(Element_Validate);
          textbox.IsVisibleChanged += new DependencyPropertyChangedEventHandler(Element_Validate);
          Validate(textbox);
        }
        else if (obj is ComboBox)
        {
          ComboBox combobox = (ComboBox)obj;
          combobox.Loaded += new RoutedEventHandler(Element_Validate);
          combobox.SelectionChanged += new SelectionChangedEventHandler(Element_Validate);
          combobox.IsVisibleChanged += new DependencyPropertyChangedEventHandler(Element_Validate);
          Validate(combobox);
        }
        else
        {
          throw new NotSupportedException(string.Format("Type {0} not supported", obj.GetType().ToString()));
        }

      }
    }

    private static void ShowValidationBorder(UIElement element, bool show)
    {

      AdornerLayer layer = AdornerLayer.GetAdornerLayer(element);

      // Element not jet loaded
      if (layer is null) return;

      ValidationBorder adorner = (ValidationBorder)layer.GetAdorners(element)?.First();

      if (adorner is null)
      {
        adorner = new ValidationBorder(element);
        AdornerLayer.GetAdornerLayer(element).Add(adorner);
      }

      if (element.IsVisible && show)
      {
        adorner.Visibility = Visibility.Visible;
      }
      else
      {
        adorner.Visibility = Visibility.Collapsed;
      }

    }

    private static void Element_Validate(object sender, EventArgs e)
    {
      Validate((UIElement)sender);
    }

    private static void Element_Validate(object sender, DependencyPropertyChangedEventArgs e)
    {
      Validate((UIElement)sender);
    }

    private static void Validate(UIElement element)
    {
      ShowValidationBorder(element, !IsValid(element));
    }

    public static bool IsValid(UIElement element)
    {

      if (element is TextBox)
      {
        TextBox textbox = (TextBox)element;
        return (!(bool)textbox.GetValue(Validation.RequiredProperty) || !string.IsNullOrEmpty(textbox.Text));
      }
      else if (element is ComboBox)
      {
        ComboBox combobox = (ComboBox)element;
        return (!(bool)combobox.GetValue(Validation.RequiredProperty) || !(combobox.SelectedValue is null));
      }
      else
      {
        throw new NotSupportedException(string.Format("Type {0} not supported", element.GetType().ToString()));
      }

    }

    private class ValidationBorder : Adorner
    {

      Pen pen;

      public ValidationBorder(UIElement adornedElement) :
        base(adornedElement)
      {
        pen = new Pen();
        pen.Thickness = 1;
        pen.Brush = Brushes.Red;
      }

      protected override void OnRender(DrawingContext drawingContext)
      {
        drawingContext.DrawRectangle(null, pen, new Rect(0, 0, ActualWidth, ActualHeight));
      }

    }

  }
}
