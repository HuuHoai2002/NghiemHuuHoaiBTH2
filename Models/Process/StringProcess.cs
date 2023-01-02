namespace NET2_Main.Models.Process;

public class StringProcess
{
  public string RemoveRemainingWhileSpace(string input)
  {
    string result = "";
    input = input.Trim();

    while (input.IndexOf("  ") > 0)
    {
      input = input.Replace("  ", " ");
    }
    result = input;

    return result;
  }

  public string LowerToUpper(string input)
  {
    return input.ToUpper();
  }
  public string UpperToLower(string input)
  {
    return input.ToLower();
  }

  public string CapitalizeOneFirstCharacter(string input)
  {
    return input.Substring(0, 1).ToUpper() + input.Substring(1);
  }

  public string CapitalizeFirstCharacter(string input)
  {
    string[] arr = input.Split(" ");
    string result = "";

    for (int i = 0; i < arr.Length; i++)
    {
      result += arr[i].Substring(0, 1).ToUpper() + arr[i].Substring(1) + " ";
    }

    return result;
  }

  public string RemoveVietNameseAcccents(string input)
  {
    string[] arr1 = new string[] { "aAeEoOuUiIdDyY" };
    string[] arr2 = new string[] { "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };

    for (int i = 1; i < arr2.Length; i++)
    {
      for (int j = 0; j < arr2[i].Length; j++)
      {
        input = input.Replace(arr2[i][j], arr1[0][i - 1]);
      }
    }

    return input;
  }

}