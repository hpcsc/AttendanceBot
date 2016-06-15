namespace AttendanceBot.Models
{
    public class AttendanceEntry
    {
        public string UserId { get; private set; }
        public string Name { get; private set; }
        public string Message { get; private set; }
        public AttendanceStatus Status { get; private set; }

        public AttendanceEntry(string userId, string name, AttendanceStatus status, string message = null)
        {
            UserId = userId;
            Name = name;
            Status = status;
            Message = message;
        }

        public void In()
        {
            Status = AttendanceStatus.Yes;
        }

        public void Out(string message = null)
        {
            Status = AttendanceStatus.No;
            Message = message;
        }

        public void Maybe(string message = null)
        {
            Status = AttendanceStatus.Maybe;
            Message = message;
        }
    }
}