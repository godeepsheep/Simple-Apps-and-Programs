import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.ArrayList;

public class Server implements Runnable {
    private int port;
    private ServerSocket serverSocket;

    ArrayList<String> userIdList = new ArrayList<>();
    ArrayList<Message> messageList = new ArrayList<>();


    public Server(int port) {
        this.port = port;
    }

    public boolean serverStart() {
        try {
            serverSocket = new ServerSocket(port);
            new Thread(this).start();
            System.out.println("server started");
            userIdList.add("Sab");
            userIdList.add("Michalis");
            userIdList.add("Yasuo");
            return true;
        } catch (IOException e){
            return false;
        }
    }

    @Override //overrides the method of the parent class.
    //in every thread that you create you have to give it a method to run on
    public void run() {
        while(!serverSocket.isClosed()) {
            try {
                Socket socket = serverSocket.accept();
                System.out.println("We got a new connection from" + socket.getInetAddress().getHostAddress());
                ClientHandler clientHandler = new ClientHandler(socket, this);
                new Thread(clientHandler).start();
            } catch (IOException e) {
                System.out.println("There was an error while accepting a client connection.");
            }
        }
    }

    public void addNewMessage(Message message) {
        messageList.add(message);
    }

    public ArrayList<Message> getMessagesForClient(String userId) {
        ArrayList<Message> listOfMessagesForClient = new ArrayList<>();
        ArrayList<Message> listOfMessagesToRemove = new ArrayList<>();

        for (Message message : messageList) {
            if (message.getReceiver().equals(userId)) {
                listOfMessagesForClient.add(message);
                listOfMessagesToRemove.add(message);
            }
        }
        messageList.removeAll(listOfMessagesToRemove);

        return listOfMessagesForClient;
    }

    public static void main(String[] args) {
        Server server = new Server(6969);
        server.serverStart();
    }
}

