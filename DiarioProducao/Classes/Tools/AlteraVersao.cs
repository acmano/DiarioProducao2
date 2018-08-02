using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;

namespace DiarioProducao.Classes.Tools
{
  public class  AlteraVersao
  {
    readonly Comum _comum = new Comum ( );

    public static DateTime ConvertIntDateTime ( double d )
    {
      var startTime = TimeZone.CurrentTimeZone.ToLocalTime ( new DateTime ( 1970, 1, 1 ) );
      var time = startTime.AddMilliseconds ( d );
      return time;
    }


    public static void TrocaVersao ( Activity context, string pProcesso, ref Boolean pAtualizaVersao )
    {
      //Verifica se Existe Nova Versão da Aplicação
      var file = @"/mnt/sdcard/.sistemas/apks/com.lorenzetti.mixprodutos-Signed.apk";
      const string fileMd5 = "/mnt/sdcard/.sistemas/apks/com.lorenzetti.mixprodutos-Signed.md5";
      var processo = "com.lorenzetti.mixprodutos";
      if ( !System.IO.File.Exists ( file ) )
      {
        return;
      }
      if ( pProcesso != "" )
      {
        file = "/mnt/sdcard/.sistemas/apks/" + pProcesso.ToString ( ).Trim ( ) + "-Signed.apk";
        processo = pProcesso;
      }
      var md5Numero = "";
      var md5 = CalculateChecksum ( file );
      md5Numero = File.Exists( fileMd5 ) ? GetIniProperty( fileMd5, "MD5" ) : md5;
      var pm = context.PackageManager;
      var info = pm.GetApplicationInfo ( processo, 0 );
      var arq = info.SourceDir;
      var installed = new Java.IO.File ( arq ).LastModified ( );
      var dtprocesso = ConvertIntDateTime ( installed );
      if ( System.IO.File.Exists ( file ) )
      {
        var packageUri = Android.Net.Uri.Parse ( processo );
        var apkInstalar = new Java.IO.File ( file );
        var apki = apkInstalar.GetHashCode ( );
        var myIntent = new Intent ( Android.Content.Intent.ActionView, packageUri );
        myIntent.SetDataAndType ( Android.Net.Uri.FromFile ( apkInstalar ), "application/vnd.android.package-archive" );
        var apkdate = new Java.IO.File ( file ).LastModified ( );
        var dtapk = ConvertIntDateTime ( apkdate );
        var tamanhoapkInstalado = new Java.IO.File ( arq ).Length ( );
        var tamanhoapk = new Java.IO.File ( file ).Length ( );
        tamanhoapk = ( tamanhoapk / 1024 );
        tamanhoapkInstalado = ( tamanhoapkInstalado / 1024 );
        if ( dtapk > dtprocesso && md5.ToString ( ).Trim ( ) == md5Numero.ToString ( ).Trim ( ) )
        {
          pAtualizaVersao = true;
          context.StartActivity ( myIntent );
          context.Finish ( );
        }
        if ( md5.ToString ( ).Trim ( ) != md5Numero.ToString ( ).Trim ( ) )
        {
          Comum.MessageBox ( context, "Atenção. Comunique o erro de instalação ao depto de TI. \n\nSetor de Suporte.\n\nRamal 7332.\n\nProcesso " + processo.ToString ( ) );
        }
      }
      //EOF
    }


    public static string GetIniProperty ( string iniFileName, string searchKey )
    {
      string keyValue = null;
      var keyList = new List<string> ( );
      using ( var sr = new StreamReader ( iniFileName ) )
      {
        string inp;
        while ( ( inp = sr.ReadLine ( ) ) != null )
        {
          var parts = inp.Split ( new char [ ] { ',', '=' } );
          if ( ( parts.Length == 2 ) && ( parts [ 0 ] == searchKey ) )
          {
            keyValue = parts [ 1 ];
          }
        }
      }
      return keyValue;
    }

    private static string CalculateChecksum ( string file )
    {
      var md5 = System.Security.Cryptography.MD5.Create ( );
      using ( var stream = File.OpenRead ( file ) )
      {
        var checksum = md5.ComputeHash ( stream );
        var sum = BitConverter.ToString ( checksum );
        return sum.Substring ( 0, sum.Length - 2 ).Replace ( "-", string.Empty ).ToLower ( );
      }
    }
  }
}