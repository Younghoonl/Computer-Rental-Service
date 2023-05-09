namespace Assignment1;

//User클래스를 상속받은 Worker클래스
public class Worker : User
{
    //타입과 사용목적 프로퍼티 초기화
    public Worker(int userId) : base(userId)
    {
        Type = "OfficeWorkers";
        UsedFor = new string[] { "internet" };
    }

    public int WorkerId { get; set; }  //직장인 아이디 프로퍼티
}