import java.io.IOException;
import java.net.Socket;

public class Client implements Runnable {
    private String name;
    private int port, color;
    private String[] message;

    public Client(String name, int port, String[] message, int color) {
        this.name = name;
        this.port = port;
        this.color = color;
        this.message = message;

        new Thread(this).start();
    }

    public void run() {
        try (var connection = new Socket("localhost", port)) {

            Utils.println(this, color, "["+name+"] is online" );
            NetworkIO networkIO = new NetworkIO(connection);

            networkIO.sendLine(name); //sending identification
            networkIO.sendLine(""+color); //sending identification

            if(message.length>0) {
                for (String m : message) {

                    networkIO.sendLine(m);
                    var result = networkIO.readLine(); //wating for answer
                    Utils.println(this, color, "modtager ["+name+"]: '" + result + "'");
                }
            }

            networkIO.sendLine("<exit>");
            networkIO.close();
            Utils.println(this, color, "["+name+"] is offline" );

        } catch (IOException ex) {
            Utils.println(this, color, "Error: " + ex.getMessage());
        }
    }

}
