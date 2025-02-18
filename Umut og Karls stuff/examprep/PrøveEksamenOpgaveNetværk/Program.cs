/* Opgave 1: Simpel* Klient og Server
Klienten sender en række tal til serveren i form af tekststykker (f.eks.: "82", "19", "43", "2" og "61"), 
og serveren svarer for hver gang om tallet er lige eller ulige 
(f.eks.: "lige", "ulige", "ulige", "lige" og "ulige").

Lav ændringer i dit program, så serveren kun svarer "lige" og "ulige" første gang 
den modtager et lige eller ulige tal, 
mens den alle efterfølgende gange, vil svare "igen lige" eller "igen ulige", 
alt efter om tallet er lige eller ulige 
(f.eks.: "lige", "ulige", "igen ulige", "igen lige" og "igen ulige").

Lav en testanvendelse, der viser en klient, der gør dette med mindst fem tal.

[lidt mindre end en normal eksamensopgave, forventet løsningstid ca. 45-50 min.]

*) "Simpel" betyder at serveren ikke behøver at være flertrådet. */

using PrøveEksamenOpgaveNetværk;

var port = 12345;
var server = new Server(port); // opretter en ny server med den angivene port
var serverThread = new Thread(server.Run); // starter serverens thread ved at angive Run-metoden som trådens opgave
serverThread.Start(); // starter thread


// Start klient
Thread.Sleep(1000); // venter man 1 sek, for at være sikker på serveren er startet
var client = new Client(port); // opretter en client med angivene port
client.Run(); // starter klientens kommunikation med serveren