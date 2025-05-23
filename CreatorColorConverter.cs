using System.Globalization;
using Postgres.Models;

namespace AutomobileRegisty__kursovaya_;

public class CreatorColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int creatorId)
        {
            if (parameter is User currentUser)
            {
                return creatorId == currentUser.Id ? Color.FromRgba(0.9, 0.9, 1, 0.7) : Colors.Transparent;
            }
        }

        return Colors.Transparent;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
} 