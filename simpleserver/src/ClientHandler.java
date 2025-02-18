import java.io.*;
import java.net.Socket;
import java.util.ArrayList;

public class ClientHandler implements Runnable {

    private BufferedReader reader;
    private PrintWriter writer;
    private Server server;
    private boolean isLoggedIn;
    private String userId;

    public ClientHandler(Socket socket, Server server) {
        try {
            this.reader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
            this.writer = new PrintWriter(new OutputStreamWriter(socket.getOutputStream()));
            this.server = server;
            writer.write("Please use the command: LOGIN <username>");
            writer.write("\n");
            writer.flush();
            isLoggedIn = false;
        } catch (IOException e) {
            System.out.println("there was an error with the client");;
        }
    }

    public void handleSendMessage(String clientInput) {
        String second = clientInput.split(" ")[1];
        if (!isLoggedIn) {
            writer.write("You are not logged in\n");

        }
        if (userId.equals(second)) {
            writer.write("You cant write to your self, you lonely fkin noob\n");


        }
        if (!server.userIdList.contains(second)) {
            writer.write("This user does not exist\n");

        } //MESSAGE Sab Hey hey = clientInput
        String [] bits = clientInput.split(second);
        String secondbit = bits[1];
        Message message = new Message(userId,second,secondbit);
        server.addNewMessage(message);

        //server.addNewMessage(new Message(userId,second,clientInput.split(second)[1]));

    }

    public void handleLogin(String clientInput) {
        String second = clientInput.split(" ")[1];
        if (isLoggedIn) {
            writer.write("You are already logged in\n");

            return;
        }
        if (server.userIdList.contains(second)) {
            userId = second;
            isLoggedIn = true;
            writer.write("Welcome" + userId + "\n");

        }
        else {
            writer.write("This user does not exist\n");

        }

    }

    public void handleLogout() {
        if (!isLoggedIn) {
            writer.write("You are not logged in\n");
            writer.flush();
            return;
        }
        userId = null;
        isLoggedIn = false;
        writer.write("Bai bitch \n");

    }

    public void handleGet() {
        if (!isLoggedIn) {
            writer.write("You are not logged in\n");

            return;
        }
        ArrayList<Message> messageForClient = server.getMessagesForClient(userId);
        if (messageForClient.isEmpty()) {
            writer.write("you dont have friends \n");

            return;
        }
        for (Message message : messageForClient) {
            writer.write(message.getSender() + ": " + message.getMessage() + "\n");


        }
    }

    @Override
    public void run() {
        while(true){
            String clientInput;
            try {
                //reading the line and forwards it to the method to handle it.
                while ((clientInput = reader.readLine()) != null) {
                    System.out.println("Client" + clientInput);
                    String [] parts = clientInput.split(" "); //LOGIN
                    String first = parts[0];
                    String second = null;
                    if (parts.length > 1) {
                        second = parts[1];
                    }

                    switch(first) {
                        case "MESSAGE":
                           handleSendMessage(clientInput);
                           break;

                        case "LOGIN":
                            if (second == null) {
                                writer.write("Please use the following format to login: LOGIN <UserId>\n");
                                writer.flush();
                            }
                            handleLogin(clientInput);
                            break;

                        case "LOGOUT":
                            handleLogout();
                            break;

                        case "GET":
                            handleGet();
                            break;

                        default:
                            writer.write("Please type one of the following commands:LOGIN \n MESSAGE \n LOGOUT \n GET \n");
                            break;
                    }
                    writer.flush();
                }
            } catch (IOException a) {
                System.out.println("There was an error");
            }
        }
    }
}