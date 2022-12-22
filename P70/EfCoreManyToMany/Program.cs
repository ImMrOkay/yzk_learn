// See https://aka.ms/new-console-template for more information

using EfCoreManyToMany;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

Student s1 = new Student() { Name = "Tom" };
Student s2 = new Student() { Name = "Lily" };
Student s3 = new Student() { Name = "Lucy" };
Student s4 = new Student() { Name = "Tim" };
Student s5 = new Student() { Name = "Linda" };


Teacher t1 = new Teacher() { Name = "杨中科" };
Teacher t2 = new Teacher() { Name = "张三" };
Teacher t3 = new Teacher() { Name = "李四" };

t1.Students.Add(s1);
t1.Students.Add(s2);
t1.Students.Add(s3);

t2.Students.Add(s1);
t2.Students.Add(s3);
t2.Students.Add(s5);

t3.Students.Add(s2);
t3.Students.Add(s4);

using (MyDbContext myDbContext = new MyDbContext())
{
    myDbContext.AddRange(s1,s2,s3,s4,s5);
    myDbContext.AddRange(t1,t2,t3);
    await myDbContext.SaveChangesAsync();

    foreach (var teacher in myDbContext.Tteachers.Include(t=>t.Students))
    {
        Console.WriteLine($"老师：{teacher.Name}");
        foreach (var student in teacher.Students)
        {
            Console.WriteLine($"---{student.Name}");
        }
    }
}


