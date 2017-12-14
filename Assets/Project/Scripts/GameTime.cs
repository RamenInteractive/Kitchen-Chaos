/**
 * =========
 * Game Time
 * =========
 * 
 * Represents a time of day.
 */ 
public class GameTime {
    private int hour { get; set; }

    private int minute { get; set; }

    /**
     * Instantiates a new time at 00:00
     */
    public GameTime() {
        hour = 0;
        minute = 0;
    }
    
    /**
     * Instantiates a new copy of the given time
     */
    public GameTime(GameTime time) {
        hour = time.hour;
        minute = time.minute;
    }

    /**
     * Instantiates a new time with the hour and minute given
     * 
     * @param hour The hour of the time
     * @param minute The minute of the time
     */ 
    public GameTime(int hour, int minute) {
        this.hour = hour;
        this.minute = minute;
        rollOver();
    }

    /**
     * Instantiates a new time based on the number of minutes elapsed into the day
     * 
     * @param minutes Number of minutes elapsed so far in the day
     */ 
    public GameTime(int minutes) {
        hour = 0;
        minute = minutes;
        rollOver();
    }

    /**
     * Instantiates a new time based on the given time in format HH:mm
     * Should range between 00:00 and 23:59
     * 
     * eg. "10:30" is 10 hours and 30 minutes
     *     "18:00" is 18 hours and 0 minutes
     * 
     * @param HHmm Formatted string that represents time in the day  
     */   
    public GameTime(string HHmm) {
        string[] time = HHmm.Split(':');
        if(time.Length == 2) {
            hour = int.Parse(time[0]);
            minute = int.Parse(time[1]);
        } else {
            throw new InvalidStringFormatException(); 
        }
        rollOver();
    }

    /**
     * Returns the number of minutes elapsed in the day.
     * 
     * @return int Minutes elapsed in the day
     */ 
    public int asMinutes() {
        return hour * 60 + minute;
    }

    /**
     * Override of default ToString
     * Outputs the time of day in a formatted string in the form of HH:mmPP
     * 
     * eg. 8h 30m = 8:30AM
     *     21 15m = 9:15PM
     *     
     * @return string Formatted time
     */ 
    public override string ToString() {
        return string.Format("{0:##}:{1:00}", (hour > 12 && (hour % 12 != 0) ? hour % 12 : hour), minute) + (hour >= 12 ? "PM" : "AM");
    }

    /**
     * Outputs the time of day in a string formatted in either 12 or 24 hour format
     * 
     *         | 12 hour |  24 hour
     * 8h 30m  | 8:30AM  |  08:30
     * 21h 15m | 9:15PM  |  21:15
     * 
     * @param is24Hour whether to format 24 hour or not
     * @return string Formatted time
     */ 
    public string ToString(bool is24Hour) {
        if(is24Hour) {
            return string.Format("{0:00}:{1:00}", hour, minute);
        } else {
            return ToString();
        }
    }

    /**
     * Adds a number of minutes to the time of day.
     * 
     * @param minutes Number of minutes to add
     * @return GameTime Resulting time object
     */ 
    public GameTime addMinutes(int minutes) {
        minute += minutes;
        rollOver();
        return this;
    }

    /**
     * Add a number of hours to the time of day.
     * 
     * @param minutes Number of hours to add
     * @return GameTime Resulting time object
     */ 
    public GameTime addHours(int hours) {
        hour += hours;
        rollOver();
        return this;
    }

    public static int operator +(GameTime l, GameTime r) {
        return l.asMinutes() + r.asMinutes();
    }

    public static int operator -(GameTime l, GameTime r) {
        return l.asMinutes() - r.asMinutes();
    }

    public static GameTime operator +(GameTime l, int r) {
        return new GameTime(l).addMinutes(r);
    }

    public static GameTime operator -(GameTime l, int r) {
        return new GameTime(l).addMinutes(-r);
    }

    public static GameTime operator ++(GameTime l) {
        GameTime old = new GameTime(l);
        l.addMinutes(1);
        return old;
    }

    public static GameTime operator --(GameTime l) {
        GameTime old = new GameTime(l);
        l.addMinutes(-1);
        return old;
    }

    public static bool operator >(GameTime l, GameTime r) {
        if (l.hour == r.hour)
            return l.minute > r.minute;
        return l.hour > r.hour;
    }

    public static bool operator <(GameTime l, GameTime r) {
        if (l.hour == r.hour)
            return l.minute < r.minute;
        return l.hour < r.hour;
    }

    public static bool operator >=(GameTime l, GameTime r) {
        return !(l < r);
    }

    public static bool operator <=(GameTime l, GameTime r) {
        return !(l > r);
    }

    public static bool operator ==(GameTime l, GameTime r) {
        if (l.hour == r.hour)
            return l.minute == r.minute;
        return false;
    }

    public static bool operator !=(GameTime l, GameTime r) {
        return !(l == r);
    }

    public class InvalidStringFormatException : System.Exception {}

    private void rollOver() {
        while (minute < 0) {
            hour--;
            minute += 60;
        }
        while (hour < 0)
            hour += 24;
        hour += minute / 60;
        minute %= 60;
        hour %= 24;
    }
}
