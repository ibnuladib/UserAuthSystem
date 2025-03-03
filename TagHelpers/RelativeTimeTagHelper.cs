using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Task4_UserAuth 
{
    public static class DateTimeExtensions
    {
        public static string ToRelativeTime(this DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalMinutes < 1)
                return "just now";
            if (timeSpan.TotalHours < 1)
                return $"{(int)timeSpan.TotalMinutes} minutes ago";
            if (timeSpan.TotalDays < 1)
                return $"{(int)timeSpan.TotalHours} hours ago";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} days ago";

            return dateTime.ToString("MMMM dd, yyyy");
        }
    }
}