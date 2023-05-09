using System.Threading.Tasks.Dataflow;

namespace Assignment1;

public class ComputerManager
{
    private Computer[] arrComp;          //컴퓨터를 오버라이딩 하기 위한 Computer배열 
    private User[] arrUser;              //사용자를 오버라이딩 하기 위한 User배열

    private int computerIdx = 0;         //컴퓨터 배열에서 사용하는 인덱스 변수
    private int numberOfComputers = 0;   //컴퓨터 개수
    private int numberOfNotebooks = 0;   //Notebook 개수
    private int numberOfDesktops = 0;    //Desktop 개수
    private int numberOfNetbooks = 0;    //Netbook 개수

    private int userIdx = 0;             //사용자 배열에서 사용하는 인덱스 변수
    private int numberOfUsers = 0;       //사용자 수
    private int numberOfStudent = 0;     //학생 수 
    private int numberOfWorker = 0;      //직장인 수 
    private int numberOfGamer = 0;       //게이머 수 
    
    private int totalCost = 0;           //총 비용
    private int cnt = 0;
    private int index;    

    private string[] writeLine = new string[10];        //TextFileData 클래스에 반환할 문자열 저장 변수

    //컴퓨터, 노트북, 넷북, 데스크탑 수를 가지고 arrComp배열을 초기화/Computer 객체 생성/Computer객체 프로퍼티 초기화
    public void SetComputerArray(int _numberOfComputers, int _numberOfNotebooks, int _numberOfDesktops, int _numberOfNetbooks)
    {
        numberOfComputers = _numberOfComputers;
        numberOfNotebooks = _numberOfNotebooks;
        numberOfDesktops = _numberOfDesktops;
        numberOfNetbooks = _numberOfNetbooks;

        arrComp = new Computer[numberOfComputers];  

        //netbooks 초기화
        for (cnt = 1; cnt <= numberOfNetbooks; cnt++)
        {
            computerIdx = cnt - 1;   
            arrComp[computerIdx] = new Netbook(cnt);     //넷북 생성
            arrComp[computerIdx].TypeId = cnt;           //넥북 아이디 초기화
        }  
        
        //notebook 초기화
        for (cnt = 1; cnt <= numberOfNotebooks; cnt++)   
        {
            computerIdx = numberOfNetbooks + cnt - 1;   
            arrComp[computerIdx] = new Notebook(computerIdx + 1);   //노트북 생성
            arrComp[computerIdx].TypeId = cnt;                               //노트북 아이디 초기화
        }
        
        //desktop 초기화
        for (cnt = 1; cnt <= numberOfDesktops; cnt++)
        {
            computerIdx = numberOfNetbooks + numberOfNotebooks + cnt - 1;   
            arrComp[computerIdx] = new Desktop(computerIdx + 1);   //데스크탑 생성
            arrComp[computerIdx].TypeId = cnt;                              //데스크탑 아이디 초기화
        }
    }

    //arrUser배열 초기화 함수 
    public void InitUserArray(int numberOfUsers)       
    {
        this.numberOfUsers = numberOfUsers;
        arrUser = new User[numberOfUsers];
    }
    
    //사용자 티입과 사용자 이름을 사용자 배열에 저장
    public void SetUserArray(string userType, string name )
    {
        
        if (userType.Equals("Student"))            //사용자 타입에 Student일때
        {
            Student student = new Student(userIdx + 1)   //Student객체 생성, 프로퍼티 초기화 
            {
                Name = name,                       
                StudentId = numberOfStudent + 1,   
                TypeId = numberOfStudent + 1       
            };
            numberOfStudent++;                   
            arrUser[userIdx] = student;            //사용자 배열에 Student객체 업캐스팅하여 저장
        }
        else if (userType.Equals("Gamer"))         //사용자 타입에 Gamer일때 
        {
            Gamer gamer = new Gamer(userIdx + 1)    //Gamer객체 생성, 프로퍼티 초기화 
            {
                Name = name,
                GamerId = numberOfGamer + 1,
                TypeId = numberOfGamer + 1
            };
            numberOfGamer++; 
            arrUser[userIdx] = gamer;              //사용자 배열에 Gamer객체 업캐스팅하여 저장
        }
        else if (userType.Equals("Worker"))        //사용자 타입에 Worker일때 
        {
            Worker worker = new Worker(userIdx + 1)   //Worker객체 생성, 프로퍼티 초기화 
            {
                Name = name,
                WorkerId = numberOfWorker + 1,
                TypeId = numberOfWorker + 1
            };
            numberOfWorker++;
            arrUser[userIdx] = worker;          //사용자 배열에 Worker객체 업캐스팅하여 저장
        }
        
        userIdx++; 
    }

    //A: 사용자 아이디, 요청 일 수 가  입력되면 대여 개능한 컴퓨터를 찾아 사용자에게 대여해 줌
    public string[] AssignComputerToUser(int userId, int requestedDay)
    {
        if (arrUser[userId - 1].Type == "Students")    //대여 요청한 사람이 학생일때
        {
            index = Array.FindIndex(arrComp, element => (element.Type == "Notebook" && element.Avail == "Y")
                                                        || (element.Type == "Desktop" && element.Avail == "Y"));
        }
        else if (arrUser[userId - 1].Type == "Gamers")    //대여 요청한 사람이 게이머일때
        {
            index = Array.FindIndex(arrComp, element => (element.Type == "Desktop" && element.Avail == "Y"));
        }
        else                        //대여 요청한 사람이 직장인일때
        {
            index = Array.FindIndex(arrComp, element => element.Avail == "Y");
        }

        //컴퓨터 프로퍼티 Set
        arrComp[index].UserId = userId;
        arrComp[index].Avail = "N";
        arrComp[index].DaysRequested = requestedDay;
        arrComp[index].DaysLeft = requestedDay;
        arrComp[index].DayUsed = 0;

        //사용자 프로퍼티 Set
        arrUser[userId - 1].Rent = "Y";
        arrUser[userId - 1].RentedComputerId = index + 1;

        //출력 문구 저장하여 반환
        writeLine[0] = $"Computer #{arrComp[index].ComId} has been assigned to User #{arrUser[userId - 1].UserId}" +
                       "\n";
        writeLine[0] += "===============================================================================================";

        return writeLine;
    }

    //R: 대여한 컴퓨터 반환하는 메소드
    public string ReturnComputer(char command, int userId)
    {
        index = Array.FindIndex(arrComp, element => element.UserId == userId);

        //사용시간이 남았는데 R을 통해 반환하는 경우 
        if (command.Equals('R'))
        {
            writeLine[0] = $"User #{arrUser[userId - 1].UserId} has returned " +
                           $"Computer #{arrUser[userId - 1].RentedComputerId} " +
                           $"and paid {arrComp[index].Price * arrComp[index].DayUsed} won.";

            writeLine[0] += "\n" + "====================================================================================";
        }
        //대여기간 x 하루 대여료를 곱해 총 요금 게산
        totalCost += (arrComp[index].Price * arrComp[index].DayUsed);
        
        //사용자 정보 갱신
        arrUser[userId - 1].Rent = "N";
        arrUser[userId - 1].RentedComputerId = 0;

        //컴퓨터 정보 갱신
        arrComp[index].Avail = "Y";
        arrComp[index].UserId = 0;
        arrComp[index].DaysRequested = 0;
        arrComp[index].DaysLeft = 0;
        arrComp[index].DayUsed = 0;

        return writeLine[0];
    }

    //T: 하루 증가시키는 메소드
    public string[] PassOneDay()
    {
        writeLine[0] = "It has passed one day...";

        //컴퓨터 배열을 하나씩 훓으며 사용 기간, 남은 기간 프로퍼티 갱신
        for (cnt = 0; cnt < numberOfComputers; cnt++)
        {
            if (arrComp[cnt].Avail == "N")
            {
                arrComp[cnt].DayUsed += 1;
                arrComp[cnt].DaysLeft -= 1;

                //사용기간이 종료된 경우 ReturnComputer메소드 호출
                if (arrComp[cnt].DaysLeft <= 0)
                {
                    writeLine[0] += "\n" + $"Time for Computer #{arrComp[cnt].ComId} has expired. "
                                         + $"User #{arrComp[cnt].UserId} has returned Computer #{arrComp[cnt].ComId} "
                                         + $"and paid {arrComp[cnt].Price * arrComp[cnt].DayUsed} won.";
                    ReturnComputer('T', arrComp[cnt].UserId);
                }
            }
        }
        //출력 문자열 반환
        writeLine[0] += "\n" + "========================================================================================";
        return writeLine;
    }

    //S: 현재 총 지불된 금액,  컴퓨터 대여 상황, 사용자 정보 출력
    public string[] PrintAll()
    {
        //Total Cost 출력
        writeLine[0] = $"Total Cost: {totalCost}" + "\n";

        //각 Computer 정보 출력
        writeLine[0] += $"Computer List:" + "\n";
        
        for (cnt = 0; cnt < numberOfComputers; cnt++)
        {
            writeLine[0] += $"({arrComp[cnt].ComId}) type: {arrComp[cnt].Type}, ComId: {arrComp[cnt].ComId}, ";
            if (arrComp[cnt].Type == "Netbook")     //냇북일때
            {
                writeLine[0] += $"NetId: {arrComp[cnt].TypeId}, " + $"Used for: {arrComp[cnt].UsedFor[0]}, ";
            }
            else if (arrComp[cnt].Type == "Notebook")   //노트북일때
            {
                writeLine[0] += $"NoteId: {arrComp[cnt].TypeId}, " +
                                $"Used for: {arrComp[cnt].UsedFor[0]}, {arrComp[cnt].UsedFor[1]}, ";
            }
            else            //데스크탑일떄 
            {
                writeLine[0] += $"DeskId: {arrComp[cnt].TypeId}, " +
                                $"Used for: {arrComp[cnt].UsedFor[0]}, {arrComp[cnt].UsedFor[1]}, {arrComp[cnt].UsedFor[2]}, ";
            }

            writeLine[0] += $"Avail: {arrComp[cnt].Avail}" + "\n";

            if (arrComp[cnt].Avail == "N")
            {
                writeLine[0] += $"(UsedId: {arrComp[cnt].UserId}, DR: {arrComp[cnt].DaysRequested}, " +
                                $"DL: {arrComp[cnt].DaysLeft}, DU: {arrComp[cnt].DayUsed})" + "\n";
            }
        }

        //각 User 정보 출력
        writeLine[1] = "User List:" + "\n";
        for (cnt = 0; cnt < numberOfUsers; cnt++)
        {
            writeLine[1] += $"({arrUser[cnt].UserId}) type: {arrUser[cnt].Type}, " +
                            $"Name: {arrUser[cnt].Name}, UserId: {arrUser[cnt].UserId}, ";

            if (arrUser[cnt].Type == "Students")   //학생일때
            {
                writeLine[1] += $"StudId: {arrUser[cnt].TypeId}, " +
                                $"Used for: {arrUser[cnt].UsedFor[0]}, {arrUser[cnt].UsedFor[1]}, ";
            }
            else if (arrUser[cnt].Type == "Gamers")   //게이머일때
            {
                writeLine[1] += $"GamerId: {arrUser[cnt].TypeId}, " +
                                $"Used for: {arrUser[cnt].UsedFor[0]}, {arrUser[cnt].UsedFor[1]}, ";
            }
            else //워커일때
            {
                writeLine[1] += $"WorkerId: {arrUser[cnt].TypeId}, Used for: {arrUser[cnt].UsedFor[0]}, ";
            }

            writeLine[1] += $"Rent: {arrUser[cnt].Rent}" + "\n";

            //컴퓨터를 대여중인 경우 
            if (arrUser[cnt].Rent == "Y")
            {
                writeLine[1] += $"(RentCompId: {arrUser[cnt].RentedComputerId})" + "\n";
            }
        }

        writeLine[2] = "================================================================================================";
        return writeLine;
    }
    
}