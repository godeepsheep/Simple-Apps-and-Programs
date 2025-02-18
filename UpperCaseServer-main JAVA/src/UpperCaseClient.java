import java.net.*;  // Import classes for networking (e.g., Socket)
import java.io.*;   // Import classes for input/output (e.g., BufferedReader, PrintWriter)

public class UpperCaseClient {
    public static void main(String[] args) {
        try {
            // Connect to the server.
            // The client connects to the server running on the same machine (localhost or 127.0.0.1) on port 6010.
            Socket socket = new Socket("127.0.0.1", 6010);
            System.out.println("Connected to the server!");

            // Set up input and output streams to communicate with the server.
            // PrintWriter is used to send text to the server.
            PrintWriter out = new PrintWriter(socket.getOutputStream(), true);
            // BufferedReader is used to receive text from the server.
            BufferedReader in = new BufferedReader(new InputStreamReader(socket.getInputStream()));

            // Send some messages to the server.
            // These are just example messages.
            out.println("Hello Server!");  // Sending a message to the server.
            out.println("This is a test.");  // Sending another message.
            out.println("<end>");  // This message tells the server to stop.

            // Read and print responses from the server.
            String response;
            // Read responses from the server until there are no more lines to read.
            while ((response = in.readLine()) != null) {
                System.out.println("Received from server: " + response);  // Print the server's response.
            }

            // Clean up resources (close the connections).
            in.close();  // Close the input stream.
            out.close();  // Close the output stream.
            socket.close();  // Close the socket, ending the connection to the server.
            System.out.println("Client is now offline.");
        } catch (IOException e) {
            // Handle exceptions related to input/output operations.
            System.out.println("Client error: " + e.getMessage());
        }
    }
}