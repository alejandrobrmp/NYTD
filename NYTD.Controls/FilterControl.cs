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

namespace NYTD.Controls
{
    public class FilterControl : Control
    {
        public static readonly DependencyProperty BarProperty =
                DependencyProperty.Register("Bar", typeof(object), typeof(FilterControl),
                    new FrameworkPropertyMetadata(null,
                        FrameworkPropertyMetadataOptions.AffectsRender |
                        FrameworkPropertyMetadataOptions.AffectsMeasure));

        public object Bar
        {
            get { return (object)GetValue(BarProperty); }
            set { SetValue(BarProperty, value); }
        }

        public static readonly DependencyProperty FilterContentProperty =
                DependencyProperty.Register("FilterContent", typeof(object), typeof(FilterControl),
                    new FrameworkPropertyMetadata(null,
                        FrameworkPropertyMetadataOptions.AffectsRender |
                        FrameworkPropertyMetadataOptions.AffectsMeasure));

        public object FilterContent
        {
            get { return (object)GetValue(FilterContentProperty); }
            set { SetValue(FilterContentProperty, value); }
        }

        public static readonly DependencyProperty IsExpandedProperty =
                DependencyProperty.Register("IsExpanded", typeof(bool), typeof(FilterControl),
                    new FrameworkPropertyMetadata(false,
                        FrameworkPropertyMetadataOptions.AffectsRender |
                        FrameworkPropertyMetadataOptions.AffectsMeasure));

        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        public static readonly DependencyProperty ExpandSizeProperty =
                DependencyProperty.Register("ExpandSize", typeof(double), typeof(FilterControl),
                    new FrameworkPropertyMetadata(0d,
                        FrameworkPropertyMetadataOptions.AffectsRender |
                        FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double ExpandSize
        {
            get { return (double)GetValue(ExpandSizeProperty); }
            set { SetValue(ExpandSizeProperty, value); }
        }



        static FilterControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FilterControl), new FrameworkPropertyMetadata(typeof(FilterControl)));
        }
    }
}
