// See https://aka.ms/new-console-template for more information
public interface IStudent
{
    void PercentageCalculator();
    void GradeAssigning();
    void StudentDetailsDisplay();
}

class Student : IStudent
{
    public string? Name;
    public int sub1=0, sub2=0, sub3=0, sub4 = 0;

    public int obtained_marks;

    public int Total = 400;

    public double Percentage;

    public char Grade;

    public void PercentageCalculator()
    {
        obtained_marks = sub1 + sub2 + sub3 + sub4;

        Percentage = (obtained_marks * 100) / Total;
    }

    public void GradeAssigning()
    {
        if (Percentage >= 90)
            Grade = 'A';
        else if (Percentage >= 75)
            Grade = 'B';
        else if (Percentage >= 60)
            Grade = 'C';
        else if (Percentage >= 40)
            Grade = 'D';
        else
            Grade = 'F';

    }

    public void StudentDetailsDisplay()
    {
        Console.WriteLine("------------------------------");
        Console.WriteLine($"Name        : {Name}");
        Console.WriteLine($"Total Marks : {obtained_marks}/400");
        Console.WriteLine($"Percentage  : {Percentage}%");
        Console.WriteLine($"Grade       : {Grade}");
    }

}

class Program
{
    static public void Main(string[] args)
    {
        Student s = new Student();


        Console.Write("Enter student name:");
        s.Name = Console.ReadLine();
        Console.Write("Enter the subject 1 marks: ");
        s.sub1 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter the subject 2 marks: ");
        s.sub2 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter the subject 3 marks: ");
        s.sub3 = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter the subject 4 marks: ");
        s.sub4 = Convert.ToInt32(Console.ReadLine());
        
        s.PercentageCalculator();
        s.GradeAssigning();
        s.StudentDetailsDisplay();

    }
}
