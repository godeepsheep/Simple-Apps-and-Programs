
public class Main {
    public static void main(String[] args) {
        int port = 6000;
        ServerMaster server = new ServerMaster(port);

        new Client("Klient mars", port, new String[]{"cykel", "banan", "delfin", "anker", "egern"}, 32);
        new Client("Klient pluto", port, new String[]{"cirkus", "bænk", "diamant", "abe", "ekko"}, 31);

        server.shutDown(4);
    }
}