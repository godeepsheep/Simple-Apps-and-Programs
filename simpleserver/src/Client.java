import java.io.IOException;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;
import java.io.*;
import java.util.Scanner;

public class Client implements Runnable {

    private String userId;
    private Socket socket;

    private BufferedReader reader;
    private PrintWriter writer;

    public Client(String userId) {
        this.userId = userId;
    }

    public boolean connect(InetAddress address, int port) {
        try {
            socket = new Socket(address, port);

            this.writer = new PrintWriter(new OutputStreamWriter(socket.getOutputStream()));
            this.reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            new Thread(this).start();
            System.out.println("Connected to " + socket.getInetAddress().getHostAddress() + ":" + socket.getPort());
            return true;
        } catch (IOException e) {
            System.out.println("there was an error");
            return false;
        }
    }

    public void sendToServer(String message) {
        writer.write(message);
        writer.write("\n");
        writer.flush();
        System.out.println("Sending message: " + message);
    }

    @Override
    public void run() {
        String line;
        try {
            while ((line = reader.readLine()) != null) {
                System.out.println("Server: " + line);
            }
        } catch (IOException e) {
            System.out.println("Server is closed");
        }
    }

    public static void main(String[] args) throws UnknownHostException {
        Client client = new Client("Sab");
        client.connect(InetAddress.getByName("localhost"), 6969);
        Scanner scanner = new Scanner(System.in);
        while (scanner.hasNextLine()) {
            String line = scanner.nextLine();
            client.sendToServer(line);
        }
    }
}
