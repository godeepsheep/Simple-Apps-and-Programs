using MultiThreadServerTemplate;

var server = new Server();
var klient = new Klient();

// Start serveren i en separat tråd
var serverThread = new Thread(server.Run); // Bemærk at vi bruger "run" metoden direkte
serverThread.Start();  // Starter servertråden

// Vent lidt for at sikre, at serveren er oppe og kører
Thread.Sleep(500);  // Serveren får tid til at starte op

// Start klienten i en separat tråd
var klientThread = new Thread(klient.Run); // Vi bruger "run" metoden for klienten
klientThread.Start();  // Starter klienttråden
