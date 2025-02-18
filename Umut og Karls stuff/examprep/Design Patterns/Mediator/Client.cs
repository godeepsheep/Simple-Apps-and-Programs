namespace Mediator;

using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        Mediator mediator = new Mediator();

        ConcreteColleague colleague1 = new ConcreteColleague();
        ConcreteColleague colleague2 = new ConcreteColleague();

        mediator.RegisterColleague("Colleague1", colleague1);
        mediator.RegisterColleague("Colleague2", colleague2);

        colleague1.SendMessage("Hello from Colleague1!"); // Colleague2 receives the message
        colleague2.SendMessage("Hi from Colleague2!"); // Colleague1 receives the message
    }
}
