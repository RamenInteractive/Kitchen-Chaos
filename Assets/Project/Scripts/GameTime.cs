public class GameTime {
    private int hour { get; set; }

    private int minute { get; set; }

    public GameTime() {
        hour = 0;
        minute = 0;
    }
    
    public GameTime(GameTime time) {
        hour = time.hour;
        minute = time.minute;
    }

    public GameTime(int hour, int minute) {
        this.hour = hour;
        this.minute = minute;
        rollOver();
    }

    public GameTime(int HHmm) {
        hour = HHmm / 100;
        minute = HHmm % 100;
        rollOver();
    }

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

    public int asMinutes() {
        return hour * 60 + minute;
    }

    public override string ToString() {
        return string.Format("{0:##}:{1:00}", (hour > 12 && (hour % 12 != 0) ? hour % 12 : hour), minute) + (hour >= 12 ? "PM" : "AM");
    }

    public string ToString(bool is24Hour) {
        if(is24Hour) {
            return string.Format("{0:00}:{1:00}", hour, minute);
        } else {
            return ToString();
        }
    }

    public GameTime addMinutes(int minutes) {
        minute += minutes;
        rollOver();
        return this;
    }

    public GameTime addHours(int hours) {
        hour += hours;
        rollOver();
        return this;
    }

    public static GameTime operator +(GameTime l, GameTime r) {
        GameTime time = new GameTime(l);
        time.addHours(r.hour);
        time.addMinutes(r.minute);
        return time;
    }

    public static GameTime operator -(GameTime l, GameTime r) {
        GameTime time = new GameTime(l);
        time.addHours(-r.hour);
        time.addMinutes(-r.minute);
        return time;
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
