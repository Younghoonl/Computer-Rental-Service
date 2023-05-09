namespace Assignment1;

//User클래스를 상속받은 Student클래스
public class Student : User
{
    //타입과 사용목적 프로퍼티 초기화
    public Student(int userId) : base(userId)
    {
        Type = "Students";
        UsedFor = new string[] { "internet", "scientific" };
    }
    public int StudentId { get; set; }   //학생 아이디 프로퍼티
}