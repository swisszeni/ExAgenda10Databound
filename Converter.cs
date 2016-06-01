using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ExAgenda10DataboundMultiwindow
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool visiblity = (bool)value;
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return ((Visibility)value == Visibility.Visible);
        }
    }

    public class PriorityToBrushConverter : IValueConverter
    {
        public Brush NormalBrush { get; set; }
        public Brush HighBrush { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((Priority)value)
            {
                case Priority.Normal: return NormalBrush;
                case Priority.High: return HighBrush;
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class PriorityToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((Priority)value)
            {
                case Priority.Normal: return Visibility.Visible;
                case Priority.High: return Visibility.Visible;
                default: return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class CategoryToBrushConverter : IValueConverter
    {
        public Brush DefaultBrush { get; set; }
        public Brush BusinessBrush { get; set; }
        public Brush PrivateBrush { get; set; }
        public Brush EducationBrush { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((Category)value)
            {
                case Category.Business: return BusinessBrush;
                case Category.Private: return PrivateBrush;
                case Category.Education: return EducationBrush;
                default: return DefaultBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class CategoryToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch ((Category)value)
            {
                case Category.Business: return "Business";
                case Category.Private: return "Private";
                case Category.Education: return "Education";
                default: return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeToRowConverter : IValueConverter
    {
        public int MinutesPerRow { get; set; }

        public int Offset { get; set; }
         
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Int32)
                return (int)value / MinutesPerRow + Offset;

            if (value is DateTime)
                return (((DateTime)value).Hour * 60 + ((DateTime)value).Minute) / MinutesPerRow + Offset;

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class DurationToRowSpanConverter : IValueConverter
    {
        public int MinutesPerRow { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Int32)
                return (int)value / MinutesPerRow;

            return 0.0f;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeToWeekdayConverter : IValueConverter
    {
        public int Offset { get; set; }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return ((int)((DateTime)value).DayOfWeek + 6) % 7 + Offset;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
