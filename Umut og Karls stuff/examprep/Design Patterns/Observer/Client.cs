namespace Observer;

public class Program
{
    public static void Main()
    {
        ConcreteSubject subject = new ConcreteSubject();
        ConcreteObserver observer1 = new ConcreteObserver(subject);
        ConcreteObserver observer2 = new ConcreteObserver(subject);

        subject.State = 5;
        subject.State = 10;
    }
}
