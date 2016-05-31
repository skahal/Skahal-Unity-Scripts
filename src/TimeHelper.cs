using System;

public static class TimeHelper
{
	public static string GetFormatForSeconds(float seconds, string format)
	{
		var timeSpan = TimeSpan.FromSeconds(seconds);
		
		switch (format)
		{
			case "mm:ss":
				return String.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		}
		
		return seconds.ToString();
	}
	
}

