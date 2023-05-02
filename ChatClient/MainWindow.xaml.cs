using ChatClient.ServiceChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnetced = false;
        ServiceChatClient client;
        int clientId;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
        }

        void ConnectUser()
        {
            if(!isConnetced)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                clientId = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                bConnDis.Content = "Disconnect";
                isConnetced= true;
            }
        }

        void DisconnectUser()
        {
            if (isConnetced)
            {
                client.Disconnect(clientId);
                client = null;
                tbUserName.IsEnabled = true;
                bConnDis.Content = "Connect";
                isConnetced = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnetced)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void msgCallBack(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(client!= null && e.Key == Key.Enter)
            {
                client.SendMsg(tbMessage.Text, clientId);
                tbMessage.Text = "";
            }
        }
    }
}
