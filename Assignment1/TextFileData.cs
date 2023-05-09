namespace Assignment1;
using System;
using System.IO;

//input.txt파일을 읽어 읽은 정보를 처리하는 클래스
public class TextFileData
{
    ComputerManager computerManager;
    //readStream으로 읽어온 한줄을 임시 저장
    string readLine; 
    string[] tmpWriteLine;
    string writeLine = "";

    //컴퓨터 수
    private int numberOfComputers;
    private int numberOfNotebooks;
    private int numberOfDesktops;
    private int numberOfNetbooks;
    
    //컴픁터 종류에 따른 각각의 갯수
    private string[] computerInput;
    //사용자 타입, 이름을 담는 변수
    private string[] userInput;
    //A, S, T, R, Q 명령을 담는 배열
    private string[] commandInput;

    //사용자 수
    private int numberOfUsers;

    //현재 가리키는 순서
    private int lineCount = 0;

    public TextFileData()   //computerManager객체 생성
    {
        computerManager = new ComputerManager();
    }
    
    //input.txt파일을 읽는 메소드
    public void SetTextFile()     
    {
        StreamReader sr = new StreamReader(@"../input.txt");    //파일을 읽는 객체
        StreamWriter sw = new StreamWriter(@"../output.txt");   //파일에 쓰는 객체
        
        //input.txt 파일의 내용을 한 줄씩 읽는 반복문
        for (lineCount = 1; sr.Peek() >= 0; lineCount++)
        {
            readLine = sr.ReadLine();       //한줄 읽어오기

            //총 컴퓨터 수 읽기
            if (lineCount == 1) 
            {
                numberOfComputers = Int32.Parse(readLine);
            }
            //노트북, 데스크톱, 넷북 수 읽기
            else if (lineCount == 2)
            {
                //공백을 기준으로 각각의 컴퓨터 수 저장
                computerInput = readLine.Split(' ');
                numberOfNotebooks = System.Convert.ToInt32(computerInput[0]);
                numberOfDesktops = System.Convert.ToInt32(computerInput[1]);
                numberOfNetbooks = System.Convert.ToInt32(computerInput[2]);

                computerManager.SetComputerArray(numberOfComputers, numberOfNotebooks, numberOfDesktops, numberOfNetbooks);
            }
            //전체 사용자 수 읽기
            else if (lineCount == 3)    
            {
                numberOfUsers = System.Convert.ToInt32(readLine);
                computerManager.InitUserArray(numberOfUsers);
            }
            //사용자 정보 일기
            else if (lineCount >= 4 && lineCount < 4 + numberOfUsers)
            {
                userInput = readLine.Split(' ');
                computerManager.SetUserArray(userInput[0], userInput[1]);
            }
            //컴퓨터 대여 처리 
            else
            {
                if (readLine.StartsWith("A"))   //컴퓨터 대여
                {
                    commandInput = readLine.Split(' ');
                    tmpWriteLine = computerManager.AssignComputerToUser(System.Convert.ToInt32(commandInput[1]), System.Convert.ToInt32(commandInput[2]));
                    writeLine = tmpWriteLine[0];
                    sw.WriteLine(writeLine);
                }
                else if (readLine.StartsWith("R"))  //컴퓨터 반납
                {
                    commandInput = readLine.Split(' ');
                    writeLine = computerManager.ReturnComputer('R', System.Convert.ToInt32(commandInput[1]));
                    sw.WriteLine(writeLine);
                }
                else if (readLine.Equals("T"))    //하루 지남
                {
                    tmpWriteLine = computerManager.PassOneDay();
                    writeLine = tmpWriteLine[0];
                    sw.WriteLine(writeLine);
                }
                else if (readLine.Equals("S"))  //정보 출력
                {
                    tmpWriteLine = computerManager.PrintAll();
                    writeLine = tmpWriteLine[0] + tmpWriteLine[1] + tmpWriteLine[2];
                    sw.WriteLine(writeLine);
                }
            }
        }
        sw.Close();
        sr.Close();
    }
}