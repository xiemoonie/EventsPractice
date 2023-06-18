using System;

namespace Eventos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NotifierClass n = new NotifierClass();
            n.Notification += OnNotificationRecived;
            n.ProcessCompleted += OnProcessReceived;
            n.PersonalEvent += OnPersonalProcessReceived;

            n.StartProgram();

        }
        public static void OnNotificationRecived(object sender, EventArgs e)
        {
            Console.WriteLine("Notification received");
        }

        public static void OnProcessReceived(object sender, bool b)
        {
            Console.WriteLine("Process received");
        }

        public static void OnPersonalProcessReceived(object sender, ProcessEventArgs data)
        {
            Console.WriteLine("Personal Process " + (data.Successful ? "Completed Successfully" : "failed"));
            Console.WriteLine("Completion Time: " + data.Time.ToLongDateString());
        }

    }
}
public class NotifierClass
{
    public event EventHandler Notification;
    public event EventHandler<bool> ProcessCompleted;
    public event EventHandler<ProcessEventArgs> PersonalEvent;

    public void StartProgram()
    {
        var data = new ProcessEventArgs();
        OnNotificationFinished(EventArgs.Empty);
        data.Successful = true;
        data.Time = DateTime.Now;
        OnPersonalProcessCompleted(data);
        try {
            OnProcessCompleted(true);
        }
        catch (Exception e)
        {
            OnProcessCompleted(false);
        }
    }
    public virtual void OnNotificationFinished(EventArgs e)
    {
        Console.WriteLine("Notification Finished");
        Notification?.Invoke(this, e);
    }

    public virtual void OnProcessCompleted(bool b)
    {
        Console.WriteLine("Process Finished");
        ProcessCompleted?.Invoke(this, b);
    }

    public virtual void OnPersonalProcessCompleted(ProcessEventArgs data)
    {
        Console.WriteLine("Personal Process Finished");
        PersonalEvent?.Invoke(this, data);
    }
}

public class ProcessEventArgs : EventArgs
{
    public bool Successful { set; get; }
    public DateTime Time { set; get; }
}

