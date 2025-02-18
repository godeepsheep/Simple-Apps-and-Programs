import java.io.*;
import java.net.Socket;
import java.util.Random;

public class NetworkIO {
    private BufferedWriter writer;
    private BufferedReader reader;

    public NetworkIO(Socket connection) {
        reader = createReader(connection);
        writer = createWriter(connection);
    }
    private BufferedReader createReader(Socket connection) {
        try {
            return new BufferedReader(
                    new InputStreamReader(connection.getInputStream())
            );
        } catch (IOException ex) { return null;}
    }

    private BufferedWriter createWriter(Socket connection) {
        try {
            return new BufferedWriter(
                    new OutputStreamWriter(connection.getOutputStream())
            );
        } catch (IOException ex) {return null;}
    }


    public String readLine() {
        try {
            return reader.readLine();

        } catch (IOException ex) {}

        return null;
    }

    public void sendLine(String line) {
        try {
            writer.write(line);
            writer.newLine();
            writer.flush();

            Utils.sleep( new Random().nextInt(2000)/1000 );

        } catch (IOException ex) {}

    }

    public void close() {
        try {
            reader.close();
            writer.close();
        } catch (IOException ex) {}
    }
}
