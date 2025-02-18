import java.io.IOException;
import java.net.ServerSocket;
import java.net.SocketTimeoutException;
import java.util.LinkedList;
import java.util.List;

public class ServerMaster implements Runnable {
    private List<ServerMinion> minions;
    private static int port;
    private int color;
    private boolean stop;

    public ServerMaster(int port) {
        this.port = port;
        stop = false;
        minions = new LinkedList();
        color = 37;

        // starting server
        new Thread(this).start();
    }

    public void run() {
        try (var server = new ServerSocket( port, 10 )) { // creating server

            server.setSoTimeout( 100 ); // timeout for the while loop
            Utils.println(this, color,"is online");

            while (!stop) {
                try {
                    var connection = server.accept(); // waiting for connection to create a minion

                    minions.add(
                        new ServerMinion(connection)
                    );

                } catch (SocketTimeoutException e) {
                    // keep running when timed out
                }
            }

            Utils.println(this, color, "is offline");

        }
        catch ( IOException e ) {
            Utils.println(this, color, "I/O error");
        }
    }

    private boolean shutDownCalled = false;

    public void shutDown(int delay_seconds) {
        if(shutDownCalled) return;
        shutDownCalled = true;

        Utils.sleep(delay_seconds); // delay
        stop = true;
    }

}
