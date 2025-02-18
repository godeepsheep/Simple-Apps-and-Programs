using KlientServerOpgBregnSumLigeUligetal;
using System.Threading;

var port = 12345;
var server = new Server(port); // opretter en ny server med den angivene port
var serverThread = new Thread(server.Run); // starter serverens thread ved at angive Run-metoden som trådens opgave
serverThread.Start(); // starter thread


// Start klient
Thread.Sleep(1000); // venter man 1 sek, for at være sikker på serveren er startet
var client = new Client(port); // opretter en client med angivene port
client.Run(); // starter klientens kommunikation med serveren