using System;

namespace DiarioProducao.Classes.Tools
{
  using Android.Content.Res;

  public class AcessoSql
  {

    private readonly String _openQuery;
    private readonly String _server;
    private readonly String _userName;
    private readonly String _password;

    public enum AcessoTipo
    {
      Desenvolvimento
    , Producao
    }

    public string OpenQuery
    {
      get { return _openQuery; }
    }

    public string Server
    {
      get { return _server; }
    }

    public string UserName
    {
      get { return _userName; }
    }

    public string Password
    {
      get { return _password; }
    }

    public AcessoSql(AcessoTipo acessoTipo)
    {
      switch (acessoTipo)
      {
        case AcessoTipo.Desenvolvimento:
          _openQuery = Classes.Resources.BancoSQLDesenv.OpenQuery;
          _server = Classes.Resources.BancoSQLDesenv.Server;
          _userName = Classes.Resources.BancoSQLDesenv.Username;
          _password = Classes.Resources.BancoSQLDesenv.Password;
          break;
        case AcessoTipo.Producao:
          _openQuery = Classes.Resources.BancoSQLProd.OpenQuery;
          _server = Classes.Resources.BancoSQLProd.Server;
          _userName = Classes.Resources.BancoSQLProd.Username;
          _password = Classes.Resources.BancoSQLProd.Password;
          break;
        default:
          throw new Exception( "Unexpected Case" );
      }
    }
  }
}