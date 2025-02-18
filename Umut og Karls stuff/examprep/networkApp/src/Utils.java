public class Utils {

    public static void sleep(int seconds) {
        try {
            Thread.sleep(seconds*1000);
        } catch (InterruptedException ex) {
            // error handler
        }
    }

    public static void println(Object obj, int color, String message) {
        System.out.println( "\u001B["+color+"m ["+ obj.getClass().getName() + "/" + Thread.currentThread().getName()+"] " + message);
    }
}
