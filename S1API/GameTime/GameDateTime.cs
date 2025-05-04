#if (IL2CPPMELON || IL2CPPBEPINEX)
using S1GameDateTime = Il2CppScheduleOne.GameTime.GameDateTime;
using S1TimeManager = Il2CppScheduleOne.GameTime.TimeManager;
using S1GameDateTimeData = Il2CppScheduleOne.Persistence.Datas.GameDateTimeData;
#elif (MONOMELON || MONOBEPINEX || IL2CPPBEPINEX)
using S1GameDateTime = ScheduleOne.GameTime.GameDateTime;
using S1TimeManager = ScheduleOne.GameTime.TimeManager;
using S1GameDateTimeData = ScheduleOne.Persistence.Datas.GameDateTimeData;
#endif

using System;

namespace S1API.GameTime
{
	/// <summary>
	/// Represents an in-game datetime (elapsed days and 24-hour time).
	/// </summary>
	public struct GameDateTime
	{
		/// <summary>
		/// Gets or sets the number of elapsed days.
		/// </summary>
		public int ElapsedDays { get; set; }

		/// <summary>
		/// Gets or sets the time of day in 24-hour format (e.g. 1430 = 2:30 PM).
		/// </summary>
		public int Time { get; set; }

		/// <summary>
		/// Constructs a GameDateTime from elapsed days and 24-hour time.
		/// </summary>
		public GameDateTime(int elapsedDays, int time)
		{
			ElapsedDays = elapsedDays;
			Time = time;
		}

		/// <summary>
		/// Constructs a GameDateTime from total minutes.
		/// </summary>
		public GameDateTime(int minSum)
		{
			ElapsedDays = minSum / 1440;
			var minutesInDay = minSum % 1440;
			if (minSum < 0)
			{
				minutesInDay = -minSum % 1440;
			}

			Time = S1TimeManager.Get24HourTimeFromMinSum(minutesInDay);
		}

		/// <summary>
		/// Constructs a GameDateTime from an internal GameDateTimeData.
		/// </summary>
		public GameDateTime(S1GameDateTimeData data)
		{
			ElapsedDays = data.ElapsedDays;
			Time = data.Time;
		}

		/// <summary>
		/// Constructs a GameDateTime from the internal GameDateTime struct.
		/// </summary>
		public GameDateTime(S1GameDateTime gameDateTime)
		{
			ElapsedDays = gameDateTime.elapsedDays;
			Time = gameDateTime.time;
		}

		/// <summary>
		/// Returns the total minute sum (days * 1440 + minutes of day).
		/// </summary>
		public int GetMinSum() => ElapsedDays * 1440 + S1TimeManager.GetMinSumFrom24HourTime(Time);

		/// <summary>
		/// Returns a new GameDateTime with additional minutes added.
		/// </summary>
		public GameDateTime AddMinutes(int minutes) => new(GetMinSum() + minutes);

		/// <summary>
		/// Converts this wrapper to the internal GameDateTime struct.
		/// </summary>
		public S1GameDateTime ToS1() => new(ElapsedDays, Time);

		/// <summary>
		/// Returns the current time formatted as a 12-hour AM/PM string.
		/// Example: "12:30 PM"
		/// </summary>
		public string GetFormattedTime() => S1TimeManager.Get12HourTime(Time);

		/// <summary>
		/// Returns true if the time is considered nighttime.
		/// (Before 6AM or after 6PM)
		/// </summary>
		public bool IsNightTime() => Time < 600 || Time >= 1800;

		/// <summary>
		/// Returns true if the two GameDateTimes are on the same day (ignores time).
		/// </summary>
		public bool IsSameDay(GameDateTime other) => ElapsedDays == other.ElapsedDays;

		/// <summary>
		/// Returns true if the two GameDateTimes are at the same day and time.
		/// </summary>
		public bool IsSameTime(GameDateTime other) => ElapsedDays == other.ElapsedDays && Time == other.Time;

		/// <summary>
		/// String representation: "Day 3, 2:30 PM"
		/// </summary>
		public override string ToString() => $"Day {ElapsedDays}, {GetFormattedTime()}";

		/// <summary>
		/// Adds two GameDateTimes by combining their total minute sums.
		/// </summary>
		/// <param name="a">The first GameDateTime.</param>
		/// <param name="b">The second GameDateTime.</param>
		/// <returns>A new GameDateTime representing the sum.</returns>
		public static GameDateTime operator +(GameDateTime a, GameDateTime b) => new(a.GetMinSum() + b.GetMinSum());

		/// <summary>
		/// Subtracts two GameDateTimes by finding the difference between their total minute sums.
		/// </summary>
		/// <param name="a">The GameDateTime to subtract from.</param>
		/// <param name="b">The GameDateTime to subtract.</param>
		/// <returns>A new GameDateTime representing the difference.</returns>
		public static GameDateTime operator -(GameDateTime a, GameDateTime b) => new(a.GetMinSum() - b.GetMinSum());

		/// <summary>
		/// Determines if the first GameDateTime is later than the second by comparing minute sums.
		/// </summary>
		/// <param name="a">The first GameDateTime to compare.</param>
		/// <param name="b">The second GameDateTime to compare.</param>
		/// <returns>True if the first GameDateTime is later than the second.</returns>
		public static bool operator >(GameDateTime a, GameDateTime b) => a.GetMinSum() > b.GetMinSum();

		/// <summary>
		/// Determines if the first GameDateTime is earlier than the second by comparing minute sums.
		/// </summary>
		/// <param name="a">The first GameDateTime to compare.</param>
		/// <param name="b">The second GameDateTime to compare.</param>
		/// <returns>True if the first GameDateTime is earlier than the second.</returns>
		public static bool operator <(GameDateTime a, GameDateTime b) => a.GetMinSum() < b.GetMinSum();
	}
}
