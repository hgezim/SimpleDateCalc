using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleDateCalcApp.Shared
{
    /// <summary>
    /// A simple date class to represent dates.
    /// Supports years from year 1 and other valid dates including leap years. Does not support timezone data.
    /// </summary>
    public class SimpleDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        private Dictionary<int, int> MonthLengths = new Dictionary<int,int> {
            {1,31},
            {2,28},
            {3,31},
            {4,30},
            {5,31},
            {6,30},
            {7,31},
            {8,31},
            {9,30},
            {10,31},
            {11,30},
            {12,31},
        };

        /// <summary>
        /// Create SimpleDate from given string.
        /// </summary>
        /// <param name="dateString">String reprsentaion of the date: Format: yyyy/mm/dd</param>
        public SimpleDate(string dateString)
        {
            String[] dateBits = dateString.Split('/');

            if (dateBits.Count() < 3)
            {
                throw new ArgumentException("Cannot initialize object with given argument. Format must be yyyy/mm/dd");
            }

            int year, month, day;
            bool yearParsed, monthParsed, dayParsed;

            /*
             * Attempt to parse year, month and date from strings. Throw exception if uncessful.
            */
            yearParsed = int.TryParse(dateBits[0], out year);
            monthParsed = int.TryParse(dateBits[1], out month);
            dayParsed = int.TryParse(dateBits[2], out day);
            if (!yearParsed)
            {
                throw new ArgumentException("Could not parse year.");
            }
            if (!monthParsed)
            {
                throw new ArgumentException("Could not parse month.");
            }
            if (!dayParsed)
            {
                throw new ArgumentException("Could not parse day.");
            }

            // Set property values
            this.Year = year;
            this.Month = month;
            this.Day = day;

            // Validate to ensure that year, month, and day make sense.
            this.ValidateYear();
            this.ValidateMonth();
            this.ValidateDay();

        }

        /// <summary>
        /// Validate the year to ensure greater than 0. Throw ArgumentException if it's not.
        /// </summary>
        private void ValidateYear()
        {
            if (this.Year < 1)
            {
                throw new ArgumentException("Year must be at least 1.");
            }
        }

        /// <summary>
        /// Validate the month to ensure it's between 1-12. Throw ArgumentException if it's not.
        /// </summary>
        private void ValidateMonth(){
            if (this.Month < 1 || this.Month > 12)
            {
                throw new ArgumentException("Month must be a value between 1-12.");
            }
        }

        /// <summary>
        /// Validate day to ensure it exists in that month.
        /// </summary>
        private void ValidateDay()
        {
            // Fix valid day data for leap year.
            if (this.IsLeapYear())
                MonthLengths[2] = 29;

            int lastDayOfMonth = MonthLengths[this.Month];
            if (this.Day > lastDayOfMonth || this.Day < 1)
            {
                throw new ArgumentException(String.Format("Day must be a value between {0}-{1}.", 1, lastDayOfMonth));
            }
        }

        /// <summary>
        /// Deteremine if year is a leap year or not.
        /// </summary>
        /// <returns>True if year is a leap year, false otherwise.</returns>
        private bool IsLeapYear()
        {
            return this.Year % 4 == 0;
        }

        /// <summary>
        /// Subtracton operator. Gives the difference between two dates.
        /// </summary>
        /// <param name="dateA">Smaller date.</param>
        /// <param name="dateB">Larger date.</param>
        /// <returns>Now many days the larger date is apart from smaller date.</returns>
        public static int operator -(SimpleDate dateA, SimpleDate dateB)
        {
            int dayB=dateB.Day, dayA=dateA.Day, monthB=dateB.Month, monthA=dateA.Month, yearB=dateB.Year, yearA=dateA.Year;

            int dayDiff = 0, monthDiff = 0, yearDiff = 0;

            if (dayB < dayA)
            {
                monthB--;
                dayB = dayB + dateA.MonthLengths[monthB];
                dayDiff = dayB - dayA;
            }
            else
            {
                dayDiff = dayB - dayA;
            }

            if (monthB < monthA)
            {
                yearB--;
                monthB = monthB + 12;
                monthDiff = monthB - monthA;

                int monthAdded = 0;
                int monthIndex = monthA + 1;
                while (monthAdded < monthDiff)
                {
                    if (monthIndex > 12)
                        monthIndex = 1;

                    dayDiff += dateB.MonthLengths[monthIndex];

                    monthAdded++;
                    monthIndex++;
                }
            }

            if (yearB > yearA)
            {
                yearDiff = yearB - yearA;
                dayDiff += yearDiff * 365;
            }

            return dayDiff;
        }
    }
}