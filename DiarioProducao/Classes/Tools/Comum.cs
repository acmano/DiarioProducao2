using Android.App;

namespace DiarioProducao.Classes.Tools
{
  internal class Comum
  {

    public static void MessageBox(Activity context, string pMsg)
    {
      var builder = new AlertDialog.Builder(context);
      builder.SetMessage(pMsg);
      builder.SetTitle("");
      builder.SetNegativeButton("OK", delegate { });
      builder.Show();
    }
  }
}