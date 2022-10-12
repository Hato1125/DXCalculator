using DxLibDLL;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace DXCalculator;

internal class Calculation
{
    int order = 0;
    StringBuilder leftNumBuffer = new();
    StringBuilder rightNumBuffer = new();
    public StringBuilder text = new StringBuilder();
    string calType = string.Empty;

    public void Calculating()
    {
        switch(order)
        {
            // 左辺を入力
            case 0:
                foreach (var item in AppMain.screenManeger.BtnScreen.Buttons)
                {
                    if (item.Value.IsClicked())
                    {
                        // textに0が入っていたら初期化
                        if (text.ToString() == "0")
                            text.Clear();

                        if (item.Key != "+" && item.Key != "-" && item.Key != "x" && item.Key != "/"
                            && item.Key != "=" && item.Key != "%" && item.Key != "c" && item.Key != "x2"
                            && item.Key != "+/-")
                        {
                            leftNumBuffer.Append(item.Key);
                            text.Append(item.Key);
                            Debug.WriteLine(item.Key);
                        }
                        else
                        {
                            if (item.Key == "c")
                            {
                                Init();
                            }
                            else if (item.Key == "x2")
                            {
                                decimal temp = 0;
                                temp = decimal.Parse(leftNumBuffer.ToString());
                                temp = temp * temp;
                                leftNumBuffer.Clear();
                                text.Clear();
                                leftNumBuffer.Append(temp.ToString());
                                text.Append(temp.ToString());
                            }
                            else if (item.Key == "+/-")
                            {
                                string temp = leftNumBuffer.ToString();
                                string result1 = string.Empty;
                                leftNumBuffer.Clear();
                                text.Clear();

                                if (temp.Equals($"-{temp.Replace("-", "")}"))
                                    result1 = temp.Replace("-", "");
                                else
                                    result1 = $"-{temp}";

                                leftNumBuffer.Append(result1);
                                text.Append(result1);
                            }
                            else
                            {
                                calType = item.Key;
                                if (item.Key == "/")
                                    text.Append($"÷");
                                else
                                    text.Append($"{item.Key}");

                                order++;
                            }
                        }
                    }
                }
                break;

            // 右辺を入力
            case 1:
                foreach (var item in AppMain.screenManeger.BtnScreen.Buttons)
                {
                    if (item.Value.IsClicked())
                    {
                        if (item.Key != "+" && item.Key != "-" && item.Key != "x" && item.Key != "/"
                            && item.Key != "=" && item.Key != "%" && item.Key != "c" && item.Key != "x2"
                            && item.Key != "+/-")
                        {
                            rightNumBuffer.Append(item.Key);
                            text.Append(item.Key);
                        }
                        else
                        {
                            if (item.Key == "c")
                            {
                                Init();
                            }
                            else if (item.Key == "x2")
                            {
                                decimal temp = 0;
                                temp = decimal.Parse(rightNumBuffer.ToString());
                                temp = temp * temp;
                                rightNumBuffer.Clear();
                                text.Clear();
                                rightNumBuffer.Append(temp.ToString());
                                text.Append(temp.ToString());
                            }
                            else if (item.Key == "+/-")
                            {
                                string temp = rightNumBuffer.ToString();
                                string result1 = string.Empty;
                                rightNumBuffer.Clear();
                                text.Clear();

                                if (temp.Equals($"-{temp.Replace("-", "")}"))
                                    result1 = temp.Replace("-", "");
                                else
                                    result1 = $"-{temp}";

                                rightNumBuffer.Append(result1);
                                text.Append(result1);
                            }
                            else if(item.Key == "=")
                            {
                                text.Append("=");
                                decimal left = decimal.Parse(leftNumBuffer.ToString());
                                decimal right = decimal.Parse(rightNumBuffer.ToString());
                                decimal result = Cal(calType, left, right);
                                text.Append(result);
                            }
                        }
                    }
                }
                break;
        }
    }

    public void Init()
    {
        leftNumBuffer.Clear();
        rightNumBuffer.Clear();
        text.Clear();
        text.Append("0");
        order = 0;
    }

    private decimal Cal(string calType, decimal leftNum, decimal rightNum)
    {
        decimal result = 0;

        switch(calType)
        {
            case "+":
                result = leftNum + rightNum;
                break;

            case "-":
                result = leftNum - rightNum;
                break;

            case "x":
                result = leftNum * rightNum;
                break;

            case "/":
                // TODO: 0÷ の場合は右辺には０を入れられないようにする
                // DivideByZeroExceptionを避ける
                if (leftNum == 0 && rightNum == 0)
                    result = 0;
                else
                    result = leftNum / rightNum;
                break;

            case "%":
                result = leftNum % rightNum;
                break;

            default:
                return 0;
        }

        return result;
    }
}
