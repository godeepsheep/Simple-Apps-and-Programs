using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Public_Key_Encryptor {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    private RSACryptoServiceProvider myRSA, otherRSA;

    public MainWindow() {
      InitializeComponent();
    }

    private void OnWindowLoaded(object sender, RoutedEventArgs e) {
      myRSA = new RSACryptoServiceProvider();
      otherRSA = new RSACryptoServiceProvider();

      DisplayKeys();
    }

    private void OnLoadPrivateKeyClicked( object sender, RoutedEventArgs e ) {
      OpenFileDialog dialog = new OpenFileDialog();

      if (dialog.ShowDialog() == true) {
        myRSA.FromXmlString(File.ReadAllText(dialog.FileName));
        DisplayKeys();
      }

      //MessageBox.Show( "Hello World!" );
    }

    private void OnSavePrivateKeyClicked( object sender, RoutedEventArgs e ) {
      SaveFileDialog dialog = new SaveFileDialog();

      if (dialog.ShowDialog() == true)
        File.WriteAllText(dialog.FileName, myRSA.ToXmlString(true));
    }

    private void OnImportPublicKeyClicked( object sender, RoutedEventArgs e ) {
      OpenFileDialog dialog = new OpenFileDialog();

      if (dialog.ShowDialog() == true) {
        otherRSA.FromXmlString(File.ReadAllText(dialog.FileName));
        DisplayKeys();
      }
    }

    private void OnSaveEncryptedMessageClicked( object sender, RoutedEventArgs e ) {
      SaveFileDialog dialog = new SaveFileDialog();

      if (dialog.ShowDialog() == true) {
        string plainText = messageTextBox.Text;
        byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
        byte[] cypherBytes = otherRSA.Encrypt(plainBytes, true);
        string cypherText = Convert.ToBase64String(cypherBytes);
        File.WriteAllBytes(dialog.FileName, cypherBytes);
      }
    }

    private void OnExportPublicKeyClicked( object sender, RoutedEventArgs e ) {
      SaveFileDialog dialog = new SaveFileDialog();

      if (dialog.ShowDialog() == true)
        File.WriteAllText(dialog.FileName, myRSA.ToXmlString(false));
    }

    private void OnLoadEncryptedMessageClicked( object sender, RoutedEventArgs e ) {
      OpenFileDialog dialog = new OpenFileDialog();

      if (dialog.ShowDialog() == true ) {
        byte[] cypherBytes = File.ReadAllBytes(dialog.FileName);
        byte[] plainBytes = myRSA.Decrypt(cypherBytes, true);
        string plainText = Encoding.Unicode.GetString(plainBytes);
        messageTextBox.Text = plainText;
      }
    }

    private void DisplayKeys() {
      RSAParameters myRSAParameters = myRSA.ExportParameters(true);
      textBox1.Text = Convert.ToBase64String(myRSAParameters.D);
      textBox3.Text = Convert.ToBase64String(myRSAParameters.Modulus);

      RSAParameters otherRSAParameters = otherRSA.ExportParameters(false);
      textBox2.Text = Convert.ToBase64String(otherRSAParameters.Modulus);
    }
  }
}
