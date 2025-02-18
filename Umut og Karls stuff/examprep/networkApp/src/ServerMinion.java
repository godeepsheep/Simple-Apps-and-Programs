import java.net.Socket;
import java.util.ArrayList;

public class ServerMinion implements Runnable {
    private Socket connection;
    private String clientName;
    private int wordsCount, color;
    private ArrayList<String> words;

    public ServerMinion(Socket connection) {
        this.connection = connection;
        wordsCount = 0;
        words = new ArrayList<>();
        color = 33;
        // starting Minion
        new Thread(this).start();
    }


    public void run() {
        Utils.println(this, color,"is online");

        NetworkIO networkIO = new NetworkIO(connection);

        clientName = networkIO.readLine();
        color = Integer.parseInt(networkIO.readLine());

        String line = "";
        while ((line = networkIO.readLine()) != null) {
            Utils.println(this, color, "modtager fra ["+clientName+"]: '" + line + "'");
            if (line.equals("<exit>")) break;


            wordsCount ++;
            words.add(line);
            words.sort(null);

            networkIO.sendLine(wordsCount + " "  + words.getFirst());
        }

        networkIO.close();
        Utils.println(this, color,"is offline" );

    }
}
