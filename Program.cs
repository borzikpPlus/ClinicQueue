using System.Text;

namespace ClinicQueue;

public class Patient
{
    public string Name { get; }

    public Patient(string name)
    {
        Name = name;
    }
}

public class Clinic
{
    private readonly Queue<Patient> queue = new Queue<Patient>();
    private readonly int receptionTime;

    public Clinic(int receptionTime)
    {
        this.receptionTime = receptionTime;
    }

    public bool HasPatients => queue.Count > 0;

    public void AddPatient(Patient patient)
    {
        queue.Enqueue(patient);
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Новий пацієнт: {patient.Name}");
        PrintQueue();
    }

    public void ReceivePatient()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Черга порожня, лікар вільний");
            return;
        }

        Patient patient = queue.Dequeue();
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Лікар приймає: {patient.Name}");
        Thread.Sleep(receptionTime);
        PrintQueue();
    }

    public void PrintQueue()
    {
        if (queue.Count == 0)
        {
            Console.WriteLine("Черга порожня");
        }
        else
        {
            Console.WriteLine("Черга: " + string.Join(", ", queue.Select(patient => patient.Name)));
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Clinic clinic = new Clinic(2000);

        clinic.AddPatient(new Patient("Іван"));
        clinic.AddPatient(new Patient("Олена"));
        clinic.AddPatient(new Patient("Петро"));
        clinic.AddPatient(new Patient("Марія"));

        clinic.ReceivePatient();
        clinic.AddPatient(new Patient("Андрій"));

        while (clinic.HasPatients)
        {
            clinic.ReceivePatient();
        }

        Console.WriteLine("Усіх пацієнтів прийнято");
    }
}
