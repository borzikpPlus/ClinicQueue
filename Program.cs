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

class Doctor
{
    public string Name;
    public int Time;

    public Doctor(string name, int time)
    {
        Name = name;
        Time = time;
    }

    public void Receive(Patient patient)
    {
        Console.WriteLine("Лікар " + Name + " приймає: " + patient.Name);
        Thread.Sleep(Time);
    }
}

class Clinic
{
    Queue<Patient> queue = new Queue<Patient>();
    Doctor doctor;

    public Clinic(Doctor doctor)
    {
        this.doctor = doctor;
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
        doctor.Receive(patient);
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

        Doctor doctor = new Doctor("Коваленко", 2000);
        Clinic clinic = new Clinic(doctor);

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
