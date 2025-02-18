import java.net.*;  // Import necessary classes for networking (e.g., Socket, ServerSocket)
import java.io.*;   // Import classes for input/output (e.g., BufferedReader, PrintWriter)

public class UpperCaseServer {
    public static void main(String[] args) {
        try {
            // Create a ServerSocket that listens on port 6010.
            // This is like a telephone waiting for someone to call.
            ServerSocket serverSocket = new ServerSocket(6010);
            System.out.println("Server is waiting for a client on port 6010...");

            // The server waits for a client to connect.
            // The accept() method blocks (waits) until a client connects to the server.
            // Once a connection is made, it returns a Socket object representing the connection.
            Socket clientSocket = serverSocket.accept();
            System.out.println("Client connected!");

            // Set up input and output streams to communicate with the client.
            // BufferedReader is used to read text from the input stream (data coming from the client).
            BufferedReader in = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
            // PrintWriter is used to send text to the output stream (data going to the client).
            PrintWriter out = new PrintWriter(clientSocket.getOutputStream(), true);

            String line;
            // The server enters a loop to keep reading messages from the client.
            // It reads the input line by line using in.readLine().
            while ((line = in.readLine()) != null) {
                // If the client sends "<end>", the server will break out of the loop and stop.
                if (line.equalsIgnoreCase("<end>")) {
                    break;
                }
                // Convert the message to uppercase.
                String upperCaseLine = line.toUpperCase();
                // Send the uppercase message back to the client.
                out.println(upperCaseLine);
                // Print what was sent for server-side logging.
                System.out.println("Sent to client: " + upperCaseLine);
            }

            // Clean up resources (close the connections).
            // Closing the BufferedReader and PrintWriter closes the underlying socket as well.
            in.close();
            out.close();
            clientSocket.close();
            serverSocket.close();  // This closes the server socket and stops listening for new connections.
            System.out.println("Server is now offline.");
        } catch (IOException e) {
            // Handle exceptions related to input/output operations.
            System.out.println("Server error: " + e.getMessage());
        }
    }
}