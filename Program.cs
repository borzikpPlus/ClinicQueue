using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class Patient
{
    public string Name;

    public Patient(string name)
    {
        Name = name;
    }
}

class Clinic
{
    Queue<Patient> queue = new Queue<Patient>();
    int time;

    public Clinic(int time)
    {
        this.time = time;
    }

    public void AddPatient(Patient patient)
    {
        queue.Enqueue(patient);
        Console.WriteLine("Новий пацієнт: " + patient.Name);
        ShowQueue();
    }

    public void ReceivePatient()
    {
        Patient patient = queue.Dequeue();
        Console.WriteLine("Лікар приймає: " + patient.Name);
        Thread.Sleep(time);
        ShowQueue();
    }

    public int Count()
    {
        return queue.Count;
    }

    public void ShowQueue()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Черга порожня");
        }
        else
        {
            Console.Write("Черга: ");
            foreach (Patient p in queue)
            {
                Console.Write(p.Name + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Clinic clinic = new Clinic(2000);

        clinic.AddPatient(new Patient("Іван"));
        clinic.AddPatient(new Patient("Олена"));
        clinic.AddPatient(new Patient("Петро"));
        clinic.AddPatient(new Patient("Марія"));

        clinic.ReceivePatient();
        clinic.AddPatient(new Patient("Андрій"));

        while (clinic.Count() > 0)
        {
            clinic.ReceivePatient();
        }

        Console.WriteLine("Всіх пацієнтів прийнято");
    }
}
