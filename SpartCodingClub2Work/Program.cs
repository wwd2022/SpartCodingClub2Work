using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace SpartCodingClub2Work
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) {
                Console.Clear();
                Console.WriteLine("어떤 게임을 하시겠습니까? 1.숫자게임 2.틱택토");
                int inputNum;
                if (int.TryParse(Console.ReadLine(), out inputNum) && inputNum >= 1 && inputNum <= 2)
                {
                    if (inputNum == 1) NumGame();
                    else if (inputNum == 2) TicTacToeGame();
                }
                else Console.WriteLine("1부터 2까지의 정수를 입력해주세요.");
            }
        }

        static void NumGame()
        {
            int tryNumber = 0;
            Random rand = new Random();
            int num = new Random().Next(1, 101);
            bool isLoop = true;
            while (isLoop)
            {
                Console.WriteLine("숫자 맞추기 게임을 시작합니다. 1에서 100까지의 숫자 중 하나를 맞춰보세요.");
                Console.Write("숫자를 입력하세요: ");
                int inputNum;
                if (int.TryParse(Console.ReadLine(), out inputNum) && inputNum >= 1 && inputNum <= 100)
                    if (inputNum < num) Console.WriteLine("너무 작습니다!");
                    else if (inputNum > num) Console.WriteLine("너무 큽니다!");
                    else isLoop = false;
                else
                {
                    Console.WriteLine("1부터 100까지의 정수를 입력해주세요.");
                    continue;
                }
                tryNumber++;
            }
            Console.WriteLine($"축하합니다 {tryNumber}번 만에 숫자를 맞추었습니다.");
        }

        static void TicTacToeGame()
        {
            char[] arr = new char[9];
            for (int i = 0; i < 9; i++) { arr[i] = (i + 1).ToString()[0]; }
            bool oneTurn = true;
            bool isGameEnd = false;
            string inputStr = "";
            while (true)
            {
                Console.Clear();
                Console.WriteLine($@"플레이어 1: X, 플레이어 2: O

플레이어 {(oneTurn ? 1 : 2)}의 차례

     |     |     
  {arr[0]}  |  {arr[1]}  |  {arr[2]}  
_____|_____|_____
     |     |     
  {arr[3]}  |  {arr[4]}  |  {arr[5]}  
_____|_____|_____
     |     |     
  {arr[6]}  |  {arr[7]}  |  {arr[8]}  
     |     |     
");
                if (isGameEnd)
                {
                    Console.WriteLine("///////////////////////////////////////////////////////");
                    Console.WriteLine($"     {(!oneTurn ? 1 : 2)}번 플레이어({(!oneTurn ? "X" : "O")})가 승리했습니다.");
                    Console.WriteLine("///////////////////////////////////////////////////////");
                    Console.ReadLine();
                    return;
                }
                // 사용자입력 받기
                if (inputStr == "")
                {
                    Console.Write($"어디에 {(oneTurn ? "X" : "O")}를 그리시겠습니까? : ");
                    inputStr = Console.ReadLine();
                }
                // 입력값 정수로 변환후 게임진행
                int inputNum;
                if (int.TryParse(inputStr, out inputNum)
                    && inputNum >= 1
                    && inputNum <= 9)
                {
                    if (arr[inputNum - 1] != 'X' && arr[inputNum - 1] != 'O')
                    {
                        char choiceChar = oneTurn ? 'X' : 'O';
                        arr[inputNum - 1] = choiceChar; // 'X' or 'O'
                        int[,] clearArr = {
                            { 0, 1, 2 }   // 1번째 가로줄
                            , { 3, 4, 5 } // 2번째 가로줄
                            , { 6, 7, 8 } // 3번째 가로줄
                            , { 0, 3, 6 } // 1번째 세로줄
                            , { 1, 4, 7 } // 2번째 세로줄
                            , { 2, 5, 8 } // 3번째 세로줄
                            , { 0, 4, 8 } // 하향 대각선
                            , { 6, 4, 2 } // 상향 대각선
                        };
                        
                        // 정답줄(가로,세로,대각선) 순회
                        for (int i = 0; i < clearArr.GetLength(0); i++)
                        {
                            // 지금 그려진 위치(inputNum)가 있는 정답줄에 있는지 체크
                            for (int j = 0; j < clearArr.GetLength(1); j++)
                            {
                                // 있다면?
                                if (clearArr[i, j] == inputNum - 1)
                                {
                                    // 해당줄에 전부 같은 값이라면 승리
                                    if (arr[clearArr[i, 0]] == choiceChar
                                        && arr[clearArr[i, 1]] == choiceChar
                                        && arr[clearArr[i, 2]] == choiceChar)
                                    {
                                        // 게임끝을 체크
                                        isGameEnd = true;
                                    }
                                    else // 아니라면 다음 정답줄 순회
                                    {
                                        break;
                                    }
                                }
                            }
                        }

                        // 그리기 마무리
                        oneTurn = !oneTurn;
                        inputStr = "";
                    }
                    else
                    {
                        Console.WriteLine($"이미 {arr[inputNum - 1]}가 그려져있습니다.");
                        Console.Write($"어디에 {(oneTurn ? "X" : "O")}를 그리시겠습니까? : ");
                        inputStr = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("1부터 9사이의 정수를 입력해주세요.");
                    Console.Write($"어디에 {(oneTurn ? "X" : "O")}를 그리시겠습니까? : ");
                    inputStr = Console.ReadLine();
                }
            }
        }
    }
}
