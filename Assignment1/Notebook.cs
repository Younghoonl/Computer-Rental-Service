namespace Assignment1;

//Computer클래스를 상속받은 Notebook 클래스
public class Notebook : Computer
{
    //생성자에서 컴퓨터 타입, 사용목적, 가격 프로퍼티 초기화
    public Notebook(int computerId) : base(computerId)
    {
        Type = "Notebook";
        UsedFor = new string[] { "internet", "scientific" };
        Price = 10000;
    }
}