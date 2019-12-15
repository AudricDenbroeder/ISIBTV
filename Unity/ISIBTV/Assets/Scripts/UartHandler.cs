using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO.Ports;

public class UartHandler : MonoBehaviour
{
    public string portName = "COM4";
    public int baudRate = 115200;
    private string whButton;

    private Thread thread;
    private Queue rxQueue;

    private SerialPort stream;
 
    public void Start (){
        // Creates and starts the thread
        rxQueue = Queue.Synchronized(new Queue());

        thread = new Thread (ThreadLoop);
        thread.Start();
    }

    private void OnApplicationQuit()
    {
        stream.Close();
        thread.Abort();
    }

    public void ThreadLoop (){
    // The code of the thread goes here...
        stream = new SerialPort(portName,
                                baudRate,
                                Parity.None,
                                8,
                                StopBits.One);
        //stream.ReadTimeout = 200;
        stream.Open();

        while(true){
            //string result = ReadFromUC((timeout));
            string result = stream.ReadByte().ToString();
            if (result != null)
                rxQueue.Enqueue(result);
        }
    }

    public string ReadUART(){
        if (rxQueue.Count == 0)
            return null;

        return (string) rxQueue.Dequeue();
    }
}
