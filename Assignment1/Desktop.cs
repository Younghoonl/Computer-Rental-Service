namespace Assignment1;

//Computer클래스를 상속받은 Desktop 클래스
public class Desktop : Computer
{
    //생성자에서 컴퓨터 타입, 사용목적, 가격 프로퍼티 초기화
    public Desktop(int computerId) : base(computerId)
    {
        Type = "Desktop";
        UsedFor = new string[] { "internet", "scientific", "game" };
        Price = 13000;
    }
}

